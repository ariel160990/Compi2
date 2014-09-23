using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Toub.Sound.Midi;

namespace Proyecto1
{
    class Canal
    {
        List<NoteOn> listaNotas;
        public int canal;
        public Canal() {
            MidiPlayer.OpenMidi();
            listaNotas = new List<NoteOn>();
            Console.WriteLine("Main thread: Start a second thread.");
            //Thread t = new Thread(new ThreadStart(ThreadProc()));
            Thread t = new Thread(new ThreadStart(procedimiento));

            t.Start();
            //Thread.Sleep(0);
            /*for (int i = 0; i < 4; i++){
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }
            t.Join();
            */

        }


        void procedimiento() {
            int pos_nota = 0;
            while (true) {
                if (listaNotas.Count > pos_nota) {
                    MidiPlayer.Play(listaNotas[pos_nota]);
                    Thread.Sleep((int)listaNotas[pos_nota].DeltaTime);
                    pos_nota++;
                }
            }
        }

        public void agregar_nota(NoteOn nota_nueva) {
            listaNotas.Add(nota_nueva);
        }

        public static void ThreadProc()
        {
            int i = 0;
            while (true) {
                Console.WriteLine("ThreadProc: {0}", i);
                Thread.Sleep(0);
                i++;
                
            }
        }    


    }
}
