using MiniGameFramework.Models;
using MiniGameFramework.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameTest
{
    internal class CraftingCompositeObject : IItemComposite, IWorldObject
    {
        private List<IWorldObject> items = new List<IWorldObject>();

        public string Name { get; set; }
        public string? Description { get; set; }
        public Position? ObjectPosition { get; set; }

        public void Add(IWorldObject item)
        {
            items.Add(item);
        }

        public List<IWorldObject> GetItems()
        {
            return items;
        }

        public void Remove(IWorldObject item)
        {
            items.Remove(item);
        }
    }
}
