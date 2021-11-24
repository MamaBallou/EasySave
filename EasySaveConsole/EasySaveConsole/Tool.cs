using System;
using System.IO;

namespace EasySaveConsole
{
    /// <summary>
    /// it is a tool that allows us to have information on the file
    /// </summary>
    public class Tool
    {
        private static Tool uniqueInstance;

        private Tool() { }
        /// <summary>
        /// to access the tools instance and make it unique
        /// </summary>
        /// <returns></returns>
        public static Tool getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Tool();
            }
            return uniqueInstance;
        }

        /// <summary>
        /// method getFileSize allows you to determine the size of a file 
        /// </summary>
        /// <param name="path">this parameter is for the access path to the file</param>
        /// <returns>it returns the size of the file a int</returns>
        public int getFileSize(Uri path)
        {
            FileInfo fi = new FileInfo(@path.ToString());
            //int resultat = FileInfo.Lenght;
            int resultat = (int)fi.Length;
            return resultat;
        }

        /// <summary>
        /// method checkAccess allows you to see if you have access to a file;
        /// </summary>
        /// <param name="path">this parameter for the access path to the file </param>
        /// <returns>it returns a boolean telling if you have access to the file or not</returns>
        public Boolean checkAccess(Uri path)
        {
            /*
            //throw new NotImplementedException();
            Boolean resultat = File.ReadAllText("test_files.txt");
            Boolean resultat = File.ReadAllText("String path");
            return resultat;
            */
            throw new NotImplementedException();
        }

        /// <summary>
        /// method checkExistance allows you to see if a file exists;
        /// </summary>
        /// <param name="path">this parameter for the access path to the file</param>
        /// <returns>it returns a boolean telling you if a file exists or not</returns>
        public Boolean checkExistance(Uri path)
        {
            //throw new NotImplementedException();
            Boolean res = File.Exists(path.ToString());
            return res;
        }
    }
}
