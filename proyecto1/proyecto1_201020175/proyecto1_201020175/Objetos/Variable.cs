using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    class Variable
    {
        public string id;
        /*
         * entero
         * doble
         * cadena
         * boolean
         */
        public string tipo;
        public object valor;
        public bool keep;
        public int nivel;
        public int vof;//0 nada, 1 var, 2 func
        public Variable() {
            id = "";
            tipo = "";
            valor = null;
            keep = false;
            nivel = 0;
            vof = 0;
        }
    }
}
