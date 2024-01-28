using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeRacerClone.Helpers
{
    /// <summary>
    /// Contains Helper methods for Console Display/Organization
    /// </summary>
    public class ConsoleHelpers
    {
        /// <summary>
        /// Centers Header or Anything involving whitespace or blocky (asthetic) views
        /// </summary>
        /// <param name="header">Header string</param>
        public static void CenterHeader(string header)
        {
            var lines = header.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            var longestLength = lines.Max(line => line.Length);
            var leadingSpaces = new string(' ', (Console.WindowWidth - longestLength) / 2);
            var centeredText = string.Join(Environment.NewLine, lines.Select(line => leadingSpaces + line));

            Console.WriteLine(centeredText);
        }

        /// <summary>
        /// Centers normal string lists
        /// </summary>
        /// <param name="lines">Output strings to be aligned</param>
        public static void CenterLines(params string[] lines)
        {
            foreach (var line in lines)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                Console.WriteLine(line);
            }
        }

        public static void ClearLine(int lines = 1)
        {
            for(int i = 0; i <= lines; i++)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

    }
}
