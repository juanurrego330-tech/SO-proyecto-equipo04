using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    internal class RoundRobin
    {
        private int quantum;
        private int relojGlobal;
        private List<Procesos> listaProcesos;
        private Queue<int> colaListos;
        private bool simulacionActiva;

        private readonly object cpuLock = new object();
        private readonly object queueLock = new object();

        private Archivo logs;
        private Utilidades utilidades;

        public event Action<int, string, Color, int> OnActualizarUI;
        public event Action OnSimulacionTerminada;

        public RoundRobin(List<Procesos> procesos, int quantum, Archivo logs, Utilidades utilidades)
        {
            this.listaProcesos = procesos;
            this.quantum = quantum;
            this.logs = logs;
            this.utilidades = utilidades;
            this.colaListos = new Queue<int>();
            this.relojGlobal = 0;
            this.simulacionActiva = true;
        }

        public void DetenerSimulacion()
        {
            simulacionActiva = false;
        }

        public void Iniciar()
        {
            Thread director = new Thread(() =>
            {
                relojGlobal = 0;
                simulacionActiva = true;

                while (simulacionActiva && listaProcesos.Any(p => p.Estado != "TERMINADO"))
                {
                    lock (queueLock)
                    {
                        for (int i = 0; i < listaProcesos.Count; i++)
                        {
                            if (!simulacionActiva) break;

                            Procesos procesoActual = listaProcesos[i];

                            if (procesoActual.Estado == "ESPERANDO ARRIVAL" && procesoActual.ArrivalTime <= relojGlobal)
                            {
                                int index = i;
                                string nombre = procesoActual.Identificador;
                                int burst = procesoActual.BurstTime;

                                colaListos.Enqueue(index);
                                procesoActual.Estado = "LISTO (Ready)";

                                OnActualizarUI?.Invoke(index, "LISTO (Ready)", Color.Orange, 0);

                                Thread h = new Thread(() => LogicaProceso(index, nombre, burst));
                                h.IsBackground = true;
                                h.Start();

                                logs.SysCall_WriteLog($"ADMISIÓN: PROCESO {nombre} entró al sistema en T={relojGlobal}.");
                            }
                        }
                    }

                    Thread.Sleep(1000);
                    relojGlobal++;
                }

                if (simulacionActiva)
                {
                    OnSimulacionTerminada?.Invoke();
                }
            });

            director.IsBackground = true;
            director.Start();
        }

        private void LogicaProceso(int fila, string nombreID, int burst)
        {
            int tiempoRestante = burst;

            while (simulacionActiva && tiempoRestante > 0)
            {
                bool miTurno = false;

                while (simulacionActiva && !miTurno)
                {
                    lock (queueLock)
                    {
                        if (colaListos.Count > 0 && colaListos.Peek() == fila)
                            miTurno = true;
                    }

                    if (!miTurno)
                    {
                        Thread.Sleep(200);
                    }
                }

                if (!simulacionActiva) return;

                lock (cpuLock)
                {
                    if (listaProcesos[fila].FirstRunTime == -1)
                    {
                        listaProcesos[fila].FirstRunTime = relojGlobal;
                    }

                    if (listaProcesos[fila].MemoriaUsada == 0)
                    {
                        listaProcesos[fila].MemoriaUsada = new Random().Next(1024, 8192);
                    }
                    int aEjecutar = Math.Min(tiempoRestante, quantum);
                    logs.SysCall_WriteLog($"KERNEL: {nombreID} inicia ráfaga de {aEjecutar}s.");

                    for (int i = 0; i < aEjecutar; i++)
                    {
                        if (!simulacionActiva) return;

                        listaProcesos[fila].Estado = "EJECUCIÓN (Running)";
                        OnActualizarUI?.Invoke(fila, "EJECUCIÓN (Running)", Color.LightGreen, utilidades.CalcularPorcentaje(burst, tiempoRestante));

                        Thread.Sleep(1000);
                        tiempoRestante--;
                    }
                    if (tiempoRestante <= 0)
                    {
                        listaProcesos[fila].Estado = "TERMINADO";
                        listaProcesos[fila].FinishTime = relojGlobal;
                        OnActualizarUI?.Invoke(fila, "TERMINADO", Color.LightGray, 100);
                        logs.SysCall_WriteLog($"SISTEMA: {nombreID} terminó.");
                    }
                    else
                    {
                        logs.SysCall_WriteLog($"SISTEMA: {nombreID} suspendido (le faltan {tiempoRestante}s).");
                    }
                }

                lock (queueLock)
                {
                    if (colaListos.Count > 0 && colaListos.Peek() == fila)
                    {
                        colaListos.Dequeue();
                    }

                    if (tiempoRestante > 0 && simulacionActiva)
                    {
                        colaListos.Enqueue(fila);
                        listaProcesos[fila].Estado = "LISTO (Ready)";
                        OnActualizarUI?.Invoke(fila, "LISTO (Ready)", Color.Orange, utilidades.CalcularPorcentaje(burst, tiempoRestante));
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}