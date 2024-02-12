
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
        public static KeyValuePair<string, string> randomEntry;
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
            gameStarted = true;

            if (TimeElapsed < TimeSpan.FromSeconds(10))
                Console.ForegroundColor = ConsoleColor.Red;

            Console.ResetColor();

            //Delete text from before
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ConsoleHelpers.ClearConsoleLines(3);

            //randomize parsed prompts:
            Random rand = new Random();
            randomEntry = promptDict.ElementAt(rand.Next(0, promptDict.Count));
            string prompt = randomEntry.Key;
            string quote = "- " + randomEntry.Value;

            Console.WriteLine("\r\n"+ prompt + "\r\n");
            ConsoleHelpers.CenterLines(quote);


            //read user inputs here?
            ConsoleKeyInfo userInput;
            List<char> userInputchars = new List<char>();
            char[] promptArr = randomEntry.Key.ToCharArray();

            int counter = 0;

            do
            {
                userInput  = Console.ReadKey(true);

                userInputchars.Add(userInput.KeyChar);
               
                int index = userInputchars.IndexOf(userInput.KeyChar);

                if (userInput.Key == ConsoleKey.Backspace)
                {
                    //remove last item?
                }

                if (promptArr[index] == userInputchars[index])
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(userInput.KeyChar);
                }
                else if(promptArr[index] != userInputchars[index])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(userInput.KeyChar);
                }

            } while (userInput.Key != ConsoleKey.Enter);


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
                            //next prompt?
                        }
                        break;

                    case ConsoleKey.Escape:
                        break;
                    
                }
            }
        }

        public static void StringComparer()
        {
            //everytime letter is added check for sub string?
            //check whole input for now i guess?
            bool wrongInput = false;
            string? answer = Console.ReadLine();
            var promptLetters = randomEntry.Key.ToCharArray();
            var userletters = answer.ToCharArray();

            char tempholder;
            //char[] results = new char[userletters.Length];

            for (int i=0; i < userletters.Length; i++)
            {

                if (userletters[i] == promptLetters[i])
                {
                    wrongInput = false;
                    Console.ForegroundColor = ConsoleColor.White;
                    tempholder = userletters[i];
                    Console.Write(tempholder);
                }
                else
                {
                    wrongInput = true;
                    Console.ForegroundColor = ConsoleColor.Red;
                    tempholder = userletters[i];
                    Console.Write(tempholder);

                }
            }



        }
        
       
    }
}
