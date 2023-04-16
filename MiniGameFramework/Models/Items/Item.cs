
namespace MiniGameFramework.Models.Items
{
    public abstract class Item
    {
        private int itemId = 0;

        public Item( string name, string? description = null)
        {
            ItemId = itemId++;
            Name = name;
            Description = description;
        }

        public int ItemId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
