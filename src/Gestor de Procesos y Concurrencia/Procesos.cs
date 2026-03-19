using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Gestor_de_Procesos_y_Concurrencia
{
    internal class Procesos
    {
        public enum EstadoProceso
        {
            NEW,
            READY,
            RUNNING,
            TERMINATED
        }

        public class Proceso
        {
            public string Id { get; set; }
            public int ArrivalTime { get; set; }
            public int BurstTime { get; set; }
            public int RemainingTime { get; set; }
            public EstadoProceso Estado { get; set; }
            public int CompletionTime { get; set; }
            public int TurnaroundTime { get; set; }
            public int WaitingTime { get; set; }

            public Proceso(string id, int arrival, int burst)
            {
                Id = id;
                ArrivalTime = arrival;
                BurstTime = burst;
                RemainingTime = burst;
                Estado = EstadoProceso.NEW;
            }
        }

        public class SimulationClock
        {
            private int tiempoGlobal = 0;
            private readonly object lockTiempo = new object();

            public int ObtenerTiempo()
            {
                lock (lockTiempo)
                    return tiempoGlobal;
            }

            public void Incrementar()
            {
                lock (lockTiempo)
                    tiempoGlobal++;
            }
        }

        public class RoundRobinScheduler
        {
            private readonly List<Proceso> procesos;
            private readonly Queue<Proceso> colaReady = new Queue<Proceso>();
            private readonly SimulationClock clock;
            private readonly int quantum;
            private readonly object lockProcesos = new object();

            public bool Activo { get; private set; } = true;

            public RoundRobinScheduler(List<Proceso> procesos, int quantum, SimulationClock clock)
            {
                this.procesos = procesos;
                this.quantum = quantum;
                this.clock = clock;
            }

            public List<Proceso> ObtenerProcesos()
            {
                lock (lockProcesos)
                    return procesos.ToList();
            }

            private void RevisarLlegadas()
            {
                foreach (var p in procesos)
                {
                    if (p.ArrivalTime <= clock.ObtenerTiempo() && p.Estado == EstadoProceso.NEW)
                    {
                        p.Estado = EstadoProceso.READY;
                        colaReady.Enqueue(p);
                    }
                }
            }

            public void Ejecutar()
            {
                while (true)
                {
                    lock (lockProcesos)
                    {
                        if (!procesos.Any(p => p.Estado != EstadoProceso.TERMINATED))
                            break;

                        RevisarLlegadas();
                    }

                    if (colaReady.Count > 0)
                    {
                        Proceso actual;

                        lock (lockProcesos)
                        {
                            actual = colaReady.Dequeue();
                            actual.Estado = EstadoProceso.RUNNING;
                        }

                        int tiempoEjecutado = 0;

                        while (tiempoEjecutado < quantum)
                        {
                            Thread.Sleep(1000);

                            lock (lockProcesos)
                            {
                                if (actual.RemainingTime <= 0)
                                    break;

                                actual.RemainingTime--;
                            }

                            clock.Incrementar();
                            tiempoEjecutado++;

                            lock (lockProcesos)
                                RevisarLlegadas();
                        }

                        lock (lockProcesos)
                        {
                            if (actual.RemainingTime > 0)
                            {
                                actual.Estado = EstadoProceso.READY;
                                colaReady.Enqueue(actual);
                            }
                            else
                            {
                                actual.Estado = EstadoProceso.TERMINATED;


                                actual.CompletionTime = clock.ObtenerTiempo();
                                actual.TurnaroundTime = actual.CompletionTime - actual.ArrivalTime;
                                actual.WaitingTime = actual.TurnaroundTime - actual.BurstTime;
                            }
                        }
                    }
                    else
                    {
                        Thread.Sleep(1000);
                        clock.Incrementar();

                        lock (lockProcesos)
                            RevisarLlegadas();
                    }
                }

                Activo = false;
            }
        }
    }
}