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
        private List<string[]> listaProcesos = new List<string[]>();
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
            Proceso[0] = textBox3.Text;
            Proceso[1] = "Arrival Time: " + textBox4.Text;
            Proceso[2] = "Burst Time: " + textBox5.Text;
            listBox1.Items.Add(Proceso);
        }
        /*
        int id = contadorProcesos;
        int tiempoLlegada = int.Parse(textBox3.Text);
        int rafaga = int.Parse(textBox4.Text);
        int prioridad = int.Parse(textBox5.Text);
        Proceso nuevoProceso = new Proceso(id, tiempoLlegada, rafaga, prioridad);
        listBox1.Items.Add(nuevoProceso);
        contadorProcesos++;
        */
        private void button2_Click(object sender, EventArgs e)
        {
            if (ValidarCampos(textBox3.Text) && ValidarCampos(textBox4.Text) && ValidarCampos(textBox5.Text))
            {
                if (ValidarNumerosIngresoDatos())
                {
                    contadorProcesos++;
                    limpiarCampos();
                    if (contadorProcesos > numeroProcesos)
                    {                        
                        cambioPermitido = true;
                        tabControl1.SelectedTab = tabPage3;
                        cambioPermitido = false;
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
    }
}
