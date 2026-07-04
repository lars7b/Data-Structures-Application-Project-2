
namespace Model
{
    public class StackPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Stack;
        public PathFinderType algType { get => _algType; set {} }
        public Stack<int[]> ShortestPath { get; private set; } = new Stack<int[]>();
        public int SearchSteps { get; private set; }
        public int PathCost { get; private set; }
        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            ShortestPath.Clear();
            SearchSteps = 0;
            PathCost = 0;

            if (maze.MazeArray == null || maze.MazeArray.Length == 0 || maze.MazeArray[0].Length == 0 ||
                pos == null || pos.Length != 2 ||
                !maze.IsValidMove(pos[0], pos[1]) ||
                visitedPositions.Any(_ => _[0] == pos[0] && _[1] == pos[1]) ||
                visitedPositions.Any(_ => _[0] == maze.End[0] && _[1] == maze.End[1])
               )
            {
                return;
            }

            var stack = new Stack<int[]>();
            stack.Push(pos);

            var parentMap = new Dictionary<string, int[]>();

            while (stack.Count > 0)
            {
                var currentPos = stack.Pop();
                visitedPositions.Enqueue(currentPos);
                
                if (currentPos[0] == maze.End[0] && currentPos[1] == maze.End[1])
                {
                    int[] curr = maze.End;
                    
                    while (curr[0] != maze.Begin[0] || curr[1] != maze.Begin[1])
                    {
                        ShortestPath.Push(curr);
                        string key = $"{curr[0]},{curr[1]}";
                        
                        if (parentMap.ContainsKey(key))
                        {
                            curr = parentMap[key];
                        }
                        else
                        {
                            break; 
                        }
                    }
                    ShortestPath.Push(maze.Begin);
                    
                    SearchSteps = visitedPositions.Count;
                    PathCost = ShortestPath.Count;
                    
                    return;
                }

                foreach (var move in maze.moves)
                {
                    int x = currentPos[0] + move[0];
                    int y = currentPos[1] + move[1];
                    int[] nextPos = [x, y];
                    
                    bool isVisited = visitedPositions.Any(_ => _[0] == nextPos[0] && _[1] == nextPos[1]);
                    bool isValidated = stack.Any(_ => _[0] == nextPos[0] && _[1] == nextPos[1]);

                    if (maze.IsValidMove(nextPos[0], nextPos[1]) && !isVisited && !isValidated)
                    {
                        stack.Push(nextPos);
                        parentMap[$"{nextPos[0]},{nextPos[1]}"] = currentPos;
                    }
                }
            }

            SearchSteps = visitedPositions.Count;
            PathCost = 0;
        }       
    }
}

            
