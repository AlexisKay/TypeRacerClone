
using TypeRacerClone.Helpers;

public class Program
{
    public static void Main(string[] args)
    {
        #region Title

        Console.Title = "TypeRacerClone - By AlexisKay.";
        ConsoleHelpers.CenterHeader("" +
            "\r\n████████╗██╗░░░██╗██████╗░███████╗██████╗░░█████╗░░█████╗░███████╗██████╗░" +
            "\r\n╚══██╔══╝╚██╗░██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔══██╗██╔════╝██╔══██╗" +
            "\r\n░░░██║░░░░╚████╔╝░██████╔╝█████╗░░██████╔╝███████║██║░░╚═╝█████╗░░██████╔╝" +
            "\r\n░░░██║░░░░░╚██╔╝░░██╔═══╝░██╔══╝░░██╔══██╗██╔══██║██║░░██╗██╔══╝░░██╔══██╗" +
            "\r\n░░░██║░░░░░░██║░░░██║░░░░░███████╗██║░░██║██║░░██║╚█████╔╝███████╗██║░░██║" +
            "\r\n░░░╚═╝░░░░░░╚═╝░░░╚═╝░░░░░╚══════╝╚═╝░░╚═╝╚═╝░░╚═╝░╚════╝░╚══════╝╚═╝░░╚═╝");

        ConsoleHelpers.CenterLines("A TypeRacer clone, by AlexisKay \r\n");
        #endregion 

        ConsoleHelpers.CenterLines("Enter Your Name: \r\n");
        string? userName = Console.ReadLine();

        ClearConsoleLines(3);
        ConsoleHelpers.CenterLines($"Welcome, {userName}, Ready to play? \r\n");


        //Delete name entry before welcome line displays
        //pop up menus in box 

        ConsoleHelpers.CenterLines("ENTER to play", "BACKSPACE to see scores", "ESC to exit");

    }

    public static void ClearConsoleLines(int lines = 1)
    {
        for(int i = 0; i < lines; i++)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
}