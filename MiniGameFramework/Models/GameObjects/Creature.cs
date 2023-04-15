using MiniGameFramework.Logging;
using MiniGameFramework.Models.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        /// <summary>
        /// Sets default values to a creture from a config file
        /// </summary>
        /// <param name="defaultDamage"></param>
        /// <param name="defaultHeath"></param>
        public static void SetDefaultValues(int defaultDamage, int defaultHeath)
        {
            DefaultDamage = defaultDamage;
            DefaultHealth = defaultHeath;
            Logger.GetInstance().Log(TraceEventType.Information, $"Default values for creature are set. Default damage: {defaultDamage}, default health: {defaultHeath}");
        }

        /// <summary>
        /// Direct hit from another creature (target known)
        /// </summary>
        /// <param name="attackItem"></param>
        /// <param name="creature"></param>
        public void Hit(AttackItem attackItem, Creature creature)
        {
            creature.ReceiveHit(attackItem.Damage, creature);
        }
        /// <summary>
        /// Hit a creature or an object in specific position (targer unknown)
        /// </summary>
        /// <param name="attackItem"></param>
        /// <param name="position"></param>
        /// <returns>HitResult - all items and/or single object picked</returns>
        public HitResult Hit(AttackItem attackItem, Position position)
        {
            var result = new HitResult();

            if (ObjectPosition != null && position.GetDistance(ObjectPosition) <= attackItem.Range)
            {
                int damage = attackItem.Damage + this.Damage;
                result = GetHitResult(position, damage);
            }
            return result;
        }

        /// <summary>
        /// Make a hit without attach item
        /// </summary>
        /// <param name="position"></param>
        /// <returns>HitResult - all items and/or single object picked</returns>
        public HitResult Hit(Position position)
        {
            return GetHitResult(position, this.Damage);
        }

        private HitResult GetHitResult(Position position, int damage)
        {
            List<GameObject> objects = new List<GameObject>();
            var result = new HitResult();


            objects = GetObjects(position);

            foreach (GameObject obj in objects)
            {
                if (obj is WorldObject)
                {
                    WorldObject worldObject = (WorldObject)obj;
                    WorldObject? pickedObject = worldObject.Pick();
                    List<Item>? lootedItems = worldObject.Loot();

                    if (pickedObject != null)
                        result.HitObject = pickedObject;
                    if (lootedItems != null)
                        result.HitItems = lootedItems;
                }
                if (obj is Creature)
                {
                    Creature creature = (Creature)obj;
                    creature.ReceiveHit(damage, creature);
                }
            }
            return result;
        }

        /// <summary>
        /// Receive damage
        /// If health of a creature is 0 or below, creature is dead
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="creature"></param>
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
