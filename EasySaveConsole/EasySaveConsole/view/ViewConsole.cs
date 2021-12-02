using System;

namespace EasySaveConsole.view
{
    public class ViewConsole
    {
        /// <summary>
        /// Ask for an input in the Console.
        /// </summary>
        /// <returns>The entry from the user.</returns>
        public string input()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(false); // true = hide input
            }
            return Console.ReadLine();
        }

        /// <summary>
        /// Write output in Console.
        /// </summary>
        /// <param name="s">The string to write.</param>
        public void output(String s)
        {
            Console.WriteLine(s);
        }

    }
}
