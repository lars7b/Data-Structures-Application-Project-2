

using Model;

namespace View
{
    public class MazeView
    {
        //View
    
        public void DisplayMaze(Maze maze)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            //Console.Clear();
            var array = maze.MazeArray;
           
            Console.SetCursorPosition(0, 14);


            // Loop over the elements of the maze array
            // and display as characters.
            for (int rowIdx = 0; rowIdx < array.Length; rowIdx++)
            {
                var row = array[rowIdx];
                for (int colIdx = 0; colIdx < row.Length; colIdx++)
                {
                    switch (row[colIdx])
                    {
                        case -1:
                            Console.Write("🟦");   //walls
                            break;
                        case 1:
                            Console.Write("🏠");   //begin 
                            break;
                        case 2:
                            Console.Write("🍦");   //end
                            break;
                        case 0:
                            Console.Write("  ");   //not visited
                            break;
                        //Marking strategy
                        case 10:
                            Console.Write("🏅");   //completed
                            break;
                        case 4:
                            Console.Write("⚽️");   //visited
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array[0].Length; colIdx++)
                Console.Write("🟦");
            Console.WriteLine("\n");
        }

        public void DisplayMaze(Maze maze, int[] currPos)
        {
            var array = maze.MazeArray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            var rand = new Random();
            // Loop over the elements of the maze array
            // and display as characters.
            for (int rowIdx = 0; rowIdx < array.Length; rowIdx++)
            {
                var row = array[rowIdx];
                for (int colIdx = 0; colIdx < row.Length; colIdx++)
                {
                    switch (row[colIdx])
                    {
                        case -1:
                            Console.Write("🟦");   //walls
                            break;
                        case 1:                    //begin 
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else
                                Console.Write("🏠");
                            break;
                        case 2:
                            Console.Write("🍦");    //end
                            break;
                        case 0:                     //not visited
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else
                                Console.Write("  ");
                            break;
                        //Marking strategy
                        case 10:
                            Console.Write("🏅");   //completed
                            break;
                        case 4:
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else
                            {
                                if (rand.NextDouble() < 0.3)
                                    Console.Write("🦖");
                                else if (rand.NextDouble() >= 0.3 && rand.NextDouble() < 0.6)
                                    Console.Write("🦕");
                                else
                                    Console.Write("🐈");
                            }
                            //Console.Write("⚽️");   //visited
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array[0].Length; colIdx++)
                Console.Write("🟦");
            Console.WriteLine();
        }

        public void DisplayMaze(Maze maze, int[] currPos, string[] symbolsArr, Queue<int[]> visitedPositions, PathFinderType algType = PathFinderType.Manual)
        {
            var array = maze.MazeArray;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 10);
              
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n\n{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3) )}{"  " + algType + "  "}{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3))}");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        
            System.Console.WriteLine("                         ");

            int[,] gridCopy = new int[array.Length, array[0].Length];
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[0].Length; j++)
                {
                    gridCopy[i, j] = -1;
                }
            }

            int stepCounter = 0;
            foreach (var pos in visitedPositions)
            {
                gridCopy[pos[0], pos[1]] = stepCounter;
                stepCounter++;
            }

            // Loop over the elements of the maze array
            // and display as characters.
            for (int rowIdx = 0; rowIdx < array.Length; rowIdx++)
            {
                var row = array[rowIdx];
                for (int colIdx = 0; colIdx < row.Length; colIdx++)
                {
                    switch (row[colIdx])
                    {
                        case -1:
                            Console.Write("🟦");   //walls
                            break;
                        case 1:                    //begin 
                            //if (currPos[0] == rowIdx && currPos[1] == colIdx)
                            //    Console.Write("⚽️");
                            //else
                                Console.Write("🏠");
                            break;
                        case 2:
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("🏅");    //completed
                            else
                                Console.Write("🍦");    //end     
                            break;
                        case 0:                     //not visited
                            if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                Console.Write("⚽️");
                            else if (gridCopy[rowIdx, colIdx] != -1)
                            {
                                int orderIndex = gridCopy[rowIdx, colIdx];
                                int totalSteps = visitedPositions.Count;
                                string symbol = symbolsArr[(totalSteps -  orderIndex) % symbolsArr.Length]; 
                                Console.Write(symbol);
                            }
                            else
                                Console.Write("  ");
                            break;
                        //Marking strategy:
                        case 10:
                            Console.Write("🏅");    //completed
                            break;
                        case 4:
                            if (currPos[0] == rowIdx && currPos[1] == colIdx) {
                                Console.Write("⚽️");
                            }
                            else
                            {
                                Console.Write("🏃");
                            }
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array[0].Length; colIdx++)
                Console.Write("🟦");
            
            if(algType == PathFinderType.Manual){
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\n👉 👉 👉        Press S to go start again.    👈 👈 👈");
                Console.WriteLine("👉 👉 👉 Press M or ⬅️  to go back to the Menu. 👈 👈 👈\n");
            }
            
            if (!maze.IsValidMove(currPos[0], currPos[1], true))
            {
                PrintWrongMove(currPos);
            }

            if (currPos[0] == maze.End[0] && currPos[1] ==maze.End[1]) //completed
            {
                //Reset Maze
                visitedPositions = new Queue<int[]>(); 
                Console.WriteLine("\n");
                Console.WriteLine("👍 DONE!!! AMAZING!!! 👍. Press any key to continue.");
                Console.ReadKey();
                return;
                
            }
       
        }

        public void DisplayMaze(Maze maze, string[] symbolsArr, int timeInterval, Queue<int[]> visitedPositions)
        {

            var array = maze.MazeMDArray;

            var toBeShownPositions = new Queue<int[]>(visitedPositions);
            var shownPositions = new Queue<int[]>();

            int[,] gridCopy = new int[array.GetLength(0), array.GetLength(1)];
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        gridCopy[i, j] = -1;
                    }
                }

            int stepCounter = 0;

            while (toBeShownPositions.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 10);

                var currPos = toBeShownPositions.Dequeue();
                shownPositions.Enqueue(currPos);

                gridCopy[currPos[0], currPos[1]] = stepCounter;

                //Marking strategy:

                // if (array[currPos[0], currPos[1]] == 2)
                //     array[currPos[0], currPos[1]] = 10;
                // else
                //     array[currPos[0], currPos[1]] = 4;


                // Loop over the elements of the maze array
                // and display as characters.
                for (int rowIdx = 0; rowIdx < array.GetLength(0); rowIdx++)
                {
                    for (int colIdx = 0; colIdx < array.GetLength(1); colIdx++)
                    {
                        switch (array[rowIdx, colIdx])
                        {
                            case -1:
                                Console.Write("🟦");   //walls
                                break;
                            case 1:                    //begin 
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else
                                    Console.Write("🏠");
                                break;
                            case 2:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("🏅");    //completed
                                else
                                    Console.Write("🍦");    //end                           
                                break;
                            case 0:                     //not visited or visited in a not marked array
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else if (gridCopy[rowIdx, colIdx] != -1)
                                {
                                    int orderIndex = gridCopy[rowIdx, colIdx];
                                    string symbol = symbolsArr[(stepCounter -  orderIndex) % symbolsArr.Length]; 
                                    Console.Write(symbol);
                                }
                                else
                                    Console.Write("  ");
                                break;
                            //Marking strategy 
                            case 10:
                                Console.Write("🏅");    //completed
                                break;
                            case 4:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else
                                {
                                    Console.Write("🏃");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("🟦");
                }

                for (int colIdx = 0; colIdx <= array.GetLength(1); colIdx++)
                    Console.Write("🟦");
                Console.WriteLine();
                
                stepCounter++;
                
                Thread.Sleep(timeInterval);
                //Console.Clear();
            }
        }

        public void DisplayMaze(Maze maze, string[] symbolsArr, int timeInterval, Queue<int[]> visitedPositions, PathFinderType algType = PathFinderType.Manual)
        {
            var array = maze.MazeMDArray;

            var toBeShownPositions = new Queue<int[]>(visitedPositions);
            var shownPositions = new Queue<int[]>();

            int[,] gridCopy = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    gridCopy[i, j] = -1;
                }
            }

            int stepCounter = 0;

            while (toBeShownPositions.Count > 0)
            {
                Console.SetCursorPosition(0, 10);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;


                var currPos = toBeShownPositions.Dequeue();
                shownPositions.Enqueue(currPos);

                gridCopy[currPos[0], currPos[1]] = stepCounter;
                
                //Marking strategy:

                // if (array[currPos[0], currPos[1]] == 2)
                //     array[currPos[0], currPos[1]] = 10;
                // else
                //     array[currPos[0], currPos[1]] = 4;


                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n\n{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3) )}{"  " + algType + "  "}{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3))}");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("                                      ");

                // Loop over the elements of the maze array
                // and display as characters.
                for (int rowIdx = 0; rowIdx < array.GetLength(0); rowIdx++)
                {
                    for (int colIdx = 0; colIdx < array.GetLength(1); colIdx++)
                    {
                        switch (array[rowIdx, colIdx])
                        {
                            case -1:
                                Console.Write("🟦");   //walls
                                break;
                            case 1:                    //begin 
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("⚽️");
                                else
                                    Console.Write("🏠");
                                break;
                            case 2:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                    Console.Write("🏅");    //completed
                                else
                                    Console.Write("🍦");    //end                           
                                break;
                            case 0:                     //not visited or visited in a not marked array
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else if (gridCopy[rowIdx, colIdx] != -1)
                                {
                                    int orderIndex = gridCopy[rowIdx, colIdx];
                                    string symbol = symbolsArr[(stepCounter -  orderIndex) % symbolsArr.Length]; 
                                    Console.Write(symbol);
                                }
                                else
                                    Console.Write("  ");
                                break;
                            //Marking strategy 
                            case 10:
                                Console.Write("🏅");    //completed
                                break;
                            case 4:
                                if (currPos[0] == rowIdx && currPos[1] == colIdx)
                                {
                                    Console.Write("⚽️");
                                }
                                else
                                {
                                    Console.Write("🏃");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("🟦");
                }

                for (int colIdx = 0; colIdx <= array.GetLength(1); colIdx++)
                    Console.Write("🟦");
                Console.WriteLine();

                stepCounter++;

                Thread.Sleep(timeInterval);
                //Console.Clear();
            }
        }

        public void HighlightShortestPath(Maze maze, Stack<int[]> shortestPath, PathFinderType algType, Queue<int[]> visitedPositions)
        {
            var array = maze.MazeMDArray;

            Console.SetCursorPosition(0, 10);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine($"\n\n{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3) )}{"  " + algType + "  "}{String.Concat(Enumerable.Repeat("🟨", maze.MazeMDArray.GetLength(1)/2 - algType.ToString().Length/3))}");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("                                      ");

            for (int rowIdx = 0; rowIdx < array.GetLength(0); rowIdx++)
            {
                for (int colIdx = 0; colIdx < array.GetLength(1); colIdx++)
                {
                    bool isPath = shortestPath.Any(p => p[0] == rowIdx && p[1] == colIdx);
                    bool isVisited = visitedPositions.Any(p => p[0] == rowIdx && p[1] == colIdx);

                    if (array[rowIdx, colIdx] == -1) Console.Write("🟦");
                    else if (rowIdx == maze.Begin[0] && colIdx == maze.Begin[1]) Console.Write("🏠");
                    else if (rowIdx == maze.End[0] && colIdx == maze.End[1]) Console.Write("🏅");
                    else if (isPath) Console.Write("🌟");
                    else if (isVisited) Console.Write("❌");
                    else Console.Write("  "); 
                }
                Console.WriteLine("🟦");
            }

            for (int colIdx = 0; colIdx <= array.GetLength(1); colIdx++)
                Console.Write("🟦");
        }

        public string[] generateSymbols(int spaces)
        {
            var rnd = new Random(); 
            var symbols = new string[2*spaces];
            for (int i = 0; i < 2 * spaces; i++)
            {
            /*
                if(i < 10)
                    symbols[i] = ":" + i;
                else
                    symbols[i] = (i % 100) < 10 ? ":" + (i % 100) : (i % 100) + "";

            */
                if (rnd.NextDouble() < 0.3)
                {
                    symbols[i] = "🦖";
                }

                else if (rnd.NextDouble() < 0.7)
                {
                    symbols[i] = "🐈";
                }

                else
                {
                    symbols[i] = "🦕";
                }
            }

            return symbols;
        }

        public void DisplaySuccess(bool success, string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(success ? msg + "🎉 Path found! 🎊. Press any key to continue." : msg + "🔎  No path found. 🔎 Press any key to continue.");
            Console.ReadKey();
        }
        private void PrintWrongMove(int[] tmppos)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Wrong Direction -> {tmppos[0]}, {tmppos[1]}");

            Thread.Sleep(100);
            Console.BackgroundColor = ConsoleColor.White;       
        }
    }
}
