using FMCore.Interfaces;
using FMCore.Models;
using System;
using System.IO;
using System.Text.Json;

namespace FMCore.Engine
{
    public class ConfigParser
    {
        public ConfigParser(ILogger logger)
        {
            this.logger = logger;
        }

        ILogger logger;
        private readonly string _configFile = "D:\\appConfig.json";

        public Config Parse()
        {
            try
            {
                string json = File.ReadAllText(_configFile);
                JsonSerializer.Deserialize<Config>(json);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
            }
            return null;

        }
    }
}
