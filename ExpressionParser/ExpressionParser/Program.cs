using System;
using System.Collections.Generic;

namespace ExpressionParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "512  + 7*8+9^2 / 37";

            List<string> subStrings = new List<string>();
            string number = String.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsWhiteSpace(s[i]))
                {
                    if (!IsOperator(s[i]))
                    {
                        number += s[i];
                    }
                    else
                    {
                        string op = string.Empty;
                        op += s[i];
                        subStrings.Add(number);
                        subStrings.Add(op);
                        number = string.Empty;
                    }
                }
            }
            if(number != string.Empty)
            {
                subStrings.Add(number);
            }

            Console.WriteLine(s);
            foreach (string sub in subStrings)
            {
                Console.Write("{0}", sub);
            }

            Console.WriteLine();
            Queue<string> postFixNotation = PostFixParser.ToPostFix(subStrings.ToArray());
            string[] strArray = PostFixParserAlternative.ToPostFix(subStrings.ToArray());
            string[] c = postFixNotation.ToArray();
 
            foreach (string ch in c)
            {
                Console.Write(ch + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Testing new PostFixParser:");
            foreach (string ch in strArray)
            {
                Console.Write("{0} ", ch);
            }
            Console.WriteLine();
            double result = ExpressionEvaluator.Evaluate(postFixNotation);
            Console.WriteLine();
            Console.WriteLine("Result is {0}", result);
            Console.ReadLine();
        }

        public static bool IsOperator(char c)
        {
            switch (c)
            {
                case '^':
                case '*':
                case '/':
                case '+':
                case '-':
                    return true;
                default:
                    return false;
            }
        }
    }
}