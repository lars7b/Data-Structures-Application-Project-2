
namespace Model
{
    public enum PathFinderType{Recursive, Stack, Astar, Dijkstra, Manual};

    public interface IPathFinder
    {
        PathFinderType algType{get; set;}
        Stack<int[]> ShortestPath => new Stack<int[]>();
        int SearchSteps => 0;
        int PathCost => 0;
        void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions);
    }
}
