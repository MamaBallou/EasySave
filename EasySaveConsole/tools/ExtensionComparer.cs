using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasySaveConsole.tools
{
    public class ExtensionComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if(Crypter.IsToEncrypt(x) && Crypter.IsToEncrypt(y))
            {
                return String.Compare(x, y);
            }
            if(Crypter.IsToEncrypt(x))
            {
                return 1;
            }
            if(Crypter.IsToEncrypt(y))
            {
                return -1;
            }
            return String.Compare(x, y);
        }
    }
}
