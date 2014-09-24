namespace Proyecto1
{
    partial class Ventana
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdAnalizar = new System.Windows.Forms.Button();
            this.txtEntrada = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cmdGuardarPista = new System.Windows.Forms.Button();
            this.cmdCargarPista = new System.Windows.Forms.Button();
            this.txtNombrePista = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtEntradaListas = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cmdAnalizarLista = new System.Windows.Forms.Button();
            this.txtNombreLista = new System.Windows.Forms.TextBox();
            this.cmdGuardarLista = new System.Windows.Forms.Button();
            this.cmdCargarLista = new System.Windows.Forms.Button();
            this.txtMensaje = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdAnalizar
            // 
            this.cmdAnalizar.Location = new System.Drawing.Point(344, 297);
            this.cmdAnalizar.Name = "cmdAnalizar";
            this.cmdAnalizar.Size = new System.Drawing.Size(75, 23);
            this.cmdAnalizar.TabIndex = 1;
            this.cmdAnalizar.Text = "Analizar";
            this.cmdAnalizar.UseVisualStyleBackColor = true;
            this.cmdAnalizar.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtEntrada
            // 
            this.txtEntrada.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.txtEntrada.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEntrada.Location = new System.Drawing.Point(6, 6);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(413, 285);
            this.txtEntrada.TabIndex = 2;
            // 
            // cmdGuardarPista
            // 
            this.cmdGuardarPista.Location = new System.Drawing.Point(458, 24);
            this.cmdGuardarPista.Name = "cmdGuardarPista";
            this.cmdGuardarPista.Size = new System.Drawing.Size(100, 23);
            this.cmdGuardarPista.TabIndex = 3;
            this.cmdGuardarPista.Text = "Guardar Pista";
            this.cmdGuardarPista.UseVisualStyleBackColor = true;
            this.cmdGuardarPista.Click += new System.EventHandler(this.cmdGuardarPista_Click);
            // 
            // cmdCargarPista
            // 
            this.cmdCargarPista.Location = new System.Drawing.Point(458, 53);
            this.cmdCargarPista.Name = "cmdCargarPista";
            this.cmdCargarPista.Size = new System.Drawing.Size(100, 23);
            this.cmdCargarPista.TabIndex = 4;
            this.cmdCargarPista.Text = "Cargar Pista";
            this.cmdCargarPista.UseVisualStyleBackColor = true;
            this.cmdCargarPista.Click += new System.EventHandler(this.cmdCargarPista_Click);
            // 
            // txtNombrePista
            // 
            this.txtNombrePista.Location = new System.Drawing.Point(564, 26);
            this.txtNombrePista.Name = "txtNombrePista";
            this.txtNombrePista.Size = new System.Drawing.Size(150, 20);
            this.txtNombrePista.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(877, 352);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Gray;
            this.tabPage1.Controls.Add(this.txtEntrada);
            this.tabPage1.Controls.Add(this.txtNombrePista);
            this.tabPage1.Controls.Add(this.cmdGuardarPista);
            this.tabPage1.Controls.Add(this.cmdAnalizar);
            this.tabPage1.Controls.Add(this.cmdCargarPista);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(864, 326);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Gray;
            this.tabPage2.Controls.Add(this.cmdCargarLista);
            this.tabPage2.Controls.Add(this.cmdGuardarLista);
            this.tabPage2.Controls.Add(this.txtNombreLista);
            this.tabPage2.Controls.Add(this.cmdAnalizarLista);
            this.tabPage2.Controls.Add(this.txtEntradaListas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(869, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            // 
            // txtEntradaListas
            // 
            this.txtEntradaListas.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.txtEntradaListas.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEntradaListas.Location = new System.Drawing.Point(6, 6);
            this.txtEntradaListas.Name = "txtEntradaListas";
            this.txtEntradaListas.Size = new System.Drawing.Size(416, 260);
            this.txtEntradaListas.TabIndex = 0;
            // 
            // cmdAnalizarLista
            // 
            this.cmdAnalizarLista.Location = new System.Drawing.Point(304, 285);
            this.cmdAnalizarLista.Name = "cmdAnalizarLista";
            this.cmdAnalizarLista.Size = new System.Drawing.Size(99, 23);
            this.cmdAnalizarLista.TabIndex = 1;
            this.cmdAnalizarLista.Text = "Analizar Lista";
            this.cmdAnalizarLista.UseVisualStyleBackColor = true;
            this.cmdAnalizarLista.Click += new System.EventHandler(this.cmdAnalizarLista_Click);
            // 
            // txtNombreLista
            // 
            this.txtNombreLista.Location = new System.Drawing.Point(438, 82);
            this.txtNombreLista.Name = "txtNombreLista";
            this.txtNombreLista.Size = new System.Drawing.Size(141, 20);
            this.txtNombreLista.TabIndex = 2;
            // 
            // cmdGuardarLista
            // 
            this.cmdGuardarLista.Location = new System.Drawing.Point(438, 23);
            this.cmdGuardarLista.Name = "cmdGuardarLista";
            this.cmdGuardarLista.Size = new System.Drawing.Size(111, 23);
            this.cmdGuardarLista.TabIndex = 3;
            this.cmdGuardarLista.Text = "Guardar Lista";
            this.cmdGuardarLista.UseVisualStyleBackColor = true;
            this.cmdGuardarLista.Click += new System.EventHandler(this.cmdGuardarLista_Click);
            // 
            // cmdCargarLista
            // 
            this.cmdCargarLista.Location = new System.Drawing.Point(438, 53);
            this.cmdCargarLista.Name = "cmdCargarLista";
            this.cmdCargarLista.Size = new System.Drawing.Size(111, 23);
            this.cmdCargarLista.TabIndex = 4;
            this.cmdCargarLista.Text = "Cargar Lista";
            this.cmdCargarLista.UseVisualStyleBackColor = true;
            this.cmdCargarLista.Click += new System.EventHandler(this.cmdCargarLista_Click);
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(22, 430);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(513, 172);
            this.txtMensaje.TabIndex = 7;
            this.txtMensaje.Text = "";
            // 
            // Ventana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 614);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.tabControl1);
            this.Location = new System.Drawing.Point(100, 0);
            this.Name = "Ventana";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Ventana_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdAnalizar;
        private FastColoredTextBoxNS.FastColoredTextBox txtEntrada;
        private System.Windows.Forms.Button cmdGuardarPista;
        private System.Windows.Forms.Button cmdCargarPista;
        private System.Windows.Forms.TextBox txtNombrePista;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdCargarLista;
        private System.Windows.Forms.Button cmdGuardarLista;
        private System.Windows.Forms.TextBox txtNombreLista;
        private System.Windows.Forms.Button cmdAnalizarLista;
        private FastColoredTextBoxNS.FastColoredTextBox txtEntradaListas;
        private System.Windows.Forms.RichTextBox txtMensaje;
    }
}

