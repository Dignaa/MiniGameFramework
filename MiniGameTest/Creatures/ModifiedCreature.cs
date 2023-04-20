using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using MiniGameFramework.Models;
using MiniGameFramework.Models.GameObjects.Creatures;
using MiniGameFramework.Models.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameTest.Creatures
{
    internal class ModifiedCreature : Creature
    {
        public ModifiedCreature(string name, ILogger logger, Position position, int? damage = null, int? health = null, IInventory? inventory = null, AttackObject? primaryAttackItem = null, DefenceObject? primaryDefenceItem = null) : base(name, logger, position, damage, health, inventory, primaryAttackItem, primaryDefenceItem)
        {
            ChangeState(new BoostedState());
        }
    }
}
