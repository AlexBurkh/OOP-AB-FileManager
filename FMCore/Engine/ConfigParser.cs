using FMCore.Interfaces;
using FMCore.Models;
using System;
using System.IO;
using System.Text.Json;

namespace FMCore.Engine
{
    public class ConfigParser
    {
        private readonly string _configFile = "D:\\appConfig.json";

        public Config Parse()
        {
            try
            {
                string json = File.ReadAllText(_configFile);
                return JsonSerializer.Deserialize<Config>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;

        }
    }
}
