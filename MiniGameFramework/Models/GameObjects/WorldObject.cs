using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using MiniGameFramework.Models.Items;
using System.Diagnostics;

namespace MiniGameFramework.Models.GameObjects
{
    public class WorldObject : GameObject
    {
        private ILogger _logger;
        public WorldObject(string name, bool lootable, bool removable, Position position, Inventory? inventory, ILogger logger)
            : base(name, position, inventory, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Lootable = lootable;
            Removable = removable;
        }
        public bool Lootable { get; set; }
        public bool Removable { get; set; }

        /// <summary>
        /// Loot an object if it is lootable
        /// Get all items from objects inventory
        /// </summary>
        /// <returns>List of items</returns>
        public List<Item> Loot()
        {
            World? world = World._instance;
            if (this.Lootable == true && this.Inventory != null)
            {
                List<Item> foundItems = new List<Item>(Inventory.Items);

                if (world != null && world.GameObjects != null)
                    return foundItems;
            }
            else
                _logger.Log(TraceEventType.Error, "Cannot loot objects as the world and/or any objects do not exist");

            return new List<Item>();
        }

        /// <summary>
        /// Picks an object if it is removable
        /// Removes it from world
        /// </summary>
        /// <returns>World object</returns>
        public WorldObject? Pick()
        {
            if (Removable == true)
            {
                   RemoveFromWorld();
                   return this;
            }
            return null;

        }

    }
}
