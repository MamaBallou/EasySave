using System;
using System.IO;

namespace EasySaveConsole
{
    /// <summary>
    /// It is a tool that allows us to have informations on a file.
    /// </summary>
    public class Tool
    {
        private static Tool uniqueInstance;

        private Tool() { }
        /// <summary>
        /// To access the Tool instance because it's unique (Singleton).
        /// </summary>
        /// <returns>Instance of Tool.</returns>
        public static Tool getInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Tool();
            }
            return uniqueInstance;
        }

        /// <summary>
        /// Allows you to determine the size of a file.
        /// </summary>
        /// <param name="path">tAccess path to the file.</param>
        /// <returns>Size of the file.</returns>
        public int getFileSize(Uri path)
        {
            FileInfo fi = new FileInfo(@path.ToString());
            //int resultat = FileInfo.Lenght;
            int resultat = (int)fi.Length;
            return resultat;
        }

        /// <summary>
        /// Allows you to see if a file exists.
        /// </summary>
        /// <param name="path">Access path to the file.</param>
        /// <returns>A boolean telling you if a file exists.</returns>
        public Boolean checkExistance(Uri path)
        {
            //throw new NotImplementedException();
            Boolean res = File.Exists(path.ToString());
            return res;
        }
    }
}
