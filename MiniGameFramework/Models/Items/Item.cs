using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameFramework.Models.Items
{
    public abstract class Item
    {
        public Item(string name, string? description = null)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
