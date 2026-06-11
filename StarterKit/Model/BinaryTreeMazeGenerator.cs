using Model;

public class BinaryTreeNode
{
    public int Row{get;set;}
    public int Col{get;set;}
    public BinaryTreeNode Parent {get;set;}
    public BinaryTreeNode North {get;set;}
    public BinaryTreeNode East {get;set;}
    
}
public class BinaryTree
{
    public BinaryTreeNode Root {get;set;}
    public BinaryTreeNode[,] Nodes {get;set;}
    public BinaryTree(int rows, int cols)
    {
        Nodes = new BinaryTreeNode[rows, cols];
        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                Nodes[row,col] = new BinaryTreeNode{Row = row, Col = col};
            }
        }
        Root = Nodes[0, cols - 1];
    }
    public void Connect(BinaryTreeNode parent, BinaryTreeNode child, bool isNorth)
    {
        child.Parent = parent;
        if (isNorth)
        {
            parent.North = child;
        }
        else
        {
            parent.East = child;
        }
    }
}
public static class BinaryTreeMazeGenerator
{
    public static int[][] BinaryTreeMazeGeneration(int rows, int cols)
    {
        var tree = new BinaryTree(rows, cols);
        var nodes = tree.Nodes;
        
        Random random = new Random();
        for(int row = 0; row < rows; row++){
            for(int col = 0; col < cols; col++){
                BinaryTreeNode current = nodes[row,col];
                bool NorthAvailable = row > 0;
                bool EastAvailable = col < cols - 1;
                if(NorthAvailable && EastAvailable)
                {
                    if(random.Next(2) == 0)
                    {
                        tree.Connect(current, nodes[row - 1, col], true);
                    }
                    else
                    {
                        tree.Connect(current, nodes[row, col + 1], false);
                    }
                }
                else if (NorthAvailable)
                {
                    tree.Connect(current, nodes[row - 1, col], true);
                }
                else if (EastAvailable)
                {
                    tree.Connect(current, nodes[row, col + 1], false);
                }
            }
        }

        int height = 2 * rows;
        int width = 2 * cols;
        int[][] maze = new int[height][];
        for(int row = 0; row < height; row++)
        {
            maze[row] = new int[width];
            for(int col = 0; col < width; col++)
            {
                maze[row][col] = -1;
            } 
        }

        for(int row = 0; row < rows; row++)
        {
            for(int col = 0; col < cols; col++)
            {
                BinaryTreeNode node = nodes[row, col];
                int gridRow = 2 * row + 1;
                int gridCol = 2 * col + 1;
                
                maze[gridRow][gridCol] = 0;

                if(node.North != null)
                {
                    maze[gridRow - 1][gridCol] = 0;
                }
                if(node.East != null)
                {
                    maze[gridRow][gridCol + 1] = 0;
                }
            }
        }
        return maze;
    }
}