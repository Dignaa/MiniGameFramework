namespace MiniGameFramework.Models.Objects
{
    public interface IWorldObject
    {
        int ItemId
        {
            get { return ItemId; }
            set { ItemId += ItemId; }
        }
        string Name { get; set; }
        string? Description { get; set; }
        Position? ObjectPosition { get; set; }

    }
}