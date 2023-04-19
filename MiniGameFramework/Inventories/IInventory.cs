using MiniGameFramework.Models.Objects;

namespace MiniGameFramework.Inventories
{
    public interface IInventory
    {
        List<IWorldObject> Items { get; set; }

        void AddItem(IWorldObject item);
        void RemoveItem(int id);
    }
}