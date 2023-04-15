using MiniGameFramework.Logging;
using System.Diagnostics;

namespace MiniGameFramework.Models.GameObjects
{
    public abstract class GameObject
    {
        public GameObject(string name, Position? objectPosition)
        {
            Name = name;
            ObjectPosition = objectPosition;
        }
        public string Name { get; set; }
        public Position? ObjectPosition { get; set; }

        /// <summary>
        /// Gets all the objects from a specified position in a world
        /// </summary>
        /// <param name="position"></param>
        /// <returns>List of game objects</returns>
        public static List<GameObject> GetObjects(Position position)
        {
            Logger logger = Logger.GetInstance();
            World? world = World._instance;
            if (world != null)
            {
                List<GameObject> objects = world.GameObjects?.Where(o => o.ObjectPosition == position)?.ToList() ?? new List<GameObject>();
                return objects;
            }
            else
            {
                logger.Log(TraceEventType.Error, "Cannot get any objects as the world is not created");
                return new List<GameObject>();
            }
        }


    }
}
