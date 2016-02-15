using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vectorGameV2
{
    class Program
    {
        static void Main(string[] args)
        {
            int xDim = 5;
            int yDim = 5;
            string[] playerNames;
            int numberOfPlayers = 1;
            int mainMenuChoice = 0;
            bool manualPlacement = false; 

            Random random = new Random();

            bool mainMenuRunning = true;
            while (mainMenuRunning)
            { 
                while ((mainMenuChoice != 1) && (mainMenuChoice != 2) && (mainMenuChoice != 3))
                {
                    Console.Clear();

                    try
                    {
                        Console.WindowHeight = 45;
                        Console.WindowWidth = 180;
                    }
                    catch (Exception e) { Console.WriteLine("Warning: " + e.Message + "\n"); }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Welcome To vector Bomb Game");
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("[1] New Game \n[2] Settings\n[3] Exit");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n\nInput: ");

                    try { mainMenuChoice = int.Parse(Console.ReadLine()); }
                    catch (Exception e)
                    {
                        Console.WriteLine(("Error: " + e.Message));
                    }

                    if ((mainMenuChoice != 1) && (mainMenuChoice != 2) && (mainMenuChoice != 3))
                        Console.WriteLine("Invalid Choice, try again\n");
                }


                switch (mainMenuChoice)
                {
                    case 1:
                        Game vectorGame = new Game();

                        Console.Write("Choose players [1-4]: ");
                        try
                        {
                            numberOfPlayers = int.Parse(Console.ReadLine());
                        } catch
                        {
                            numberOfPlayers = 1;
                        }

                        playerNames = new string[numberOfPlayers];

                        for (int i = 0; i < numberOfPlayers; i++)
                        {

                            Console.Write("Write name of player " + (i + 1) + ": ");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            playerNames[i] = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        vectorGame.StartGame(numberOfPlayers, xDim, yDim, playerNames, manualPlacement, random);

                        mainMenuChoice = 0;

                        break;

                    case 2:

                        int settingsChoice = 0;
                        while ((settingsChoice != 1) && (settingsChoice != 2))
                        {

                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("\n    SETTINGS \n\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("[1] Change Dimension\n[2] Other Options");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            try { settingsChoice = int.Parse(Console.ReadLine()); }
                            catch (Exception e) { Console.WriteLine("Error: " + e.Message); }

                            if ((settingsChoice != 1) && (settingsChoice != 2))
                            {
                                Console.WriteLine("\n\n Unknown choice... Press any key to try again");
                                Console.ReadKey();
                            }

                        }

                        if (settingsChoice == 1)
                        {

                            xDim = 0;
                            yDim = 0;

                            while (xDim <= 3 || yDim <= 3)
                            {
                                Console.Clear();
                                Console.WriteLine("\n   Change Dimensions\n\n");

                                try
                                {  
                                    Console.Write("xDimensions (minimum: 3): ");
                                    xDim = int.Parse(Console.ReadLine());
                                    Console.Write("yDimensions (minimum: 3): ");
                                    yDim = int.Parse(Console.ReadLine());

                                    if (xDim <= 3 || yDim <= 3)
                                    { 
                                        Console.WriteLine("\nValues not legal (matrix needs to be at least 3x3");
                                        Console.WriteLine("\nPress any key to try again...");
                                        Console.ReadKey();
                                    }

                                }
                                catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
                            }

                            Console.WriteLine("\nDimensions changed to " + yDim + "x" + xDim + ".\n\nPress any key to return...\n");
                            Console.ReadKey();
                        }
                        else if(settingsChoice == 2)
                        {
                            string placeMentChoice = "";

                            Console.Clear();
                            while ((placeMentChoice != "R") && (placeMentChoice != "M"))
                            {

                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("\n   Other Options\n\n");
                                Console.ForegroundColor = ConsoleColor.White;

                                if((placeMentChoice != "R") && (placeMentChoice != "M") && (placeMentChoice != ""))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nInvalid Choice, try again");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                Console.WriteLine("[R]andom Boat Placement [default]\n[M]anuall Boat Placement");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("Input: ");

                                placeMentChoice = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;

                                if ((placeMentChoice != "R") && (placeMentChoice != "M"))
                                {
                                    Console.Clear();
                                    
                                }
                                else if (placeMentChoice == "R")
                                {
                                    Console.WriteLine("\n\n Random Placement of boats set\n\nPress any key to return..");
                                    Console.ReadKey();

                                    manualPlacement = false;

                                }
                                else if (placeMentChoice == "M")
                                {
                                    Console.WriteLine("\n\n Manual Placement of boats set\n\nPress any key to return..");
                                    Console.ReadKey();

                                    manualPlacement = true;
                                }
                            }

                        }

                        mainMenuChoice = 0;
                        break;
                        
                    case 3:
                        return;
                }
            }
        }
    }
}
