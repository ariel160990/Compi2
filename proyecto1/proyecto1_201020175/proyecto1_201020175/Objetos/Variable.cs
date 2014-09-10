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
        public Variable() {
            id = "";
            tipo = "";
            valor = null;
            keep = false;
        }
    }
}
