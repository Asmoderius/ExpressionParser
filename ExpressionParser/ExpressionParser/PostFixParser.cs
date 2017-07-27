using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionParser
{
    public static class PostFixParser
    {

        public static Queue<string> ToPostFix(string[] inFix)
        {
            Queue<string> postFix = new Queue<string>();
            Stack<string> postStack = new Stack<string>();
            postStack.Push("#");
            for (int i = 0; i < inFix.Length; i++)
            {
                string symbol = inFix[i];
                if (!IsOperator(symbol) && !IsBracket(symbol))
                {
                    postFix.Enqueue(symbol);
                }
                else if (IsOperator(symbol))
                {
                    while (IsOperator(postStack.Peek()) && GetPrecedence(postStack.Peek()) >= GetPrecedence(symbol))
                    {
                        postFix.Enqueue(postStack.Pop());
                    }
                    postStack.Push(symbol);
                }
                else if (symbol == "(")
                {
                    postStack.Push(symbol);
                }
                else if (symbol == ")")
                {
                    string top = string.Empty;
                    while(postStack.Count > 0 && (top = postStack.Pop()) != "(")
                    {
                        postFix.Enqueue(top);
                    }
                    if (top != "(") throw new ArgumentException("Mismatched parentheses. Missing (");
                }
            }
            while(postStack.Peek() != "#")
            {
                string c = postStack.Pop();
                if (c == "(")
                {
                    Console.WriteLine("Mismatched parentheses. Missing )");
                    postFix.Clear();
                    break;
                }
                postFix.Enqueue(c);
            }
          
            return postFix;
        }



        public static bool IsOperator(string c)
        {
            switch (c)
            {
                case "^":
                case "*":
                case "/":
                case "+":
                case "-":
                    return true;
                default:
                    return false;  
            }
        }

        private static bool IsBracket(string c)
        {
            return (c == "(" || c == ")") ? true : false;
        }
        private static byte GetPrecedence(string op)
        {
            switch (op)
            {
                case "^":
                    return 4;
                case "*":
                case "/":
                    return 3;
                case "+":
                case "-":
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
