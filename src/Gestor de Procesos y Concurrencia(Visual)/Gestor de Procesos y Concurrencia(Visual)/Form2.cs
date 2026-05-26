using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();

            string conexionComando = "Server=LAPTOP-BS0QDBE1; Database=GestorProcesosDB; integrated security=true; TrustServerCertificate=True;";
            string ComandoVerTodo = "SELECT * FROM HistoricoProcesos";
            SqlConnection conexion = new SqlConnection(conexionComando);
            SqlCommand VerTodo = new SqlCommand(ComandoVerTodo, conexion);
            conexion.Open();
            SqlDataReader comandoLeer = VerTodo.ExecuteReader();
            while (comandoLeer.Read())
            {
                string idSimulacion = comandoLeer["Id"].ToString();
                string fecha = Convert.ToDateTime(comandoLeer["FechaSimulacion"]).ToString("yyyy-MM-dd HH:mm:ss");
                string procesoId = comandoLeer["ProcesoId"].ToString();
                string burst = comandoLeer["BurstTime"].ToString();
                string arrival = comandoLeer["ArrivalTime"].ToString();
                string memoria = comandoLeer["MemoriaKB"].ToString();
                string turnaround = comandoLeer["Turnaround"].ToString();
                string espera = comandoLeer["Espera"].ToString();
                string respuesta = comandoLeer["Respuesta"].ToString();

                dataGridView1.Rows.Add(
                    idSimulacion,
                    fecha,
                    procesoId,
                    burst,
                    arrival,
                    $"{memoria} KB",
                    $"{turnaround} s",
                    $"{espera} s",
                    $"{respuesta} s"
                );
            }
            conexion.Close();
        }
    }
}
