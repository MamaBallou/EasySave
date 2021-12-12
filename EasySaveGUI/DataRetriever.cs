using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using EasySaveConsole.model;

namespace EasySaveGUI
{
    public class DataRetriever
    {
        private static string PATH = @".\logs\json\log.json";
        public static List<ModelLog> getModelLog()
        {
            try
            {
                string json = File.ReadAllText(PATH);
                return JsonSerializer.Deserialize<List<ModelLog>>(json);
            } catch
            {
                return new List<ModelLog>();
            }
        }
    }
}
