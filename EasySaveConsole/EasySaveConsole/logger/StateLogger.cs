using System;
using System.Collections.Generic;
using System.Text;

namespace EasySaveConsole.logger
{
    public class StateLogger
    {
        private static string PATH = "../../logs";
        private static StateLogger _instance;

        private StateLogger() { }

        public static StateLogger getInstance()
        {
            if (_instance == null)
            {
                _instance = new StateLogger();
            }
            return _instance;
        }

        public void write(string data)
        {
            throw new NotImplementedException();
        }
    }
}
