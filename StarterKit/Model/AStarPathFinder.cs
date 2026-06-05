namespace Model
{
    public class AStarPathFinder : IPathFinder
    {
        PathFinderType _algType = PathFinderType.Astar;
        public PathFinderType algType { get => _algType; set {} }

        public class Node
        {
            public int[] Coords {get;set;}
            public int Heuristic {get;set;}
            public int Travelled {get;set;}
            public int Priority => Heuristic + Travelled;
            public Node? Parent {get;set;}
        }

        public PriorityQueue<Node, int> Open {get;set;} = new();
        // public PriorityQueue<int[], int>Open{get;set;} = new();
        public Dictionary<string, int> Gscore {get;set;} = new();
        public string Key(int[] pos) => $"{pos[0]}, {pos[1]}";

        public int ManhattanDistance(int[] pos, int[] goal)
        {
            return Math.Abs(goal[0] - pos[0]) + Math.Abs(goal[1] - pos[1]);
        }

        public void FindPath(Maze maze, int[] pos, Queue<int[]> visitedPositions)
        {
            //start
            int[] goal = maze.End;
            Gscore = new Dictionary<string, int>();
            Open.Clear();
            int travelled = 0;
            Node start = new Node{Coords=pos, Travelled=travelled, Heuristic= ManhattanDistance(pos, goal)};
            Open.Enqueue(start, start.Priority);
            Gscore[Key(pos)] = 0;

            while(Open.Count > 0)
            {
                Node current = Open.Dequeue();
                string currentKey = Key(current.Coords);
                
                if(current.Travelled > Gscore[currentKey]) continue;

                if(current.Coords[0] == goal[0] && current.Coords[1] == goal[1])
                {
                    Stack<int[]> path = new Stack<int[]>();
                    Node temp = current;
                    while(temp != null)
                    {
                        path.Push(temp.Coords);
                        temp = temp.Parent;
                    }
                    while(path.Count > 0)
                    {
                        visitedPositions.Enqueue(path.Pop());
                    }
                    return;
                }
                //enqueue valid neigbors
                foreach (int[] move in maze.moves)
                {
                    int[] check = {current.Coords[0] + move[0], current.Coords[1] + move[1]};
                    if(!maze.IsValidMove(check[0], check[1]))
                    {
                        continue;
                    }
                    string nextKey = Key(check);
                    int tentativeG = current.Travelled +1;
                    if(!Gscore.ContainsKey(nextKey) || tentativeG < Gscore[nextKey])
                    {
                        Gscore[nextKey] = tentativeG;
                        
                        Node neighbor = new Node(){
                            Coords= check,
                            Travelled = current.Travelled + 1,
                            Heuristic= ManhattanDistance(check, goal),
                            Parent = current
                        };
                        Open.Enqueue(neighbor, neighbor.Priority);
                    }
                }
            }
        }
    }
}

            

