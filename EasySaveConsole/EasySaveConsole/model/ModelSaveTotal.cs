/*récupérer tous les fichiers texte d’un répertoire et de ses sous-répertoires, puis les déplacer vers un nouveau répertoire. 
 Une fois les fichiers déplacés, ils n’existent plus dans les répertoires d’origine.*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.model
{
    public class ModelSaveTotal : ModelSave
    {
        public ModelSaveTotal(string name, string sourceFile, string targetFile) : base(name, sourceFile, targetFile) { }
        public override void save()
        {
            bool sourceExists = Tools.getInstance().checkExistance(sourceFile);
            bool targetExists = Tools.getInstance().checkExistance(targetFile);
            if (sourceExists & targetExists)
            {
                try
                {
                    var TotalFiles = Directory.EnumerateFiles(sourceFile.ToString());
                    foreach (string currentFile in TotalFiles)
                    {
                        string fileName = Path.GetFileName(currentFile.ToString());
                        File.Copy(currentFile, String.Concat(targetFile, "/", fileName));
                        //Directory.Move(currentFile, Path.Combine(targetFile, fileName));
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
