using MiniGameFramework.Models.GameObjects.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameTest.Creatures
{
    internal class BoostedState : ICreatureState
    {
        public void HandleStateChange(Creature creature)
        {
            creature.Damage *= 2;
        }
    }
}
