using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGameFramework.Models.GameObjects;

namespace MiniGameFramework.Models
{
    public class World
    {
        private static World instance = null;

        private World(int maxX, int maxY, List<GameObject>? gameObjects)
        {
            MaxX = maxX;
            MaxY = maxY;
            GameObjects = gameObjects;
        }

        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public List<GameObject>? GameObjects { get; set; }

        public static World GetInstance()
        {
            if (instance == null)
            {
                
            }
            return instance;
        }

        public static void CreateWorld(int maxX, int maxY, List<GameObject>? gameObjects)
        {
            if (instance == null)
            {
                instance = new World(maxX, maxY, gameObjects);
            }
        }
    }
}
