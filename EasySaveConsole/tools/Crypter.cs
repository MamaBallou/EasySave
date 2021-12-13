using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace EasySaveConsole.tools
{
    public class Crypter
    {
        private static List<string> toEncrypt = new List<string>();
        public static List<string> ToEncrypt
        {
            get => toEncrypt;
            set => toEncrypt = value;
        }


        /// <summary>
        /// Crypt and copy a file in a folder.
        /// </summary>
        /// <param name="currentFile">Source file.</param>
        /// <param name="folderTargetPath">Target folder.</param>
        public static void crypAndCopy(string currentFile, string folderTargetPath)
        {
            Process process = new Process();
            process.StartInfo.FileName = "Cryptosoft/CryptoSoft.exe";
            string s = Path.GetFullPath(currentFile);
            string ss = Path.GetFullPath(folderTargetPath);
            process.StartInfo.Arguments = $"/e \"{s}\" \"{ss}{Path.GetFileName(s)}\" 1234567891234567";
            process.Start();
            process.WaitForExit();
            File.SetLastWriteTime($"{ss}{Path.GetFileName(s)}", File.GetLastWriteTime(currentFile));
        }

        internal static bool IsToEncrypt(string fileExt)
        {
            return toEncrypt.Exists(ext => ext == fileExt);
        }
    }
}
