namespace CG2._1
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbGraficos = new System.Windows.Forms.PictureBox();
            this.Primitivas = new System.Windows.Forms.TabControl();
            this.Graficas = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPMElipse = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rbPMCirc = new System.Windows.Forms.RadioButton();
            this.rbGeralCirc = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.rbTrignometria = new System.Windows.Forms.RadioButton();
            this.rbPMReta = new System.Windows.Forms.RadioButton();
            this.rbGeralReta = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbDDA = new System.Windows.Forms.RadioButton();
            this.Poligonos = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraficos)).BeginInit();
            this.Primitivas.SuspendLayout();
            this.Graficas.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbGraficos
            // 
            this.pbGraficos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGraficos.Image = ((System.Drawing.Image)(resources.GetObject("pbGraficos.Image")));
            this.pbGraficos.InitialImage = null;
            this.pbGraficos.Location = new System.Drawing.Point(12, 12);
            this.pbGraficos.Name = "pbGraficos";
            this.pbGraficos.Size = new System.Drawing.Size(677, 519);
            this.pbGraficos.TabIndex = 1;
            this.pbGraficos.TabStop = false;
            this.pbGraficos.Click += new System.EventHandler(this.PbGraficos_Click);
            // 
            // Primitivas
            // 
            this.Primitivas.AccessibleName = "";
            this.Primitivas.Controls.Add(this.Graficas);
            this.Primitivas.Controls.Add(this.Poligonos);
            this.Primitivas.Location = new System.Drawing.Point(731, 12);
            this.Primitivas.Name = "Primitivas";
            this.Primitivas.SelectedIndex = 0;
            this.Primitivas.Size = new System.Drawing.Size(208, 518);
            this.Primitivas.TabIndex = 2;
            // 
            // Graficas
            // 
            this.Graficas.Controls.Add(this.groupBox1);
            this.Graficas.Location = new System.Drawing.Point(4, 22);
            this.Graficas.Name = "Graficas";
            this.Graficas.Padding = new System.Windows.Forms.Padding(3);
            this.Graficas.Size = new System.Drawing.Size(200, 492);
            this.Graficas.TabIndex = 0;
            this.Graficas.Text = "Gráficas";
            this.Graficas.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPMElipse);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rbPMCirc);
            this.groupBox1.Controls.Add(this.rbGeralCirc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbTrignometria);
            this.groupBox1.Controls.Add(this.rbPMReta);
            this.groupBox1.Controls.Add(this.rbGeralReta);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbDDA);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 492);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbPMElipse
            // 
            this.rbPMElipse.AutoSize = true;
            this.rbPMElipse.Location = new System.Drawing.Point(59, 235);
            this.rbPMElipse.Name = "rbPMElipse";
            this.rbPMElipse.Size = new System.Drawing.Size(85, 17);
            this.rbPMElipse.TabIndex = 9;
            this.rbPMElipse.TabStop = true;
            this.rbPMElipse.Text = "Ponto Médio";
            this.rbPMElipse.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Elipse:";
            // 
            // rbPMCirc
            // 
            this.rbPMCirc.AutoSize = true;
            this.rbPMCirc.Location = new System.Drawing.Point(59, 184);
            this.rbPMCirc.Name = "rbPMCirc";
            this.rbPMCirc.Size = new System.Drawing.Size(85, 17);
            this.rbPMCirc.TabIndex = 7;
            this.rbPMCirc.TabStop = true;
            this.rbPMCirc.Text = "Ponto Médio";
            this.rbPMCirc.UseVisualStyleBackColor = true;
            // 
            // rbGeralCirc
            // 
            this.rbGeralCirc.AutoSize = true;
            this.rbGeralCirc.Location = new System.Drawing.Point(59, 138);
            this.rbGeralCirc.Name = "rbGeralCirc";
            this.rbGeralCirc.Size = new System.Drawing.Size(69, 17);
            this.rbGeralCirc.TabIndex = 6;
            this.rbGeralCirc.TabStop = true;
            this.rbGeralCirc.Text = "Eq. Geral";
            this.rbGeralCirc.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Circuferência:";
            // 
            // rbTrignometria
            // 
            this.rbTrignometria.AutoSize = true;
            this.rbTrignometria.Location = new System.Drawing.Point(59, 161);
            this.rbTrignometria.Name = "rbTrignometria";
            this.rbTrignometria.Size = new System.Drawing.Size(89, 17);
            this.rbTrignometria.TabIndex = 4;
            this.rbTrignometria.TabStop = true;
            this.rbTrignometria.Text = "Trigonometria";
            this.rbTrignometria.UseVisualStyleBackColor = true;
            // 
            // rbPMReta
            // 
            this.rbPMReta.AutoSize = true;
            this.rbPMReta.Location = new System.Drawing.Point(59, 89);
            this.rbPMReta.Name = "rbPMReta";
            this.rbPMReta.Size = new System.Drawing.Size(85, 17);
            this.rbPMReta.TabIndex = 3;
            this.rbPMReta.TabStop = true;
            this.rbPMReta.Text = "Ponto Médio";
            this.rbPMReta.UseVisualStyleBackColor = true;
            // 
            // rbGeralReta
            // 
            this.rbGeralReta.AutoSize = true;
            this.rbGeralReta.Location = new System.Drawing.Point(59, 43);
            this.rbGeralReta.Name = "rbGeralReta";
            this.rbGeralReta.Size = new System.Drawing.Size(69, 17);
            this.rbGeralReta.TabIndex = 2;
            this.rbGeralReta.TabStop = true;
            this.rbGeralReta.Text = "Eq. Geral";
            this.rbGeralReta.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Retas:";
            // 
            // rbDDA
            // 
            this.rbDDA.AutoSize = true;
            this.rbDDA.Location = new System.Drawing.Point(59, 66);
            this.rbDDA.Name = "rbDDA";
            this.rbDDA.Size = new System.Drawing.Size(48, 17);
            this.rbDDA.TabIndex = 0;
            this.rbDDA.TabStop = true;
            this.rbDDA.Text = "DDA";
            this.rbDDA.UseVisualStyleBackColor = true;
            this.rbDDA.CheckedChanged += new System.EventHandler(this.RbDDA_CheckedChanged);
            // 
            // Poligonos
            // 
            this.Poligonos.Location = new System.Drawing.Point(4, 22);
            this.Poligonos.Name = "Poligonos";
            this.Poligonos.Padding = new System.Windows.Forms.Padding(3);
            this.Poligonos.Size = new System.Drawing.Size(200, 492);
            this.Poligonos.TabIndex = 1;
            this.Poligonos.Text = "Polígonos";
            this.Poligonos.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 543);
            this.Controls.Add(this.Primitivas);
            this.Controls.Add(this.pbGraficos);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbGraficos)).EndInit();
            this.Primitivas.ResumeLayout(false);
            this.Graficas.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pbGraficos;
        private System.Windows.Forms.TabControl Primitivas;
        private System.Windows.Forms.TabPage Graficas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPMElipse;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbPMCirc;
        private System.Windows.Forms.RadioButton rbGeralCirc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbTrignometria;
        private System.Windows.Forms.RadioButton rbPMReta;
        private System.Windows.Forms.RadioButton rbGeralReta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbDDA;
        private System.Windows.Forms.TabPage Poligonos;
    }
}

