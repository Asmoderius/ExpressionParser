using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionParser
{
    public static class ExpressionEvaluator
    {
        public static double Evaluate(Queue<string> postFix)
        {
            Console.WriteLine();
            Console.WriteLine("Evaluating..");
            double result = 0d;
            Stack<double> storage = new Stack<double>();
           while(postFix.Count > 0)
            {
                string symbol = postFix.Dequeue();
                if(!PostFixParser.IsOperator(symbol))
                {
                    double d = 0;
                    double.TryParse(symbol, out d);
                    storage.Push(d);
                }
                else
                {
                    double d2 = storage.Pop();
                    double d1 = storage.Pop();
                    Console.WriteLine("First number {0}, second number {1} and Operator is {2}", d1, d2, symbol);
                    switch (symbol)
                    {
                        case "+":
                            storage.Push(d1 + d2);
                            break;
                        case "-":
                            storage.Push(d1 - d2);
                            break;
                        case "*":
                            storage.Push(d1 * d2);
                            break;
                        case "/":
                            storage.Push(d1 / d2);
                            break;
                        case "^":
                            storage.Push(Math.Pow(d1, d2));
                            break;
                        default:
                            break;
                    }
                }
            }
            result = storage.Pop();
            return result;
        }
    }
}
