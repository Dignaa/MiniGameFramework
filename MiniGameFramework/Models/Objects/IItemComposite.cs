namespace MiniGameFramework.Models.Objects
{
    public interface IItemComposite
    {
        public void Add(IWorldObject item);

        public void Remove(IWorldObject item);

        public List<IWorldObject> GetItems();
    }
}