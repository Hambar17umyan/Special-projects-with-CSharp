using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Battleship
{
    internal class Program
    {
        //Every method has its own describtion. Hover the mouse on its call to see.
        static void Main(string[] args)
        {
            //variables
            char[,] Own1, Own2, Enemy1, Enemy2;
            int[] Ships1, Ships2;
            int[,] MotherShip1, MotherShip2;
            int ShipCount1, ShipCount2;
            string Name1, Name2;

            Own1 = new char[10, 10];    //Real open field of the Player1
            Own2 = new char[10, 10];    //Real open field of the Player2
            Enemy1 = new char[10, 10];  //The components of "Own2", which have been detected by Player1 
            Enemy2 = new char[10, 10];  //The components of "Own1", which have been detected by Player2
            ShipCount1 = 10;    //Total number of ships at the beginning
            ShipCount2 = 10;    //Total number of ships at the beginning


            Ships1 = new int[10];   //Defining the ships placed by Player1 and enumerating them
            Ships2 = new int[10];   //Defining the ships placed by Player2 and enumerating them

            MotherShip1 = new int[10, 10];  
            /*
             * If there is a ship on the (x, y) coordinates of the field of Player1, 
             * then [x, y] contains the number of the ship which is covering the cell.
             * (The numbers of the ships are stored in the array Ships1)
            */
            MotherShip2 = new int[10, 10];
            /*
             * If there is a ship on the (x, y) coordinates of the field of Player2, 
             * then [x, y] contains the number of the ship which is covering the cell.
             * (The numbers of the ships are stored in the array Ships2)
            */

            InitailField(ref Own1);
            InitailField(ref Own2);
            InitailField(ref Enemy1);
            InitailField(ref Enemy2);

            Name1 = Name2 = "";

            Pilot(ref Name1, ref Name2);
            Console.Clear();

            input(Name1, ref Own1, true, ref Ships1, ref MotherShip1);
            Console.Clear();
            Console.WriteLine("Press any key to turn to the next player!");
            Console.ReadKey();
            Console.Clear();

            for (int i = 5; i > 0; i--) //Wait 5 seconds
            {
                Console.WriteLine("Turning in " + i);
                Thread.Sleep(1000);
                Console.Clear();
            }

            input(Name2, ref Own2, false, ref Ships2, ref MotherShip2);
            Console.Clear();

            for (int i = 5; i > 0; i--)//Wait 5 seconds
            {
                Console.WriteLine("The game starts in " + i);
                Thread.Sleep(1000);
                Console.Clear();
            }

            while (true)    //Implement the queries. The loop breaks when someone wins
            {
                ShipCount1 -= query(Name1, Name2, true, ref Own1, ref Enemy1, ref Own2, ref MotherShip2, ref Ships2, ShipCount1);
                //Counts the number of remaining ships that have not been completely destroyed and implements a query

                if (ShipCount1 == 0)    //If there are no such, the program breaks and the winner is being announced
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("GAME OVER");
                    Console.WriteLine($"Congratulations {Name1}! Your fire will always be ignited inside the walls of the warground!");
                    return;
                }

                ShipCount2 -= query(Name2, Name1, false, ref Own2, ref Enemy2, ref Own1, ref MotherShip1, ref Ships1, ShipCount2);
                //Counts the number of remaining ships that have not been completely destroyed and implements a query

                if (ShipCount2 == 0) //If there are no such, the program breaks and the winner is being announced
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("GAME OVER");
                    Console.WriteLine($"Congratulations {Name2}! Your freezing breath will always blow in the mountains of the warground!");
                    return;
                }
            }
        }
        /// <summary>
        ///  Implements the query of a player about the enemy field 
        /// </summary>
        static int query(string Name_own, string Name_enemy, bool isScorpion, ref char[,] own, ref char[,] enemy, ref char[,] ActualEnemy, ref int[,] MotherShip, ref int[] Ships, int shipcount)
        {
            int ResultedBenefit = 0;
            while (true)
            {
                if (shipcount == 0)
                {
                    return ResultedBenefit;
                }
                Console.WriteLine(Name_own + " It's your turn!");
                Console.WriteLine();
                Console.WriteLine("Your field:");
                OutputTheField(own, isScorpion);
                Console.WriteLine($"{Name_enemy}'s field:");
                OutputTheField(enemy, !isScorpion);
                Console.WriteLine();
                Console.WriteLine("Now enter the coordinates you want to fire! The format is \"x y\" at the same line.");
                try
                {
                    string[] inp = new string[2];
                    inp = Console.ReadLine().Split(' ');
                    int x = int.Parse(inp[0]);
                    int y = int.Parse(inp[1]);

                    if (x >= 10 || y >= 10 || x < 0 || y < 0)
                    {
                        Console.WriteLine("Invalid input. Be careful, please!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }
                    byte CanBomb = BombTheCell(x, y, ref enemy, ref ActualEnemy);
                    if (CanBomb == 0)
                    {
                        Console.WriteLine("Failed attempt. Try better next time!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        for (int i = 5; i > 0; i--)
                        {
                            Console.WriteLine("The next player plays in " + i);
                            Thread.Sleep(1000);
                            Console.Clear();
                        }
                        break;
                    }
                    else if (CanBomb == 1)
                    {
                        Ships[MotherShip[x, y]]--;
                        if (Ships[MotherShip[x, y]] == 0)
                        {
                            ResultedBenefit++;
                            shipcount--;
                            Render(x, y, ref ActualEnemy, ref enemy);
                        }
                        Console.WriteLine("Boom. You have bombed a cell. Try again now!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }
                    else if (CanBomb == 2)
                    {
                        Console.WriteLine("You have already attempted that cell!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input. Be careful, please!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }
            }
            return ResultedBenefit;
        }
        /// <summary>
        ///  Gets an input about the ordinal ship
        /// </summary>
        static void input(string Name, ref char[,] own, bool isScorpion, ref int[] Ships, ref int[,] MotherShip)
        {
            int ship = 0;
            for (int i = 1; i < 5; i++)
            {
                for (int j = 4 - i; j >= 0; j--, ship++)
                {
                    Console.WriteLine("This is your table.");
                    Console.WriteLine();
                    OutputTheField(own, isScorpion);
                    Console.WriteLine();
                    Console.WriteLine($"{Name}, from where would you like to put a ship of length {i}.\nYou will have {j} x {i}-sized ships left.");
                    Console.WriteLine();
                    Console.WriteLine("Please, provide your input in following format:\nRow\nColumn\n'h'(if it is put horizontally) or 'v'(if it is put vertically)");
                    try
                    {
                        int x, y;
                        bool b;
                        x = int.Parse(Console.ReadLine());
                        y = int.Parse(Console.ReadLine());
                        b = char.Parse(Console.ReadLine().ToLower()) == 'h';
                        if (!CheckIfValid(x, y, b, i, own))
                        {
                            Console.WriteLine("Invalid input!. Please check the requirements again and renew your input!");
                            j++;
                            ship--;
                            Thread.Sleep(2000);

                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            PlaceShip(x, y, i, b, ref own);
                            Ships[ship] = i;
                            MotherShip[x, y] = ship;
                            if (b)
                            {
                                for (int i1 = y; i1 < y + i; i1++)
                                {
                                    MotherShip[x, i1] = ship;
                                }
                            }
                            else
                            {
                                for (int i1 = x; i1 < x + i; i1++)
                                {
                                    MotherShip[i1, y] = ship;
                                }
                            }
                            Console.Clear();
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input!. Please check the requirements again and renew your input!");
                        j++;
                        ship--;
                        Thread.Sleep(2000);

                        Console.Clear();
                        continue;
                    }
                }
            }
        }
        /// <summary>
        ///  Called on the cell [x, y], when the ship placed on it has fully been bombarded. Renders the neighboring and containing cells with correct characters.
        /// </summary>
        static void Render(int x, int y, ref char[,] Bombed, ref char[,] Terrorist)
        {
            int x1, y1;
            x1 = y1 = -1;
            Bombed[x, y] = '$';
            Terrorist[x, y] = '$';
            if (x > 0)
            {
                if (Bombed[x - 1, y] == 'X')
                {
                    x1 = x - 1;
                    y1 = y;
                }
                else if (Bombed[x - 1, y] == '$')
                {

                }
                else
                {
                    Terrorist[x - 1, y] = '.';
                    Bombed[x - 1, y] = '.';
                }
            }
            if (x < 9)
            {
                if (Bombed[x + 1, y] == 'X')
                {
                    x1 = x + 1;
                    y1 = y;
                }
                else if (Bombed[x + 1, y] == '$')
                {

                }
                else
                {
                    Terrorist[x + 1, y] = '.';
                    Bombed[x + 1, y] = '.';
                }
            }
            if (y > 0)
            {
                if (Bombed[x, y - 1] == 'X')
                {
                    x1 = x;
                    y1 = y - 1;
                }
                else if (Bombed[x, y - 1] == '$')
                {

                }
                else
                {
                    Terrorist[x, y - 1] = '.';
                    Bombed[x, y - 1] = '.';
                }
            }
            if (y < 9)
            {
                if (Bombed[x, y + 1] == 'X')
                {
                    x1 = x;
                    y1 = y + 1;
                }
                else if (Bombed[x, y + 1] == '$')
                {

                }
                else
                {
                    Terrorist[x, y + 1] = '.';
                    Bombed[x, y + 1] = '.';
                }
            }
            if (x < 9 && y < 9)
            {
                if (Bombed[x + 1, y + 1] == 'X')
                {
                    x1 = x + 1;
                    y1 = y + 1;
                }
                else if (Bombed[x + 1, y + 1] == '$')
                {

                }
                else
                {
                    Terrorist[x + 1, y + 1] = '.';
                    Bombed[x + 1, y + 1] = '.';
                }
            }
            if (x > 0 && y > 0)
            {
                if (Bombed[x - 1, y - 1] == 'X')
                {
                    x1 = x - 1;
                    y1 = y - 1;
                }
                else if (Bombed[x - 1, y - 1] == '$')
                {

                }
                else
                {
                    Terrorist[x - 1, y - 1] = '.';
                    Bombed[x - 1, y - 1] = '.';
                }
            }
            if (x < 9 && y > 0)
            {
                if (Bombed[x + 1, y - 1] == 'X')
                {
                    x1 = x + 1;
                    y1 = y - 1;
                }
                else if (Bombed[x + 1, y - 1] == '$')
                {

                }
                else
                {
                    Terrorist[x + 1, y - 1] = '.';
                    Bombed[x + 1, y - 1] = '.';
                }
            }
            if (x > 0 && y < 9)
            {
                if (Bombed[x - 1, y + 1] == 'X')
                {
                    x1 = x - 1;
                    y1 = y + 1;
                }
                else if (Bombed[x - 1, y + 1] == '$')
                {

                }
                else
                {
                    Terrorist[x - 1, y + 1] = '.';
                    Bombed[x - 1, y + 1] = '.';
                }
            }
            if (x1 != -1 && y1 != -1)
                Render(x1, y1, ref Bombed, ref Terrorist);
        }

        /// <summary>
        ///  Tries to bombard the cell [x, y] and returns an integer(0` Faile, 1` Success, 2` Invalid)
        /// </summary>
        static byte BombTheCell(int x, int y, ref char[,] Enemy, ref char[,] ActualEnemy)
        {
            //Actual Enemy -> Own_myus
            //Enemy -> Enemy_qo
            if (ActualEnemy[x, y] == '#')//Has a ship there
            {
                Enemy[x, y] = '#';
                ActualEnemy[x, y] = 'X';
                return 1;
            }
            else if (ActualEnemy[x, y] == '$' || ActualEnemy[x, y] == '.' || ActualEnemy[x, y] == 'X')//Already attempted cell
            {
                return 2;
            }
            else //if (ActualEnemy[x, y] == '■')
            {
                ActualEnemy[x, y] = '.';
                Enemy[x, y] = '.';
                return 0;
            }
        }
        /// <summary>
        ///  Puts a ship by describtion starting in the cell [x, y]
        /// </summary>
        static void PlaceShip(int x, int y, int length, bool horizonthal, ref char[,] field)
        {
            if (horizonthal)
            {
                for (int i = y; i < y + length; i++)
                {
                    field[x, i] = '#';
                }
            }
            else
            {
                for (int i = x; i < x + length; i++)
                {
                    field[i, y] = '#';
                }
            }
        }

        /// <summary>
        ///  Prints the field dependant on the Player's number(isScorpion means it's the first player)
        /// </summary>
        static void OutputTheField(char[,] field, bool isScorpion)
        {
            Console.Write(' ');
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i);
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] != '#' && field[i, j] != 'X' && field[i, j] != '$')
                        Console.Write(field[i, j]);
                    else
                    {
                        if (isScorpion)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write(field[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write(field[i, j]);
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        ///  Introduction to the players and the rules of Battleship
        /// </summary>
        static void Pilot(ref string a, ref string b)
        {
            Console.WriteLine("\r\n██████╗░░█████╗░████████╗████████╗██╗░░░░░███████╗░██████╗██╗░░██╗██╗██████╗░\r\n██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██║░░░░░██╔════╝██╔════╝██║░░██║██║██╔══██╗\r\n██████╦╝███████║░░░██║░░░░░░██║░░░██║░░░░░█████╗░░╚█████╗░███████║██║██████╔╝\r\n██╔══██╗██╔══██║░░░██║░░░░░░██║░░░██║░░░░░██╔══╝░░░╚═══██╗██╔══██║██║██╔═══╝░\r\n██████╦╝██║░░██║░░░██║░░░░░░██║░░░███████╗███████╗██████╔╝██║░░██║██║██║░░░░░\r\n╚═════╝░╚═╝░░╚═╝░░░╚═╝░░░░░░╚═╝░░░╚══════╝╚══════╝╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░░░░");
            Thread.Sleep(4000);
            Console.Clear();
            Thread.Sleep(2000);
            while (true)
            {
                Console.WriteLine("Enter the first player's name:");
                a = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Enter the second player's name:");
                b = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Be sure everything is correct and press any key except Enter! Otherwise you can press Enter to begin again.");
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ok let us try again. Be careful this time");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(2000);
                    Console.Clear();
                }
                else
                {
                    break;
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome players! This is the war environment and that means that there is no way home. Let us introduce you with your characters.");
            Console.WriteLine();
            Console.WriteLine("Press some button to do so!");
            Console.ReadKey();

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Player 1: " + a);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"You have been chosen as Scorpion, the master of fire and vengeance.\nIn this naval warfare, your superpower allows you to unleash devastating fiery attacks that can incinerate enemy ships from a distance.\nYour presence strikes fear into the hearts of your opponents, as your flames can engulf and destroy anything in their path.\nCommand your fleet with the fury of a thousand suns and dominate the seas with your unmatched power!");
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("Press some button to go on!");
            Console.ReadKey();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Player 2: " + b);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine($"You have been chosen as Sub-Zero, the legendary cryomancer with the ability to control ice.\nIn this epic ship battle, your superpower grants you the ability to freeze enemy ships in their tracks, rendering them immobile and vulnerable to your fleet's assault.\nYour icy touch can halt even the most formidable foes, creating opportunities for strategic strikes.\nLead your fleet with the cool precision of a master tactician and conquer the seas with your formidable icy prowess!");
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine("Press some button to go on!");
            Console.ReadKey();

            Console.Clear();
            Console.WriteLine("Let's design our ships.");
            Console.WriteLine("To do that you'll need to learn few rules.");
            Console.WriteLine();
            Thread.Sleep(1000);
            Console.WriteLine("1.The ships of a player should not have a common border or vertices.");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("2.Each player should have:\n4 x 1-size ships\n3 x 2-size ships\n2 x 3-size ships\nA single 4-sized ship.");
            Thread.Sleep(1000);
            Console.WriteLine();
            Console.WriteLine("Press some button to go on!");
            Console.ReadKey();
            Console.WriteLine("Let's start.");
        }

        /// <summary>
        ///  Checks if the ship put on the coordinates (x, y) is valid dependant on the field
        /// </summary>
        static bool CheckIfValid(int x, int y, bool horizonthal, int length, char[,] own)
        {
            if (x < 0 || x >= 10 || y < 0 || y >= 10)
            {
                return false;
            }
            if (horizonthal)
            {
                for (int i = y; i < y + length; i++)
                {
                    if (i >= 10)
                        return false;
                    if (!CheckCellIfValid(x, i, own))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = x; i < x + length; i++)
                {
                    if (i >= 10)
                        return false;
                    if (!CheckCellIfValid(i, y, own))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        ///  Checks if the cell can be filled with a chip component
        /// </summary>
        static bool CheckCellIfValid(int x, int y, char[,] own)
        {
            if (x > 0)
            {
                if (own[x - 1, y] == '#')
                {
                    return false;
                }
                if (y > 0)
                {
                    if (own[x - 1, y - 1] == '#')
                    {
                        return false;
                    }
                    if (own[x, y - 1] == '#')
                    {
                        return false;
                    }
                }
                if (y < 9)
                {
                    if (own[x - 1, y + 1] == '#')
                    {
                        return false;
                    }
                    if (own[x, y + 1] == '#')
                    {
                        return false;
                    }
                }
            }
            if (x < 9)
            {
                if (own[x + 1, y] == '#')
                {
                    return false;
                }
                if (y > 0)
                {
                    if (own[x + 1, y - 1] == '#')
                    {
                        return false;
                    }
                    if (own[x, y - 1] == '#')
                    {
                        return false;
                    }
                }
                if (y < 9)
                {
                    if (own[x + 1, y + 1] == '#')
                    {
                        return false;
                    }
                    if (own[x, y + 1] == '#')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        ///  Assigns to all elements of the array the value '■' 
        /// </summary>
        static void InitailField(ref char[,] field)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    field[i, j] = '■';
                }
            }
        }

    }
}
