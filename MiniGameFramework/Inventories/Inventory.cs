using System;
using MiniGameFramework.Models.Objects;

namespace MiniGameFramework.Inventories
{
    public class Inventory : IInventory
    {
        public Inventory()
        {
            Items = new List<IWorldObject>();
        }
        public Inventory(List<IWorldObject> items)
        {
            Items = items;
        }
        public List<IWorldObject> Items { get; set; }

        public void AddItem(IWorldObject item)
        {
            if (item != null)
                Items.Add(item);
        }
        public void RemoveItem(int id)
        {
            IWorldObject? itemToDelete = Items.Where(i => i.ItemId == id).FirstOrDefault();

            if (itemToDelete != null)
                Items.Remove(itemToDelete);
        }
    }
}
