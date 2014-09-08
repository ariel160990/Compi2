using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Irony.Ast;
//using Irony.Parsing;

namespace Proyecto1
{
    class Sintactic: Grammar
    {
        public Sintactic(){
            var numero = TerminalFactory.CreateCSharpNumber("numero");

            CommentTerminal comm = new CommentTerminal("comm", "\n", "\r");
            CommentTerminal comline = new CommentTerminal("comline",">>","\n");
            CommentTerminal comblock = new CommentTerminal("comblock","<-","->");

            //RegexBasedTerminal entero = new RegexBasedTerminal("entero","[0-9]+");
            //RegexBasedTerminal doble = new RegexBasedTerminal("doble", "([0-9]+.[0-9]+)|([0-9]+.)|(.[0-9]+)");
            

            //RegexBasedTerminal id = new RegexBasedTerminal("id","([A-Z]|[a-z])([A-Z]|[a-z]|[0-9]|_)*");
            //RegexBasedTerminal caracter = new RegexBasedTerminal("caracter", "\'([^\r\n\t\f\b\\\'\"])|(#t)|(#n)|(#r)\'");

            //var chr = new RegexBasedTerminal("char", "'([^#']|#'|#n|#t|##)'");
            RegexBasedTerminal caracter = new RegexBasedTerminal("caracter", "'([^#']|#'|#n|#t|##)'");
            //var str = new RegexBasedTerminal("str", "\"([^#\"]|#\"|#n|#t|##)*\"");
            //RegexBasedTerminal cadena = new RegexBasedTerminal("cadena", "\"[^\r\n\"]+\"");
            RegexBasedTerminal cadena = new RegexBasedTerminal("cadena", "\"([^#\"]|#\"|#n|#t|##)*\"");

            RegexBasedTerminal valor_bool = new RegexBasedTerminal("valor_bool","verdadero|falso");

            /*
             * \n -----> Nueva Linea.
\t -----> Tabulador.
\r -----> Retroceso de Carro.
\f -----> Comienzo de Pagina.
\b -----> Borrado a la Izquierda.
\\ -----> El carácter barra inversa ( \ ).
\' -----> El carácter prima simple ( ' ).
\" -----> El carácter prima doble o bi-prima ( " ).
             */

            base.NonGrammarTerminals.Add(comm);
            base.NonGrammarTerminals.Add(comline);
            base.NonGrammarTerminals.Add(comblock);

            IdentifierTerminal id = new IdentifierTerminal("id");

            //no terminales
            NonTerminal s0 = new NonTerminal("s0"),
                        def_pista = new NonTerminal("def_pista"),
                        extiende = new NonTerminal("extiende"),
                        lista_extender = new NonTerminal("lista_extender"),
                        exp = new NonTerminal("exp"),
                        dec_var = new NonTerminal("dec_var"),
                        tipo_var = new NonTerminal("tipo_var"),
                        lista_ids = new NonTerminal("lista_ids"),
                        asig_var = new NonTerminal("asig_var"),
                        sen_si = new NonTerminal("sen_si"),
                        sen_switch = new NonTerminal("sen_switch"),
                        sen_caso = new NonTerminal("sen_caso"),
                        lista_caso = new NonTerminal("lista_caso"),
                        sen_para = new NonTerminal("sen_para"),
                        asig_para = new NonTerminal("asig_para"),
                        dec_para = new NonTerminal("dec_para"),
                        sen_mientras = new NonTerminal("sen_mientras"),
                        sen_hacer = new NonTerminal("sen_hacer"),
                        sen_fun_proc = new NonTerminal("sen_fun_proc"),
                        lista_dec_param = new NonTerminal("lista_dec_param"),
                        sen_fun_reproducir = new NonTerminal("sen_fun_reproducir"),
                        nota = new NonTerminal("nota"),
                        sen_fun_espera = new NonTerminal("sen_fun_espera"),
                        sen_fun_principal = new NonTerminal("sen_fun_principal"),
                        sen_fun_mensaje = new NonTerminal("sen_fun_mensaje"),
                        llamada_a_funcion = new NonTerminal("llamada_a_funcion"),
                        lista_param = new NonTerminal("lista_param"),
                        arreglo = new NonTerminal("arreglo"),
                        instrucciones = new NonTerminal("instrucciones");
            s0.Rule = def_pista;
            def_pista.Rule = ToTerm("pista")+id+ extiende + Eos +Indent + instrucciones + Dedent
                    | ToTerm("pista") + id + Eos + Indent + instrucciones + Dedent;
            extiende.Rule = ToTerm("extiende") + lista_extender;
            lista_extender.Rule = lista_extender + "," + id
                    | id ;
            exp.Rule = exp + "+" + exp
                    | exp + "-" + exp
                    | exp + "*" + exp
                    | exp + "/" + exp
                    | exp + "%" + exp
                    | exp + "^" + exp
                //operadores relacionales
                    | exp + "==" + exp
                    | exp + "!=" + exp
                    | exp + ">" + exp
                    | exp + "<" + exp
                    | exp + ">=" + exp
                    | exp + "<=" + exp
                    | "!¡" + id
                //operadores logicos
                    | exp + "&&" + exp
                    | exp + "!&&" + exp
                    | exp + "||" + exp
                    | exp + "!||" + exp
                    | exp + "&|" + exp
                    | ToTerm("!") + exp
                    | ToTerm("(") + exp + ")"
                    | ToTerm("++") + id
                    | ToTerm("--") + id
                    | id + "++"
                    | id + "--"
                    | cadena
                    | id
                    | numero
                //| doble
                    | llamada_a_funcion
                    | caracter
                //| ToTerm("verdadero")
                //| ToTerm("falso");
                    | valor_bool;
            arreglo.Rule = arreglo + "," + "{" + exp + "}"
                    | ToTerm("{") + exp + "}";
            dec_var.Rule = ToTerm("keep") + "var" + tipo_var + lista_ids + Eos
                    | ToTerm("keep") + "var" + tipo_var + "arreglo" + lista_ids + Eos
                    | ToTerm("var") + tipo_var + lista_ids+ Eos
                    |  ToTerm("var") + tipo_var + "arreglo" + lista_ids + Eos;
            tipo_var.Rule = ToTerm("entero")
                    | ToTerm("doble")
                    | ToTerm("boolean")
                    | ToTerm("caracter")
                    | ToTerm("cadena");
            lista_ids.Rule = lista_ids + "," + id
                    | lista_ids + "," + id + "=" + exp
                    | id + "=" + exp
                    | id;
            asig_var.Rule = id + "=" + exp + Eos
                    | id + "+=" + exp + Eos
                    | id + "++" + Eos
                    | id + "--" + Eos;
            //sentencia si
            sen_si.Rule = ToTerm("si") + "(" + exp + ")" + Eos + Indent + instrucciones + Dedent
                    | ToTerm("si") + "(" + exp + ")" + Eos + Indent + instrucciones + Dedent + "sino" + Eos + Indent + instrucciones + Dedent;
            //sentencia switch
            sen_switch.Rule = ToTerm("switch") + "(" + exp + ")" + Eos + Indent + lista_caso + Dedent;
            lista_caso.Rule = lista_caso + sen_caso
                    | sen_caso;
            sen_caso.Rule = ToTerm("caso") + exp + Eos + Indent + instrucciones + Dedent
                    | ToTerm("default") + exp + Eos + Indent + instrucciones + Dedent;
            //sentencia para
            sen_para.Rule = ToTerm("para") + "(" + dec_para + ";" + exp + ";" + asig_para + ")" + Eos + Indent + instrucciones + Dedent;
            dec_para.Rule = ToTerm("var") + tipo_var + id + "=" + exp
                    | asig_para;
            asig_para.Rule = id + "=" + exp
                    | id + "+=" + exp
                    | id + "++"
                    | id + "--";
            //sentencia mientras
            sen_mientras.Rule = ToTerm("mientras") + "(" + exp + ")" + Eos + Indent + instrucciones + Dedent;
            //sentencia hacer
            sen_hacer.Rule = ToTerm("hacer") + Eos + instrucciones + "mientras" + "(" + exp + ")" + Eos;
            //funciones y procedimientos
            sen_fun_proc.Rule = ToTerm("keep") + tipo_var + id + "(" + lista_dec_param + ")" + Eos + Indent + instrucciones + Dedent
                    | ToTerm("keep") + tipo_var + id + "(" + ")" + Eos + Indent + instrucciones + Dedent
                    | ToTerm("keep") + id + "(" + lista_dec_param + ")" + Eos + Indent + instrucciones + Dedent
                    | ToTerm("keep") + id + "(" + ")" + Eos + Indent + instrucciones + Dedent
                    | tipo_var + id + "(" + lista_dec_param + ")" + Eos + Indent + instrucciones + Dedent
                    | tipo_var + id + "(" + ")" + Eos + Indent + instrucciones + Dedent
                    | id + "(" + lista_dec_param + ")" + Eos + Indent + instrucciones + Dedent
                    | id + "(" + ")" + Eos + Indent + instrucciones + Dedent
                    | id + "(" + ")" + Eos
                    | id + "(" + lista_param + ")" + Eos;
            lista_dec_param.Rule = lista_dec_param + "," +tipo_var + id
                    | tipo_var + id;
            //llamada a funcion
            llamada_a_funcion.Rule = id + "(" + ")"
                    | id + "(" + lista_param +")";
            lista_param.Rule = lista_param + "," + exp
                    | exp;
            /*
            //funcion reproducir
            sen_fun_reproducir.Rule = ToTerm("reproducir") + "(" + nota + "," + exp + "," + exp + "," + exp + ")";
            nota.Rule = ToTerm("do") | ToTerm("re") | ToTerm("mi") | ToTerm("fa") | ToTerm("sol") | ToTerm("la") | ToTerm("si");
            // funcion espera
            sen_fun_espera.Rule = ToTerm("esperar") + "(" + exp + "," + exp + ")" + Eos;   
            // funcion principal
            sen_fun_principal.Rule = ToTerm("principal") + "(" + ")" + Eos + Indent + instrucciones + Dedent;
            // funcion mensaje
            sen_fun_mensaje.Rule = ToTerm("mensaje") + "(" + exp + ")";
            */
            //{ { { 1, 2, 3 }, { 4, 5, 6 } },{ { 7, 8, 9 }, { 10, 11, 12 } } }
            instrucciones.Rule = instrucciones + asig_var
                    | instrucciones + dec_var
                    | instrucciones + sen_si
                    | instrucciones + sen_switch
                    | instrucciones + sen_para
                    | instrucciones + sen_mientras
                    | instrucciones + sen_hacer
                    //| instrucciones + sen_fun_reproducir + Eos
                    //| instrucciones + sen_fun_espera
                    //| instrucciones + sen_fun_principal
                    | instrucciones + sen_fun_proc
                    //| instrucciones + llamada_a_funcion + Eos
                    | instrucciones + "retorna" + exp + Eos
                    | instrucciones + "salir" + Eos
                    | instrucciones + "continuar" + Eos
                    | asig_var
                    | dec_var
                    | sen_si
                    | sen_switch
                    | sen_para
                    | sen_mientras
                    | sen_hacer
                    //| sen_fun_reproducir + Eos
                    //| sen_fun_espera
                    //| sen_fun_principal
                    | sen_fun_proc
                    //| llamada_a_funcion + Eos
                    | "retorna" + exp + Eos
                    | "salir" + Eos
                    | "continuar" + Eos;


            this.Root = s0;

            RegisterOperators(1, "||", "!||", "&|");
            RegisterOperators(2, "&&", "!&&");
            RegisterOperators(3, "!");

            RegisterOperators(4, "==","!=",">","<",">=","<=");
            RegisterOperators(5, "!¡");

            RegisterOperators(6, "+", "-");
            RegisterOperators(7, "*", "/","%");
            RegisterOperators(8,"^");

            //MarkPunctuation("(", ")");    

        }

        public override void CreateTokenFilters(LanguageData language, TokenFilterList filters)
        {
            var outlineFilter = new CodeOutlineFilter(language.GrammarData,
              OutlineOptions.ProduceIndents | OutlineOptions.CheckBraces, ToTerm(@"\")); // "\" is continuation symbol
            filters.Add(outlineFilter);
        }

    }
}