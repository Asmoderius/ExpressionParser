using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionParser
{
    public static class PostFixParserAlternative
    {
        private static readonly Dictionary<string, Tuple<string, byte, bool>> operators = new Dictionary<string, Tuple<string, byte, bool>>
        {
            {"+", new Tuple<string, byte, bool>("+", 2, false) },
            {"-", new Tuple<string, byte, bool>("-", 2, false) },
            {"*", new Tuple<string, byte, bool>("*", 3, false) },
            {"/", new Tuple<string, byte, bool>("/", 3, false) },
            {"^", new Tuple<string, byte, bool>("^", 4, true) },
        };

        public static string[] ToPostFix(string[] inFix)
        {
            List<string> postFixNotation = new List<string>();
            Stack<string> postFixStack = new Stack<string>();
            foreach (string token in inFix)
            {
                if(int.TryParse(token, out _))
                {
                    postFixNotation.Add(token);
                }
                else if(operators.TryGetValue(token, out Tuple<string, byte, bool> op1))
                {
                    while (postFixStack.Count > 0 && operators.TryGetValue(postFixStack.Peek(), out var op2))
                    {
                        int c = op1.Item2.CompareTo(op2.Item2);
                        if (c < 0 || !op1.Item3 && c <= 0)
                        {
                            postFixNotation.Add(postFixStack.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    postFixStack.Push(token);
                }
                else if (token == "(")
                {
                    postFixStack.Push(token);
                }
                else if (token == ")")
                {
                    string top = "";
                    while (postFixStack.Count > 0 && (top = postFixStack.Pop()) != "(")
                    {
                        postFixNotation.Add(top);
                    }
                    if (top != "(") throw new ArgumentException("No matching left parenthesis.");
                }
            }
            while (postFixStack.Count > 0)
            {
                var top = postFixStack.Pop();
                if (!operators.ContainsKey(top)) throw new ArgumentException("No matching right parenthesis.");
                postFixNotation.Add(top);
            }
            return postFixNotation.ToArray();
        }

    }


}
