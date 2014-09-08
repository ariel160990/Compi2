using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    class Variables
    {
        public List<Variable> lista;
        public Variables() { 
            lista = new List<Variable>();
        }

        public void agregar(Variable n){
            lista.Add(n);
        }

        public Variable buscar(string id) { 
            for(int i=0; i<lista.Count(); i++){
                if (lista[i].id.Equals(id)) { 
                    return lista[i];
                }
            }
            return null;
        }
    }
}
