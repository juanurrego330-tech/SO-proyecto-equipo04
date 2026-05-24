using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    internal class Procesos
    {
        public int NumeroProceso { get; set; }
        public string Identificador { get; set; }
        public int ArrivalTime { get; set; }
        public int BurstTime { get; set; }
        public int TiempoRestante { get; set; }
        public int MemoriaRAM { get; set; }
        public string Estado { get; set; } = "ESPERANDO ARRIVAL";
        public int FirstRunTime { get; set; } = -1;
        public int FinishTime { get; set; }
        public long MemoriaUsada { get; set; }

        public Procesos(int numero, string id, int arrival, int burst, int ram)
        {
            NumeroProceso = numero;
            Identificador = id;
            ArrivalTime = arrival;
            BurstTime = burst;
            TiempoRestante = burst;
            MemoriaRAM = ram;
            Estado = "ESPERANDO";
        }
    }
}
