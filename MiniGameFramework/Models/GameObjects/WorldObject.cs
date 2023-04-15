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
        public WorldObject(string name, bool lootable, bool removable, List<Item>? inventory, Position? position)
            : base(name, position)
        {
            Inventory = inventory;
            Lootable = lootable;
            Removable = removable;
        }
        public bool Lootable { get; set; }
        public bool Removable { get; set; }
        public List<Item>? Inventory { get; set; }

        /// <summary>
        /// Loot an object if it is lootable
        /// Get all items from objects inventory
        /// </summary>
        /// <returns>List of items</returns>
        public List<Item> Loot()
        {
            if (this.Lootable == true && this.Inventory != null)
            {
                List<Item> inventory = new List<Item>(this.Inventory);
                this.Inventory.Clear();
                return inventory;
            }
            return new List<Item>();
        }

        /// <summary>
        /// Picks an object if it is removable
        /// Removes it from world
        /// </summary>
        /// <returns>World object</returns>
        public WorldObject? Pick()
        {
            if (this.Removable == true)
            {
                this.ObjectPosition = null;
                return this;
            }
            return null;

        }

    }
}
