using System;
using System.Collections.Generic;
using System.Threading;
using Gestor_de_Procesos_y_Concurrencia;

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

            int cantidad = LeerEntero("Cantidad de procesos: ");

            for (int i = 0; i < cantidad; i++)
            {
                Console.WriteLine($"\nProceso {i + 1}");

                Console.Write("ID: ");
                string id = Console.ReadLine();

                int arrival = LeerEntero("Arrival Time: ", true);
                int burst = LeerEntero("Burst Time: ");

            }

            int quantum = LeerEntero("\nQuantum: ");
        }
    }
}