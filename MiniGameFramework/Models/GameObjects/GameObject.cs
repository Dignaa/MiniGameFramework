using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using System.Diagnostics;

namespace MiniGameFramework.Models.GameObjects
{
    public abstract class GameObject
    {
        private static ILogger? _logger;
        public GameObject(string name, Position objectPosition, Inventory? inventory, ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Name = name;
            ObjectPosition = objectPosition;
            Inventory = inventory ?? new Inventory();
        }
        public string Name { get; set; }
        public Position ObjectPosition { get; set; }
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Gets all the objects from a specified position in a world
        /// </summary>
        /// <param name="position"></param>
        /// <returns>List of game objects</returns>
        public static List<GameObject> GetObjects(Position position)
        {
            World? world = World._instance;
            if (world != null)
            {
                List<GameObject> objects = world.GameObjects?.Where(o => o.ObjectPosition == position)?.ToList() ?? new List<GameObject>();
                return objects;
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot get any objects as the world is not created");
                return new List<GameObject>();
            }
        }

        /// <summary>
        /// Returns a list with all objects in the world
        /// </summary>
        /// <returns>A list of game objects, if world or objects are null, returns new list</returns>
        public static List<GameObject> GetAllObjects()
        {

            World? world = World._instance;
            if (world != null && world.GameObjects != null)
            {
                return world.GameObjects;
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot get objects from the world as the world or no objects exist");

                return new List<GameObject>();
            }
        }

        /// <summary>
        /// Removes an object from world if ir exist
        /// </summary>
        public void RemoveFromWorld()
        {
            List<GameObject> objects = GetAllObjects();
            if (objects.Contains(this))
            {
                objects.Remove(this);
            }
            else
            {
                _logger?.Log(TraceEventType.Warning, "Object is not in the game world so it cannot be removed.");
            }
        }

        /// <summary>
        /// Add a new object to the world if it is not already there
        /// </summary>
        public  void AddToWorld()
        {
            List<GameObject> objects = GetAllObjects();
            if (!objects.Contains(this))
            {
                objects.Add(this);
            }
            else
            {
                _logger?.Log(TraceEventType.Warning, "Object is already in the game world.");
            }
        }
    }
}
