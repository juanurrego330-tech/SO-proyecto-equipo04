namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            button2 = new Button();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            tabPage3 = new TabPage();
            button3 = new Button();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            tabPage4 = new TabPage();
            button4 = new Button();
            dataGridView2 = new DataGridView();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ActiveCaptionText;
            label1.Font = new Font("Segoe UI", 14F);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(229, 274);
            label1.Name = "label1";
            label1.Size = new Size(349, 32);
            label1.TabIndex = 0;
            label1.Text = "Ingrese el numero de procesos:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ActiveCaptionText;
            label2.Font = new Font("Segoe UI", 14F);
            label2.ForeColor = SystemColors.ButtonHighlight;
            label2.Location = new Point(248, 325);
            label2.Name = "label2";
            label2.Size = new Size(330, 32);
            label2.TabIndex = 1;
            label2.Text = "Ingrese el valor del Quantum:";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.None;
            textBox1.Location = new Point(584, 279);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 27);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.None;
            textBox2.Location = new Point(584, 330);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 27);
            textBox2.TabIndex = 3;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.Location = new Point(584, 386);
            button1.Name = "button1";
            button1.Size = new Size(100, 28);
            button1.TabIndex = 4;
            button1.Text = "Aceptar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1062, 673);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 5;
            tabControl1.Selecting += tabControl1_Selecting;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Black;
            tabPage1.BackgroundImageLayout = ImageLayout.None;
            tabPage1.BorderStyle = BorderStyle.Fixed3D;
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(textBox2);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Font = new Font("Segoe UI", 9F);
            tabPage1.Location = new Point(4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1054, 664);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Inicio";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.Black;
            tabPage2.Controls.Add(textBox5);
            tabPage2.Controls.Add(textBox4);
            tabPage2.Controls.Add(textBox3);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1054, 645);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Ingreso Datos";
            // 
            // textBox5
            // 
            textBox5.Anchor = AnchorStyles.None;
            textBox5.Location = new Point(472, 306);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 27);
            textBox5.TabIndex = 8;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.None;
            textBox4.Location = new Point(472, 260);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 27);
            textBox4.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.None;
            textBox3.Location = new Point(472, 213);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 27);
            textBox3.TabIndex = 6;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.None;
            button2.Location = new Point(472, 357);
            button2.Name = "button2";
            button2.Size = new Size(100, 27);
            button2.TabIndex = 9;
            button2.Text = "Aceptar";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.BackColor = Color.Black;
            label6.Font = new Font("Segoe UI", 14F);
            label6.ForeColor = SystemColors.ButtonHighlight;
            label6.Location = new Point(337, 301);
            label6.Name = "label6";
            label6.Size = new Size(133, 32);
            label6.TabIndex = 3;
            label6.Text = "Burst Time:";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.BackColor = Color.Black;
            label5.Font = new Font("Segoe UI", 14F);
            label5.ForeColor = SystemColors.ButtonHighlight;
            label5.Location = new Point(324, 255);
            label5.Name = "label5";
            label5.Size = new Size(146, 32);
            label5.TabIndex = 2;
            label5.Text = "Arrival Time:";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.BackColor = Color.Black;
            label4.Font = new Font("Segoe UI", 14F);
            label4.ForeColor = SystemColors.ButtonHighlight;
            label4.Location = new Point(428, 207);
            label4.Name = "label4";
            label4.Size = new Size(42, 32);
            label4.TabIndex = 1;
            label4.Text = "ID:";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.BackColor = Color.Black;
            label3.Font = new Font("Segoe UI", 14F);
            label3.ForeColor = SystemColors.ButtonHighlight;
            label3.Location = new Point(342, 165);
            label3.Name = "label3";
            label3.Size = new Size(131, 32);
            label3.TabIndex = 0;
            label3.Text = "Proceso #1";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.Black;
            tabPage3.Controls.Add(button3);
            tabPage3.Controls.Add(dataGridView1);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1054, 645);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Datos Ingresados";
            // 
            // button3
            // 
            button3.Location = new Point(783, 266);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 1;
            button3.Text = "Iniciar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1054, 645);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "PROCESO";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Width = 125;
            // 
            // Column2
            // 
            Column2.HeaderText = "ID";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 125;
            // 
            // Column3
            // 
            Column3.HeaderText = "ARRIVAL";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.Width = 125;
            // 
            // Column4
            // 
            Column4.HeaderText = "BURST";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.Width = 125;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.Black;
            tabPage4.Controls.Add(button4);
            tabPage4.Controls.Add(dataGridView2);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new Size(1054, 645);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Ejecucion";
            // 
            // button4
            // 
            button4.Location = new Point(881, 269);
            button4.Name = "button4";
            button4.Size = new Size(94, 29);
            button4.TabIndex = 2;
            button4.Text = "Reiniciar";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { Column5, Column6, Column7, Column8, Column9, Column10 });
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(0, 0);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new Size(1054, 645);
            dataGridView2.TabIndex = 0;
            // 
            // Column5
            // 
            Column5.HeaderText = "PROCESO";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Width = 125;
            // 
            // Column6
            // 
            Column6.HeaderText = "ID";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.Width = 125;
            // 
            // Column7
            // 
            Column7.HeaderText = "ARRIVAL";
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.Width = 125;
            // 
            // Column8
            // 
            Column8.HeaderText = "BURST";
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";
            Column8.Width = 125;
            // 
            // Column9
            // 
            Column9.HeaderText = "ESTADO";
            Column9.MinimumWidth = 6;
            Column9.Name = "Column9";
            Column9.Width = 125;
            // 
            // Column10
            // 
            Column10.HeaderText = "PORCENTAJE";
            Column10.MinimumWidth = 6;
            Column10.Name = "Column10";
            Column10.Width = 125;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1062, 673);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Gestor de Procesos y Concurrencia";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label label3;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button button2;
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private Button button3;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column10;
        private Button button4;
    }
}
