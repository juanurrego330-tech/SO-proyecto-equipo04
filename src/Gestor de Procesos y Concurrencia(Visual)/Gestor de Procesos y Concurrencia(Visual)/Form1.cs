using Microsoft.VisualBasic.Devices;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing;
using Microsoft.Data.SqlClient;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button9.Visible = false;
        }
        private int quantum;
        private int numeroProcesos;
        private int contadorProcesos = 1;
        private bool cambioPermitido = false;

        private List<Procesos> listaProcesos = new List<Procesos>();
        private RoundRobin planificadorRoundRobin;
        Archivo Logs = new Archivo();
        Utilidades V = new Utilidades();

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!cambioPermitido)
            {
                e.Cancel = true;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (V.ValidarCampos(textBox1.Text) && V.ValidarCampos(textBox2.Text))
            {
                if (V.ValidarNumerosInicio(textBox1.Text, textBox2.Text))
                {
                    numeroProcesos = int.Parse(textBox1.Text);
                    quantum = int.Parse(textBox2.Text);
                    cambioPermitido = true;
                    tabControl1.SelectedTab = tabPage2;
                    cambioPermitido = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (V.ValidarCampos(textBox3.Text) && V.ValidarCampos(textBox4.Text) && V.ValidarCampos(textBox5.Text))
            {
                if (V.ValidarNumerosIngresoDatos(textBox4.Text, textBox5.Text))
                {
                    listaProcesos.Add(new Procesos(contadorProcesos, textBox3.Text, int.Parse(textBox4.Text), int.Parse(textBox5.Text), 0));
                    if (contadorProcesos >= numeroProcesos)
                    {
                        cambioPermitido = true;
                        tabControl1.SelectedTab = tabPage3;
                        cambioPermitido = false;
                        V.mostrarDatosProcesos(dataGridView1, listaProcesos);
                    }
                    else
                    {
                        contadorProcesos++;
                        label3.Text = "Proceso #" + contadorProcesos;
                        V.limpiarCampo(textBox3);
                        V.limpiarCampo(textBox4);
                        V.limpiarCampo(textBox5);
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cambioPermitido = true;
            tabControl1.SelectedTab = tabPage4;
            cambioPermitido = false;
            Logs.DestruirArchivo();
            dataGridView2.Rows.Clear();
            foreach (var a in listaProcesos)
            {
                a.Estado = "ESPERANDO ARRIVAL";
                dataGridView2.Rows.Add(a.NumeroProceso, a.Identificador, a.ArrivalTime, a.BurstTime, a.Estado, "0%");
            }

            button4.Visible = false;
            button6.Visible = false;
            planificadorRoundRobin = new RoundRobin(listaProcesos, quantum, Logs, V);
            planificadorRoundRobin.OnActualizarUI += ActualizarFilasTabla;
            planificadorRoundRobin.OnSimulacionTerminada += () =>
            {
                this.Invoke(new Action(() => button4.Visible = true));
                this.Invoke(new Action(() => button6.Visible = true));
            };

            planificadorRoundRobin.Iniciar();
        }
        private void ActualizarFilasTabla(int fila, string estado, Color color, int progreso)
        {
            dataGridView2.Rows[fila].Cells[4].Value = estado;
            dataGridView2.Rows[fila].Cells[5].Value = progreso + "%";
            dataGridView2.Rows[fila].DefaultCellStyle.BackColor = color;
        }
        private void CalcularYMostrarEstadisticas()
        {
            dataGridView3.Rows.Clear();

            double sumaT = 0, sumaW = 0, sumaR = 0;
            SqlConnection conexion = new SqlConnection("Server=LAPTOP-BS0QDBE1; Database=GestorProcesosDB; integrated security=true; TrustServerCertificate=True;");

            string insertBasedeDatos = @"INSERT INTO HistoricoProcesos (ProcesoId, BurstTime, ArrivalTime, MemoriaKB, Turnaround, Espera, Respuesta)" +
            @" VALUES (@ProcesoId, @BurstTime, @ArrivalTime, @MemoriaKB, @Turnaround, @Espera, @Respuesta)";


            SqlCommand comandoSql = new SqlCommand(insertBasedeDatos, conexion);
            conexion.Open();
            foreach (var p in listaProcesos)
            {
                int turnaround = p.FinishTime - p.ArrivalTime;
                int espera = turnaround - p.BurstTime;
                int respuesta = p.FirstRunTime - p.ArrivalTime;

                sumaT += turnaround;
                sumaW += espera;
                sumaR += respuesta;

                dataGridView3.Rows.Add(
                    p.Identificador,
                    $"{p.MemoriaUsada} KB",
                    $"{turnaround} s",
                    $"{espera} s",
                    $"{respuesta} s"
                );
                comandoSql.Parameters.AddWithValue("@ProcesoId", p.Identificador);
                comandoSql.Parameters.AddWithValue("@BurstTime", p.BurstTime);
                comandoSql.Parameters.AddWithValue("@ArrivalTime", p.ArrivalTime);
                comandoSql.Parameters.AddWithValue("@MemoriaKB", p.MemoriaUsada);
                comandoSql.Parameters.AddWithValue("@Turnaround", turnaround);
                comandoSql.Parameters.AddWithValue("@Espera", espera);
                comandoSql.Parameters.AddWithValue("@Respuesta", respuesta);
                try { comandoSql.ExecuteNonQuery(); } catch { }
                comandoSql.Parameters.Clear();
            }
            conexion.Close();
            double promMemoria = listaProcesos.Average(p => p.MemoriaUsada);
            double promT = sumaT / listaProcesos.Count;
            double promW = sumaW / listaProcesos.Count;
            double promR = sumaR / listaProcesos.Count;

            dataGridView3.Rows.Add("PROMEDIOS", $"{promMemoria:F2} KB", $"{promT:F2} s", $"{promW:F2} s", $"{promR:F2} s");
            dataGridView3.Rows[dataGridView3.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightCyan;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            numeroProcesos = 0;
            quantum = 0;
            listaProcesos.Clear();
            contadorProcesos = 1;
            label3.Text = "Proceso #1";
            cambioPermitido = true;
            tabControl1.SelectedTab = tabPage1;
            cambioPermitido = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            CalcularYMostrarEstadisticas();
            cambioPermitido = true;
            tabControl1.SelectedTab = tabPage5;
            cambioPermitido = false;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            numeroProcesos = 0;
            quantum = 0;
            contadorProcesos = 1;
            label3.Text = "Proceso #1";
            button9.Visible = true;
            cambioPermitido = true;
            tabControl1.SelectedTab = tabPage1;
            cambioPermitido = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            listaProcesos.Clear();
            dataGridView2.Rows.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form2 mostrarInformacion = new Form2();
            mostrarInformacion.ShowDialog();
        }
    }
}