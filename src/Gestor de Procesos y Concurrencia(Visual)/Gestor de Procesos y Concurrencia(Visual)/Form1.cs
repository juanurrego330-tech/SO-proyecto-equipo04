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
        int quantum;
        int numeroProcesos;
        int contadorProcesos = 1;
        bool cambioPermitido = false;
        int relojGlobal = 0;
        private List<string[]> listaProcesos = new List<string[]>();
        private readonly object cpuLock = new object();
        private readonly object fileLock = new object();
        private Queue<int> colaListos = new Queue<int>();
        private readonly object queueLock = new object();
        public bool ValidarCampos(string a)
        {
            if (a == "")
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return false;
            }
            return true;
        }
        public bool ValidarNumerosInicio()
        {
            if (!int.TryParse(textBox1.Text, out int numeroproces))
            {
                MessageBox.Show("Por favor, ingrese un número válido de procesos (Entero)");
                return false;
            }
            if (!int.TryParse(textBox2.Text, out int valorquantum))
            {
                MessageBox.Show("Por favor, ingrese un número válido para el quantum (Entero)");
                return false;
            }
            numeroProcesos = numeroproces;
            quantum = valorquantum;
            return true;
        }
        public bool ValidarNumerosIngresoDatos()
        {
            if (!int.TryParse(textBox4.Text, out int arrivaltime))
            {
                MessageBox.Show("Por favor, ingrese un número válido de Arrival (Entero)");
                return false;
            }
            if (!int.TryParse(textBox5.Text, out int bursttime))
            {
                MessageBox.Show("Por favor, ingrese un número válido para el Burst (Entero)");
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidarCampos(textBox1.Text) && ValidarCampos(textBox2.Text))
            {
                if (ValidarNumerosInicio())
                {
                    cambioPermitido = true;
                    tabControl1.SelectedTab = tabPage2;
                    cambioPermitido = false;
                }
            }
        }
        public void limpiarCampos()
        {
            label3.Text = "Proceso #" + contadorProcesos;
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        public void GuardarDatosProceso()
        {
            string[] Proceso = new string[4];
            Proceso[0] = "#" + contadorProcesos;
            Proceso[1] = textBox3.Text;
            Proceso[2] = textBox4.Text;
            Proceso[3] = textBox5.Text;
            listaProcesos.Add(Proceso);
        }

        public void mostrarDatosProcesos()
        {
            dataGridView1.Rows.Clear();
            foreach (string[] a in listaProcesos)
            {
                dataGridView1.Rows.Add(a);
            }
        }

        private void SysCall_WriteLog(string mensaje)
        {
            lock (fileLock)
            {
                try
                {
                    string carpetaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                    string rutaArchivo = Path.Combine(carpetaDescargas, "log_procesos.txt");

                    using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
                    {
                        string registro = $"[{DateTime.Now:HH:mm:ss.fff}] - {mensaje}";
                        sw.WriteLine(registro);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidarCampos(textBox3.Text) && ValidarCampos(textBox4.Text) && ValidarCampos(textBox5.Text))
            {
                if (ValidarNumerosIngresoDatos())
                {
                    GuardarDatosProceso();

                    if (contadorProcesos >= numeroProcesos)
                    {
                        cambioPermitido = true;
                        tabControl1.SelectedTab = tabPage3;
                        cambioPermitido = false;
                        mostrarDatosProcesos();
                    }
                    else
                    {
                        contadorProcesos++;
                        limpiarCampos();
                    }
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!cambioPermitido)
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cambioPermitido = true;
            tabControl1.SelectedTab = tabPage4;
            cambioPermitido = false;
            try
            {
                string carpetaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string rutaArchivo = Path.Combine(carpetaDescargas, "log_procesos.txt");
                if (File.Exists(rutaArchivo)) File.Delete(rutaArchivo);
            }
            catch { }

            dataGridView2.Rows.Clear();
            foreach (var a in listaProcesos)
            {
                dataGridView2.Rows.Add(a[0], a[1], a[2], a[3], "ESPERANDO ARRIVAL", "0%");
            }

            button4.Visible = false;

            Thread director = new Thread(() => {
                relojGlobal = 0;
                int procesosAdmitidos = 0;
                List<Thread> hilosProcesos = new List<Thread>();

                while (procesosAdmitidos < listaProcesos.Count || hilosProcesos.Any(h => h.IsAlive))
                {
                    for (int i = 0; i < listaProcesos.Count; i++)
                    {
                        int arrival = int.Parse(listaProcesos[i][2]);
                        string estadoActual = "";

                        this.Invoke(new Action(() => {
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
                            SysCall_WriteLog($"ADMISIÓN: PROCESO {nombre} entró al sistema en T={relojGlobal}.");
                        }
                    }

                    Thread.Sleep(1000); 
                    relojGlobal++;
                }

                this.Invoke(new Action(() => {
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
                        ActualizarUI(fila, "LISTO (Ready)", Color.Orange, CalcularPorcentaje(burst, tiempoRestante));
                        Thread.Sleep(100);
                    }
                }

                lock (cpuLock)
                {
                    int aEjecutar = Math.Min(tiempoRestante, quantum);
                    SysCall_WriteLog($"KERNEL: {nombreID} inicia ráfaga de {aEjecutar}s.");

                    for (int i = 0; i < aEjecutar; i++)
                    {
                        ActualizarUI(fila, "EJECUCIÓN (Running)", Color.LightGreen, CalcularPorcentaje(burst, tiempoRestante));
                        Thread.Sleep(600);
                        tiempoRestante--;
                        ActualizarUI(fila, "EJECUCIÓN (Running)", Color.LightGreen, CalcularPorcentaje(burst, tiempoRestante));
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
                        ActualizarUI(fila, "LISTO (Ready)", Color.Orange, CalcularPorcentaje(burst, tiempoRestante));
                        SysCall_WriteLog($"SISTEMA: {nombreID} suspendido (le faltan {tiempoRestante}s).");
                    }
                }
                Thread.Sleep(200);
            }

            ActualizarUI(fila, "TERMINADO", Color.LightGray, 100);
            SysCall_WriteLog($"SISTEMA: {nombreID} terminó.");
        }

        private int CalcularPorcentaje(int total, int restante)
        {
            return 100 - (restante * 100 / total);
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
    }
}