using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EasySaveConsole
{
    public class Tools
    {
        public static int Lenght { get; private set; }

        public Tools() { }

        public int getFileSize(Uri path)
        {
            FileInfo fi = new FileInfo(@path.ToString());
            //int resultat = FileInfo.Lenght;
            int resultat = (int) fi.Length;
            return resultat;
        }



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



        public Boolean checkExistance(Uri path)
        {
            //throw new NotImplementedException();
            Boolean res = File.Exists(path.ToString());
            return res;
        }

        public bool testcheckAccess(Uri path)
        {
            throw new NotImplementedException();
        }
    }
}
