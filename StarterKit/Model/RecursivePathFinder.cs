
namespace Model
{
    public class RecursivePathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Recursive;
        public PathFinderType algType { get => _algType; set {} }
        bool EndFound = false;

        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            //ToDo implement this method
            // while(pos != maze.End)
            // {
            //     if(maze.IsValidMove(maze.moves[1][0], maze.moves[0][-1]))
            //     {
            //         visitedPositions.Enqueue(pos);
            //         FindPath(maze, pos = [0,1], visitedPositions);
            //     }
            // }
            if (EndFound) return;
            visitedPositions.Enqueue(pos);
            if(pos[0] == maze.End[0] && pos[1] == maze.End[1])
            {
                EndFound = true;
                return;
            }
            foreach(int[] move in maze.moves)
            {
                bool notVisited = true;
                int[] check = new int[pos.Length];
                Array.Copy(pos,check, pos.Length);
                check[0] += move[0];
                check[1] += move[1];
                if(maze.IsValidMove(check[0],check[1]))
                {
                    foreach(int[] compare in visitedPositions)
                    {
                        if(check[0] == compare[0] && check[1] == compare[1])
                        {
                            notVisited = false;
                        }
                    }
                    if (notVisited)
                    {
                        FindPath(maze, check, visitedPositions);
                        if(EndFound) return;
                    }
                }
            }
            // visitedPositions.Enqueue(pos); //remove this line
        }
    }
}
