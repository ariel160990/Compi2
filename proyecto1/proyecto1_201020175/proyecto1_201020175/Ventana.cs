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
using Toub.Sound.Midi;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Proyecto1
{
    public partial class Ventana : Form
    {
        private int idnodografo;
        private string cadenagrafo;

        public Ventana()
        {
            /*Canales a =  new Canales();
            NoteOn nueva = new NoteOn(100, 1, "c3", 127);
            a.agregarNotas(1, nueva);
            nueva = new NoteOn(100, 1, "c3", 127);
            a.agregarNotas(1, nueva);
            nueva = new NoteOn(1000, 1, "d3", 127);
            a.agregarNotas(1, nueva);
            nueva = new NoteOn(100, 1, "e3", 127);
            a.agregarNotas(1, nueva);
            nueva = new NoteOn(100, 1, "f3", 127);
            a.agregarNotas(1, nueva);
            nueva = new NoteOn(100, 1, "g3", 127);
            a.agregarNotas(1, nueva);*/
            /*MidiPlayer.OpenMidi();
            MidiPlayer.Play(new NoteOn(0, 1, "c3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 1, "c3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "e3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "e3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "g3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "g3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "f3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "f3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "f3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "f3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "e3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "e3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "d3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "d3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "c3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "c3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "e3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "e3", 127));
            MidiPlayer.Play(new NoteOn(0, 10, "c3", 127));
            Thread.Sleep(1000);
            MidiPlayer.Play(new NoteOff(0, 10, "c3", 127));*/
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
                    MessageBox.Show("Reproduciendo");
                }
            }
            graficarArbol();
            //System.Diagnostics.Process.Start(@"C:\\Reportes\\arbol.bat");
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

                var archivo = "C:\\Reportes\\arbol.txt";
                // eliminar el fichero si ya existe
                if (File.Exists(archivo))
                    File.Delete(archivo);
                // crear el fichero
                using (var fileStream = File.Create(archivo))
                {
                    var texto = new UTF8Encoding(true).GetBytes(cadenagrafo);
                    //var texto = new UnicodeEncoding().GetBytes(cadenagrafo);
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
                cad1 = cad1.Replace("{","\\{");
                cad1 = cad1.Replace("}","\\}");
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
            //Console.Beep(100, 10000);
            //Console.Beep(10000, 100000);
            /*double da = 2.212;
            int db = (int)da;
            Console.WriteLine("valor1: "+da);
            Console.WriteLine("valor: "+db);
            */
            /*int ci = 40;
            char ca = (char)ci;
            Console.WriteLine("valor1: " + ci);
            Console.WriteLine("valor: " + ca);
            */

            /*char ch = 'a';
            int chs = (int)ch;
            Console.WriteLine("write: "+chs);
            */
            Console.WriteLine("lennnn: "+"\n".Length);
            string ab = "a";
            Console.WriteLine(ab.ToCharArray());

            string a = "a,b";
            Console.WriteLine(a.Split(',')[1].ToString());
            Console.WriteLine(a.Split(',')[a.Split(',').Length-1].ToString());

            //var entero arreglo arr [3][3][2] = {{{1,2,3},{4,5,6},{7,8,9}},{{10,11,12},{13,14,15},{16,17,18}}}
            //var entero arreglo arr [3][4][2][6] = {{{{1,2,3},{4,5,6},{7,8,9},{10,11,12}},{{13,14,15},{16,17,18},{19,20,21},{22,23,24}}},{{{25,26,27},{28,29,30},{31,32,33},{34,35,36}},{{37,38,39},{40,41,42},{43,44,45},{46,47,48}}},{{{49,50,51},{52,53,54},{55,56,57},{58,59,60}},{{61,62,63},{64,65,66},{67,68,69},{70,71,72}}},{{{73,74,75},{76,77,78},{79,80,81},{82,83,84}},{{85,86,87},{88,89,90},{91,92,93},{94,95,96}}},{{{97,98,99},{100,101,102},{103,104,105},{106,107,108}},{{109,110,111},{112,113,114},{115,116,117},{118,119,120}}},{{{121,122,123},{124,125,126},{127,128,129},{130,131,132}},{{133,134,135},{136,137,138},{139,140,141},{142,143,144}}}}
            //int[] indices = new int[] {1,0,0,0};
            //int[] totales = new int[] {3,4,2,6};//al revez
            //int[] arreglo = new int[144] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70,71,72,73,74,75,76,77,78,79,80,81,82,83,84,85,86,87,88,89,90,91,92,93,94,95,96,97,98,99,100,101,102,103,104,105,106,107,108,109,110,111,112,113,114,115,116,117,118,119,120,121,122,123,124,125,126,127,128,129,130,131,132,133,134,135,136,137,138,139,140,141,142,143,144};

            int[] dim = new int[] { 2, 3, 3 };
            int[] indices = new int[] {1,0,2};
            //Console.WriteLine("len: "+dim.Length);
            int[] arreglo = new int[18] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18};
            Console.WriteLine(arreglo[getIndice(indices,dim)]);
            //Console.WriteLine(mapeo(indices,totales,indices.Length-1));
            //Console.WriteLine("valor: " + arreglo[(int)mapeo(indices, totales, indices.Length - 1)]);
        }

        public object mapeo(int[] indices, int[] totales, int nivel){
            int retorno;
            if (nivel > 0) {
                //retorno = (int)mapeo(indices, totales, nivel - 1) * totales[totales.Length-nivel-1] + indices[nivel];
                retorno = (int)mapeo(indices, totales, nivel - 1) * totales[nivel] + indices[nivel];
            } else {
                retorno = indices[nivel];
            }
            return retorno;
        }

        private int getIndice(int[] indices, int[] dim)
        {
            int sumatoria = indices[0];
            for (int x = 1; x < indices.Length; x++)
                sumatoria = (sumatoria * dim[x]) + indices[x];
            return sumatoria;
        }

        private void cmdGuardarPista_Click(object sender, EventArgs e)
        {
            guardarPista(txtNombrePista.Text);
        }

        private void cmdCargarPista_Click(object sender, EventArgs e)
        {
            cargarPista(txtNombrePista.Text);
        }

        private void guardarPista(string nombre) {
            try {
                FileStream fs = new FileStream(@"..\..\pistas_guardadas\" + nombre + ".dat", FileMode.Create);
                BinaryFormatter formateo = new BinaryFormatter();
                formateo.Serialize(fs, txtEntrada.Text);
                fs.Close();
                MessageBox.Show("La pista "+nombre.ToString()+" ha sido guardada...!");
            }catch(Exception e){
                MessageBox.Show(e.ToString());
            }
        }

        private void guardarLista(string nombre)
        {
            try
            {
                FileStream fs = new FileStream(@"..\..\listas_guardadas\" + nombre + ".dat", FileMode.Create);
                BinaryFormatter formateo = new BinaryFormatter();
                formateo.Serialize(fs, txtEntradaListas.Text);
                fs.Close();
                MessageBox.Show("La lista " + nombre.ToString() + " ha sido guardada...!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void cargarPista(string nombre) {
            try {
                FileStream fs = new FileStream(@"..\..\pistas_guardadas\" + nombre + ".dat", FileMode.Open);
                BinaryFormatter formateo = new BinaryFormatter();
                String cad = formateo.Deserialize(fs) as string;
                fs.Close();
                txtEntrada.Text = cad;
            }catch(Exception e){
                MessageBox.Show(e.ToString());
            }    
        }

        private void cargarLista(string nombre) {
            try{
                FileStream fs = new FileStream(@"..\..\listas_guardadas\" + nombre + ".dat", FileMode.Open);
                BinaryFormatter formateo = new BinaryFormatter();
                String cad = formateo.Deserialize(fs) as string;
                fs.Close();
                txtEntradaListas.Text = cad;
            }catch (Exception e){
                MessageBox.Show(e.ToString());
            }  
        }

        private void cmdAnalizarLista_Click(object sender, EventArgs e)
        {
            if (txtEntradaListas.Text != null)
            {
                AnalizadorListas a = new AnalizadorListas();
                a.inciar_mensaje(txtMensaje);
                bool str = a.parse(txtEntradaListas.Text);
                if (str)
                {
                    MessageBox.Show("Lista Analizada");
                }
                else
                {
                    MessageBox.Show("La cadena de lista contiene errores...");
                }
            }
            else {
                txtMensaje.Text="Cadena de entrada vacia...!!";
            }
        }

        private void cmdGuardarLista_Click(object sender, EventArgs e)
        {
            guardarLista(txtNombreLista.Text);
        }

        private void cmdCargarLista_Click(object sender, EventArgs e)
        {
            cargarLista(txtNombreLista.Text);
        }

        private void txtEntrada_Load(object sender, EventArgs e)
        {

        }

        private void txtEntradaListas_Load(object sender, EventArgs e)
        {

        }

    }
}


