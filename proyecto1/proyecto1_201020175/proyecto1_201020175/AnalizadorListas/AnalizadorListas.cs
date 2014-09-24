using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Windows.Forms;

namespace Proyecto1
{
    class AnalizadorListas
    {
        private LanguageData lenguaje;
        private AccionesListas action;
        private Parser p;
        RichTextBox txtMensaje;
        
        public AnalizadorListas (){
            lenguaje = new LanguageData(new SintacticListas());
            action = new AccionesListas();
            p = new Parser(lenguaje);
        }

        public bool parse(string str) {
            bool exito = true;
            ParseTree s_tree = p.Parse(str);
            if (s_tree.Root != null)
            {
                action.do_action(s_tree.Root);
                if (txtMensaje.Text.Equals(""))
                {

                }
                else {
                    exito = false;
                }
            }
            else {
                foreach (Irony.LogMessage a in s_tree.ParserMessages)
                {
                    set_mensaje("sintax error: " + a.Message + "   location: " + a.Location + "   state: " + a.ParserState);
                    //Console.WriteLine("sintax error: " + a.Message + "   location: " + a.Location + "   state: " + a.ParserState);
                }
                exito = false;
            }
            return exito;
        }

        public void inciar_mensaje(RichTextBox txt){
            txt.Text="";
            txtMensaje=txt;
        }

        public void set_mensaje(string str_mensaje){
            txtMensaje.AppendText("\n"+str_mensaje);
        }

    }
}
