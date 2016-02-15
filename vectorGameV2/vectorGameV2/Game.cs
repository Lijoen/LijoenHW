using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace vectorGameV2
{
    class Game
    {
        private int[,,] gameField;
        private string[] playerNames;
        private int numberOfPlayers;
        private int xDimensions;
        private int yDimensions;
        private int currentPlayerIndex = 0;
        private bool manualUnitsPlacement = false; 
        Random random;

        Unit[] ship;
        Unit[] cruizer;
        Unit[] submarine;


        private void SetNewManualCoordinates()
        {
            int attackerIndex; 

            for(int i = 0; i < numberOfPlayers; i++)
            {

                int a = 5;
                int b = 0;
                int z = a / b;

                if (i == numberOfPlayers - 1)
                {
                    attackerIndex = 0;
                }
                else
                    attackerIndex = i + 1;

                currentPlayerIndex = attackerIndex;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; 
                Console.WriteLine("\n\nPlayer " + playerNames[i] + " press any key to start Unit Placement. Your field will be attacked by " + playerNames[attackerIndex] + ".");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();

                Console.Clear();

                int xCoord = 0;
                int yCoord = 0; 
                
                
                while((xCoord > xDimensions) || (yCoord > yDimensions) || (xCoord < 1) || (yCoord < 1))
                {
                    Console.Clear();
                    DrawGrid(1);
                    Console.Write("\n\n" + playerNames[i] + ", place the Submarine (1 square)\n\nX coordinator: ");
                    xCoord = int.Parse(Console.ReadLine());
                    Console.Write("nY coordinator: ");
                    yCoord = int.Parse(Console.ReadLine());

                    if ((xCoord > xDimensions) || (yCoord > yDimensions) || (xCoord < 1) || (yCoord < 1))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nERROR: you tried to set the unit out of bounds... Press any key to try again");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine("\n");
                    submarine[attackerIndex].xyUnitPositions[xCoord - 1, yCoord - 1] = 1;
                    gameField[xCoord - 1, yCoord - 1, attackerIndex] = 1;
                    DrawGrid(1);
                    gameField[xCoord - 1, yCoord - 1, attackerIndex] = 0;

                    Console.WriteLine("\n\nSubmarine placed. Press any key to continue...");
                    Console.ReadKey();
                    
                }

                xCoord = 0;
                yCoord = 0; 

                while ((xCoord > xDimensions) || (yCoord > yDimensions) || (xCoord < 1) || (yCoord < 1))
                {
                    Console.Clear();
                    DrawGrid(1);
                    Console.Write("\n\n" + playerNames[i] + ", place the Submarine (1 ship)\n\nFirst X coordinator: ");
                    xCoord = int.Parse(Console.ReadLine());
                    Console.Write("nFirst Y coordinator: ");
                    yCoord = int.Parse(Console.ReadLine());

                    if ((xCoord > xDimensions) || (yCoord > yDimensions) || (xCoord < 1) || (yCoord < 1))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nERROR: you tried to set the unit out of bounds... Press any key to try again");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine("\n");
                    submarine[attackerIndex].xyUnitPositions[xCoord - 1, yCoord - 1] = 1;
                    gameField[xCoord - 1, yCoord - 1, attackerIndex] = 1;
                    DrawGrid(1);
                    gameField[xCoord - 1, yCoord - 1, attackerIndex] = 0;

                    xCoord = 0;
                    yCoord = 0; 

                    Console.WriteLine("\n\nSecond X coordinator: ");
                    xCoord = int.Parse(Console.ReadLine());
                    Console.Write("Second Y coordinator: ");
                    yCoord = int.Parse(Console.ReadLine());

                   // if(x, y too far)

                  //  else 
                        Console.WriteLine("\n\nSubmarine placed. Press any key to continue...");

                    Console.ReadKey();

                }




            }

        }

        /// <summary>
        /// Returns winning player number, or 0
        /// </summary>
        /// <returns></returns>
        private int CheckWinningConditions()
        {
            bool submarineSunk = false;
            int shipHits = 0;
            int cruizerHits = 0; 

            for(int i = 0; i < numberOfPlayers; i++)
            {
                for(int x = 0; x < xDimensions; x++)
                {

                    for(int y = 0; y < yDimensions; y++)
                    {
                        if ((submarine[i].xyUnitPositions[x, y] == 1))
                            submarineSunk = true;

                        else if ((ship[i].xyUnitPositions[x, y] == 1))
                            shipHits += 1;

                        else if ((cruizer[i].xyUnitPositions[x, y] == 1))
                            cruizerHits += 1;
                        
                    }

                }
                
                if ((submarineSunk = true && (shipHits == 2) && (cruizerHits == 3)))
                    return (i + 1);
            }


            return 0;
        }


        private void CreateUnits()
        {
            ship = new Unit[numberOfPlayers + 1];
            cruizer = new Unit[numberOfPlayers + 1];
            submarine = new Unit[numberOfPlayers + 1];

            for (int i = 0; i < this.numberOfPlayers; i++)
            {
                ship[i] = new Unit("ship", this.xDimensions, this.yDimensions, this.random);
                cruizer[i] = new Unit("cruizer", this.xDimensions, this.yDimensions, this.random);
                submarine[i] = new Unit("submarine", this.xDimensions, this.yDimensions, this.random);
            }

            if (!manualUnitsPlacement)
            { 
                for (int i = 0; i < this.numberOfPlayers; i++)
                {

                    ship[i].SetNewRandomCoordinates(this.xDimensions, this.yDimensions);
                    cruizer[i].SetNewRandomCoordinates(this.xDimensions, this.yDimensions);
                    submarine[i].SetNewRandomCoordinates(this.xDimensions, this.yDimensions);
                }
            }
            else
            {
                SetNewManualCoordinates();
            }
            
        }

        public void StartGame(int players, int xDim, int yDim, string[] nameOfPlayers, bool manualPlacementChoice, Random globalRandom)
        {

            this.numberOfPlayers = players;
            this.random = globalRandom;
            this.manualUnitsPlacement = manualPlacementChoice;

            playerNames = new string[numberOfPlayers];
            playerNames = nameOfPlayers;

            this.xDimensions = xDim;
            this.yDimensions = yDim;

            gameField = new int[this.xDimensions, this.yDimensions, numberOfPlayers];

            //checks for ambiguity in targets (ship overlapping cruizer etc)
            bool ambiguous = true;

            //Potentialy extreme complexity; possible stuck-in-loop hazard (depending in dimensions)
            //Should be no problem with dim > 5x5 and only a few units
            //What it does: it randomly guesses a solution and checks if its fine. Otherwise it tries again, none the wiser.
            CreateUnits();

            if(!manualPlacementChoice)
            { 
                while (ambiguous)
                {
                    ambiguous = false;
                    for (int k = 0; k < this.numberOfPlayers; k++)
                    {
                        for (int j = 0; j < this.yDimensions; j++)
                        {
                            for (int i = 0; i < this.xDimensions; i++)
                            {
                                if ((ship[k].xyUnitPositions[i, j] == 1) && (cruizer[k].xyUnitPositions[i, j]) == 1)
                                {
                                    ambiguous = true;
                                    CreateUnits();
                                }
                                else if ((ship[k].xyUnitPositions[i, j] == 1) && (submarine[k].xyUnitPositions[i, j]) == 1)
                                {
                                    ambiguous = true;
                                    CreateUnits();
                                }
                                else if ((submarine[k].xyUnitPositions[i, j] == 1) && (cruizer[k].xyUnitPositions[i, j]) == 1)
                                {
                                    ambiguous = true;
                                    CreateUnits();
                                }
                            }

                        }

                    }
                
                }

            }
            Console.Clear();


            //Random player starting
            currentPlayerIndex = random.Next(0, numberOfPlayers);
            bool gameRunning = true; 

            while(gameRunning)
            {
                Console.Clear();
                DrawGrid();
                DrawMenu();

                int menuInput = 0;
                while(menuInput == 0)
                { 
                    try
                    { 
                        menuInput = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }

                if (menuInput == 2) { return; }
                else if (menuInput == 1)
                {
                    int xCordBomb = 0;
                    int yCordBomb = 0; 

                    while((xCordBomb == 0) || (yCordBomb == 0))
                    {
                        try
                        {  
                            Console.Write("\nX cord: ");
                            xCordBomb = int.Parse(Console.ReadLine());
                            Console.Write("Y cord: ");
                            yCordBomb = int.Parse(Console.ReadLine());
                        }
                        catch (Exception e) { Console.WriteLine("\nError: " + e.Message + "\n\n"); }
                    }

                    Bomb(xCordBomb, yCordBomb);

                    Console.WriteLine("\n Updated field: ");
                    this.DrawGrid();

                    Console.WriteLine("\n\nPress any key...");
                    Console.ReadKey();

                    int winner = CheckWinningConditions();

                    if (winner != 0)
                    { 
                        Console.WriteLine("Player " + playerNames[currentPlayerIndex] + " won!");
                        Console.ReadKey();
                    }
                    NextPlayer();
                }
                else if(menuInput == 42)
                {
                    //TEST VERSION - DISPLAY SOLUTION 

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int i = 0; i < this.xDimensions; i++)
                    {
                        for(int j = 0; j < this.yDimensions; j++)
                        {
                            if (ship[currentPlayerIndex].xyUnitPositions[i, j] == 1)
                                Console.WriteLine("Ship at positions: X = " + (i + 1) + " Y = " + (j + 1));

                            if (cruizer[currentPlayerIndex].xyUnitPositions[i, j] == 1)
                                Console.WriteLine("Cruizer at positions: X = " + (i + 1) + " Y = " + (j + 1));

                            if (submarine[currentPlayerIndex].xyUnitPositions[i, j] == 1)
                                Console.WriteLine("Submarine at position: X = " + (i + 1) + " Y = " + (j + 1));
                        }
                        
                    }

                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\n Press any key to return ... ");
                    Console.ReadKey();
                        


                }

            }

            Console.ReadKey();
           
        }

        private void NextPlayer()
        {
            if (currentPlayerIndex == (this.numberOfPlayers - 1))
                currentPlayerIndex = 0;
            else
                currentPlayerIndex += 1;
            

        }

        private void DrawMenu()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\bTest option: type 42 to get the targets.");
            
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nIt is player " + playerNames[currentPlayerIndex] + " turn\n");
            Console.WriteLine("[1] Bomb ");
            Console.WriteLine("[2] Exit ");
            Console.Write("Input: ");

        }

        /// <summary>
        /// Draws the playing field 
        /// </summary>
        public void DrawGrid(int moveIndex = 0)
        {
            if(moveIndex == 1)
            {
                if (currentPlayerIndex == numberOfPlayers - 1)
                    currentPlayerIndex = 0;
                else
                    currentPlayerIndex += 1;

            }

            if (currentPlayerIndex == 0)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (currentPlayerIndex == 1)
                Console.ForegroundColor = ConsoleColor.Cyan;
            else if (currentPlayerIndex == 2)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            else if (currentPlayerIndex == 2)
                Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write("   ");
            for (int x = 0; x < this.xDimensions; x++)
            {
                if(x < 9)
                    Console.Write("| " + (x+1) + " ");
                else
                    Console.Write("| " + (x + 1) + ""); 
            }
            Console.WriteLine("| [X]");

            for (int y = 0; y < this.yDimensions; y++)
            {
                if(y < 9) 
                    Console.Write("|" + (y + 1) + "|");
                else
                    Console.Write("|" + (y + 1) + "");


                for (int x = 0; x < this.xDimensions; x++)
                {
                    if (gameField[x, y, this.currentPlayerIndex] == 1)
                    {
                        if (ship[currentPlayerIndex].xyUnitPositions[x, y] == 1)
                            Console.Write("|SHP");
                        else if (cruizer[currentPlayerIndex].xyUnitPositions[x, y] == 1)
                            Console.Write("|CRZ");
                        else if (submarine[currentPlayerIndex].xyUnitPositions[x, y] == 1)
                            Console.Write("|SUB");
                        else
                            Console.Write("|***");
                    }

                    else
                        Console.Write("|___");
                }

                Console.Write("|" + Environment.NewLine);
                
            }
            Console.Write("[Y]");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public bool Bomb(int x, int y)
        {
            
            try
            {
                //We are bombing this square: set it to 1
                this.gameField[x - 1, y - 1, currentPlayerIndex] = 1;

                //if we hit submarine
                if (this.submarine[currentPlayerIndex].XyUnitPositions[x - 1, y - 1] == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hit submarine!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();

                    return true;
                }
                //if we hit ship
                else if (this.ship[currentPlayerIndex].XyUnitPositions[x - 1, y - 1] == 1)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hit ship!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();

                    return true;
                }
                //if we hit cruizer 
                else if (this.cruizer[currentPlayerIndex].XyUnitPositions[x - 1, y - 1] == 1)
                {

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hit cruizer!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey();

                    return true;  
                }

              
                return false;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + Environment.NewLine + "I.E. You screwed up.");
                Console.ReadKey();
                return false;
            }
        }

    }
}
