
using System.Security.Cryptography.X509Certificates;
using TypeRacerClone.Helpers;
using TypeRacerClone.MenuPages;

public class Program
{
    public static void Main(string[] args)
    {
        MainGame mainGame = new MainGame();

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

        ConsoleHelpers.ClearConsoleLines(3);
        ConsoleHelpers.CenterLines($"Welcome, {userName}, Ready to play? \r\n");

        ConsoleHelpers.CenterLines("╔═══════════════════╗");
        ConsoleHelpers.CenterLines("[1] to play", "[2] for highscores", "[ESC] to exit");
        ConsoleHelpers.CenterLines("╚═══════════════════╝");

        ConsoleKeyInfo keyInfo = Console.ReadKey();


        if (keyInfo.Key == ConsoleKey.D1)
        {
            //go to play
            mainGame.InitializeGame();

        }
        else if (keyInfo.Key == ConsoleKey.D2)
        {
            //go to scoreboard 
        }
        else if(keyInfo.Key == ConsoleKey.Escape)
        {
            Console.WriteLine("Shutting down game..");
            Environment.Exit(0);
        }
    }

}