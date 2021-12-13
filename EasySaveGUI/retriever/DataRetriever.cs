using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EasySaveConsole.model.log;

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
    }
}
