using System.Diagnostics;
using MiniGameFramework.Logging;
using MiniGameFramework.Models.GameObjects.Creatures;
using MiniGameFramework.Models.Objects;

namespace MiniGameFramework.Models
{
    public class World
    {
        public static World? _instance;
        private static ILogger? _logger;
        private World(int? maxX, int? maxY, ILogger logger, List<IWorldObject>? worldObjects = null, List<Creature>? creatures = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            MaxX = maxX;
            MaxY = maxY;
            WorldObjects = worldObjects ?? new List<IWorldObject>();
            Creatures = creatures ?? new List<Creature>();
        }
        public static int DefaultMaxX { get; set; } = 100;
        public static int DefaultMaxY { get; set; } = 100;

        public int? MaxX { get; set; } = DefaultMaxX;
        public int? MaxY { get; set; } = DefaultMaxY;
        public List<IWorldObject> WorldObjects { get; set; }
        public List<Creature> Creatures { get; set; }

        /// <summary>
        /// Creates world instance
        /// If instance already exists, throws exception
        /// </summary>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="gameObjects"></param>
        /// <returns>World</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static World CreateInstance(int? maxX, int? maxY, List<IWorldObject>? worldObjects, List<Creature>? creatures, ILogger logger)
        {
            if (_instance == null)
            {
                _instance = new World(maxX, maxY, logger, worldObjects, creatures);
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

        /// <summary>
        /// Gets all the objects from a specified position in a world
        /// </summary>
        /// <param name="position"></param>
        /// <returns>List of game objects</returns>
        public static (List<IWorldObject>, List<Creature>) GetObjects(Position position)
        {
            World? world = _instance;
            if (world != null)
            {
                List<IWorldObject> objects = world.WorldObjects.Where(o => o.ObjectPosition == position)?.ToList() ?? new List<IWorldObject>();
                List<Creature> creatures = world.Creatures.Where(c => c.ObjectPosition == position)?.ToList() ?? new List<Creature>();
                return (objects, creatures);
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot get any objects as the world is not created");
                return (new List<IWorldObject>(), new List<Creature>());
            }
        }

        /// <summary>
        /// Returns a list with all objects and creatures in the world
        /// </summary>
        /// <returns>A list of game objects, if world or objects are null, returns new list</returns>
        public static (List<IWorldObject>, List<Creature>) GetAllObjects()
        {

            World? world = _instance;
            if (world != null)
            {
                return (world.WorldObjects ?? new List<IWorldObject>(), world.Creatures ?? new List<Creature>());
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot get objects from the world as the world is not created");

                return (new List<IWorldObject>(), new List<Creature>());
            }
        }

        /// <summary>
        /// Removes an object from world if ir exist
        /// </summary>
        public void RemoveObjectFromWorld(IWorldObject worldObject)
        {
            World? world = _instance;
            if (world != null)
            {
                List<IWorldObject> objects = world.WorldObjects ?? new List<IWorldObject>();
                if (objects.Contains(worldObject))
                {
                    objects.Remove(worldObject);
                }
                else
                {
                    _logger?.Log(TraceEventType.Warning, "Object is not in the game world so it cannot be removed.");
                }
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot remove object from the world as the world is not created");
            }
        }

        /// <summary>
        /// Add a new object to the world if it is not already there
        /// </summary>
        public void AddObjectToWorld(IWorldObject worldObject)
        {
            World? world = _instance;
            if (world != null)
            {
                List<IWorldObject> objects = world.WorldObjects ?? new List<IWorldObject>();
                if (!objects.Contains(worldObject))
                {
                    objects.Add(worldObject);
                }
                else
                {
                    _logger?.Log(TraceEventType.Warning, "Object is already in the game world.");
                }
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot add object to the world as the world is not created");
            }
        }

        /// <summary>
        /// Add creature to the game world
        /// </summary>
        /// <param name="creature"></param>
        public void AddCreatureToWorld(Creature creature)
        {
            World? world = _instance;
            if (world != null)
            {
                List<Creature> creatures = world.Creatures ?? new List<Creature>();
                if (!creatures.Contains(creature))
                {
                    creatures.Add(creature);
                }
                else
                {
                    _logger?.Log(TraceEventType.Warning, "Creature is already in the game world.");
                }
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot add creature to the world as the world is not created");
            }
        }

        /// <summary>
        /// Removes creature from game world
        /// </summary>
        /// <param name="creature"></param>
        public void RemoveCreatureFromWorld(Creature creature)
        {
            World? world = _instance;
            if (world != null)
            {
                List<Creature> creatures = world.Creatures ?? new List<Creature>();
                if (creatures.Contains(creature))
                {
                    creatures.Remove(creature);
                }
                else
                {
                    _logger?.Log(TraceEventType.Warning, "Creature is not in the game world so it cannot be removed.");
                }
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot remove creature from the world as the world is not created");
            }
        }

    }
}

