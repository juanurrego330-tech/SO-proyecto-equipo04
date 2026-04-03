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
    }
}
