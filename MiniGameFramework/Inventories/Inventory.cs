using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniGameFramework.Models.Items;

namespace MiniGameFramework.Inventories
{
    public class Inventory
    {
        public Inventory()
        {
            Items = new List<Item>();
        }
        public Inventory(List<Item> items)
        {
            Items = items;
        }
        public List<Item> Items { get; set; }

        public void AddItem(Item item)
        {
            if(item != null)
                Items.Add(item);
        }
        public void RemoveItem(int id)
        {
            Item? itemToDelete = Items.Where(i => i.ItemId== id).FirstOrDefault();

            if(itemToDelete != null) 
                Items.Remove(itemToDelete);
        }
    }
}
