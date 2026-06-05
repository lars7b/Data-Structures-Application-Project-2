namespace Model;

public class DijkstraPathFinder : IPathFinder
{
    public PathFinderType algType { get; set; } = PathFinderType.Dijkstra;


    public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
    {
        var rows = maze.MazeArray.Length;
        var cols = maze.MazeArray[0].Length;

        var graph = new Graph(rows, cols);

        var start = new Node(pos[0], pos[1])
        {
            Distance = 0
        };

        Node? end = null;

        var queue = new PriorityQueue<Node, int>();
        queue.Enqueue(start, start.Distance);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Row == maze.End[0] && current.Col == maze.End[1])
            {
                end = current;
                break;
            }

            foreach (var move in maze.moves)
            {
                var nRow = current.Row + move[0];
                var nCol = current.Col + move[1];

                if (!maze.IsValidMove(nRow, nCol)) continue;
                var neighbour = graph.Grid[nRow, nCol];
                var newDistance = neighbour.Distance + 1;

                if (newDistance >= neighbour.Distance) continue;
                neighbour.Distance = newDistance;
                neighbour.Previous = current;

                queue.Enqueue(neighbour, neighbour.Distance);
            }
        }

        if (end == null) return;
        {
            var stack = new Stack<int[]>();
            var current = end;

            while (current != null)
            {
                stack.Push([current.Row, current.Col]);
                current = current.Previous;
            }

            while (stack.Count > 0) visitedPositions.Enqueue(stack.Pop());
        }
    }
}

internal class Graph
{
    public Graph(int rows, int cols)
    {
        Grid = new Node[rows, cols];
        for (var r = 0; r < rows; r++)
        for (var c = 0; c < cols; c++)
            Grid[r, c] = new Node(r, c);
    }

    public Node[,] Grid { get; set; }
}

internal class Node(int row, int col) : IComparable<Node>
{
    public int Row { get; set; } = row;
    public int Col { get; set; } = col;
    public int Distance { get; set; } = int.MaxValue;
    public Node? Previous { get; set; }

    public int CompareTo(Node? other)
    {
        return other == null ? 1 : Distance.CompareTo(other.Distance);
    }
}