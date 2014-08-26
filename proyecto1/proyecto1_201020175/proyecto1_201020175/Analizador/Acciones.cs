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
                case "e":
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
                    }
            }
            return result;
        }
    }

 
    
}
