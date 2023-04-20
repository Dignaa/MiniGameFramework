namespace MiniGameFramework.Models.GameObjects.Creatures
{
    public class InjuredState : ICreatureState
    {
        public void HandleStateChange(Creature creature)
        {
            creature.Damage = Creature.DefaultDamage / 2;
        }
    }
}
