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
            this.SuspendLayout();
            // 
            // cmdAnalizar
            // 
            this.cmdAnalizar.Location = new System.Drawing.Point(510, 293);
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
            this.txtEntrada.Location = new System.Drawing.Point(30, 25);
            this.txtEntrada.Name = "txtEntrada";
            this.txtEntrada.Size = new System.Drawing.Size(682, 227);
            this.txtEntrada.TabIndex = 2;
            // 
            // Ventana
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 355);
            this.Controls.Add(this.txtEntrada);
            this.Controls.Add(this.cmdAnalizar);
            this.Name = "Ventana";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Ventana_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdAnalizar;
        private FastColoredTextBoxNS.FastColoredTextBox txtEntrada;
    }
}

