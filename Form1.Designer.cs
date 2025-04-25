namespace OpenTKGUI
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
            glControl1 = new OpenTK.GLControl.GLControl();
            menuStrip1 = new MenuStrip();
            archivoToolStripMenuItem = new ToolStripMenuItem();
            abrirToolStripMenuItem = new ToolStripMenuItem();
            guardarComoToolStripMenuItem = new ToolStripMenuItem();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            numericUpDown1 = new NumericUpDown();
            labelEscenario = new Label();
            labelTraslacion = new Label();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown6 = new NumericUpDown();
            labelEscalacion = new Label();
            numericUpDown5 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            numericUpDown7 = new NumericUpDown();
            numericUpDown8 = new NumericUpDown();
            labelRotacion = new Label();
            numericUpDown9 = new NumericUpDown();
            comboBox1 = new ComboBox();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown9).BeginInit();
            SuspendLayout();
            // 
            // glControl1
            // 
            glControl1.API = OpenTK.Windowing.Common.ContextAPI.OpenGL;
            glControl1.APIVersion = new Version(3, 3, 0, 0);
            glControl1.Flags = OpenTK.Windowing.Common.ContextFlags.Default;
            glControl1.IsEventDriven = true;
            glControl1.Location = new Point(380, 12);
            glControl1.Name = "glControl1";
            glControl1.Profile = OpenTK.Windowing.Common.ContextProfile.Core;
            glControl1.SharedContext = null;
            glControl1.Size = new Size(761, 618);
            glControl1.TabIndex = 0;
            glControl1.Load += glControl1_Load;
            glControl1.Paint += glControl1_Paint;
            glControl1.MouseDown += glControl1_MouseDown;
            glControl1.MouseMove += glControl1_MouseMove;
            glControl1.MouseUp += glControl1_MouseUp;
            glControl1.Resize += glControl1_Resize;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { archivoToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1153, 28);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            archivoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { abrirToolStripMenuItem, guardarComoToolStripMenuItem });
            archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            archivoToolStripMenuItem.Size = new Size(73, 24);
            archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            abrirToolStripMenuItem.Size = new Size(189, 26);
            abrirToolStripMenuItem.Text = "Abrir";
            abrirToolStripMenuItem.Click += abrirToolStripMenuItem_Click;
            // 
            // guardarComoToolStripMenuItem
            // 
            guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            guardarComoToolStripMenuItem.Size = new Size(189, 26);
            guardarComoToolStripMenuItem.Text = "Guardar Como";
            guardarComoToolStripMenuItem.Click += guardarComoToolStripMenuItem_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(101, 166);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(82, 27);
            numericUpDown1.TabIndex = 2;
            // 
            // labelEscenario
            // 
            labelEscenario.AutoSize = true;
            labelEscenario.Location = new Point(22, 40);
            labelEscenario.Name = "labelEscenario";
            labelEscenario.Size = new Size(72, 20);
            labelEscenario.TabIndex = 3;
            labelEscenario.Text = "Escenario";
            // 
            // labelTraslacion
            // 
            labelTraslacion.AutoSize = true;
            labelTraslacion.Location = new Point(22, 168);
            labelTraslacion.Name = "labelTraslacion";
            labelTraslacion.Size = new Size(73, 20);
            labelTraslacion.TabIndex = 4;
            labelTraslacion.Text = "traslacion";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(189, 166);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(82, 27);
            numericUpDown2.TabIndex = 5;
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(277, 166);
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(82, 27);
            numericUpDown3.TabIndex = 6;
            // 
            // numericUpDown6
            // 
            numericUpDown6.Location = new Point(277, 220);
            numericUpDown6.Name = "numericUpDown6";
            numericUpDown6.Size = new Size(82, 27);
            numericUpDown6.TabIndex = 7;
            // 
            // labelEscalacion
            // 
            labelEscalacion.AutoSize = true;
            labelEscalacion.Location = new Point(22, 220);
            labelEscalacion.Name = "labelEscalacion";
            labelEscalacion.Size = new Size(78, 20);
            labelEscalacion.TabIndex = 8;
            labelEscalacion.Text = "escalacion";
            // 
            // numericUpDown5
            // 
            numericUpDown5.Location = new Point(189, 220);
            numericUpDown5.Name = "numericUpDown5";
            numericUpDown5.Size = new Size(82, 27);
            numericUpDown5.TabIndex = 9;
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(101, 220);
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(82, 27);
            numericUpDown4.TabIndex = 10;
            // 
            // numericUpDown7
            // 
            numericUpDown7.Location = new Point(101, 266);
            numericUpDown7.Name = "numericUpDown7";
            numericUpDown7.Size = new Size(82, 27);
            numericUpDown7.TabIndex = 14;
            // 
            // numericUpDown8
            // 
            numericUpDown8.Location = new Point(189, 266);
            numericUpDown8.Name = "numericUpDown8";
            numericUpDown8.Size = new Size(82, 27);
            numericUpDown8.TabIndex = 13;
            // 
            // labelRotacion
            // 
            labelRotacion.AutoSize = true;
            labelRotacion.Location = new Point(22, 266);
            labelRotacion.Name = "labelRotacion";
            labelRotacion.Size = new Size(64, 20);
            labelRotacion.TabIndex = 12;
            labelRotacion.Text = "rotacion";
            // 
            // numericUpDown9
            // 
            numericUpDown9.Location = new Point(277, 266);
            numericUpDown9.Name = "numericUpDown9";
            numericUpDown9.Size = new Size(82, 27);
            numericUpDown9.TabIndex = 11;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Escenario" });
            comboBox1.Location = new Point(22, 76);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 28);
            comboBox1.TabIndex = 15;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1153, 642);
            Controls.Add(comboBox1);
            Controls.Add(numericUpDown7);
            Controls.Add(numericUpDown8);
            Controls.Add(labelRotacion);
            Controls.Add(numericUpDown9);
            Controls.Add(numericUpDown4);
            Controls.Add(numericUpDown5);
            Controls.Add(labelEscalacion);
            Controls.Add(numericUpDown6);
            Controls.Add(numericUpDown3);
            Controls.Add(numericUpDown2);
            Controls.Add(labelTraslacion);
            Controls.Add(labelEscenario);
            Controls.Add(numericUpDown1);
            Controls.Add(glControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown6).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown5).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown7).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown8).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown9).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenTK.GLControl.GLControl glControl1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem archivoToolStripMenuItem;
        private ToolStripMenuItem abrirToolStripMenuItem;
        private ToolStripMenuItem guardarComoToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private NumericUpDown numericUpDown1;
        private Label labelEscenario;
        private Label labelTraslacion;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown6;
        private Label labelEscalacion;
        private NumericUpDown numericUpDown5;
        private NumericUpDown numericUpDown4;
        private NumericUpDown numericUpDown7;
        private NumericUpDown numericUpDown8;
        private Label labelRotacion;
        private NumericUpDown numericUpDown9;
        private ComboBox comboBox1;
    }
}
