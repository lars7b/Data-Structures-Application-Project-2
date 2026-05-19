using MazeSolver.Controller;
using Spectre.Console;

namespace MazeSolver.View;

public static class GameView
{
    public static void Run()
    {
        while (true)
        {
            DrawPanel();
            var choices = new List<string> { "Recursive", "Stack", "A*", "Dijkstra", "Manual", "Exit" };

            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(
                    "Select a pathfinder:")
                .AddChoices(choices)).ToLower();

            switch (choice)
            {
                case "recursive":
                    GameController.Recursive();
                    break;
                case "stack":
                    GameController.Stack();
                    break;
                case "a*":
                    GameController.AStar();
                    break;
                case "dijkstra":
                    GameController.Dijkstra();
                    break;
                case "manual":
                    GameController.Manual();
                    break;
                case "exit":
                    return;
            }
        }
    }

    private static void DrawPanel()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Panel(BuildMaze()).Header("[blue bold] Welcome to the amazing Maze [/]")
            .RoundedBorder().BorderColor(Color.Blue));
    }

    private static Canvas BuildMaze()
    {
        var canvas = new Canvas(50, 25);
        for (var i = 0; i < 50; i++)
        for (var j = 0; j < 25; j++)
            canvas.SetPixel(i, j, Color.Blue);

        return canvas;
    }

    private static void DrawProgressBar()
    {
    }
}