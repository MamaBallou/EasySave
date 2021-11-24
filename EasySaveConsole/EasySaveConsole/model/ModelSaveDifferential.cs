using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using EasySaveConsole.exception;

namespace EasySaveConsole.model
{
    public class ModelSaveDifferential : ModelSave
    {
        public ModelSaveDifferential(string name, string sourceFile, string targetFile) : base(name, sourceFile, targetFile)
        {
        }

        public override void save()
        {
            bool sourceExists = Tools.getInstance().checkExistance(sourceFile);
            bool targetExists = Tools.getInstance().checkExistance(targetFile);

            try
            {
                if (!sourceExists)
                {
                    // This statement ensures that the file is created,
                    // but the handle is not kept.
                    throw new PathNotFoundException();
                }
                

                // Move the file.
                string fileName = Path.GetFileName(sourceFile.ToString());
                File.Copy(sourceFile.ToString(), String.Concat(targetFile, "/", fileName));
                Console.WriteLine("{0} was moved to {1}.", sourceFile.ToString(), targetFile.ToString());
                //File.Move(sourceFile.ToString(), targetFile.ToString());
                //File.Delete(sourceFile.ToString());

            }
            catch (ArgumentException e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
