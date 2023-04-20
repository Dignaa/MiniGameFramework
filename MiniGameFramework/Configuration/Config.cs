using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects.Creatures;
using System.Diagnostics;
using System.Xml;

namespace MiniGameFramework.Configuration
{
    public class Config : IConfig
    {
        private static ILogger? _logger;
        private static XmlDocument configDoc = new XmlDocument();


        /// <summary>
        /// Add configuration from a file to the game
        /// </summary>
        /// <param name="fileName"></param>
        public ILogger ConfigureFromFile(string filePath)
        {
            configDoc.Load(filePath);

            _logger = ConfigureLogger(filePath);
            ConfigureWorld();
            ConfigureCreature();

            return _logger;
        }

        /// <summary>
        /// Creates a logger file from the info in config file. Has to be created
        /// </summary>
        /// <returns></returns>
        private static ILogger ConfigureLogger(string filePath)
        {
            configDoc.Load(filePath);
            string path = "";

            XmlNode? xNode = configDoc.DocumentElement?.SelectSingleNode("path");

            if (xNode != null)
                path = xNode.InnerText.Trim();

             _logger = Logger.CreateInstance(path);

            _logger.Log(TraceEventType.Information, "Logger configuration done: " + DateTime.Now);
            return _logger;
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
            _logger.Log(TraceEventType.Information, "World configuration done: " + DateTime.Now);

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
            _logger.Log(TraceEventType.Information, "Creature configuration done: " + DateTime.Now);

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