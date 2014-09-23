using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toub.Sound.Midi;

namespace Proyecto1
{
    class Canales
    {
        List<Canal> lista_canales;
        public Canales(){
            lista_canales = new List<Canal>();
        }

        public void agregarNotas(int ncanal,NoteOn nnota) { 
            Canal canal_temp = null;
            for (int i = 0; i < lista_canales.Count; i++) {
                if (lista_canales[i].canal == ncanal) {
                    canal_temp = lista_canales[i];
                }
            }
            if (canal_temp == null) {
                canal_temp = new Canal();
                canal_temp.canal = ncanal;
            }
            canal_temp.agregar_nota(nnota);
            lista_canales.Add(canal_temp);
        }
    }
}
