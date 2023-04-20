using MiniGameFramework.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MiniGameFramework.Models.GameObjects.Creatures
{
    internal class DeadState : ICreatureState
    {
        private ILogger? _logger;
        public DeadState(ILogger? logger) 
        {
            _logger = logger;
        }
        public void HandleStateChange(Creature creature)
        {
            creature.IsDead = true;
            World? world = World._instance;
            if (world != null)
            {
                world.RemoveCreatureFromWorld(creature);
                _logger?.Log(TraceEventType.Information, $"Creature --- {creature.Name} --- is dead and removed from world");
            }
            else
            {
                _logger?.Log(TraceEventType.Error, "Cannot remove a creature if the world doesn't exist");

            }
        }
    }
}
