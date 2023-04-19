namespace MiniGameFramework.Models.Objects
{
    public class AttackObject : IWorldObject
    {
        public AttackObject(string name, string description, int damage, int range, Position? position = null)
        {
            Name = name;
            Description = description;
            Damage = damage;
            Range = range;
            ObjectPosition = position;
        }

        public int Damage { get; set; }
        public int Range { get; set; }
        public string? Description { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public Position? ObjectPosition { get; set; }
    }
}
