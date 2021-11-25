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
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                uint folderSize = 0;
                foreach (FileInfo file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
                {
                    folderSize += (uint)file.Length;
                }
                return folderSize;
            }
            return (uint)new FileInfo(path).Length;
        }

        /// <summary>
        /// Allows you to see if a file exists.
        /// </summary>
        /// <param name="path">Access path to the file.</param>
        /// <returns>A boolean telling you if a file exists.</returns>
        public Boolean checkExistance(string path)
        {
            FileAttributes attr;
            try
            {
                // get the file attributes for file or directory
                attr = File.GetAttributes(path);
            } catch
            {
                return false;
            }
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return Directory.Exists(path);
            }
            //throw new NotImplementedException();
            return File.Exists(@path);
        }

        public uint getNbFiles(string path)
        {
            return (uint)Directory.GetFiles(path, ".", SearchOption.AllDirectories).Length;
        }
    }
}
