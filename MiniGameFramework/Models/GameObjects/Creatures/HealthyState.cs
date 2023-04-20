namespace MiniGameFramework.Models.GameObjects.Creatures
{
    public class HealthyState : ICreatureState
    {
        public void HandleStateChange(Creature creature)
        {
            creature.Damage = Creature.DefaultDamage;
        }
    }
}
