namespace MiniGameFramework.Models.GameObjects.Creatures
{
    public interface ICreatureState
    {
            void HandleStateChange(Creature creature);
    }
}
