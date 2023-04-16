using MiniGameFramework.Inventories;
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
        private static ILogger? _logger;
        public static int DefaultHealth { get; set; } = 100;
        public static int DefaultDamage { get; set; } = 100;

        public Creature(string name, Position position, int? damage, int? health, Inventory inventory, AttackItem primaryAttackItem, DefenceItem primaryDefenceItem, ILogger logger)
            : base(name, position, inventory, logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            if (damage != null)
                Damage = (int)damage;

            if (health != null)
                Health = (int)health;
            IsDead = false;
        }
        public int Damage { get; set; } = DefaultDamage;
        public int Health { get; set; } = DefaultHealth;
        public AttackItem PrimaryAttackItem { get; set; }
        public DefenceItem PrimaryDefenceItem { get; set; }
        public bool IsDead { get; private set; }

        /// <summary>
        /// Sets default values to a creture from a config file
        /// </summary>
        /// <param name="defaultDamage"></param>
        /// <param name="defaultHeath"></param>
        public static void SetDefaultValues(int defaultDamage, int defaultHeath)
        {
            DefaultDamage = defaultDamage;
            DefaultHealth = defaultHeath;
            _logger?.Log(TraceEventType.Information, $"Default values for creature are set. Default damage: {defaultDamage}, default health: {defaultHeath}");
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

            if (ObjectPosition != null && position.GetDistance(ObjectPosition, position) <= attackItem.Range)
            {
                int damage = PrimaryAttackItem.Damage + Damage;
                result = GetHitResult(position, damage);
                if(result.HitReturnItems != null) 
                    result.HitReturnItems.ForEach(i=>Inventory.AddItem(i));
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
                    List<Item> lootedItems = worldObject.Loot();

                    if (pickedObject != null)
                        result.HitReturnObject = pickedObject;
                    if (lootedItems != null)
                        result.HitReturnItems = lootedItems;
                        lootedItems?.ForEach(i => Inventory.AddItem(i));
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
            int damageDealt = damage - PrimaryDefenceItem.ReduceDamage;
            if(damageDealt < 0) damageDealt = 0;
            int currentHealth = creature.Health - damageDealt;

            if (currentHealth >= 0)
                creature.Health = currentHealth;
            else
                IsDead = true;
                RemoveFromWorld();
                _logger?.Log(TraceEventType.Information, $"Creature --- {creature.Name} --- is dead and removed from world");


        }
    }
}
