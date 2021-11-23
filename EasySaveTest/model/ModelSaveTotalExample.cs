/*récupérer tous les fichiers texte d’un répertoire et de ses sous-répertoires, puis les déplacer vers un nouveau répertoire. 
Une fois les fichiers déplacés, ils n’existent plus dans les répertoires d’origine.*/
using System;
using System.IO;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceDirectory = @"C:\current";
            string archiveDirectory = @"C:\archive";

            try
            {
                var txtFiles = Directory.EnumerateFiles(sourceDirectory);

                foreach (string currentFile in txtFiles)
                {
                    string fileName = currentFile.Substring(sourceDirectory.Length + 1);
                    Directory.Move(currentFile, Path.Combine(archiveDirectory, fileName));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
