using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Proyecto1
{
    class Acciones
    {
        Variables lst_variables = new Variables();
        public int nivel = 0;
        public Object do_action(ParseTreeNode pt_node) {
            return action(pt_node);
        }

        public Object action(ParseTreeNode node)
        {
            Object result = null;
            switch (node.Term.Name.ToString())
            {
                case "s0":
                    {
                        if (node.ChildNodes.Count == 1)
                        {
                            result = action(node.ChildNodes[0]);
                        }
                        break;
                    }
                case "def_pista":
                    {
                        //node.ChildNodes[2]
                        if (node.ChildNodes.Count == 3) {
                            Object instrucciones = action(node.ChildNodes[2]);
                        }
                        else if (node.ChildNodes.Count == 4) {
                            object extender = action(node.ChildNodes[2]);
                            object instrucciones = action(node.ChildNodes[3]);
                        }
                        break;
                    }
                case "instrucciones":
                    {
                        if (node.ChildNodes.Count == 2)
                        {
                            object instrucciones1 = action(node.ChildNodes[0]);
                            object instrucciones = action(node.ChildNodes[1]);
                        }
                        if (node.ChildNodes.Count == 1) {
                            object instrucciones = action(node.ChildNodes[0]);
                        }
                        break;
                    }
                case "exp":
                    {
                        if (node.ChildNodes.Count == 1) {
                            result = action(node.ChildNodes[0]);
                        }
                        else if (node.ChildNodes.Count == 2) {
                            if (node.ChildNodes[0].Token.Value.ToString().Equals("!¡")) {// espresion is null
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                                if (var_temp != null)
                                {
                                    if (var_temp.valor == null)
                                    {
                                        result = true;
                                    }
                                    else {
                                        result = false;
                                    }
                                }
                                else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. " + node.ChildNodes[1].Token.Value.ToString());
                                }
                            }
                            else if (node.ChildNodes[0].Token.Value.ToString().Equals("!")) {//expresion not
                                object obj_temp = action(node.ChildNodes[1]);
                                try {
                                    bool temp_bool = (Boolean)obj_temp;
                                    if (temp_bool)
                                    {
                                        result = false;
                                    }
                                    else {
                                        result = true;
                                    }
                                }catch(InvalidCastException e){
                                    Console.WriteLine("Error semantico: la expresion no se puede castear a booleano. ref1");
                                }
                            }else if(node.ChildNodes[0].Token.Value.ToString().Equals("++")){//expresion asignacion ++id
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                                if (var_temp != null)
                                {
                                    if (var_temp.tipo.Equals("entero"))
                                    {
                                        if (var_temp.valor != null)
                                        {
                                            var_temp.valor = (int)var_temp.valor + 1;
                                        }
                                        else {
                                            var_temp.valor = 1;
                                        }
                                        result = (int)var_temp.valor;
                                    }
                                    else if (var_temp.tipo.Equals("doble"))
                                    {
                                        if (var_temp.valor != null)
                                        {
                                            var_temp.valor = (double)var_temp.valor + 1;
                                        }
                                        else {
                                            var_temp.valor = 1;
                                        }
                                        result = (double)var_temp.valor;
                                    }
                                    else {
                                        Console.WriteLine("Error semantico: no se puede incrementar la variable por que no es numerico. ref3");
                                    }
                                }
                                else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. ref2");
                                }
                            }
                            else if (node.ChildNodes[0].Token.Value.ToString().Equals("--")) { //expresion asigacion --id
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                                if (var_temp != null)
                                {
                                    if (var_temp.tipo.Equals("entero")) {
                                        if (var_temp.valor != null)
                                        {
                                            var_temp.valor = (int)var_temp.valor - 1;
                                        }
                                        else {
                                            var_temp.valor = -1;
                                        }
                                        result = (int)var_temp.valor;
                                    }
                                    else if (var_temp.tipo.Equals("doble"))
                                    {
                                        if (var_temp.valor != null)
                                        {
                                            var_temp.valor = (double)var_temp.valor - 1;
                                        }
                                        else {
                                            var_temp.valor = -1;
                                        }
                                        result = (double)var_temp.valor;
                                    }
                                    else {
                                        Console.WriteLine("Error semantico: no se puede decrementar la variable por que no es numerico. ref5");
                                    }
                                }
                                else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. ref4");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("++")){//expresion asignacion id++
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                if (var_temp != null) {
                                    if (var_temp.tipo.Equals("entero")) {
                                        if (var_temp.valor != null)
                                        {
                                            result = (int)var_temp.valor;
                                            var_temp.valor = (int)var_temp.valor + 1;
                                        }
                                        else {
                                            result = 0;
                                            var_temp.valor = 1;
                                        }
                                    }
                                    else if (var_temp.tipo.Equals("doble"))
                                    {
                                        if (var_temp.valor != null)
                                        {
                                            result = (double)var_temp.valor;
                                            var_temp.valor = (double)var_temp.valor + 1;
                                        }
                                        else {
                                            result = 0;
                                            var_temp.valor = 1;
                                        }
                                    }
                                    else {
                                        Console.WriteLine("Error semantico: no se puede incrementar la variable por que no es numerico. ref7");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. ref6");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("--")) { //epresion asignacion id--
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                if (var_temp != null)
                                {
                                    if(var_temp.tipo.Equals("entero")){
                                        if (var_temp.valor != null) {
                                            result = (int)var_temp.valor;
                                            var_temp.valor = (int)var_temp.valor - 1;
                                        } else {
                                            result = 0;
                                            var_temp.valor = -1;
                                        }
                                    }else if(var_temp.tipo.Equals("doble")){
                                        if (var_temp.valor != null) {
                                            result = (double)var_temp.valor;
                                            var_temp.valor = (double)var_temp.valor - 1;
                                        } else {
                                            result = 0;
                                            var_temp.valor = -1;
                                        }
                                    }else{
                                        Console.WriteLine("Error semantico: no se puede decrementar la variable por que no es numerico. ref9");
                                    }
                                }
                                else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. ref8");
                                }
                            }
                        }
                        else if (node.ChildNodes.Count == 3) {
                            if (node.ChildNodes[1].Token.Value.ToString().Equals("+")) { //expresion suma: +
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is string) {
                                    if (der is int || der is double) {
                                        result = (string)izq + Convert.ToString(der);
                                    }
                                    else if (der is string)
                                    {
                                        result = (string)izq + (string)der;
                                    }
                                    else {
                                        Console.WriteLine("Error semantico: no se puede sumar valores de este tipo. ref10");
                                    }
                                }else if(izq is int){
                                    if (der is int) {
                                        result = (int)izq + (int)der;
                                    }else if(der is double){
                                        result = (int)izq + (double)der;
                                    } else if (der is string) {
                                        result = Convert.ToString(izq) + (string)der;
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede sumar los valores de este tipo. ref11");
                                    }
                                }else if(izq is double){
                                    if (der is int){
                                        result = (double)izq + (int)der;
                                    }else if (der is double){
                                        result = (double)izq + (double)der;
                                    }else if (der is string){
                                        result = Convert.ToString(izq) + (string)der;
                                    }else{
                                        Console.WriteLine("Error semantico: no se puede sumar los valores de este tipo. ref11");
                                    }
                                }else{
                                    Console.WriteLine("Error semantico: no se puede sumar valores de este tipo. ref10");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("-")){//expresion resta: -
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int) {
                                    if (der is int) {
                                        result = (int)izq - (int)der;
                                    } else if (der is double) {
                                        result = (int)izq - (double)der;
                                    } else {
                                        Console.WriteLine("Error semantico: no se pueden restar los valores. ambos deben ser numericos. ref16");
                                    }
                                }else if(izq is double){
                                    if (der is int) {
                                        result = (double)izq - (int)der;
                                    } else if (der is double) {
                                        result = (double)izq - (double)der;
                                    } else {
                                        Console.WriteLine("Error semantico: no se pueden restar los valores. ambos deben ser numericos. ref 17");
                                    }
                                }else{
                                    Console.WriteLine("Error semantico: no se pueden restar los valores. ambos deben ser numericos. ref15");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("*")){//expresion multiplicacion: *
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int)
                                {
                                    if (der is int)
                                    {
                                        result = (int)izq * (int)der;
                                    }
                                    else if (der is double)
                                    {
                                        result = (int)izq * (double)der;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se pueden multiplicar los valores. ambos deben ser numericos. ref16");
                                    }
                                }
                                else if (izq is double)
                                {
                                    if (der is int)
                                    {
                                        result = (double)izq * (int)der;
                                    }
                                    else if (der is double)
                                    {
                                        result = (double)izq * (double)der;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se pueden multiplicar los valores. ambos deben ser numericos. ref 17");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: no se pueden multiplicar los valores. ambos deben ser numericos. ref15");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("/")) { //expresion division: /
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int)
                                {
                                    if (der is int)
                                    {
                                        if ((int)der != 0)
                                        {
                                            result = (int)izq / (int)der;
                                        }
                                        else {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                    }
                                    else if (der is double)
                                    {
                                        if ((double)der != 0)
                                        {
                                            result = (int)izq / (double)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se pueden dividir los valores. ambos deben ser numericos. ref16");
                                    }
                                }
                                else if (izq is double)
                                {
                                    if (der is int)
                                    {
                                        if ((int)der != 0)
                                        {
                                            result = (double)izq / (int)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                    }
                                    else if (der is double)
                                    {
                                        if ((double)der != 0)
                                        {
                                            result = (double)izq / (double)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se pueden dividir los valores. ambos deben ser numericos. ref 17");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: no se pueden dividir los valores. ambos deben ser numericos. ref15");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("%")) { //expresion modulo: %
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int)
                                {
                                    if (der is int)
                                    {
                                        if ((int)der != 0)
                                        {
                                            result = (int)izq % (int)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                    }
                                    else if (der is double)
                                    {
                                        if ((double)der != 0)
                                        {
                                            result = (int)izq % (double)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se pueden dividir los valores. ambos deben ser numericos. ref16");
                                    }
                                }
                                else if (izq is double)
                                {
                                    if (der is int)
                                    {
                                        if ((int)der != 0)
                                        {
                                            result = (double)izq % (int)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                    }
                                    else if (der is double)
                                    {
                                        if ((double)der != 0)
                                        {
                                            result = (double)izq % (double)der;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el denominador no puede cer igual a cero. ref18");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se pueden dividir los valores. ambos deben ser numericos. ref 17");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: no se pueden dividir los valores. ambos deben ser numericos. ref15");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("^")) { //expresion exponente: ^
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int)
                                {
                                    if (der is int)
                                    {
                                        result = Math.Pow((int)izq, (int)der);
                                    }
                                    else if (der is double)
                                    {
                                        result = Math.Pow((int)izq,(double)der);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se puede usar la funcion exponente. ambos deben ser numericos. ref16");
                                    }   
                                }
                                else if (izq is double)
                                {
                                    if (der is int)
                                    {
                                        result = Math.Pow((double)izq,(int)der);
                                    }
                                    else if (der is double)
                                    {
                                        result = Math.Pow((double)izq,(double)der);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se puede usar la funcion exponente. ambos deben ser numericos. ref 17");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: no se pueden aplicar exponencia. ambos deben ser numericos. ref15");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("==")){ //expresion de comparacion igual: ==
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int) {
                                    if (der is int) {
                                        result = (int)izq == (int)der;
                                    } else if (der is double) {
                                        result = (int)izq == (double)der;
                                    } else if (der is string) {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref22");
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref21");
                                    }
                                } else if (izq is double) {
                                    if (der is int) {
                                        result = (double)izq == (int)der;
                                    } else if (der is double) {
                                        result = (double)izq == (double)der;
                                    } else if (der is string) {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref24");
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferentes tipo. ref23");
                                    }
                                } else if (izq is string) {
                                    if (der is int) {
                                        Console.WriteLine("Error semantico: no se pued comparar datos de diferente tipo. ref26");
                                    } else if (der is double) {
                                        Console.WriteLine("Error semantico: no se pued comparar datos de diferente tipo. ref25");
                                    } else if (der is string) {
                                        string vizq = (string)izq;
                                        string vder = (string)der;
                                        result = vizq.Equals(vder);
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref25");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref20");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("!=")){//expresion de comparacion no igual: !=
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int) {
                                    if (der is int) {
                                        result = ((int)izq != (int)der);
                                    } else if (der is double) {
                                        result = ((int)izq != (double)der);
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref29");
                                    }
                                } else if (izq is double) {
                                    if (der is int) {
                                        result = ((int)izq != (double)der);
                                    } else if (der is double) {
                                        result = ((double)izq != (double)der);
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref28");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref27");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals(">")){//expresion de comparacion mayor que: >
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int) {
                                    if (der is int) {
                                        result = (int)izq > (int)der;
                                    } else if (der is double) {
                                        result = (int)izq > (double)der;
                                    } else {
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en >. ref32");
                                    }
                                } else if (izq is double) {
                                    if (der is int) {
                                        result = (double)izq > (int)der;
                                    } else if (der is double) {
                                        result = (double)izq > (double)der;
                                    } else {
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en >. ref31");
                                    }
                                } else {
                                    Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en >. ref30");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("<")) { //expresion de comparacion menor que: <
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int) {
                                    if (der is int) {
                                        result = (int)izq < (int)der;
                                    } else if (der is double) {
                                        result = (int)izq < (double)der;
                                    } else {
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en <. ref35");
                                    }
                                } else if (izq is double) {
                                    if (der is int) {
                                        result = (double)izq < (int)der;
                                    } else if (der is double) {
                                        result = (double)izq < (double)der;
                                    } else {
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en <. ref34");
                                    }
                                } else {
                                    Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en <. ref33");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals(">=")){ //expresion de comparacion menor que: >=
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int){
                                    if (der is int){
                                        result = (int)izq >= (int)der;
                                    }else if (der is double){
                                        result = (int)izq >= (double)der;
                                    }else{
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en >=. ref36");
                                    }   
                                }else if (izq is double){
                                    if (der is int){
                                        result = (double)izq >= (int)der;
                                    }else if (der is double){
                                        result = (double)izq >= (double)der;
                                    }else{
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en >=. ref37");
                                    }
                                }else{
                                    Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en >=. ref38");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("<=")){ //expresion de comparacion menor que: <=
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is int){
                                    if (der is int){
                                        result = (int)izq <= (int)der;
                                    }else if (der is double){
                                        result = (int)izq <= (double)der;
                                    }else{
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en <=. ref39");
                                    }
                                }else if (izq is double){
                                    if (der is int){
                                        result = (double)izq <= (int)der;
                                    }else if (der is double){
                                        result = (double)izq <= (double)der;
                                    }else{
                                        Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en <=. ref40");
                                    }
                                }else{
                                    Console.WriteLine("Error se mantico: no se puede comparar valores que no sean numericos en <=. ref41");
                                }
                            }
                            //else if (node.ChildNodes[0].Token.Value.ToString().Equals("!¡")) { //expresion de is null: !¡
                            //    Variable temp_var = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                            //    if(temp_var!=null){
                            //        if(temp_var.valor==null){
                            //            return true;
                            //        }else{
                            //            return false;
                            //        }
                            //    }else{
                            //        Console.WriteLine("Error semantico: la variable aun no ha sido declarada. ref45");
                            //    }
                            //}
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("&&")) {// expresion and: &&
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is bool) {
                                    if (der is bool) {
                                        return (bool)izq && (bool)der;
                                    } else {
                                        Console.WriteLine("Error semantico: solo se pueden usar valores booleanos. ref47");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: solo se pueden usar valores booleanos. ref46");
                                }
                            }
                            else if (node.ChildNodes[1].Token.Value.ToString().Equals("!&&")) { // expresion nand: !&&
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is bool){
                                    if (der is bool){
                                        return !((bool)izq && (bool)der);
                                    }else{
                                        Console.WriteLine("Error semantico: solo se pueden usar valores booleanos. ref49");
                                    }
                                }else{
                                    Console.WriteLine("Error semantico: solo se pueden usar valores booleanos. ref48");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("||")){// expresion or: ||
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is bool)
                                {
                                    if (der is bool)
                                    {
                                        return (bool)izq || (bool)der;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: solo se pueden usar valores booleanos. ref49");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: solo se pueden usar valores booleanos. ref48");
                                }
                            }else if(node.ChildNodes[1].Token.Value.ToString().Equals("!||")){//expresion nor: !||
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is bool) {
                                    if (der is bool){
                                        result = !((bool)izq || (bool)der);
                                    }else {
                                        Console.WriteLine("Error semantico: solo se puede usar valores booleanos. ref51");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: solo se puede  usar valores booleanos. ref50");
                                }
                            }else if (node.ChildNodes[1].Token.Value.ToString().Equals("&|")) {//expresion xor: &|
                                object izq = action(node.ChildNodes[0]);
                                object der = action(node.ChildNodes[2]);
                                if (izq is bool) {
                                    if (der is bool) {
                                        result = (bool)izq ^ (bool)der;
                                    } else {
                                        Console.WriteLine("Error semantico: solo se puede  usar valores booleanos. ref53");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: solo se puede  usar valores booleanos. ref52");
                                }
                            }
                            else if (node.ChildNodes[0].Token.Value.ToString().Equals("(")) {//expresion (exp)
                                result = action(node.ChildNodes[1]);
                            }
                        }
                        break;
                    }
                case "numero"://listo
                    {
                        result = node.Token.Value;
                        break;
                    }
                case "valor_bool"://listo
                    {
                        if (node.Token.Value.ToString().Equals("verdadero")){
                            result = true;
                        }else {
                            result = false;
                        }
                        break;
                    }
                case "caracter"://listo
                    {
                        string cad = (string)node.Token.Value;
                        cad = cad.Substring(1, cad.Length - 2);
                        result = cad;
                        break;
                    }
                case "cadena"://listo
                    {
                        string cad = (string)node.Token.Value;
                        cad = cad.Substring(1, cad.Length - 2);
                        result = cad;
                        break;
                    }
                case "id"://listo
                    {
                        result = node.Token.Value;
                        Variable temp_var = lst_variables.buscar(node.Token.Value.ToString());
                        if(temp_var!=null){
                            result=temp_var.valor;
                        }else{
                            Console.WriteLine("Error semantico: la variable no ha sido declarada. ref9");
                        }
                        break;
                    }
                case "dec_var"://faltan declarar arreglos
                    {
                        if (node.ChildNodes.Count == 4) {
                            if (node.ChildNodes[0].Token.Value.Equals("keep"))
                            {
                                Variables variables_temp = (Variables)action(node.ChildNodes[3]);
                                string tipo=(string)action(node.ChildNodes[2]);
                                for(int i=0; i<variables_temp.lista.Count; i++){
                                    variables_temp.lista[i].tipo=tipo;
                                    if (lst_variables.buscar(variables_temp.lista[i].id) == null)
                                    {
                                        if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                            (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                            (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                            (tipo.Equals("caracter") && variables_temp.lista[i].valor is string) ||
                                            (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                        {
                                            lst_variables.agregar(variables_temp.lista[i]);
                                        }
                                        else {
                                            Console.WriteLine("Error semantico: no se puede asignar un tipo diferente a la variable: "+tipo.ToString()+" ref12");
                                        }
                                    }
                                    else {
                                        Console.WriteLine("Error semantico: la variable ya ha sido declarada antes. "+variables_temp.lista[i].id);
                                    }
                                }
                            }
                            else {
                                Variables variables_temp = (Variables)action(node.ChildNodes[3]);
                                string tipo = (string)action(node.ChildNodes[1]);
                                for (int i = 0; i < variables_temp.lista.Count; i++)
                                {
                                    variables_temp.lista[i].tipo = tipo;
                                    if (lst_variables.buscar(variables_temp.lista[i].id) == null)
                                    {
                                        if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                            (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                            (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                            (tipo.Equals("caracter") && variables_temp.lista[i].valor is string) ||
                                            (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                        {
                                            lst_variables.agregar(variables_temp.lista[i]);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: no se puede asignar un tipo diferente a la variable: " + tipo.ToString() + " ref13");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: la variable ya ha sido declarada antes. " + variables_temp.lista[i].id);
                                    }
                                }
                            }
                        }
                        else if (node.ChildNodes.Count == 5) {
                            Variables variables_temp = (Variables)action(node.ChildNodes[4]);
                            string tipo=(string)action(node.ChildNodes[2]);
                            for (int i = 0; i < variables_temp.lista.Count; i++)
                            {
                                variables_temp.lista[i].tipo = tipo;
                                if (lst_variables.buscar(variables_temp.lista[i].id) == null)
                                {
                                    if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                        (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                        (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                        (tipo.Equals("caracter") && variables_temp.lista[i].valor is string) ||
                                        (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                    {
                                        lst_variables.agregar(variables_temp.lista[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se puede asignar un tipo diferente a la variable: " + tipo.ToString() + " ref14");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: la variable ya ha sido declarada antes. " + variables_temp.lista[i].id);
                                }
                            }
                        }
                        else if (node.ChildNodes.Count == 3) {
                            Variables variables_temp = (Variables)action(node.ChildNodes[2]);
                            string tipo = (string)action(node.ChildNodes[1]);
                            for (int i = 0; i < variables_temp.lista.Count; i++)
                            {
                                variables_temp.lista[i].tipo = tipo;
                                if (lst_variables.buscar(variables_temp.lista[i].id) == null)
                                {
                                    if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                        (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                        (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                        (tipo.Equals("caracter") && variables_temp.lista[i].valor is string) ||
                                        (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                    {
                                        lst_variables.agregar(variables_temp.lista[i]);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se puede asignar un tipo diferente a la variable: " + tipo.ToString() + " ref15");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: la variable ya ha sido declarada antes. " + variables_temp.lista[i].id);
                                }
                            }
                        }
                        break;
                    }
                case "asig_var":
                    {
                        if (node.ChildNodes.Count == 2) { } else if (node.ChildNodes.Count == 3) {
                            if (node.ChildNodes[1].Token.Value.ToString().Equals("=")) {
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                if (var_temp != null) {
                                    if (var_temp.vof == 1)
                                    {
                                        object val_temp = action(node.ChildNodes[2]);
                                        if (var_temp.tipo.Equals("entero")) {
                                            if (val_temp is int) {
                                                var_temp.valor = (int)val_temp;
                                            } else if (val_temp is double) {
                                                var_temp.valor = (int)val_temp;
                                            } else if (val_temp is bool) {
                                                if ((bool)val_temp) {
                                                    var_temp.valor = 1;
                                                } else {
                                                    var_temp.valor = 0;
                                                }
                                            } else {
                                                Console.WriteLine("Error semantico: error al asginar entero. ref63");
                                            }
                                        }
                                        else if (var_temp.tipo.Equals("doble")) {
                                            if (val_temp is int) {
                                                var_temp.valor = (int)val_temp;
                                            } else if (val_temp is double) {
                                                var_temp.valor = (double)val_temp;
                                            } else {
                                                Console.WriteLine("Error semantico:  error al asignar doble. ref62");
                                            }
                                        }else if (var_temp.tipo.Equals("cadena")) {
                                            if (val_temp is int) {
                                                var_temp.valor = Convert.ToString((int)val_temp);
                                            } else if (val_temp is double) {
                                                var_temp.valor = Convert.ToString((double)val_temp);
                                            } else if (val_temp is string) {
                                                var_temp.valor = val_temp;
                                            } else if (val_temp is bool) {
                                                if ((bool)val_temp){
                                                    var_temp.valor = "verdadero";
                                                }else {
                                                    var_temp.valor = "falso";
                                                }
                                            } else {
                                                Console.WriteLine("Error semantico: error al asigar cadena ref61");
                                            }
                                        }
                                        else if (var_temp.tipo.Equals("caracter")) {//pendiente
                                            if (val_temp is string) { 
                                            }else if(val_temp is int){
                                                
                                            } else {
                                                Console.WriteLine("Error semantico: error de tipo en asignacion de caracter. ref60");
                                            }
                                        }else if (var_temp.tipo.Equals("boolean")){
                                            if (val_temp is bool){
                                                var_temp.valor = val_temp;
                                            }else{
                                                Console.WriteLine("Érror semantico: intento asigar un valor incorrecto a boolean. id: " + var_temp.id + " ref58");
                                            }
                                        }else{
                                            Console.WriteLine("Error semantico: ha ocurrido un error con el tipo de la variable. ref57");
                                        }
                                    }else {
                                        Console.WriteLine("Error semantico: el id es una funcion y solo se puede asignar a una variable. ref59");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. ref55");
                                }
                            }
                        }
                        break;
                    }
                case "tipo_var": 
                    {
                        result = node.ChildNodes[0].Token.Value.ToString();
                        break;
                    }
                case "lista_ids"://listo
                    {
                        if (node.ChildNodes.Count == 1) {
                            Variables variables_temp = new Variables();
                            Variable n = new Variable();
                            n.nivel = this.nivel;
                            n.vof=1;
                            n.id = node.ChildNodes[0].Token.Value.ToString();
                            variables_temp.agregar(n);
                            result = variables_temp;
                        }
                        else if (node.ChildNodes.Count == 3) { 
                            if(node.ChildNodes[1].Token.Value.Equals("=")){
                                Variables variables_temp = new Variables();
                                Variable n = new Variable();
                                n.nivel = this.nivel;
                                n.vof=1;
                                n.id = node.ChildNodes[0].Token.Value.ToString();
                                n.valor = action(node.ChildNodes[2]);
                                variables_temp.agregar(n);
                                result = variables_temp;
                            }else if(node.ChildNodes[1].Token.Value.Equals(",")){
                                Variables variables_temp = (Variables)action(node.ChildNodes[0]);
                                Variable n = new Variable();
                                n.nivel = this.nivel;
                                n.vof=1;
                                n.id = node.ChildNodes[2].Token.Value.ToString();
                                variables_temp.agregar(n);
                                result = variables_temp;
                            }
                        }else if(node.ChildNodes.Count==5){
                            Variables variables_temp = (Variables)action(node.ChildNodes[0]);
                            Variable n = new Variable();
                            n.nivel = this.nivel;
                            n.vof=1;
                            n.id = node.ChildNodes[2].Token.Value.ToString();
                            n.valor = action(node.ChildNodes[4]);
                            variables_temp.agregar(n);
                            result = variables_temp;
                        }
                        break;
                    }
                /*case "e":
                    {
                        if (node.ChildNodes.Count == 1)
                        {
                            result = action(node.ChildNodes[0]);
                        }
                        else if (node.ChildNodes.Count == 3)
                        {
                            double op1 = Convert.ToDouble(action(node.ChildNodes[0]).ToString());
                            double op2 = Convert.ToDouble(action(node.ChildNodes[2]).ToString());
                            if (node.ChildNodes[1].Token.Value.ToString() == "+")
                            {
                                result = op1 + op2;
                            }
                            else
                            {
                                result = op1 - op2;
                            }
                        }
                        break;
                    }
                case "t":
                    {
                        if (node.ChildNodes.Count == 1)
                        {
                            result = action(node.ChildNodes[0]);
                        }
                        else if (node.ChildNodes.Count == 3)
                        {
                            double op1 = Convert.ToDouble(action(node.ChildNodes[0]).ToString());
                            double op2 = Convert.ToDouble(action(node.ChildNodes[2]).ToString());
                            if (node.ChildNodes[1].Token.Value.ToString() == "*")
                            {
                                result = op1 * op2;
                            }
                            else
                            {
                                result = op1 / op2;
                            }
                        }
                        break;
                    }
                case "f":
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            result = action(node.ChildNodes[0]);
                        }
                        break;
                    }
                case "number":
                    {
                        result = node.Token.Value;
                        break;
                    }*/
            }
            return result;
        }
    }

 
    
}
