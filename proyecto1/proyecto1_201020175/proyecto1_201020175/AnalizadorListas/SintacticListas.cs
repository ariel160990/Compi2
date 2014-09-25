using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Irony.Ast;

namespace Proyecto1
{
    class SintacticListas:Grammar
    {
        public SintacticListas() {
            IdentifierTerminal id = new IdentifierTerminal("id");

            NonTerminal s0 = new NonTerminal("s0"),
                        nombre_lista = new NonTerminal("nombre_lista"),
                        random_lista = new NonTerminal("random_lista"),
                        circular_lista = new NonTerminal("circular_lista"),
                        pistas_lista = new NonTerminal("pistas_lista"),
                        valor_bool = new NonTerminal("valor_bool"),
                        lista_pistas = new NonTerminal("lista_pistas"),
                        lista = new NonTerminal("lista"),
                        atributos_lista = new NonTerminal("atributos_lista"),
                        grupo_listas = new NonTerminal("grupo_listas"),
                        lista_de_listas = new NonTerminal("lista_de_listas");
                        ;

            s0.Rule = lista
                    |grupo_listas;

            grupo_listas.Rule=ToTerm("{")+"grupo_listas"+":"+"["+ lista_de_listas +"]"+"}";

            lista_de_listas.Rule=MakePlusRule(lista_de_listas,ToTerm(","),lista);

            lista.Rule = ToTerm("{") +"lista"+":"+"{"+atributos_lista+"}"+ "}";

            atributos_lista.Rule = nombre_lista + "," + random_lista + "," + circular_lista + "," + pistas_lista
                    | nombre_lista +"," +pistas_lista
                    | nombre_lista +","+random_lista+","+pistas_lista
                    | nombre_lista +","+circular_lista+","+pistas_lista;

            nombre_lista.Rule = ToTerm("nombre") + ":"+"\"" + id + "\"";

            random_lista.Rule = ToTerm("random") +":"+ valor_bool;

            circular_lista.Rule = ToTerm("circular") + ":" + valor_bool;

            pistas_lista.Rule = ToTerm("pistas") + ":" + "[" + lista_pistas + "]";

            valor_bool.Rule = ToTerm("true") | ToTerm("false");

            lista_pistas.Rule = MakePlusRule(lista_pistas,ToTerm(","),id);

            this.Root = s0;

        }
    }
}
