using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects;
using System.Diagnostics;
using System.Xml;

namespace MiniGameFramework.Configuration
{
    public static class Config
    {
        private static ILogger? _logger;
        private static XmlDocument configDoc = new XmlDocument();

        /// <summary>
        /// Add configuration from a file to the game
        /// </summary>
        /// <param name="fileName"></param>
        public static void ConfigureFromFile(string filePath, ILogger logger)
        {
            _logger = logger;

            if(_logger == null)
                throw new ArgumentNullException(nameof(logger));

            configDoc.Load(filePath);

            ConfigureWorld();
            ConfigureCreature();
            _logger.Log(TraceEventType.Information, "Configuration done: " + DateTime.Now );
        }

        /// <summary>
        /// Creates a logger file from the info in config file. Has to be created
        /// </summary>
        /// <returns></returns>
        public static Logger ConfigureLogger(string filePath)
        {
            configDoc.Load(filePath);
            string path = "";

            XmlNode? xNode = configDoc.DocumentElement?.SelectSingleNode("path");

            if (xNode != null)
                path = xNode.InnerText.Trim();

             return Logger.CreateInstance(path);
        }

        private static void ConfigureWorld()
        {
            int maxX = 0;
            int maxY = 0;

            XmlNode? xNode = configDoc.DocumentElement?.SelectSingleNode("world/maxX");

            if (xNode != null)
                maxX = ConvertInt(xNode);

            XmlNode? yNode = configDoc.DocumentElement?.SelectSingleNode("world/maxY");

            if (yNode != null)
                maxY = ConvertInt(yNode);

            World.SetDefaultValues(maxX, maxY);
        }

        private static void ConfigureCreature()
        {
            int startHealth = 0;
            int damage = 0;

            XmlNode? hNode = configDoc.DocumentElement?.SelectSingleNode("creature/startHealth");

            if (hNode != null)
                startHealth = ConvertInt(hNode);

            XmlNode? dNode = configDoc.DocumentElement?.SelectSingleNode("creature/damage");

            if (dNode != null)
                damage = ConvertInt(dNode);

            Creature.SetDefaultValues(damage, startHealth);
        }

        private static int ConvertInt(XmlNode xxNode)
        {
            try
            {
                string xxStr = xxNode.InnerText.Trim();

                int xx = Convert.ToInt32(xxStr);

                return xx;
            }
            catch (FormatException)
            {
                _logger?.Log(TraceEventType.Error, $"Couldn't recover value of: {xxNode.Name}, value will be set to 0");
                return 0;
            }
            catch (ArgumentException) 
            { 
                _logger?.Log(TraceEventType.Error, $"Couldn't recover value of: {xxNode.Name}, value will be set to 0");
                return 0;
            }

        }
    }
}
