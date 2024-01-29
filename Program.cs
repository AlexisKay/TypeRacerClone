
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

        ConsoleHelpers.CenterLines($"Welcome, {userName}, Ready to play? \r\n");
    }
}