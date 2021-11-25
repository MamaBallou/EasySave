using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.view
{
    public class ViewConsole
    {
        public string input()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false); // true = hide input
            }
            return Console.ReadLine();
        }

        public void output(String s)
        {
            Console.WriteLine(s);
        }

    }
}
