/*récupérer tous les fichiers texte d’un répertoire et de ses sous-répertoires, puis les déplacer vers un nouveau répertoire. 
 Une fois les fichiers déplacés, ils n’existent plus dans les répertoires d’origine.*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    class ModelSaveTotal : ModelSave
    {
        public override void save()
        {
          
                bool sourceExists = ModelSaveTotal.sourceFile.Exists();
                bool targetExists = ModelSaveTotal.targetFile.Exixts();
                if (sourceExists & targetExists)
                {
                    try
                    {
                        var TotalFiles = Directory.EnumerateFiles(sourceFile);
                        foreach (string currentFile in TotalFiles)
                        {
                            string fileName = currentFile.Substring(sourceFile.Length + 1);
                            Directory.Move(currentFile, Path.Combine(targetFile, fileName));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("ERROR!!! Please verify directories syntax, they not exist ");
                }
            

        }
    }
}
