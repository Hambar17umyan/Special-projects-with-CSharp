using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Student_registration
{
    internal class Program
    {
        public static bool check_key(out string? s)//inputs a string from console and also keeps track on pressed keys
        {
            StringBuilder sb = new StringBuilder();
            s = "";
            while (true)
            {
                var a = Console.ReadKey(intercept: true);//gets a key information pressed by the user
                                                         //intercept: true -> means that the keys pressed by the user won't be visiable on the console
                                                         //The data typed by the user is represented on the console with further steps(line 38)

                if (a.Key == ConsoleKey.Enter)//if the user presses the enter button
                {
                    Console.WriteLine();//type a '\n' on the console
                    break;//this means that the data typed by the user is now finished
                }

                if (a.KeyChar >= 32 && a.KeyChar <= 126)//if the key pressed by the user is typeable
                {
                    sb.Append(a.KeyChar);//add the character to our string that the user wants to write
                    Console.Write(a.KeyChar);//Print the char symbol of that key
                }
                else if (a.Key == ConsoleKey.Backspace && sb.Length > 0)//if the user tries to change something and presses backspace
                {
                    sb.Remove(sb.Length - 1, 1);//delete the last symbol from builder, as the user wants it to be removed
                    Console.Write("\b \b");//replace the cursor on digit left and write space instead of old character, then place the cursor back again to obtain further data
                }
                else if (a.Key == ConsoleKey.F5)//if the user presses F5
                {
                    //making sure that the user did not press the key by accident
                    Console.Beep();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nDo you want to quit? Type yes if you want. Any other input will be considered as \"no\"");
                    Console.ForegroundColor = ConsoleColor.White;

                    //getting the answer
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "yes")//the word "yes" can be represented by any register. For example "yEs" "YEs"...
                    {
                        return false;
                        //inform, that the function was interrupted by the user and the data collected in this cycle is incomplete
                    }
                    else
                    {
                        sb.Clear();//rewrite the information about the student as the work was interrupted by a special key F5
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Ok I won't quit. Please retype the last information.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            s = sb.ToString();//changing the value of the argument to the resulting string to obtain the data given by the user
            return true;//informing that the user still wants to continue the work
        }
        static void Main(string[] args)
        {
            string line = new string(' ', 30); //For later separation

            Console.WriteLine("How many students do you want to register?");
            int numberOfStudents = Convert.ToInt32(Console.ReadLine());

            //For saving the data
            string?[] names = new string[numberOfStudents];
            string?[] professions = new string[numberOfStudents];
            int[] ages = new int[numberOfStudents];

            bool b;

            for (int i = 0; i < numberOfStudents; i++)
            {
                try
                {
                    string? name, profession;
                    int age;
                    string ag; //for the reading of age as a string

                    Console.WriteLine("Enter your name:");
                    b = check_key(out name);

                    if (!b)//if the user asks for the program to be over
                    {
                        numberOfStudents = i; //set a new number of students, which is the real number of registered students before program break
                        Console.WriteLine("I have stopped the program.");
                        break;
                    }

                    Console.WriteLine("Enter your age:");
                    b = check_key(out ag);
                    if (!b)//if the user asks for the program to be over
                    {
                        numberOfStudents = i; //set a new number of students, which is the real number of registered students before program break
                        Console.WriteLine("I have stopped the program.");
                        break;
                    }
                    age = int.Parse(ag);

                    Console.WriteLine("Enter your profession:");
                    b = check_key(out profession);
                    if (!b)//if the user asks for the program to be over
                    {
                        numberOfStudents = i; //set a new number of students, which is the real number of registered students before program break
                        Console.WriteLine("I have stopped the program.");
                        break;
                    }

                    //data storation
                    names[i] = name;
                    professions[i] = profession;
                    ages[i] = age;


                    Console.WriteLine($"Success registered Student! Name:{name}, Age: {age},  Profession:{profession}.");

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Press F5 to stop the program anytime!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine(line);//separation
                }
                catch //Conversion error
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine("Something went wrong. Please try to reregister this student again!");
                    Console.BackgroundColor = ConsoleColor.Black;
                    i--; //Reiteration
                }
            }

            if (numberOfStudents == 0)
            {
                Console.WriteLine("You have registered 0 students");
            }
            else
            {
                Console.WriteLine($"You have registered {numberOfStudents} students. Here are they:");
            }

            //data demonstration
            for (int i = 0; i < numberOfStudents; i++)
            {
                Console.WriteLine($"Student N{i + 1}");
                Console.WriteLine($"Name: {names[i]}");
                Console.WriteLine($"Name: {ages[i]}");
                Console.WriteLine($"Name: {professions[i]}");
                Console.WriteLine(line);
            }
        }
    }
}
