using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole
{
    public class Tools
   public class Tools
    {
        private static Tools uniqueInstance;
        public static Tools getInstance()
        {
            return uniqueInstance;
        }
        private Tools() { }

        public int getFileSize(Uri path)
        {
            throw new NotImplementedException();

        }

        public Boolean checkAccess(Uri path)
        {
            throw new NotImplementedException();
        }

        public Boolean checkExistance(Uri path)
        {
            throw new NotImplementedException();
        }
    }
}
