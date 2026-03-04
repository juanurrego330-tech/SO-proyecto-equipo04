using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using static Gestor_de_Procesos_y_Concurrencia.Procesos;

namespace Gestor_de_Procesos_y_Concurrencia
{
    internal class Program
    {
        static int LeerEntero(string mensaje, bool permitirCero = false)
        {
            int valor;

            while (true)
            {
                Console.Write(mensaje);
                string input = Console.ReadLine();

                if (int.TryParse(input, out valor))
                {
                    if (valor < 0)
                        Console.WriteLine("No puede ser negativo.");
                    else if (!permitirCero && valor == 0)
                        Console.WriteLine("Debe ser mayor que cero.");
                    else
                        return valor;
                }
                else
                {
                    Console.WriteLine("Entrada inválida.");
                }
            }
        }

        static void Main(string[] args)
        {
            var clock = new SimulationClock();

            int cantidad = LeerEntero("Cantidad de procesos: ");
            var procesos = new List<Proceso>();

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"\nProceso {i + 1}");

                Console.Write("ID: ");
                string id = Console.ReadLine();

                int arrival = LeerEntero("Arrival Time: ", true);
                int burst = LeerEntero("Burst Time: ");

                procesos.Add(new Proceso(id, arrival, burst));
            }

            int quantum = LeerEntero("\nQuantum: ");

            var scheduler = new RoundRobinScheduler(procesos, quantum, clock);

            Thread hilo = new Thread(scheduler.Ejecutar);
            hilo.Start();

            while (scheduler.Activo)
            {
                Console.Clear();
                Console.WriteLine($"Tiempo: {clock.ObtenerTiempo()}");
                Console.WriteLine("ID\tEstado\tRemaining");

                foreach (var p in scheduler.ObtenerProcesos())
                    Console.WriteLine($"{p.Id}\t{p.Estado}\t{p.RemainingTime}");

                Thread.Sleep(500);
            }

            hilo.Join();
            Console.WriteLine("\nSimulación terminada.");
            Console.ReadLine();
        }
    }
}