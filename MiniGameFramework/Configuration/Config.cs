using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects;
using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace MiniGameFramework.Configuration
{
    public class Config
    {
        XmlDocument configDoc = new XmlDocument();

        /// <summary>
        /// Add configuration from a file to the game
        /// </summary>
        /// <param name="fileName"></param>
        public void ConfigureFromFile(string filePath)
        {
            configDoc.Load(filePath);

            ConfigureLog();
            ConfigureWorld();
            ConfigureCreature();
            Logger.GetInstance().Log(TraceEventType.Information, "Configuration done: " + DateTime.Now );
        }

        private void ConfigureLog()
        {
            string path = "";

            XmlNode? xNode = configDoc.DocumentElement?.SelectSingleNode("path");

            if (xNode != null)
                path = xNode.InnerText.Trim();


            Logger logger = Logger.CreateInstance(path);
        }

        private void ConfigureWorld()
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

        private void ConfigureCreature()
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

        private int ConvertInt(XmlNode xxNode)
        {
            try
            {
                string xxStr = xxNode.InnerText.Trim();

                int xx = Convert.ToInt32("test");

                return xx;
            }
            catch (FormatException)
            {
                Logger.GetInstance().Log(TraceEventType.Error, "Couldn't recover value of:" + xxNode.Name);
                return 0;
            }
            catch (ArgumentException) 
            { 
                Logger.GetInstance().Log(TraceEventType.Error, "Couldn't recover value of:" + xxNode.Name);
                return 0;
            }

        }
    }
}
