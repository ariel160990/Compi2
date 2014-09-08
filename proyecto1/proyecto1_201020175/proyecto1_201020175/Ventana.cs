using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Irony.Parsing;

namespace Proyecto1
{
    public partial class Ventana : Form
    {
        private int idnodografo;
        private string cadenagrafo;

        public Ventana()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtEntrada.Text != null) {
                Analizador a = new Analizador();
                Object str = a.parse(txtEntrada.Text);
                if (str != null)
                {
                    MessageBox.Show(str.ToString());
                }
                else {
                    MessageBox.Show("Error");
                }
            }
            graficarArbol();
            probarCadena();
        }

        private void probarCadena() {
            Analizador an = new Analizador();
            string entrada = txtEntrada.Text;
            Grammar gramatica = new Sintactic();
            if (an.isValid(entrada, gramatica))
            {
                Console.WriteLine("exito");
            }
            else {
                Console.WriteLine("no exito");
            }
        }

        private void graficarArbol()
        {
            Analizador an = new Analizador();
            string entrada = txtEntrada.Text;
            Grammar gramatica = new Sintactic();
            if (an.isValid(entrada, gramatica))
            {
                Console.WriteLine("éxito!!!!!!!!!!!!!!!");
                ParseTreeNode raiz = an.getRoot(entrada, gramatica);
                idnodografo = 0;
                cadenagrafo = "";
                graficar(raiz);

                var archivo = "C:\\compi2\\reporte\\cadenaarbol.txt";
                // eliminar el fichero si ya existe
                if (File.Exists(archivo))
                    File.Delete(archivo);
                // crear el fichero
                using (var fileStream = File.Create(archivo))
                {
                    //var texto = new UTF8Encoding(true).GetBytes(cadenagrafo);
                    var texto = new UnicodeEncoding().GetBytes(cadenagrafo);
                    fileStream.Write(texto, 0, texto.Length);
                    fileStream.Flush();
                }
            }
            else
            {
                Console.WriteLine("La cadena tiene errores y no se puede finalizar el análisis");
            }
        }

        private void graficar(ParseTreeNode raiz)
        {
            cadenagrafo = "digraph G { \n node [shape=record,height=.1];";
            correr(raiz, idnodografo);
            cadenagrafo = cadenagrafo + "}";
            Console.WriteLine(cadenagrafo);
        }

        private void correr(ParseTreeNode nodo, int n)
        {
            if (nodo.Token == null)
            {
                cadenagrafo = cadenagrafo + "node" + n.ToString() + "[label=\"" + nodo.Term.Name + "\"];\n";
            }
            else
            {
                string cad1 = nodo.Token.Text.Replace(">", "\\>");
                cad1 = cad1.Replace("<", "\\<");
                cad1 = cad1.Replace("\"","\\\"");
                //cadenagrafo = cadenagrafo + "node" + n.ToString() + "[label=\"" + nodo.Token.Text + "\"];\n";
                cadenagrafo = cadenagrafo + "node" + n.ToString() + "[label=\"" + cad1 + "\"];\n";
            }

            foreach (ParseTreeNode child in nodo.ChildNodes)
            {
                int n1 = nuevoidnodografo();
                correr(child, n1);
                cadenagrafo = cadenagrafo + "node" + n.ToString() + "->node" + n1.ToString() + ";";
            }
        }

        private int nuevoidnodografo()
        {
            idnodografo++;
            return idnodografo;
        }

        private void Ventana_Load(object sender, EventArgs e)
        {

        }

    }
}
