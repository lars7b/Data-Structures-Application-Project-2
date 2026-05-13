using Spectre.Console;

namespace MazeSolver.View;

public static class GameView
{
    public static void Run()
    {
        while (true)
        {
            DrawPanel();
            var choices = new List<string> { "Recursive", "Stack", "A*", "Dijkstra", "Manually", "Exit" };

            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title(
                    "Select a pathfinder:")
                .AddChoices(choices)).ToLower();

            switch (choice)
            {
                case "recursive":
                    throw new NotImplementedException();
                case "stack":
                    throw new NotImplementedException();
                case "a*":
                    throw new NotImplementedException();
                case "dijkstra":
                    throw new NotImplementedException();
                case "manually":
                    throw new NotImplementedException();
                case "exit":
                    return;
            }
        }
    }

    private static void DrawPanel()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Panel("Hello World!").Header("[blue bold]Welcome to the amazing Maze[/]").RoundedBorder()
            .Expand());
    }

    private static Canvas DrawMaze()
    {
        return new Canvas(4, 3);
    }

    private static void DrawProgressBar()
    {
    }
}