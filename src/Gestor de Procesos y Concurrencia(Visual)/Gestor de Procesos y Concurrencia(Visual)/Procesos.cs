using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    internal class Procesos
    {
        public int valorQuantum { get; set; }
        public int numeroProceso { get; set; }
        public string identificador { get; set; }
        public int ArrivalTime { get; set; }
        public int BursTime { get; set; }

        public Procesos(int numeroP, string ID, string AT, string BT)
        {
            numeroProceso = numeroP;
            identificador = ID;
            ArrivalTime = int.Parse(AT);
            BursTime = int.Parse(BT);
        }
    }
}
