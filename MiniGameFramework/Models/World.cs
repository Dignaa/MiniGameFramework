using System.Diagnostics;
using MiniGameFramework.Logging;
using MiniGameFramework.Models.GameObjects;

namespace MiniGameFramework.Models
{
    public class World
    {
        public static World? _instance;
        private static ILogger? _logger;
        private World(int? maxX, int? maxY, List<GameObject>? gameObjects, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MaxX = maxX;
            MaxY = maxY;
            GameObjects = gameObjects ?? new List<GameObject>();
        }
        public static int DefaultMaxX { get; set; } = 100;
        public static int DefaultMaxY { get; set; } = 100;

        public int? MaxX { get; set; } = DefaultMaxX;
        public int? MaxY { get; set; } = DefaultMaxY;
        public List<GameObject> GameObjects { get; set; }

        /// <summary>
        /// Creates world instance
        /// If instance already exists, throws exception
        /// </summary>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="gameObjects"></param>
        /// <returns>World</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static World CreateInstance(int? maxX, int? maxY, List<GameObject>? gameObjects, ILogger logger)
        {
            if (_instance == null)
            {
                _instance = new World(maxX, maxY, gameObjects, logger);
                _logger?.Log(TraceEventType.Information, "World is created");
            }
            else
            {
                throw new InvalidOperationException("Object instance is already created");
            }
            return _instance;
        }

        /// <summary>
        /// Assigns default values for the world from config file
        /// </summary>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        public static void SetDefaultValues(int maxX, int maxY)
        {
            ILogger _logger = Logger.GetInstance();
            DefaultMaxX = maxX;
            DefaultMaxY = maxY;
            _logger.Log(TraceEventType.Information, $"Default values for world are set. MaxX: {maxX}, MaxY: {maxY}");
        }

    }
}
