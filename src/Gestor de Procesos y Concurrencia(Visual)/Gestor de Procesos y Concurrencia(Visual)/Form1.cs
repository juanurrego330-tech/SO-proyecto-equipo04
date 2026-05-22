using Microsoft.VisualBasic.Devices;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private int quantum;
        private int numeroProcesos;
        private int contadorProcesos = 1;
        private bool cambioPermitido = false;
        private int relojGlobal = 0;
        private List<string[]> listaProcesos = new List<string[]>();
        private List<Procesos> listaProcesos2 = new List<Procesos>();
        private readonly object cpuLock = new object();
        private Queue<int> colaListos = new Queue<int>();
        private readonly object queueLock = new object();
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
                    listaProcesos2.Add(new Procesos(contadorProcesos, textBox3.Text, textBox4.Text, textBox5.Text));

                    if (contadorProcesos >= numeroProcesos)
                    {
                        cambioPermitido = true;
                        tabControl1.SelectedTab = tabPage3;
                        cambioPermitido = false;
                        V.mostrarDatosProcesos(dataGridView1, listaProcesos2);
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
                dataGridView2.Rows.Add(a[0], a[1], a[2], a[3], "ESPERANDO ARRIVAL", "0%");
            }

            button4.Visible = false;

            Thread director = new Thread(() =>
            {
                relojGlobal = 0;
                int procesosAdmitidos = 0;
                List<Thread> hilosProcesos = new List<Thread>();

                while (procesosAdmitidos < listaProcesos.Count || hilosProcesos.Any(h => h.IsAlive))
                {
                    for (int i = 0; i < listaProcesos.Count; i++)
                    {
                        int arrival = int.Parse(listaProcesos[i][2]);
                        string estadoActual = "";

                        this.Invoke(new Action(() =>
                        {
                            if (dataGridView2.Rows[i].Cells[4].Value != null)
                                estadoActual = dataGridView2.Rows[i].Cells[4].Value.ToString();
                        }));

                        if (estadoActual == "ESPERANDO ARRIVAL" && arrival <= relojGlobal)
                        {
                            int index = i;
                            string nombre = listaProcesos[i][0];
                            int burst = int.Parse(listaProcesos[i][3]);
                            lock (queueLock)
                            {
                                colaListos.Enqueue(index);
                            }
                            Thread h = new Thread(() => LogicaProceso(index, nombre, burst));
                            h.IsBackground = true;
                            hilosProcesos.Add(h);
                            h.Start();

                            procesosAdmitidos++;
                            Logs.SysCall_WriteLog($"ADMISIÓN: PROCESO {nombre} entró al sistema en T={relojGlobal}.");
                        }
                    }

                    Thread.Sleep(1000);
                    relojGlobal++;
                }

                this.Invoke(new Action(() =>
                {
                    button4.Visible = true;
                }));
            });

            director.Start();
        }
        private void LogicaProceso(int fila, string nombreID, int burst)
        {
            int tiempoRestante = burst;

            while (tiempoRestante > 0)
            {
                bool miTurno = false;
                while (!miTurno)
                {
                    lock (queueLock)
                    {
                        if (colaListos.Count > 0 && colaListos.Peek() == fila)
                            miTurno = true;
                    }
                    if (!miTurno)
                    {
                        ActualizarUI(fila, "LISTO (Ready)", Color.Orange, V.CalcularPorcentaje(burst, tiempoRestante));
                        Thread.Sleep(100);
                    }
                }

                lock (cpuLock)
                {
                    int aEjecutar = Math.Min(tiempoRestante, quantum);
                    Logs.SysCall_WriteLog($"KERNEL: {nombreID} inicia ráfaga de {aEjecutar}s.");

                    for (int i = 0; i < aEjecutar; i++)
                    {
                        ActualizarUI(fila, "EJECUCIÓN (Running)", Color.LightGreen, V.CalcularPorcentaje(burst, tiempoRestante));
                        Thread.Sleep(600);
                        tiempoRestante--;
                        ActualizarUI(fila, "EJECUCIÓN (Running)", Color.LightGreen, V.CalcularPorcentaje(burst, tiempoRestante));
                    }
                }

                lock (queueLock)
                {
                    if (colaListos.Count > 0 && colaListos.Peek() == fila)
                    {
                        colaListos.Dequeue();
                    }

                    if (tiempoRestante > 0)
                    {
                        colaListos.Enqueue(fila);
                        ActualizarUI(fila, "LISTO (Ready)", Color.Orange, V.CalcularPorcentaje(burst, tiempoRestante));
                        Logs.SysCall_WriteLog($"SISTEMA: {nombreID} suspendido (le faltan {tiempoRestante}s).");
                    }
                }
                Thread.Sleep(200);
            }

            ActualizarUI(fila, "TERMINADO", Color.LightGray, 100);
            Logs.SysCall_WriteLog($"SISTEMA: {nombreID} terminó.");
        }
        private void ActualizarUI(int fila, string estado, Color color, int progreso)
        {
            if (dataGridView2.InvokeRequired)
            {
                dataGridView2.Invoke(new Action(() =>
                {
                    dataGridView2.Rows[fila].Cells[4].Value = estado;
                    dataGridView2.Rows[fila].Cells[5].Value = progreso + "%";
                    dataGridView2.Rows[fila].DefaultCellStyle.BackColor = color;
                }));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            numeroProcesos = 0;
            quantum = 0;
            listaProcesos2.Clear();
            contadorProcesos = 1;
            label3.Text = "Proceso #1";
            cambioPermitido = true;
            tabControl1.SelectedTab = tabPage1;
            cambioPermitido = false;
        }
    }
}