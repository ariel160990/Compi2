using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Irony.Ast;
using Irony.Parsing;

namespace Proyecto1
{
    class Sintactic: Grammar
    {
        public Sintactic(){
            var number = TerminalFactory.CreateCSharpNumber("number");

            CommentTerminal comm = new CommentTerminal("comm", "\n", "\r");
            CommentTerminal comline = new CommentTerminal("comline",">>","\n");
            CommentTerminal comblock = new CommentTerminal("comblock","<-","->");

            RegexBasedTerminal entero = new RegexBasedTerminal("entero","[0-9]+");

            base.NonGrammarTerminals.Add(comm);
            base.NonGrammarTerminals.Add(comline);
            base.NonGrammarTerminals.Add(comblock);

            IdentifierTerminal id = new IdentifierTerminal("id");

            //no terminales

            NonTerminal s0 = new NonTerminal("s0"),
                        def_pista = new NonTerminal("def_pista"),
                        cuerpo_pista = new NonTerminal("cuerpo_pista"),
                        extiende = new NonTerminal("extiende"),
                        lista_extender = new NonTerminal("lista_extender"),
                        exp = new NonTerminal("exp"),
                        dec_var = new NonTerminal("dec_var"),
                        tipo_var = new NonTerminal("tipo_var"),
                        lista_ids = new NonTerminal("lista_ids"),
                        asig_var = new NonTerminal("asig_var");
                        

            s0.Rule = def_pista
                    | exp + Eos
                    | dec_var
                    | s0 + asig_var
                    | asig_var;

            def_pista.Rule = ToTerm("pista")+id+ extiende + Eos +Indent+ cuerpo_pista + Dedent
                    | ToTerm("pista") + id + Eos + Indent + cuerpo_pista + Dedent;

            extiende.Rule = "extiende" + lista_extender;

            lista_extender.Rule = lista_extender + "," + id
                    | id ;

            cuerpo_pista.Rule = cuerpo_pista+"instrucciones" + Eos
                   | cuerpo_pista + "inicia_ciclo" + Eos + Indent + cuerpo_pista + Dedent
                   | ToTerm("inicia_ciclo") + Eos + Indent + cuerpo_pista + Dedent
                   | ToTerm("instrucciones")+Eos;
            

            exp.Rule = exp + "+"+ exp
                    | exp +"-"+ exp
                    | exp+ "*"+ exp
                    | exp +"/"+ exp
                    | exp + "%" + exp
                    | exp + "^" + exp
                    | exp + "==" + exp
                    | exp + "!" + exp
                    | exp + ">" + exp
                    | exp + "<" + exp
                    | exp + ">=" + exp
                    | exp + "<=" + exp
                    | exp + "!¡" + exp
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
                    | id
                    | entero
                    | "verdadero"
                    | "falso";

            dec_var.Rule = ToTerm("keep") + "var" + tipo_var + lista_ids + Eos
                    | ToTerm("var") + tipo_var + lista_ids+ Eos;

            

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




            this.Root = s0;

            RegisterOperators(1, "||", "!||", "&|");
            RegisterOperators(2, "&&", "!&&");
            //RegisterOperators(3, "!");

            RegisterOperators(4, "==","!",">","<",">=","<=");
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
