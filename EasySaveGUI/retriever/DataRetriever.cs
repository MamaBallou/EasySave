using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EasySaveConsole.model.log;
using EasySaveConsole.tools;

namespace EasySaveGUI.retriever
{
    public class DataRetriever
    {
        private static string PATH = @".\logs\json\state.json";
        public static List<ModelState> getModelLog()
        {
            try
            {
                string json = File.ReadAllText(PATH);
                return JsonSerializer.Deserialize<List<ModelState>>(json);
            }
            catch
            {
                return new List<ModelState>();
            }
        }

        public static void SetListOfExtension()
        {
            List<string> list = new List<string>();
            foreach (string s in Properties.Settings.Default.toEncrypt)
            {
                list.Add(s);
            }
            Crypter.ToEncrypt = list;
        }
    }
}
