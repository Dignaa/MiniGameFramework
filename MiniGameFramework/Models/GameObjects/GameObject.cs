using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameFramework.Models.GameObjects
{
    public abstract class GameObject
    {

        public GameObject(string name, Position objectPosition)
        {
            Name = name;
            ObjectPosition = objectPosition;
        }
        public string Name { get; set; }
        public Position ObjectPosition { get; set; }

        public static List<GameObject> GetObjects(Position position)
        {
            World _world = World.GetInstance();
            List<GameObject> objects = _world.GameObjects?.Where(o => o.ObjectPosition == position).ToList() ?? new List<GameObject>();
            return objects;
        }
    }
}
