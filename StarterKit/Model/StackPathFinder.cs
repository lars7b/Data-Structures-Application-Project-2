
namespace Model
{
    public class StackPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Stack;
        public PathFinderType algType { get => _algType; set {} }

        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
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

            while (stack.Count > 0)
            {
                var currentPos = stack.Pop();
                visitedPositions.Enqueue(currentPos);
                
                if (currentPos[0] == maze.End[0] && currentPos[1] == maze.End[1])
                {
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
                    }
                }
            }
        }       
    }
}

            
