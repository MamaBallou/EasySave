using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using EasySaveConsole.model;

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
            } catch
            {
                return new List<ModelState>();
            }
        }
    }
}
