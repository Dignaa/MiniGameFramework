using MiniGameFramework.Logging;
using MiniGameFramework.Models.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameFramework.Models.GameObjects
{
    public class Creature : GameObject
    {
        public static int DefaultHealth { get; set; } = 100;
        public static int DefaultDamage { get; set; } = 100;

        public Creature(string name, Position position, int? damage, int? health, List<Item> items)
            : base(name, position)
        {
            if (damage != null)
                Damage = (int)damage;

            if (health != null)
                Health = (int)health;

            Items = new List<Item>(items);
            IsDead = false;
        }
        public int Damage { get; set; } = DefaultDamage;
        public int Health { get; set; } = DefaultHealth;
        public List<Item>? Items { get; set; }
        public bool IsDead { get; set; }

        //Set default values from the config file
        public static void SetDefaultValues(int defaultDamage, int defaultHeath)
        {
            DefaultDamage = defaultDamage;
            DefaultHealth = defaultHeath;
            Logger.Instance().traceSource.TraceEvent(TraceEventType.Information, 2, "Default values for creature are set");
        }

        //Direct hit to another creature
        public void Hit(AttackItem attackItem, Creature creature)
        {
            creature.ReceiveHit(attackItem.Damage, creature);
        }
        //Hit a creature or an object in specific position
        public void Hit(AttackItem attackItem, Position position)
        {
            List<GameObject> objects = new List<GameObject>();
            if (ObjectPosition != null && position.GetDistance(ObjectPosition) <= attackItem.Range)
            {
                objects = GetObjects(position);

                foreach (GameObject obj in objects)
                {
                    if (obj is WorldObject)
                    {
                        WorldObject worldObject = (WorldObject)obj;
                        worldObject.Pick();
                        worldObject.Loot();
                    }
                    if (obj is Creature)
                    {
                        Creature creature = (Creature)obj;
                        creature.ReceiveHit(attackItem.Damage, creature);
                    }

                }
            }


        }

        //Receive damage
        public void ReceiveHit(int damage, Creature creature)
        {
            int currentHealth = creature.Health - damage;

            if (currentHealth >= 0)
                creature.Health = currentHealth;
            else
                IsDead = true;

        }
    }
}
