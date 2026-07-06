
using System.Collections;

namespace Model
{
    public class RecursivePathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Recursive;
        public PathFinderType algType { get => _algType; set {} }
        public Stack<int[]> ShortestPath { get; set; } = new Stack<int[]>();
        public int SearchSteps { get; set; } = 0;
        public int PathCost { get; set; }
        bool EndFound = false;
        public class Breadcrumb
        {
            public int[]Coords { get; set; }
            public Breadcrumb? Parent { get; set; }
        }
        private Breadcrumb? previous = null;
        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            if (EndFound) return;
            visitedPositions.Enqueue(pos);
            // trail.Enqueue(breadCrumb);
            Breadcrumb breadcrumb = new Breadcrumb(){Coords=pos, Parent = previous};
            if(pos[0] == maze.End[0] && pos[1] == maze.End[1])
            {
                EndFound = true;
                // BreadCrumb temp = trail.Dequeue();
                Breadcrumb temp = breadcrumb;
                while (temp != null)
                {
                    ShortestPath.Push(temp.Coords);
                    temp = temp.Parent;
                }
                PathCost = ShortestPath.Count;
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
                        SearchSteps++;
                        previous = breadcrumb;
                        FindPath(maze, check, visitedPositions);
                        if(EndFound) return;
                    }
                }
            }
            // visitedPositions.Enqueue(pos); //remove this line
        }
    }
}
