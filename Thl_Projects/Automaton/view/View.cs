using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.view
{
    static class View
    {
        
        public static void DisplayPrompt(string prompt)
        {
            Console.WriteLine(prompt);
        }

        public static int ReadInt()
        {
            string readStuff = Console.ReadLine();
            int number = Convert.ToInt32(readStuff);
            return number;
        }
        public static string ReadString()
        {
            return Console.ReadLine();
        }

        public static void DispalyOptions(Menu menu)
        {
            foreach(Option op in menu.Options)
            {
                Console.WriteLine("No: " + op.OptionNumber + ": " + op.OptionContent);
            }
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static void PauseScreen()
        {
            Console.ReadKey();
        }
        
    }
}
