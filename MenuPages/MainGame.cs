using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeRacerClone.MenuPages
{
    public class MainGame
    {
        //dictionary will be <quote, author>
        public static ConcurrentDictionary<string, string> promptDict = new ConcurrentDictionary<string, string>();

        /// <summary>
        /// Used to initialize the dictionary with prompts
        /// </summary>
        public static void ParseTxt()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] lines = File.ReadAllLines(@"E:/Repos/TypeRacerClone/Prompts.txt");

            foreach(string line in lines)
            {
                //Dont know why this works but "-" doesnt...but ok.
                string[] temp = line.Split( "�" );
                promptDict.TryAdd(temp[0], temp[1]);
            }
            
        }


        
       
    }
}
