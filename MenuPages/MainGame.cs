
using System.Collections.Concurrent;
using System.Text;
using System.Timers;
using TypeRacerClone.Helpers;

namespace TypeRacerClone.MenuPages
{
    public class MainGame
    {
        //dictionary will be <quote, author>
        public static ConcurrentDictionary<string, string> promptDict = new ConcurrentDictionary<string, string>();

        public int PlayerScore;
        public int PlayerError;
        public int Wpm;
        public TimeSpan TimeElapsed;
        public System.Timers.Timer CountdownTimer;
        private bool gameStarted;

        const int PADDING = 12;
        public void InitializeGame()
        {
            PlayerScore = 0;
            PlayerError = 0;
            Wpm = 0;
            gameStarted = false;

            //decide on timer to start and  count down?
            TimeElapsed = TimeSpan.FromSeconds(60);
            CountdownTimer = new System.Timers.Timer();
            CountdownTimer.Elapsed += new ElapsedEventHandler(Countdown);
            CountdownTimer.Interval = 1000;
           
            ParseTxt();

            Console.Clear();


            //10 buffer for score size
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
            Console.Write("Press ESC to quit game.");
            Console.SetCursorPosition(Console.WindowWidth - PADDING, Console.CursorTop + 1);
            Console.WriteLine($"ACCURACY: {PlayerScore}");
            Console.SetCursorPosition(Console.WindowWidth - PADDING, Console.CursorTop);
            Console.WriteLine($"ERRORS: {PlayerError}");

            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleHelpers.CenterLines(TimeElapsed.ToString() + "\r\n");
            Console.ResetColor();


            ConsoleHelpers.CenterLines("Lets test your typing skills!", "Press Enter when you're ready to start.");
            ConsoleHelpers.CenterLines("________________________________________________________________________________________\r\n");
            ReadKeys();

        }

        public void Countdown(object source, ElapsedEventArgs e)
        {
            TimeElapsed = TimeElapsed.Subtract(TimeSpan.FromSeconds(1));

        }


        /// <summary>
        /// Used to initialize the dictionary with prompts
        /// </summary>
        public static void ParseTxt()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string[] lines = File.ReadAllLines(@"E:/Repos/TypeRacerClone/Prompts.txt");

            foreach(string line in lines)
            {
                //Dont know why this works but "-" doesnt...but ok.
                string[] temp = line.Split( "�" );
                promptDict.TryAdd(temp[0], temp[1]);
            }
            
        }

        public void StartGame()
        {
            //1. start counting down, may need to rerender the timer

            //2. Pull up random prompts until the timer is over, 

            //3. calculate score simultaneously
            /*
             *Algoritms for scores and WPM?
             *
             *WPM:
             * Total Keys pressed / 5 = total number of words (5 because of avg word size)
             * WPM = Total number of words / time elapsed Rounded down
             *
             *Accuracy Score: 
             *  Score % = (Correct keys/Total keys) * 100
             * 
             * AWPM: Adjusted WPM - wpm adjusted for errors made
             *  WPM * Accuracy (rounded down) 
             *  **make this overall score?
             * 
             */

            //timer start TODO: not working correctly
            CountdownTimer.Start();

            if (TimeElapsed < TimeSpan.FromSeconds(10))
                Console.ForegroundColor = ConsoleColor.Red;

            Console.ResetColor();

            //Delete text from before
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ConsoleHelpers.ClearConsoleLines(3);

            //randomize parsed prompts:
            Random rand = new Random();
            var randomEntry = promptDict.ElementAt(rand.Next(0, promptDict.Count));
            string prompt = randomEntry.Key;
            string quote = "- " + randomEntry.Value;

            Console.WriteLine("\r\n"+ prompt + "\r\n");
            ConsoleHelpers.CenterLines(quote);


        }

        private void ReadKeys()
        {
            ConsoleKeyInfo keyinfo = new();

            while(!Console.KeyAvailable && keyinfo.Key != ConsoleKey.Escape)
            {
                keyinfo = Console.ReadKey(true);

                switch(keyinfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (gameStarted == false)
                            StartGame();
                        else if (gameStarted)
                        {
                            StringComparer();
                        }
                        break;

                    case ConsoleKey.Escape:
                        break;
                    default:
                        Console.Write(keyinfo.KeyChar);
                        break;

                }
            }
        }

        private static void StringComparer()
        {


        }
        
       
    }
}
