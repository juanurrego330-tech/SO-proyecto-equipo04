using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    internal class Utilidades
    {
        public bool ValidarCampos(string a)
        {
            if (a == "")
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            return true;
        }
        public bool ValidarNumerosInicio(string numeropro, string valoquantum)
        {
            if (!int.TryParse(numeropro, out int numeroproces))
            {
                MessageBox.Show("Por favor, ingrese un número válido de procesos (Entero)");
                return false;
            }
            if (!int.TryParse(valoquantum, out int valorquantum))
            {
                MessageBox.Show("Por favor, ingrese un número válido para el quantum (Entero)");
                return false;
            }
            return true;
        }
        public bool ValidarNumerosIngresoDatos(string arrivaTime, string Burstime)
        {
            if (!int.TryParse(arrivaTime, out int a))
            {
                MessageBox.Show("Por favor, ingrese un número válido de Arrival (Entero)");
                return false;
            }
            if (!int.TryParse(Burstime, out int b))
            {
                MessageBox.Show("Por favor, ingrese un número válido para el Burst (Entero)");
                return false;
            }
            return true;
        }

        public int CalcularPorcentaje(int total, int restante)
        {
            return 100 - (restante * 100 / total);
        }
        public void limpiarCampo(TextBox a)
        {
            a.Clear();
        }
        public void mostrarDatosProcesos(DataGridView D, List<Procesos> P)
        {
            D.Rows.Clear();
            foreach (Procesos a in P)
            {
                D.Rows.Add(a.numeroProceso, a.identificador, a.ArrivalTime, a.BursTime);
            }
        }
    }
}
