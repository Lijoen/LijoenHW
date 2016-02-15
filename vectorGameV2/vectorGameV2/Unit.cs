using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vectorGameV2
{
    class Unit
    {
        public int[,] xyUnitPositions;
        private int xDimensions;
        private int yDimensions;
        private string type;
        private int unitSize;
        private int globalTestCounter = 0;
        private Random random; 
        
        public Unit(string newType, int xDim, int yDim, Random globalRandom)
        {
            this.type = newType;
            this.xDimensions = xDim;
            this.yDimensions = yDim;

            this.random = globalRandom;

            if (this.type.ToLower() == "submarine")
                this.unitSize = 1;
            else if (this.type.ToLower() == "ship")
                this.unitSize = 2;
            else if (this.type.ToLower() == "cruizer")
                this.unitSize = 3;

            xyUnitPositions = new int[this.xDimensions, this.yDimensions];
            
            /// LOGIC: each unit holds a playing field. Target is located at 1s.
            for(int i = 0; i < this.xDimensions; i++)
            {
                for(int j = 0; j < this.yDimensions; j++)
                {
                    xyUnitPositions[i, j] = 0;
                }
            }

        }


        /// <summary>
        /// Only for testing
        /// </summary>
        public void GetCoordinates()
        {
           
            // NOTE : SQRT(Length) would be valid for symmetric gamefield
            for(int i = 0; i < this.xDimensions ; i++)
            {
                for(int j = 0; j < this.yDimensions ; j++)
                {
                    if(xyUnitPositions[i, j] == 1)
                        Console.WriteLine("Mark at position: X = " + (i+1) + " Y = " + (j+1) );
                    
                }
            }
        }

        
        public void SetNewRandomCoordinates(int gridDimensionsX, int gridDimensionsY)
        {
            Console.WriteLine("Loading game . . . .");
            globalTestCounter++;

            Console.WriteLine(globalTestCounter);

            /// FIND RANDOM LEGAL COORDINATES
            /// 
            /// Obviously its not fair to have units out of bounds 
            /// When dealing with bigger units this is a bit tricky if we want to allow units in all random directions
            /// Below is a very "easy" but code heavy approach:
            /// The size  of the vector (unitSize) is always given. We need to decide: 1) Direction of vector and 2) Starting point of vector. Then we have an exact vector given.
            /// 

            //First decide if we are going up, down or diagonaly
            string direction = "";
            int result = random.Next(1, 9);

            if (result == 1)
                direction = "UP";
            else if (result == 2)
                direction = "DOWN";
            else if (result == 3)
                direction = "LEFT";
            else if (result == 4)
                direction = "RIGHT";
            else if (result == 5)
                direction = "LEFTDOWN";
            else if (result == 6)
                direction = "RIGHTDOWN";
            else if (result == 7)
                direction = "LEFTUP";
            else if (result == 8)
                direction = "RIGHTUP";

            //Starting coordinations
            int firstCordX = 0; 
            int firstCordY = 0;
            
            /// GAMEFIELD definition
            ///   X1  X2  X3  X4 ►
            /// Y1
            /// Y2
            /// Y3
            /// Y4
            /// ▼

            if (direction == "")
            {
                Console.WriteLine("Something went horribly wrong in declaration of direction...");
                return;
            }
            else if (direction == "UP")
            {
                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3  X4 ►
                // Y1 (3)
                // Y2 (2)
                // Y3 (1)*
                // Y4
                // ▼

                
                    
                //Note: the "+1" is becuase of how the random.Next function behaves 
                firstCordY = random.Next(unitSize, gridDimensionsY + 1); // *
                firstCordX = random.Next(1, gridDimensionsX + 1); //"Dont cares" (axis locked)

                for (int i = firstCordY; i > (firstCordY - unitSize); i--)
                    this.xyUnitPositions[firstCordX - 1, i - 1] = 1;
                
                                    
            }
            else if (direction == "DOWN")
            {
                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3  X4 ►
                // Y1 
                // Y2 (1)*
                // Y3 (2)
                // Y4 (3)
                // __

                firstCordY = random.Next(1, (gridDimensionsY + 1) - (unitSize - 1)); //*
                firstCordX = random.Next(1, gridDimensionsX + 1); //Dont care        

                for (int i = 0; i < unitSize; i++)
                { 
                    this.xyUnitPositions[firstCordX - 1, i + (firstCordY - 1)] = 1;
                    Console.WriteLine(i + (firstCordY - 1) + " ");
                }
            }
            else if (direction == "LEFT")
            {

                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3  X4 ►
                // Y1 (3) (2) (1)*
                // Y2       
                // Y3
                // Y4
                // ▼

                firstCordX = random.Next(unitSize, gridDimensionsX + 1); //*       
                firstCordY = random.Next(1, (gridDimensionsY + 1)); //Dont care

                for (int i = firstCordX; i > (firstCordX - unitSize); i--)
                    this.xyUnitPositions[i - 1, firstCordY - 1] = 1;


            }
                
            else if (direction == "RIGHT")
            {

                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3  X4 |
                // Y1    (1)* (2) (3)
                // Y2       
                // Y3
                // Y4
                // ▼

                firstCordX = random.Next(1, (gridDimensionsX + 1) - (unitSize - 1)); //*
                firstCordY = random.Next(1, (gridDimensionsY + 1)); //Dont care

                for (int i = firstCordX; i < (firstCordX + UnitSize); i++)
                    this.xyUnitPositions[i - 1, firstCordY - 1] = 1;

            }
            else if(direction == "LEFTDOWN")
            {
                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3  |
                // Y1    
                // Y2       (1)*    
                // Y3    (2)
                // Y4 (3)
                // __

                firstCordX = random.Next(1 + (unitSize - 1), (gridDimensionsX + 1));
                firstCordY = random.Next(1, ((gridDimensionsY - (unitSize - 1)) + 1));

                for (int i = 0; i < this.unitSize; i++)
                    xyUnitPositions[firstCordX - 1 - i, firstCordY - 1 + i] = 1;

            }
            else if (direction == "RIGHTDOWN") //gasp..
            {
                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3  |
                // Y1    
                // Y2 (1)*    
                // Y3     (2)
                // Y4         (3)
                // __

                firstCordX = random.Next(1, (gridDimensionsX + 1) - (unitSize - 1));
                firstCordY = random.Next(1, ((gridDimensionsY - (unitSize - 1)) + 1));

                for (int i = 0; i < this.unitSize; i++)
                    xyUnitPositions[firstCordX - 1 + i, firstCordY - 1 + i] = 1;


            }
            else if (direction == "LEFTUP")
            {
                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3 ►
                // Y1 (3)   
                // Y2    (2) 
                // Y3        (1)*
                // Y4         
                // ▼

                firstCordX = random.Next(1 + (unitSize - 1), (gridDimensionsX + 1));
                firstCordY = random.Next(1 + (unitSize - 1), (gridDimensionsY + 1));

                for (int i = 0; i < this.unitSize; i++)
                    xyUnitPositions[firstCordX - 1 - i, firstCordY - 1 - i] = 1;

            }
            else if (direction == "RIGHTUP")
            {
                /// EXAMPLE - unit size 3 at boarder value:
                //    X1  X2  X3 ►
                // Y1         (3)
                // Y2     (2) 
                // Y3 (1)*       
                // Y4         
                // ▼

                firstCordX = random.Next(1, (gridDimensionsX + 1) - (unitSize - 1));
                firstCordY = random.Next(1 + (unitSize - 1), (gridDimensionsY + 1));

                for (int i = 0; i < this.unitSize; i++)
                    xyUnitPositions[firstCordX - 1 + i, firstCordY - 1 - i] = 1;
            }
            
        }


        public int[,] XyUnitPositions
        {
            get { return xyUnitPositions;  }
        }

        public string Type
        {
            get { return type; }
        }

        public int UnitSize
        {
            get { return this.unitSize; }
        }


    }
}
