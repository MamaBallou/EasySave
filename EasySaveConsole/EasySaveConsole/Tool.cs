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
        public uint getFileSize(string path)
        {
            FileInfo fi = new FileInfo(@path);
            //int resultat = FileInfo.Lenght;
            int resultat = (int)fi.Length;
            return (uint)resultat;
        }

        /// <summary>
        /// Allows you to see if a file exists.
        /// </summary>
        /// <param name="path">Access path to the file.</param>
        /// <returns>A boolean telling you if a file exists.</returns>
        public Boolean checkExistance(string path)
        {
            //throw new NotImplementedException();
            Boolean res = File.Exists(@path);
            return res;
        }

        public uint getNbFiles(string path)
        {
            
            return (uint)Directory.GetFiles(path, ".", SearchOption.AllDirectories).Length;
        }
    }
}
