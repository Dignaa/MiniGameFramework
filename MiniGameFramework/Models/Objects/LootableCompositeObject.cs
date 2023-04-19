using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using MiniGameFramework.Models.GameObjects.Creatures;
using MiniGameFramework.Models.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameFramework.Models.Items
{
    public class LootableCompositeObject : IWorldObject, IItemComposite
    {
        private List<IWorldObject> items = new List<IWorldObject>();
        private ILogger _logger;

        public string? Description { get; set; }
        public int ItemId { get ; set; }
        public string Name { get; set; }
        public Position? ObjectPosition { get; set; }

        public LootableCompositeObject(string name, string description, Position position, ILogger logger)
        {
            Name = name;
            Description = description;
            ObjectPosition = position;
            _logger= logger;
        }

        public void Add(IWorldObject item)
        {
            items.Add(item);
        }

        public void Remove(IWorldObject item)
        {
            items.Remove(item);
        }

        public List<IWorldObject> GetItems()
        {
            return items;
        }

        /// <summary>
        /// Loot an object if it is lootable
        /// Get all items from objects inventory
        /// </summary>
        /// <returns>List of items</returns>
        public List<IWorldObject> Loot()
        {
            List<IWorldObject>? foundItems = GetItems();
            if (foundItems != null)
                    return foundItems;
            else
                _logger.Log(TraceEventType.Error, "Cannot loot objects as the world and/or any objects do not exist");

            return new List<IWorldObject>();
        }
    }
}
