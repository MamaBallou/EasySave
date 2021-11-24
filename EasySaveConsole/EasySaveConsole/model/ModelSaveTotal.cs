
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
            //define source and target path in bool
            bool sourceExists = Tool.getInstance().checkExistance(sourceFile);
            bool targetExists = Tool.getInstance().checkExistance(targetFile);
            if (sourceExists & targetExists)
            {
                try
                {
                    var TotalFiles = Directory.EnumerateFiles(sourceFile.ToString());
                    //for each file in Directory, copy this.
                    foreach (string currentFile in TotalFiles)
                    {
                        string fileName = Path.GetFileName(currentFile.ToString());
                        File.Copy(currentFile, String.Concat(targetFile, "/", fileName), true) ;
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                //if source doesn't exist do exception
                Console.WriteLine("ERROR!!! Please verify directories syntax, they not exist ");
            }


        }
    }
}
