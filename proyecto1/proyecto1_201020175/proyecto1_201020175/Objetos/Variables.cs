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

        public bool agregarArreglos(string id, string tipo, object valor, bool keepp, int nivel,string dimensiones)
        {
            bool exito = true;
            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista[i].id.Equals(id))
                {
                    if (lista[i].nivel <= nivel)
                    {
                        if (keepp == false)
                        {
                            exito = false;
                            Console.WriteLine("Error semantico: No se puede declarar la funcion por que ya existe un id en la lista de variables y la acutal no ha sido declarada con keep.");
                        }
                    }
                }
            }
            if (exito)
            {
                Variable nueva = new Variable();
                nueva.id = id;
                nueva.tipo = tipo;
                nueva.valor = valor;
                nueva.keep = keepp;
                nueva.nivel = nivel;
                nueva.vof = 1;
                nueva.arreglo = true;
                nueva.parametros = null;
                nueva.dimensiones = dimensiones;
                lista.Add(nueva);
            }
            return exito;
        }

        public bool agregarVariable(string id, string tipo, object valor, bool keepp, int nivel) {
            bool exito = true;
            for (int i = 0; i < lista.Count(); i++) {
                if (lista[i].id.Equals(id)) {
                    if (lista[i].nivel <= nivel) {
                        if (keepp == false) {
                            exito = false;
                            Console.WriteLine("Error semantico: No se puede declarar la funcion por que ya existe un id en la lista de variables y la acutal no ha sido declarada con keep.");
                        }
                    }
                }
            }
            if (exito) {
                Variable nueva = new Variable();
                nueva.id = id;
                nueva.tipo = tipo;
                nueva.valor = valor;
                nueva.keep = keepp;
                nueva.nivel = nivel;
                nueva.vof = 1;
                nueva.arreglo = false;
                nueva.parametros = null;
                lista.Add(nueva);
            }
            return exito;
        }

        public bool agregarFuncion(string id,string tipo, object valor, bool keepp, int nivel, List<Variable> parametros) { 
            bool exito=true;
            for (int i = 0; i < lista.Count(); i++) {
                if (lista[i].id.Equals(id)) {
                    exito=false;
                    Console.WriteLine("Error Semantico: No se puede declarar la funcion por que ya existe un id en la lista de variables.");
                    //if (lista[i].nivel == nivel) {
                    //    exito = false;
                    //}
                }
            }
            if (exito == true)
            {
                Variable nueva = new Variable();
                nueva.id = id;
                nueva.tipo = tipo;
                nueva.valor = valor;
                nueva.keep = keepp;
                nueva.nivel = nivel;
                nueva.vof = 2;
                nueva.arreglo = false;
                nueva.parametros = parametros;
                lista.Add(nueva);
            }
            return exito;
        }
    }
}
