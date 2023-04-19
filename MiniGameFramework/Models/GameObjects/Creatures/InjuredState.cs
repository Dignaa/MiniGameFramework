﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
