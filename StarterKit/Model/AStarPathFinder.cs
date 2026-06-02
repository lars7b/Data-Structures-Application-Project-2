namespace Model
{
    public class AStarPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Astar;
        public PathFinderType algType { get => _algType; set {} }
        // public PriorityQueue<Node, int>Open{get;set;} = new();
        public PriorityQueue<int[], int>Open{get;set;} = new();
        public int[][]? Closed{get;set;}
        public int ManhattanDistance(int[] pos, int[] goal)
        {
            return Math.Abs(goal[0] - pos[0]) + Math.Abs(goal[1] - pos[1]);
        }
        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            int[] goal = maze.End;
            int steps = 0;
            //start
            int[] start = {pos[0], pos[1]};
            Open.Enqueue(start, steps);
            while(Open.Count > 0)
            {
                int[] current = Open.Dequeue();
                visitedPositions.Enqueue(current);
                if(current[0] == goal[0] && current[1] == goal[1])
                {
                    //later
                    return;
                }
                //enqueue valid neigbors
                foreach (int[] move in maze.moves)
                {
                    int[] check = {current[0] + move[0], current[1] + move[1]};
                    if(maze.IsValidMove(check[0], check[1]))
                    {
                        steps = steps + 1;
                        Open.Enqueue(check, steps);
                    }
                }
                Closed?.Append(current);
            }
        }
    }
}

            

