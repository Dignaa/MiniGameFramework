namespace MiniGameFramework.Models.Objects
{
    public class DefenceObject : IWorldObject
    {
        public DefenceObject(string name, string description, int reduceDamage, Position? position = null)
        {
            Name = name;
            Description = description;
            ReduceDamage = reduceDamage;
            ObjectPosition = position;
        }
        public int ReduceDamage { get; set; }
        public string? Description { get; set ; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public Position? ObjectPosition { get; set; }
    }
}
