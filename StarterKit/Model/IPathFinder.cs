namespace Model;

public enum PathFinderType
{
    Recursive,
    Stack,
    Astar,
    Dijkstra,
    Manual
}

public interface IPathFinder
{
    PathFinderType algType { get; set; }
    // Queue<int[]> ExploredNodes { get; }
    // int SearchSteps { get; }
    // int PathCost { get; }

    void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions);
}