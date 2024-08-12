using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Program
    {
        public static bool issign(char c)
        {
            return (c == '+' || c == '-' || c == '*' || c == '/');
        }
        public static bool isbrck(char c)
        {
            return (c == '(' || c == ')');
        }
        public static string bracketadder(string exp, bool status)
        {
            if (status)
            {
                for (int i = 0; i < exp.Length; i++)
                {
                    if (exp[i] == '*' || exp[i] == '/')
                    {
                        int br = 0;
                        int start, end;
                        start = 0;
                        end = exp.Length - 1;
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (exp[j] == ')')
                            {
                                br++;
                            }
                            else if (exp[j] == '(')
                            {
                                br--;
                            }
                            if (issign(exp[j]) && br == 0)
                            {
                                start = j + 1;
                                break;
                            }
                            if ((exp[j] == '(' || exp[j] == ')') && br == 0)
                            {
                                start = j + 1;
                                break;
                            }
                        }
                        br = 0;
                        for (int j = i + 1; j < exp.Length; j++)
                        {
                            if (exp[j] == '(')
                            {
                                br++;
                            }
                            else if (exp[j] == ')')
                            {
                                br--;
                            }
                            if (issign(exp[j]) && br == 0)
                            {
                                end = j - 1;
                                break;
                            }
                            if ((exp[j] == '(' || exp[j] == ')') && br == 0)
                            {
                                end = j - 1;
                                break;
                            }
                        }
                        //Console.WriteLine(exp + ' ' + i + ' ' +  start + ' ' + end);
                        try
                        {
                            if (end == exp.Length - 1)
                            {
                                exp = exp.Substring(0, start) + '(' + exp.Substring(start, end - start + 1) + ')';
                            }
                            else
                            {
                                exp = exp.Substring(0, start) + '(' + exp.Substring(start, end - start + 1) + ')' + exp.Substring(end + 1);
                            }
                            i++;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Something went wrong. Please check your input and try again!");
                            return null;
                        }
                    }
                }
                return exp;
            }
            else
            {
                for (int i = 0; i < exp.Length; i++)
                {
                    if (exp[i] == '+' || exp[i] == '-')
                    {
                        int br = 0;
                        int start, end;
                        start = 0;
                        end = exp.Length - 1;
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (exp[j] == ')')
                            {
                                br++;
                            }
                            else if (exp[j] == '(')
                            {
                                br--;
                            }
                            if (issign(exp[j]) && br == 0)
                            {
                                start = j + 1;
                                break;
                            }
                            if ((exp[j] == '(' || exp[j] == ')') && br == 0)
                            {
                                start = j + 1;
                                break;
                            }
                        }
                        br = 0;
                        for (int j = i + 1; j < exp.Length; j++)
                        {
                            if (exp[j] == '(')
                            {
                                br++;
                            }
                            else if (exp[j] == ')')
                            {
                                br--;
                            }
                            if (issign(exp[j]) && br == 0)
                            {
                                end = j - 1;
                                break;
                            }
                            if ((exp[j] == '(' || exp[j] == ')') && br == 0)
                            {
                                end = j - 1;
                                break;
                            }
                        }
                        //Console.WriteLine(exp + ' ' + i + ' ' +  start + ' ' + end);
                        try
                        {
                            if (end == exp.Length - 1)
                            {
                                exp = exp.Substring(0, start) + '(' + exp.Substring(start, end - start + 1) + ')';
                            }
                            else
                            {
                                exp = exp.Substring(0, start) + '(' + exp.Substring(start, end - start + 1) + ')' + exp.Substring(end + 1);
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Something went wrong. Please check your input and try again!");
                            return null;
                        }
                        i++;
                    }
                }
                return exp;
            }
        }

        public static string calculator(string expression)
        {
            while (true)
            {
                bool bl = true;
                for (int i = 0; i < expression.Length; i++)
                {
                    if (issign(expression[i]))
                    {
                        if (!isbrck(expression[i - 1]) && !isbrck(expression[i + 1]))
                        {
                            bl = false;
                            int end, start;
                            start = 0;
                            end = expression.Length - 1;
                            string first, second;
                            first = second = string.Empty;
                            for (int j = i - 1; j >= 0; j--)
                            {
                                if (expression[j] == '(')
                                {
                                    start = j;
                                    try
                                    {
                                        first = expression.Substring(j + 1, i - 1 - j);
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("Something went wrong. Please check your input and try again!");
                                        return null;
                                    }
                                    break;
                                }
                                else if (issign(expression[j]))
                                {
                                    try
                                    {
                                        first = expression.Substring(j + 1, i - 1 - j);
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("Something went wrong. Please check your input and try again!");
                                        return null;
                                    }
                                    break;
                                }
                                else
                                {
                                    start = j;
                                }
                            }

                            for (int j = i + 1; j < expression.Length; j++)
                            {
                                if (expression[j] == ')')
                                {
                                    end = j;
                                    try
                                    {
                                        second = expression.Substring(i + 1, j - 1 - i);
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("Something went wrong. Please check your input and try again!");
                                        return null;
                                    }
                                    break;
                                }
                                else if (issign(expression[j]))
                                {
                                    try
                                    {
                                        second = expression.Substring(i + 1, j - 1 - i);
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("Something went wrong. Please check your input and try again!");
                                        return null;
                                    }
                                    break;
                                }
                                else
                                {
                                    end = j;
                                }
                            }

                            decimal a, b;
                            try
                            {
                                a = Convert.ToDecimal(first);
                                b = Convert.ToDecimal(second);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Something went wrong. Please check your input and try again!");
                                return null;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Something went wrong. Please check your input and try again!");
                                return null;
                            }
                            switch (expression[i])
                            {
                                case '+':
                                    a += b;
                                    break;
                                case '-':
                                    a -= b;
                                    break;
                                case '*':
                                    a *= b;
                                    break;
                                case '/':
                                    a /= b;
                                    break;
                            }
                            string result = a.ToString();
                            try
                            {
                                expression = expression.Substring(0, start) + result + expression.Substring(end + 1);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("Something went wrong. Please check your input and try again!");
                                return null;
                            }
                            break;
                        }
                    }
                }
                if (bl)
                    break;
            }
            return expression;
        }

        public static string halfcalc(string expression)
        {
            while (true)
            {
                bool bl = true;
                int br = 0;
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        br++;
                        bl = false;
                        for (int j = i + 1; j < expression.Length; j++)
                        {
                            if (expression[j] == '(')
                            {
                                br++;
                            }
                            else if (expression[j] == ')')
                            {
                                br--;
                                if (br == 0)
                                {
                                    string result;
                                    if (j == expression.Length - 1 && i == 0)
                                    {
                                        try
                                        {
                                            result = expression.Substring(1, expression.Length - 2);
                                        }
                                        catch (ArgumentOutOfRangeException)
                                        {
                                            Console.WriteLine("Something went wrong. Please check your input and try again!");
                                            return null;
                                        }
                                        result = calculator(bracketadder(bracketadder(result, true), false));
                                    }
                                    else
                                    {
                                        try
                                        {
                                            result = halfcalc(expression.Substring(i, j - i + 1));
                                        }
                                        catch (ArgumentOutOfRangeException)
                                        {
                                            Console.WriteLine("Something went wrong. Please check your input and try again!");
                                            return null;
                                        }
                                    }
                                    try
                                    {
                                        expression = expression.Substring(0, i) + result + expression.Substring(j + 1);
                                    }
                                    catch (ArgumentOutOfRangeException)
                                    {
                                        Console.WriteLine("Something went wrong. Please check your input and try again!");
                                        return null;
                                    }
                                    break;
                                }
                            }
                        }
                        break;

                    }
                }
                if (bl)
                    break;
            }
            return expression;

        }


        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please enter a valid mathematical expression using only numbers and these operators:\n+, -, *, /, (, )");
                string expression = Console.ReadLine();
                expression = halfcalc(expression);
                expression = bracketadder(bracketadder(expression, true), false);
                Console.WriteLine(calculator(expression));
            }
        }
    }
}
