using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Toub.Sound.Midi;

namespace Proyecto1
{
    class Acciones
    {
        Variables lst_variables = new Variables();
        Canales lst_canales = new Canales();
        public int nivel = 0;
        public Object do_action(ParseTreeNode pt_node) {
            
            //return action(pt_node);
            action(pt_node);
            ejecutarCodigo();
            return null;
        }

        private void ejecutarCodigo(){
            Variable metodoprincipal = null;
            for (int i = 0; i < lst_variables.lista.Count; i++) {
                if (lst_variables.lista[i].id.Equals("principal")) {
                    metodoprincipal = lst_variables.lista[i];
                }
            }
            if (metodoprincipal != null)
            {
                ParseTreeNode node_ejecucion = (ParseTreeNode)metodoprincipal.valor;
                action(node_ejecucion);
            }
            else {
                Console.WriteLine("Error: el metodo principal no se ha definido.");
            }
        }

        private int getIndice(int[] indices, int[] tam)
        {
            int sumatoria = indices[0];
            for (int x = 1; x < indices.Length; x++)
                sumatoria = (sumatoria * tam[x]) + indices[x];
            return sumatoria;   
        }

        private double frecuencia(double nota, double octava)
        {
            return (440.0 * Math.Exp(((octava - 4) + (nota - 10) / 12) * Math.Log(2)));
        }

        private object casteo(string tipo, object valor) {
            object retorno = null;
            if (tipo.Equals("cadena")) {
                retorno = valor.ToString();
            }else if(tipo.Equals("entero")){
                if (valor is int) {
                    retorno = valor;
                }
                else if (valor is bool) {
                    if ((bool)valor)
                    {
                        retorno = 1;
                    }
                    else {
                        retorno = 0;
                    }
                }
                else if (valor is double) {
                    retorno = (int)valor;
                }
                else if (valor is char) {
                    retorno = (int)valor;
                }
            }else if(tipo.Equals("boolean")){
                if(valor is bool){
                    retorno = valor;
                }
            }else if(tipo.Equals("doble")){
                if(valor is int){
                    retorno = (double)valor;
                }else if(valor is double){
                    retorno = valor;
                }
            }else if(tipo.Equals("caracter")){
                if(valor is int){
                    retorno = Convert.ToChar(valor);
                }else if(valor is char){
                    retorno = valor;
                }
            }
            return retorno;
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
                case "instrucciones_in":
                    {
                        if (node.ChildNodes.Count > 0) {
                            for (int i = 0; i < node.ChildNodes.Count; i++) {
                                object a = action(node.ChildNodes[i]);
                                if (a != null)
                                {
                                    if (a is int)
                                    {
                                        if ((int)a == 0) {
                                            result = a;
                                        }else if ((int)a == 1)
                                        {
                                            result = a;
                                            continue;//sentencia continuar
                                        }
                                        else if ((int)a == 2)
                                        {
                                            result = a;
                                            break;//sentencia break
                                        }
                                    }
                                    else
                                    {
                                        result = a;//retorna valor
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    }
                case "instrucciones_in1":
                    {
                        result = action(node.ChildNodes[0]);
                        break;
                    }
                case "exp":
                    {
                        if (node.ChildNodes.Count == 1) {
                            result = action(node.ChildNodes[0]);
                        }
                        else if (node.ChildNodes.Count == 2) {
                            if (node.ChildNodes[1].Token != null)
                            {
                                if (node.ChildNodes[0].Token.Value.ToString().Equals("!¡"))
                                {// espresion is null
                                    Variable var_temp = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                                    if (var_temp != null)
                                    {
                                        if (var_temp.valor == null)
                                        {
                                            result = true;
                                        }
                                        else
                                        {
                                            result = false;
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: la variable no ha sido declarada. " + node.ChildNodes[1].Token.Value.ToString());
                                    }
                                }
                                else if (node.ChildNodes[0].Token.Value.ToString().Equals("!"))
                                {//expresion not
                                    object obj_temp = action(node.ChildNodes[1]);
                                    try
                                    {
                                        bool temp_bool = (Boolean)obj_temp;
                                        if (temp_bool)
                                        {
                                            result = false;
                                        }
                                        else
                                        {
                                            result = true;
                                        }
                                    }
                                    catch (InvalidCastException e)
                                    {
                                        Console.WriteLine("Error semantico: la expresion no se puede castear a booleano. ref1");
                                    }
                                }
                                else if (node.ChildNodes[0].Token.Value.ToString().Equals("++"))
                                {//expresion asignacion ++id
                                    Variable var_temp = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                                    if (var_temp != null)
                                    {
                                        if (var_temp.tipo.Equals("entero"))
                                        {
                                            if (var_temp.valor != null)
                                            {
                                                var_temp.valor = (int)var_temp.valor + 1;
                                            }
                                            else
                                            {
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
                                            else
                                            {
                                                var_temp.valor = 1;
                                            }
                                            result = (double)var_temp.valor;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: no se puede incrementar la variable por que no es numerico. ref3");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: la variable no ha sido declarada. ref2");
                                    }
                                }
                                else if (node.ChildNodes[0].Token.Value.ToString().Equals("--"))
                                { //expresion asigacion --id
                                    Variable var_temp = lst_variables.buscar(node.ChildNodes[1].Token.Value.ToString());
                                    if (var_temp != null)
                                    {
                                        if (var_temp.tipo.Equals("entero"))
                                        {
                                            if (var_temp.valor != null)
                                            {
                                                var_temp.valor = (int)var_temp.valor - 1;
                                            }
                                            else
                                            {
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
                                            else
                                            {
                                                var_temp.valor = -1;
                                            }
                                            result = (double)var_temp.valor;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: no se puede decrementar la variable por que no es numerico. ref5");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: la variable no ha sido declarada. ref4");
                                    }
                                }
                                else if (node.ChildNodes[1].Token.Value.ToString().Equals("++"))
                                {//expresion asignacion id++
                                    Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                    if (var_temp != null)
                                    {
                                        if (var_temp.tipo.Equals("entero"))
                                        {
                                            if (var_temp.valor != null)
                                            {
                                                result = (int)var_temp.valor;
                                                var_temp.valor = (int)var_temp.valor + 1;
                                            }
                                            else
                                            {
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
                                            else
                                            {
                                                result = 0;
                                                var_temp.valor = 1;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: no se puede incrementar la variable por que no es numerico. ref7");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: la variable no ha sido declarada. ref6");
                                    }
                                }
                                else if (node.ChildNodes[1].Token.Value.ToString().Equals("--"))
                                { //epresion asignacion id--
                                    Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                    if (var_temp != null)
                                    {
                                        if (var_temp.tipo.Equals("entero"))
                                        {
                                            if (var_temp.valor != null)
                                            {
                                                result = (int)var_temp.valor;
                                                var_temp.valor = (int)var_temp.valor - 1;
                                            }
                                            else
                                            {
                                                result = 0;
                                                var_temp.valor = -1;
                                            }
                                        }
                                        else if (var_temp.tipo.Equals("doble"))
                                        {
                                            if (var_temp.valor != null)
                                            {
                                                result = (double)var_temp.valor;
                                                var_temp.valor = (double)var_temp.valor - 1;
                                            }
                                            else
                                            {
                                                result = 0;
                                                var_temp.valor = -1;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: no se puede decrementar la variable por que no es numerico. ref9");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: la variable no ha sido declarada. ref8");
                                    }
                                }
                            }
                            else {
                                if (node.ChildNodes[1].Term.Name.Equals("lista_dim"))
                                {//expresion id[][][]
                                    //pendiente
                                    List<object> lstDim = (List<object>)action(node.ChildNodes[1]);
                                    Variable vartemp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                    if (vartemp.arreglo)
                                    {
                                        int dim1 = lstDim.Count();
                                        int dim2 = vartemp.dimensiones.Split(',').Count();
                                        if (dim1 == dim2)
                                        {
                                            int[] tam = new int[dim1];
                                            int[] ind = new int[dim1];
                                            bool dim_correctas = true;
                                            for (int i = 0; i < dim1; i++)
                                            {
                                                ind[i] = (int)lstDim[i];
                                                tam[i] = Convert.ToInt32(vartemp.dimensiones.Split(',')[i]);
                                                //Console.WriteLine("total: " + tam[i] + "   indi: " + ind[i]);
                                                if (ind[i] > -1 && ind[i] < tam[i]) { 
                                                } else {
                                                    dim_correctas = false;
                                                }
                                            }
                                            if (dim_correctas) {
                                                List<object> listaaretornar = (List<object>)vartemp.valor;
                                                result = listaaretornar[getIndice(ind,tam)];
                                            } else {
                                                Console.WriteLine("Error semantico: el rango de dimensiones no es correcto en expresion. ref67");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: no se puede obtener el indice por que las dimensiones no son las correctas para el arreglo " + vartemp.id + " ref67");
                                        }
                                    }
                                    else
                                    {
                                        Console.Write("Error semantico: la variable no es un arreglo");
                                    }
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
                                    else if (der is char) {
                                        result = izq.ToString() + der.ToString();
                                    }
                                    else if (der is bool) {
                                        string varc = "";
                                        if ((bool)der) {
                                            varc = "verdadero";
                                        } else {
                                            varc = "falso";
                                        }
                                        result = izq.ToString() + varc.ToString();
                                    }
                                    else
                                    {
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
                                }else if(izq is char){
                                    if (der is bool)
                                    {
                                        string varc = "";
                                        if ((bool)der) {
                                            varc = "verdadero";
                                        } else {
                                            varc = "falso";
                                        }
                                        result = izq.ToString() + varc.ToString();
                                    }
                                    else {
                                        result = izq.ToString() + der.ToString();
                                    }
                                }else if(izq is bool){
                                    string varc = "";
                                    if ((bool)izq)
                                    {
                                        varc = "verdadero";
                                    }
                                    else {
                                        varc = "falso";
                                    }
                                    if (der is bool)
                                    {
                                        string varc1 = "";
                                        if ((bool)der) {
                                            varc1 = "verdadero";
                                        } else {
                                            varc1 = "falso";
                                        }
                                        result = varc.ToString() + varc1.ToString();
                                    }
                                    else {
                                        result = varc.ToString() + der.ToString();
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
                                }else if(izq is bool){
                                    if (der is bool) {
                                        result = (bool)izq == (bool)der;
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref251==");
                                    }
                                }else if(izq is char){
                                    if (der is char) {
                                        result = (char)izq==(char)der;
                                    } else {
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref252==");
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
                                }
                                else if (izq is bool){
                                    if (der is bool){
                                        result = (bool)izq != (bool)der;
                                    }else{
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref251==");
                                    }
                                }else if (izq is char){
                                    if (der is char){
                                        result = (char)izq != (char)der;
                                    }else{
                                        Console.WriteLine("Error semantico: no se puede comparar datos de diferente tipo. ref252==");
                                    }
                                }else{
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
                            if (temp_var.valor != null)
                            {
                                result = temp_var.valor;
                            }
                            else {
                                Console.WriteLine("Error semantico: la variable "+node.Token.Value.ToString()+" no ha sido inicializada. refid");
                            }
                            //result=temp_var.valor;
                        }else{
                            Console.WriteLine("Error semantico: la variable no ha sido declarada. ref9");
                        }
                        break;
                    }
                case "numero"://listo
                    {
                        result = node.Token.Value;
                        break;
                    }
                case "caracter"://listo
                    {
                        string cad = (string)node.Token.Value;
                        cad = cad.Substring(1, cad.Length - 2);
                        char caracter;
                        if (cad.Length > 1 || cad.Length == 0)
                        {
                            if (cad.Equals("#t"))
                            {
                                caracter = '\t';
                            }
                            else if (cad.Equals("#n"))
                            {
                                caracter = '\n';
                            }
                            else if (cad.Equals("##"))
                            {
                                caracter = '#';
                            }
                            else if (cad.Equals("#\\"))
                            {
                                caracter = '\\';
                            }
                            else
                            {
                                Console.WriteLine("Error Semantico:");
                                caracter ='\0';
                            }
                        }
                        else
                        {
                            caracter = cad.ToCharArray()[0];
                        }

                        result = caracter;
                        break;
                    }
                case "valor_bool"://listo
                    {
                        if (node.Token.Value.ToString().Equals("verdadero"))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                        break;
                    }
                case "arreglo"://listo
                    {
                        List<object> lstExp = new List<object>();
                        lstExp = (List<object>)action(node.ChildNodes[1]);
                        result = lstExp;
                        break;
                    }
                case "arreglo1"://listo
                    {
                        List<object> lstExp = new List<object>();
                        List<object> lsttemp;
                        if (node.ChildNodes[0].Term.Name.Equals("arreglo")) {
                            string dimensiones="";
                            bool dim_iguales = true;
                            for (int i = 0; i < node.ChildNodes.Count; i++){
                                lsttemp = (List<object>)action(node.ChildNodes[i]);
                                if (i > 0) {
                                    string dim=lsttemp.Last().ToString();
                                    if (dimensiones.Equals(dim)) {
                                    } else {
                                        dim_iguales = false;
                                    }
                                } else { 
                                    string dim=lsttemp.Last().ToString();
                                    dimensiones = dim;
                                }
                            }
                            if (dim_iguales) {
                                for (int i = 0; i < node.ChildNodes.Count; i++){
                                    lsttemp = (List<object>)action(node.ChildNodes[i]);
                                    for (int ii = 0; ii < lsttemp.Count; ii++){
                                        if (ii + 1 == lsttemp.Count){
                                        }else{
                                            lstExp.Add(lsttemp[ii]);
                                        }
                                    }
                                }
                                //dimensiones = dimensiones + "," + node.ChildNodes.Count;
                                dimensiones = node.ChildNodes.Count + "," + dimensiones;
                                lstExp.Add(dimensiones.ToString());
                            } else {
                                Console.WriteLine("Error semantico: las dimensiones de los arreglos no son iguales. "+node.Term.Name);
                            }
                        } else if (node.ChildNodes[0].Term.Name.Equals("arreglo2")) {
                            lstExp = (List<object>)action(node.ChildNodes[0]);
                        } else {
                            Console.WriteLine("Error: ha ocurrido error en arreglo. "+node.ChildNodes[0].Term.Name);
                        }
                        result = lstExp;
                        break;
                    }
                case "arreglo2":
                    {
                        List<object> lstExp = new List<object>();
                        object nuevotemp;
                        for (int i = 0; i < node.ChildNodes.Count; i++) {
                            nuevotemp = action(node.ChildNodes[i]);
                            if (nuevotemp != null){
                                lstExp.Add(nuevotemp);
                            }else {
                                Console.WriteLine("Error semantico: no se ha asignado ningun valor a arreglo. ref60");
                            }
                        }
                        lstExp.Add(node.ChildNodes.Count);
                        result = lstExp;
                        break;
                    }
                case "dec_var":
                    {
                        if (node.ChildNodes.Count == 3) {
                            Variables variables_temp = (Variables)action(node.ChildNodes[2]);
                            string tipo = (string)action(node.ChildNodes[1]);
                            for (int i = 0; i < variables_temp.lista.Count; i++)
                            {
                                //////////////////////////////////////////////////////////////
                                //id,tipo,valor,keep,nivel
                                //bool varagregada = (bool)lst_variables.agregarVariable(variables_temp.lista[i].id,tipo,variables_temp.lista[i].valor,false,variables_temp.lista[i].nivel);
                                if(lst_variables.buscar(variables_temp.lista[i].id)==null){
                                    if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                        (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                        (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                        (tipo.Equals("caracter") && variables_temp.lista[i].valor is char) ||
                                        (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                    {
                                        bool varagregada = (bool)lst_variables.agregarVariable(variables_temp.lista[i].id, tipo, variables_temp.lista[i].valor, false, variables_temp.lista[i].nivel);
                                    }else {
                                        Console.WriteLine("Error semantico: no se puede asignar un tipo diferente a la variable: " + tipo.ToString() + " ref15");
                                    }
                                }else{
                                    Console.WriteLine("Error semantico: la variable ya ha sido declarada antes. " + variables_temp.lista[i].id);
                                }
                                //////////////////////////////////////////////////////////////
                                /*variables_temp.lista[i].tipo = tipo;
                                variables_temp.lista[i].keep=false;
                                variables_temp.lista[i].nivel = nivel;
                                if (lst_variables.buscar(variables_temp.lista[i].id) == null)
                                {
                                    if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                        (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                        (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                        (tipo.Equals("caracter") && variables_temp.lista[i].valor is char) ||
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
                                }*/
                            }
                        }else if (node.ChildNodes.Count == 4) {
                            if (node.ChildNodes[0].Token.Value.Equals("keep"))
                            {
                                Variables variables_temp = (Variables)action(node.ChildNodes[3]);
                                string tipo=(string)action(node.ChildNodes[2]);
                                for(int i=0; i<variables_temp.lista.Count; i++){
                                    variables_temp.lista[i].tipo=tipo;
                                    if (lst_variables.buscar(variables_temp.lista[i].id) == null){
                                        if ((tipo.Equals("boolean") && variables_temp.lista[i].valor is bool) ||
                                            (tipo.Equals("entero") && variables_temp.lista[i].valor is int) ||
                                            (tipo.Equals("doble") && variables_temp.lista[i].valor is double) ||
                                            (tipo.Equals("caracter") && variables_temp.lista[i].valor is char) ||
                                            (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                        {
                                            //lst_variables.agregar(variables_temp.lista[i]);
                                            bool varagregada = (bool)lst_variables.agregarVariable(variables_temp.lista[i].id, tipo, variables_temp.lista[i].valor, false, variables_temp.lista[i].nivel);
                                        }else {
                                            Console.WriteLine("Error semantico: no se puede asignar un tipo diferente a la variable: "+tipo.ToString()+" ref12");
                                        }
                                    }else {
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
                                            (tipo.Equals("caracter") && variables_temp.lista[i].valor is char) ||
                                            (tipo.Equals("cadena") && variables_temp.lista[i].valor is string))
                                        {
                                            //lst_variables.agregar(variables_temp.lista[i]);
                                            bool varagregada = (bool)lst_variables.agregarVariable(variables_temp.lista[i].id, tipo, variables_temp.lista[i].valor, false, variables_temp.lista[i].nivel);
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
                                        (tipo.Equals("caracter") && variables_temp.lista[i].valor is char) ||
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
                        else if (node.ChildNodes.Count == 7) {
                            //var entero arreglo arr [3][3][2] = {{{1,2,3},{1,2,3},{1,2,3}},{{1,2,3},{1,2,3},{1,2,3}}}
                            string tipovar = (string)action(node.ChildNodes[1]);
                            List<string> lstids = (List<string>)action(node.ChildNodes[3]);
                            List<object> lstDim = (List<object>)action(node.ChildNodes[4]);
                            List<object> lstExp = (List<object>)action(node.ChildNodes[6]);
                            string dim1 = "";
                            string dim2 = "";
                            for (int i = 0; i < lstDim.Count; i++)
                            {
                                if (i > 0)
                                {
                                    dim1 = dim1 + ",";
                                }
                                dim1 = dim1 + ((int)lstDim[i]).ToString();
                            }
                            dim2 = lstExp.Last().ToString();
                            lstExp.RemoveAt(lstExp.Count - 1);
                            bool exp_tipo_iguales = true;
                            for (int i = 0; i < lstExp.Count - 1; i++)
                            {
                                if (tipovar.Equals("entero"))
                                {
                                    if (lstExp[i] is int) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("doble"))
                                {
                                    if (lstExp[i] is double) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("cadena"))
                                {
                                    if (lstExp[i] is string) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("caracter"))
                                {
                                    if (lstExp[i] is char) { 
                                    }else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("boolean"))
                                {
                                    if (lstExp[i] is bool) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                            }
                            if (exp_tipo_iguales)
                            {
                                if (dim1.Equals(dim2))
                                {
                                    for (int i = 0; i < lstids.Count; i++)
                                    {
                                        Variable vartemp = lst_variables.buscar(lstids[i]);
                                        if (vartemp == null)
                                        {
                                            vartemp = new Variable();
                                            vartemp.arreglo = true;
                                            vartemp.dimensiones = dim1;
                                            vartemp.keep =false;
                                            vartemp.vof = 1;
                                            vartemp.tipo = tipovar;
                                            vartemp.id = lstids[i].ToString();
                                            vartemp.valor = lstExp;
                                            //lst_variables.agregar(vartemp);
                                            lst_variables.agregarArreglos(lstids[i].ToString(), tipovar, lstExp, false, nivel, dim1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: la variabla " + lstids[i].ToString() + " ya ha sido declarada, por eso no se puede declarar como arreglo. ref64");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: la dimension: " + dim1 + " no es igual a la dimension " + dim2 + "   ref63");
                                }
                            }
                            else
                            {
                                Console.Write("Error semantico: las expresiones del arreglo deben ser de tipo " + tipovar + "    ref63");
                            }
                        }
                        else if (node.ChildNodes.Count == 8)
                        {
                            //var entero arreglo arr [3][3][2] = {{{1,2,3},{1,2,3},{1,2,3}},{{1,2,3},{1,2,3},{1,2,3}}}
                            string tipovar = (string)action(node.ChildNodes[2]);
                            List<string> lstids = (List<string>)action(node.ChildNodes[4]);
                            List<object> lstExp = (List<object>)action(node.ChildNodes[7]);
                            List<object> lstDim = (List<object>)action(node.ChildNodes[5]);
                            string dim1 = "";
                            string dim2 = "";
                            for (int i = 0; i < lstDim.Count; i++)
                            {
                                if (i > 0)
                                {
                                    dim1 = dim1 + ",";
                                }
                                dim1 = dim1 + ((int)lstDim[i]).ToString();
                            }
                            dim2 = lstExp.Last().ToString();
                            lstExp.RemoveAt(lstExp.Count - 1);
                            bool exp_tipo_iguales = true;
                            for (int i = 0; i < lstExp.Count - 1; i++)
                            {
                                if (tipovar.Equals("entero"))
                                {
                                    if (lstExp[i] is int) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("doble"))
                                {
                                    if (lstExp[i] is double) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("cadena"))
                                {
                                    if (lstExp[i] is string) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("caracter"))
                                {
                                    if (lstExp[i] is char) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                                else if (tipovar.Equals("boolean"))
                                {
                                    if (lstExp[i] is bool) { }
                                    else
                                    {
                                        exp_tipo_iguales = false;
                                    }
                                }
                            }
                            if (exp_tipo_iguales)
                            {
                                if (dim1.Equals(dim2))
                                {
                                    for (int i = 0; i < lstids.Count; i++)
                                    {
                                        Variable vartemp = lst_variables.buscar(lstids[i]);
                                        if (vartemp == null)
                                        {
                                            vartemp = new Variable();
                                            vartemp.arreglo = true;
                                            vartemp.dimensiones = dim1;
                                            vartemp.keep = true;
                                            vartemp.id = lstids[i].ToString();
                                            vartemp.vof = 1;
                                            vartemp.tipo = tipovar;
                                            vartemp.valor = lstExp;
                                            //lst_variables.agregar(vartemp);
                                            lst_variables.agregarArreglos(lstids[i].ToString(), tipovar, lstExp, true, this.nivel, dim1);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: la variabla " + lstids[i].ToString() + " ya ha sido declarada, por eso no se puede declarar como arreglo. ref64");
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: la dimension: " + dim1 + " no es igual a la dimension " + dim2 + "   ref63");
                                }
                            }
                            else
                            {
                                Console.Write("Error semantico: las expresiones del arreglo deben ser de tipo " + tipovar + "    ref63");
                            }
                        }
                        break;
                    }
                case "lista_dim":
                    {
                        List<object> listaDim = new List<object>();
                        for (int i = 0; i < node.ChildNodes.Count; i++) {
                            listaDim.Add(action(node.ChildNodes[i]));
                        }
                        result = listaDim;
                        break;
                    }
                case "dim_arreglo":
                    {
                        object recibe = action(node.ChildNodes[1]);
                        if (recibe is int){
                            result = recibe;
                        }else {
                            Console.WriteLine("Erro semantico: los indices de un arreglo se indican con una expresion que genra entero. refdim_arreglo");
                        }
                        break;
                    }
                case "lista_ids_arreglo":
                    {
                        List<string> listaids = new List<string>();
                        for (int i = 0; i < node.ChildNodes.Count; i++) {
                            listaids.Add(node.ChildNodes[i].Token.Value.ToString());
                            //listaids.Add((string)action(node.ChildNodes[i]));
                        }
                        result = listaids;
                        break;
                    }
                case "asig_var":
                    {
                        if (node.ChildNodes.Count == 2) {
                            Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                            if (var_temp != null) {
                                if(var_temp.vof==1){
                                    if (var_temp.valor is int) {
                                        if (node.ChildNodes[1].Token.Value.ToString().Equals("++")){
                                            var_temp.valor = (int)var_temp.valor + 1;
                                        }else if (node.ChildNodes[1].Token.Value.ToString().Equals("--")){
                                            var_temp.valor = (int)var_temp.valor - 1;
                                        }
                                    } else if (var_temp.valor is double) {
                                        if (node.ChildNodes[1].Token.Value.ToString().Equals("++")) {
                                            var_temp.valor = (double)var_temp.valor + 1;
                                        } else if (node.ChildNodes[1].Token.Value.ToString().Equals("--")) {
                                            var_temp.valor = (double)var_temp.valor - 1;
                                        }
                                    } else {
                                        Console.WriteLine("Error semantico: la variable "+var_temp.id+" no es de tipo numerico. ref ++ --");
                                    }
                                }else{
                                    Console.WriteLine("Error semantico: el id: "+var_temp.id+" es una funcion y solo se puede asignar a una variable. ref59");
                                }
                            } else {
                                Console.WriteLine("Error Semantico: la variable"+node.ChildNodes[0].Token.Value.ToString()+" no ha sido declarada. ref ++ --");
                            }
                        } else if (node.ChildNodes.Count == 3) {
                            if (node.ChildNodes[1].Token.Value.ToString().Equals("=")) {
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                if (var_temp != null) {
                                    if (var_temp.vof == 1){
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
                                        }else if (var_temp.tipo.Equals("doble")) {
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
                                            }else if(val_temp is char){
                                                var_temp.valor = val_temp.ToString();
                                            } else {
                                                Console.WriteLine("Error semantico: error al asigar cadena ref61");
                                            }
                                        }
                                        else if (var_temp.tipo.Equals("caracter")) {//pendiente
                                            if (val_temp is char) {
                                                var_temp.valor = val_temp;
                                            }else if(val_temp is int){
                                                //var_temp.valor = (Char)val_temp;
                                                var_temp.valor = Convert.ToChar(val_temp);
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
                                        Console.WriteLine("Error semantico: el id " + var_temp.id + " es una funcion y solo se puede asignar a una variable. ref591");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: la variable no ha sido declarada. ref55");
                                }
                            }else if (node.ChildNodes[1].Token.Value.ToString().Equals("+=")) {
                                Variable var_temp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                if (var_temp != null) {
                                    if (var_temp.vof == 1) {
                                        object val_temp = action(node.ChildNodes[2]);
                                        if (val_temp != null)
                                        {
                                            object val_casteado = casteo(var_temp.tipo,val_temp);
                                            if (val_casteado != null)
                                            {
                                                if (val_casteado is string) {
                                                    var_temp.valor = (string)var_temp.valor + (string)val_casteado;
                                                } else if (val_casteado is int) {
                                                    var_temp.valor = (int)var_temp.valor + (int)val_casteado;
                                                } else if (val_casteado is double) {
                                                    var_temp.valor = (double)var_temp.valor + (double)val_casteado;
                                                }
                                            }
                                            else {
                                                Console.WriteLine("Error semantico: el casteo no ha sido posible para " +var_temp.id+". ref+=");
                                            }
                                        }
                                        else {
                                            Console.WriteLine("Error semantico: la variable "+node.ChildNodes[2].Token.Value.ToString()+ " ref asig_var_+=");
                                        }
                                    } else {
                                        Console.WriteLine("Error semantico: el id es una funcion y solo se puede asignar a variables.");
                                    }
                                } else {
                                    Console.WriteLine("Error semantico: La variable no ha sido Declarada. refasig_var");
                                }
                            }
                        }
                        else if (node.ChildNodes.Count == 4) {
                            if (node.ChildNodes[1].Term.Name.Equals("lista_dim"))
                            {//expresion id[][][]
                                //pendiente
                                List<object> lstDim = (List<object>)action(node.ChildNodes[1]);
                                Variable vartemp = lst_variables.buscar(node.ChildNodes[0].Token.Value.ToString());
                                if (vartemp.arreglo)
                                {
                                    int dim1 = lstDim.Count();
                                    int dim2 = vartemp.dimensiones.Split(',').Count();
                                    if (dim1 == dim2)
                                    {
                                        int[] tam = new int[dim1];
                                        int[] ind = new int[dim1];
                                        bool dim_correctas = true;
                                        for (int i = 0; i < dim1; i++)
                                        {
                                            ind[i] = (int)lstDim[i];
                                            tam[i] = Convert.ToInt32(vartemp.dimensiones.Split(',')[i]);
                                            //Console.WriteLine("total: " + tam[i] + "   indi: " + ind[i]);
                                            if (ind[i] > -1 && ind[i] < tam[i])
                                            {
                                            }
                                            else
                                            {
                                                dim_correctas = false;
                                            }
                                        }
                                        if (dim_correctas)
                                        {
                                            //asignar arreglo
                                            List<object> listaaretornar = (List<object>)vartemp.valor;
                                            int indice = getIndice(ind, tam);
                                            if (vartemp.vof == 1)
                                            {
                                                object val_temp = action(node.ChildNodes[3]);
                                                if (vartemp.tipo.Equals("entero"))
                                                {
                                                    if (val_temp is int)
                                                    {
                                                        listaaretornar[indice] = (int)val_temp;
                                                    }
                                                    else if (val_temp is double)
                                                    {
                                                        listaaretornar[indice] = (int)val_temp;
                                                    }
                                                    else if (val_temp is bool)
                                                    {
                                                        if ((bool)val_temp)
                                                        {
                                                            listaaretornar[indice] = 1;
                                                        }
                                                        else
                                                        {
                                                            listaaretornar[indice] = 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error semantico: error al asginar entero. ref63");
                                                    }
                                                }
                                                else if (vartemp.tipo.Equals("doble"))
                                                {
                                                    if (val_temp is int)
                                                    {
                                                        listaaretornar[indice] = (int)val_temp;
                                                    }
                                                    else if (val_temp is double)
                                                    {
                                                        listaaretornar[indice] = (double)val_temp;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error semantico:  error al asignar doble. ref62");
                                                    }
                                                }
                                                else if (vartemp.tipo.Equals("cadena"))
                                                {
                                                    if (val_temp is int)
                                                    {
                                                        listaaretornar[indice] = Convert.ToString((int)val_temp);
                                                    }
                                                    else if (val_temp is double)
                                                    {
                                                        listaaretornar[indice] = Convert.ToString((double)val_temp);
                                                    }
                                                    else if (val_temp is string)
                                                    {
                                                        listaaretornar[indice] = val_temp;
                                                    }
                                                    else if (val_temp is bool)
                                                    {
                                                        if ((bool)val_temp)
                                                        {
                                                            listaaretornar[indice] = "verdadero";
                                                        }
                                                        else
                                                        {
                                                            listaaretornar[indice] = "falso";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error semantico: error al asigar cadena ref612");
                                                    }
                                                }
                                                else if (vartemp.tipo.Equals("caracter"))
                                                {
                                                    if (val_temp is char)
                                                    {
                                                        listaaretornar[indice] = val_temp;
                                                    }
                                                    else if (val_temp is int)
                                                    {
                                                        //listaaretornar[indice] = (char)val_temp;
                                                        listaaretornar[indice] = Convert.ToChar(val_temp);
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Error semantico: error de tipo en asignacion de caracter. ref60");
                                                    }
                                                }
                                                else if (vartemp.tipo.Equals("boolean"))
                                                {
                                                    if (val_temp is bool)
                                                    {
                                                        listaaretornar[indice] = val_temp;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Érror semantico: intento asigar un valor incorrecto a boolean. id: " + vartemp.id + " ref58");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Error semantico: ha ocurrido un error con el tipo de la variable. ref571");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error semantico: el id " + vartemp.id + " es una funcion y solo se puede asignar a una variable. ref592");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Error semantico: el rango de dimensiones no es correcto en expresion. ref67");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se puede obtener el indice por que las dimensiones no son las correctas para el arreglo " + vartemp.id + " ref67");
                                    }
                                }
                                else
                                {
                                    Console.Write("Error semantico: la variable no es un arreglo");
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
                case "sen_si": 
                    {
                        if (node.ChildNodes.Count() == 5) {
                            object resultado = action(node.ChildNodes[2]);
                            if (resultado is bool)
                            {
                                if ((bool)resultado) {
                                    action(node.ChildNodes[4]);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error semantico: la expresión de condición debe retornar booleano ref66");
                            }
                        }else if(node.ChildNodes.Count()==  7){
                            object resultado = action(node.ChildNodes[2]);
                            if (resultado is bool) {
                                if ((bool)resultado) {
                                    action(node.ChildNodes[4]);
                                } else {
                                    action(node.ChildNodes[6]);
                                }
                            } else {
                                Console.WriteLine("Error semantico: la expresión de condición debe retornar booleano ref67");
                            }
                        }
                        break;
                    }
                case "dec_para": //pendiente
                    {
                        if (node.ChildNodes.Count == 1) { }else if (node.ChildNodes.Count == 5) {
                            
                        }
                        break;
                    }
                case "sen_mientras":
                    {
                        if (node.ChildNodes.Count == 5) {
                            nivel++;
                            object repite = action(node.ChildNodes[2]);
                            while((bool)repite){
                                Console.WriteLine("repite mientras");
                                object a = action(node.ChildNodes[4]);
                                if (a != null) {
                                    if (a is int)
                                    {
                                        if ((int)a == 1)
                                        {
                                            continue;
                                        }
                                        else if ((int)a == 2)
                                        {
                                            break;
                                        }
                                    }
                                    else {
                                        result = a;
                                        //break;
                                    }
                                }
                                repite = action(node.ChildNodes[2]);
                            }
                            nivel--;
                        }
                        break;
                    }
                case "sen_hacer":
                    {
                        if (node.ChildNodes.Count == 6) {
                            nivel++;
                            object repite = action(node.ChildNodes[4]);
                            do{
                                Console.WriteLine("repite mientras");
                                object a = action(node.ChildNodes[1]);
                                if (a != null){
                                    if (a is int){
                                        if ((int)a == 1){
                                            continue;
                                        }else if ((int)a == 2){
                                            break;
                                        }
                                    }else{
                                        result = a;
                                        //break;
                                    }
                                }
                                repite = action(node.ChildNodes[4]);
                            }while((bool)repite);
                            nivel--;
                        }
                        break;
                    }
                case "sen_fun_proc":
                    {
                        if (node.ChildNodes.Count == 7) {
                            string idfun= node.ChildNodes[2].Token.Value.ToString();
                            string tipofun = (string)action(node.ChildNodes[1]);
                            object valorfun = node.ChildNodes[6];
                            bool keepfun = true;
                            int nivelfun = 0;
                            List<Variable> paramfun = (List<Variable>)action(node.ChildNodes[4]);
                            bool resultado = lst_variables.agregarFuncion(idfun,tipofun,valorfun,keepfun,nivelfun,paramfun);
                            if (resultado){
                            }else {
                                Console.WriteLine("Error semantico: no se ha guardado la funcion."+idfun.ToString());
                            }
                        }
                        else if (node.ChildNodes.Count == 6) {
                            //if (node.ChildNodes[0].Token.Value.ToString().Equals("keep"))
                            if (node.ChildNodes[0].Token!=null)
                            {
                                if (node.ChildNodes[1].Term.Name.ToString().Equals("tipo_var"))
                                {
                                    string idfun = node.ChildNodes[2].Token.Value.ToString();
                                    string tipofun = (string)action(node.ChildNodes[1]);
                                    object valorfun = node.ChildNodes[5];
                                    bool keepfun = true;
                                    int nivelfun = 0;
                                    List<Variable> paramfun = null;
                                    bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                                    if (resultado)
                                    {
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                                    }
                                }
                                else {
                                    string idfun = node.ChildNodes[1].Token.Value.ToString();
                                    string tipofun = "void";
                                    object valorfun = node.ChildNodes[5];
                                    bool keepfun = true;
                                    int nivelfun = 0;
                                    List<Variable> paramfun = (List<Variable>)action(node.ChildNodes[3]);
                                    bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                                    if (resultado)
                                    {
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                                    }
                                }
                            }
                            else {
                                string idfun = node.ChildNodes[1].Token.Value.ToString();
                                string tipofun = (string)action(node.ChildNodes[0]);
                                object valorfun = node.ChildNodes[5];
                                bool keepfun = false;
                                int nivelfun = 0;
                                List<Variable> paramfun = (List<Variable>)action(node.ChildNodes[3]);
                                bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                                if (resultado)
                                {
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                                }
                            }
                        }
                        else if (node.ChildNodes.Count == 5) {
                            if (node.ChildNodes[0].Token != null)
                            {
                                if (node.ChildNodes[0].Token.Value.ToString().Equals("keep"))//con keep
                                {
                                    string idfun = node.ChildNodes[1].Token.Value.ToString();
                                    string tipofun = "void";
                                    object valorfun = node.ChildNodes[4];
                                    bool keepfun = true;
                                    int nivelfun = 0;
                                    List<Variable> paramfun = null;
                                    bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                                    if (resultado)
                                    {
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                                    }
                                }
                                else { //sin keep
                                    string idfun = node.ChildNodes[0].Token.Value.ToString();
                                    string tipofun = "void";
                                    object valorfun = node.ChildNodes[4];
                                    bool keepfun = false;
                                    int nivelfun = 0;
                                    List<Variable> paramfun = (List<Variable>)action(node.ChildNodes[2]);
                                    bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                                    if (resultado){
                                    }else{
                                        Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                                    }
                                }
                            }
                            else { //sin keep
                                string idfun = node.ChildNodes[1].Token.Value.ToString();
                                string tipofun = (string)action(node.ChildNodes[0]);
                                object valorfun = node.ChildNodes[4];
                                bool keepfun = false;
                                int nivelfun = 0;
                                List<Variable> paramfun = null;
                                bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                                if (resultado)
                                {
                                }
                                else
                                {
                                    Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                                }
                            }
                        }
                        else if (node.ChildNodes.Count == 4) {
                            string idfun = node.ChildNodes[0].Token.Value.ToString();
                            string tipofun = "void";
                            object valorfun = node.ChildNodes[3];
                            bool keepfun = false;
                            int nivelfun = 0;
                            List<Variable> paramfun = null;
                            bool resultado = lst_variables.agregarFuncion(idfun, tipofun, valorfun, keepfun, nivelfun, paramfun);
                            if (resultado)
                            {
                            }
                            else
                            {
                                Console.WriteLine("Error semantico: no se ha guardado la funcion." + idfun.ToString());
                            }
                        }
                        break;
                    }
                case "lista_dec_param":
                    {
                        if (node.ChildNodes.Count == 2) {
                            List<Variable> param = new List<Variable>();
                            Variable a = new Variable();
                            a.id = node.ChildNodes[1].Token.Value.ToString();
                            a.tipo = (string)action(node.ChildNodes[0]);
                            param.Add(a);
                            result = param;
                        }
                        else if (node.ChildNodes.Count == 4) {
                            List<Variable> param = (List<Variable>)action(node.ChildNodes[0]);
                            Variable a = new Variable();
                            a.id = node.ChildNodes[3].Token.Value.ToString();
                            a.tipo = (string)action(node.ChildNodes[2]);
                            param.Add(a);
                            result = param;
                        }
                        break;
                    }
                case "llamada_a_funcion_instruccion":
                    {
                        string id = (string)action(node.ChildNodes[0]);
                        if (node.ChildNodes.Count == 3) { } else if (node.ChildNodes.Count == 4) {

                        }
                        break;
                    }
                case "sen_fun_reproducir":
                    {
                        string nota = (string)action(node.ChildNodes[2]);
                        int octava=0;
                        int milisegundos=0;
                        int canal=0;
                        bool nota_Valida = true;
                        if (action(node.ChildNodes[4]) is int){
                            octava = (int)action(node.ChildNodes[4]);
                        }else {
                            Console.WriteLine("Error semantico: el parametro de octava para la funcion reproducir debe ser entero.");
                            nota_Valida = false;
                        }
                        if (action(node.ChildNodes[6]) is int)
                        {
                            milisegundos = (int)action(node.ChildNodes[6]);
                        }
                        else
                        {
                            Console.WriteLine("Error semantico: el parametro de milisegundos para la funcion reproducir debe ser entero.");
                            nota_Valida = false;
                        }
                        if (action(node.ChildNodes[8]) is int)
                        {
                            canal = (int)action(node.ChildNodes[8]);
                        }
                        else
                        {
                            Console.WriteLine("Error semantico: el parametro de canal para la funcion reproducir debe ser entero.");
                            nota_Valida = false;
                        }
                        if (nota_Valida) {
                            NoteOn nueva = new NoteOn(milisegundos, (byte)canal,nota + octava.ToString(), 127);
                            lst_canales.agregarNotas(canal, nueva);
                        }
                        break;
                    }
                case "nota":
                    {
                        switch (node.ChildNodes[0].Token.Value.ToString())
                        {
                            case "do":{result = "c";break;}
                            case "do#": { result = "c#"; break; }
                            case "re": { result = "d"; break; }
                            case "re#": { result = "d#"; break; }
                            case "mi": { result = "e"; break; }
                            case "fa": { result = "f"; break; }
                            case "fa#": { result = "f#"; break; }
                            case "sol": { result = "g"; break; }
                            case "sol#": { result = "g#"; break; }
                            case "la": { result = "a"; break; }
                            case "la#": { result = "a#"; break; }
                            case "si": { result = "b"; break; }
                        }
                        break;
                    }
            }
            return result;
        }
    }

 
    
}
