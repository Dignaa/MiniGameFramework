using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MiniGameFramework.Logging;
using MiniGameFramework.Models.GameObjects;

namespace MiniGameFramework.Models
{
    public class World
    {
        public static World? _instance;

        private World(int? maxX, int? maxY, List<GameObject>? gameObjects)
        {
            MaxX = maxX;
            MaxY = maxY;
            GameObjects = gameObjects;
        }
        public static int DefaultMaxX { get; set; } = 100;
        public static int DefaultMaxY { get; set; } = 100;

        public int? MaxX { get; set; } = DefaultMaxX;
        public int? MaxY { get; set; } = DefaultMaxY;
        public List<GameObject>? GameObjects { get; set; }

        /// <summary>
        /// Creates world instance
        /// If instance already exists, throws exception
        /// </summary>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="gameObjects"></param>
        /// <returns>World</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static World CreateInstance(int maxX, int maxY, List<GameObject>? gameObjects)
        {
            if (_instance == null)
            {
                _instance = new World(maxX, maxY, gameObjects);
                Logger.GetInstance().Log(TraceEventType.Information, "World is created");
            }
            else
            {
                throw new InvalidOperationException("Object instance is already created");
            }
            return _instance;
        }

        public static void SetDefaultValues(int maxX, int maxY)
        {
            DefaultMaxX = maxX;
            DefaultMaxY = maxY;
            Logger.GetInstance().Log(TraceEventType.Information, $"Default values for world are set. MaxX: {maxX}, MaxY: {maxY}");
        }

    }
}
