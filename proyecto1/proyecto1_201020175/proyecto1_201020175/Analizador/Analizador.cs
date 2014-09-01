using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Proyecto1
{
    class Analizador
    {
        private LanguageData lenguaje;
        private Acciones action;
        private Parser p;

        public Analizador() {
            lenguaje = new LanguageData(new Sintactic());
            action = new Acciones();
            p = new Parser(lenguaje);
        }

        public Object parse(string str) {
            ParseTree s_tree = p.Parse(str);
            if (s_tree.Root != null) {
                return action.do_action(s_tree.Root);
            }
            return null;
        }

        public bool isValid(string codigo, Grammar grammar)
        {
            LanguageData lenguaje = new LanguageData(grammar);
            Parser p = new Parser(lenguaje);
            ParseTree arbol = p.Parse(codigo);

            if (arbol.Root == null) { 
                foreach(Irony.LogMessage a in arbol.ParserMessages){
                    Console.WriteLine("sintax error: " +a.Message + "   location: "+a.Location + "   state: "+a.ParserState);
                }
            }

            return arbol.Root != null;
        }

        public ParseTreeNode getRoot(string codigo, Grammar grammar)
        {
            LanguageData lenguaje = new LanguageData(grammar);
            Parser p = new Parser(lenguaje);
            ParseTree arbol = p.Parse(codigo);
            return arbol.Root;
        }

    }
}
