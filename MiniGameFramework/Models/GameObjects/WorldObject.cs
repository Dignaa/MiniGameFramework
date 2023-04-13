using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGameFramework.Models.Items;

namespace MiniGameFramework.Models.GameObjects
{
    public class WorldObject : GameObject
    {
        public WorldObject(string name, bool lootable, bool removable, List<Item>? inventory, Position position)
            : base(name, position)
        {
            Inventory = inventory;
            Lootable = lootable;
            Removable = removable;
        }
        public bool Lootable { get; set; }
        public bool Removable { get; set; }
        public List<Item> Inventory { get; set; }

    }
}
