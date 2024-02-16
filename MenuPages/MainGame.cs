
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
        public TimeSpan CountdownTimer;
        private bool gameStarted;
        public static KeyValuePair<string, string> randomEntry;
        const int PADDING = 12;
        private List<char> userInputchars = new List<char>();
        private int ErrorCount = 0;
        private bool isFirstStart;

        public void InitializeGame()
        {
            PlayerScore = 0;
            PlayerError = 0;
            Wpm = 0;
            gameStarted = false;

            //decide on timer to start and  count down?
            TimeElapsed = TimeSpan.FromSeconds(60);
            //CountdownTimer = new System.Timers.Timer();
            //CountdownTimer.Elapsed += new ElapsedEventHandler(Countdown);
            //CountdownTimer.Interval = 1000;
           
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
            //CountdownTimer.Start();
            if(gameStarted == false)
                gameStarted = true;

            if (TimeElapsed < TimeSpan.FromSeconds(10))
                Console.ForegroundColor = ConsoleColor.Red;

            Console.ResetColor();

            if (isFirstStart)
            {
                //Delete text from before
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ConsoleHelpers.ClearConsoleLines(3);

            }
            else if (!isFirstStart)
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + 1);
                Console.Write("Press ESC to quit game.");
                Console.SetCursorPosition(Console.WindowWidth - PADDING, Console.CursorTop + 1);
                Console.WriteLine($"ACCURACY: {PlayerScore}");
                Console.SetCursorPosition(Console.WindowWidth - PADDING, Console.CursorTop);
                Console.WriteLine($"ERRORS: {PlayerError}");

                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleHelpers.CenterLines(TimeElapsed.ToString() + "\r\n");
                Console.ResetColor();
            }

            //randomize parsed prompts:
            Random rand = new Random();
            randomEntry = promptDict.ElementAt(rand.Next(0, promptDict.Count));
            string prompt = randomEntry.Key;
            string quote = "- " + randomEntry.Value;

            Console.WriteLine("\r\n" + prompt + "\r\n");
            ConsoleHelpers.CenterLines(quote);

            //read user inputs here?
            ConsoleKeyInfo userInput;
            char[] promptArr = randomEntry.Key.ToCharArray();

            int index = 0;

            do
            {
                userInput  = Console.ReadKey(true);

                if(userInput.Key != ConsoleKey.Backspace)
                {
                    userInputchars.Add(userInput.KeyChar);

                    index = userInputchars.IndexOf(userInput.KeyChar);

                    if (promptArr[index] == userInputchars[index])
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(userInput.KeyChar);
                    }
                    else if (promptArr[index] != userInputchars[index])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(userInput.KeyChar);
                        ErrorCount++;
                    }
                }
                    
                if (userInput.Key == ConsoleKey.Backspace)
                {
                    //works!
                    if(index > 0)
                    {
                        userInputchars.Remove(userInputchars[index]);
                        Console.Write(userInput.KeyChar);
                        Console.Write(' ');
                        Console.Write(userInput.KeyChar);
                        index--;

                        //check array alignment?
                    }

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
                        {
                            isFirstStart = true;
                            StartGame();
                        }
                        else if (gameStarted)
                        {
                            isFirstStart = false;
                            //next prompt?
                            if(CountdownTimer.Seconds >= 0)
                            {
                                //generate new prompt
                                AddScore();
                                Console.Clear();
                                //redraw screen
                                StartGame();
                                
                            }
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
        
        public void GeneratePrompt()
        {
            Random rand = new Random();
            randomEntry = promptDict.ElementAt(rand.Next(0, promptDict.Count));
            string prompt = randomEntry.Key;
            string quote = "- " + randomEntry.Value;

            Console.WriteLine("\r\n" + prompt + "\r\n");
            ConsoleHelpers.CenterLines(quote);
        }

        public void AddScore()
        {
            /* *WPM:
             * Total Keys pressed / 5 = total number of words (5 because of avg word size)
             * WPM = Total number of words / time elapsed Rounded down
             *
             *Accuracy Score: 
             *  Score % = (Correct keys/Total keys) * 100
             * 
             * AWPM: Adjusted WPM - wpm adjusted for errors made
             *  WPM * Accuracy (rounded down) 
             *  **make this overall score?*/

            if (CountdownTimer.Seconds > 0)
            {
                var totalWords = (userInputchars.Count) / 5;
                var _wpm = (totalWords / 60) * 100;

                //player score can be errors noted compared to keys pressed
                //but for now....
                var _playerScore = ((userInputchars.Count - ErrorCount) / userInputchars.Count) * 100;

                if (gameStarted)
                {
                    PlayerScore += _playerScore;
                    Wpm += _wpm;
                }
            }
        }
       
    }
}
