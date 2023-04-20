using MiniGameFramework.Inventories;
using MiniGameFramework.Logging;
using MiniGameFramework.Models.Items;
using MiniGameFramework.Models.Objects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace MiniGameFramework.Models.GameObjects.Creatures
{
    public class Creature
    {
        private static ILogger? _logger;
        public static int DefaultHealth { get; set; } = 100;
        public static int DefaultDamage { get; set; } = 100;

        public Creature(string name, ILogger logger, Position position, int? damage = null, int? health = null, IInventory? inventory = null, AttackObject? primaryAttackItem = null, DefenceObject? primaryDefenceItem = null)
        {
            Name = name;
            ObjectPosition = position;
            State = new HealthyState();
            ChangeState(State);
            _logger = logger;
            if (damage != null) 
            {
                Damage = (int)damage;
                DefaultDamage = Damage;
            }

            if (health != null)
            {
                Health = (int)health;
                DefaultHealth = Health;
            }

            PrimaryAttackObject = primaryAttackItem ?? new AttackObject("No weapon", "Default no weapon attack", Damage, 1);
            PrimaryDefenceObject = primaryDefenceItem ?? new DefenceObject("No defence", "No defence object", 0);
            Inventory = inventory ?? new Inventory();
            IsDead = false;
        }
        public string Name { get; set; }
        public Position ObjectPosition { get; set; }
        public ICreatureState State { get; set; }
        public int Damage { get; set; } = DefaultDamage;
        public int Health { get; set; } = DefaultHealth;
        public AttackObject PrimaryAttackObject { get; set; }
        public DefenceObject PrimaryDefenceObject { get; set; }
        public IInventory Inventory { get; set; }
        public bool IsDead { get; set; }

        public void ChangeState(ICreatureState newState)
        {
            State = newState;
            State.HandleStateChange(this);
        }

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

        public void Move(Position distance)
        {
            ObjectPosition = new Position(ObjectPosition.X + distance.X, ObjectPosition.Y + distance.Y);
        }

        /// <summary>
        /// Direct hit from another creature (target known)
        /// </summary>
        /// <param name="attackItem"></param>
        /// <param name="creature"></param>
        public virtual void Hit(Creature creature)
        {
            if (IsDead == false)
            {
                if(ObjectPosition != null && ObjectPosition.GetDistance(ObjectPosition, creature.ObjectPosition) <= PrimaryAttackObject.Range)
                    creature.ReceiveHit((PrimaryAttackObject?.Damage ?? 0) + Damage);
                else
                    _logger?.Log(TraceEventType.Warning, "Cannot complete hit action as the target is not within the range.");
            }
            else
                _logger?.Log(TraceEventType.Warning, "Cannot complete hit action as the creature action was invoked with is dead.");
        }

        /// <summary>
        /// Hit a specific position, adds all looted objects to inventory, delivers damage to all creatures in the position
        /// </summary>
        /// <param name="position"></param>
        /// <returns>All looted and removed objects</returns>
        public virtual (List<IWorldObject>? looted, List<IWorldObject>? removed) Hit(Position position)
        {
            if (IsDead == false)
            {
                if (ObjectPosition != null && ObjectPosition.GetDistance(ObjectPosition, position) <= PrimaryAttackObject.Range)
                    return GetHitResult(position);
                else
                    _logger?.Log(TraceEventType.Warning, "Cannot complete hit action as the target is not within the range.");
            }
            else
                _logger?.Log(TraceEventType.Warning, "Cannot complete hit action as the creature action was invoked with is dead.");
           
            return (null, null);
        }

        private (List<IWorldObject>? looted, List<IWorldObject>? removed) GetHitResult(Position position)
        {
            List<IWorldObject> objects;
            List<Creature> creatures;

            List<IWorldObject> resultLootedItems = new List<IWorldObject>();
            List<IWorldObject> resultRemovedItems = new List<IWorldObject>();


            (objects, creatures) = World.GetObjects(position);

                foreach (var worldObject in objects)
                {
                    switch (worldObject)
                    {
                        case LootableCompositeObject lootableObject:
                            List<IWorldObject>? lootedItems = lootableObject.Loot();
                            if (lootedItems != null)
                            {
                                foreach (var item in lootedItems)
                                {
                                    resultLootedItems.Add(item);
                                    Inventory.AddItem(item);
                                }
                            }
                            break;

                        case RemovableObject removableObject:
                            var removedItem = removableObject.Pick();
                            if (removedItem != null)
                            {
                                resultRemovedItems.Add(removedItem);
                            }
                            break;

                        case AttackObject attackObj:
                            resultLootedItems.Add(attackObj);
                            Inventory.AddItem(attackObj);
                            attackObj.ObjectPosition = ObjectPosition;
                            break;

                        case DefenceObject defenceObj:
                            resultLootedItems.Add(defenceObj);
                            Inventory.AddItem(defenceObj);
                            defenceObj.ObjectPosition = ObjectPosition;
                        break;

                        default:
                            _logger?.Log(TraceEventType.Warning, "Object cannot be picked or looted from the game world.");
                            break;
                    }
                };
                foreach (Creature creature in creatures)
                {
                    int damage = (PrimaryAttackObject?.Damage ?? 0) + Damage;
                    creature.ReceiveHit(damage);
                }
            return (resultLootedItems, resultRemovedItems);

        }

        /// <summary>
        /// Receive damage
        /// If health of a creature is 0 or below, creature is dead
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="creature"></param>
        private void ReceiveHit(int damage)
        {
            int damageDealt = damage - PrimaryDefenceObject.ReduceDamage;
            if (damageDealt < 0) damageDealt = 0;

            int currentHealth = Health - damageDealt;

            if (currentHealth > 0)
            {
                if (currentHealth < 50 && State is not InjuredState)
                    ChangeState(new InjuredState());
                Health = currentHealth;
            }
            else
            {
                Health = 0;
                ChangeState(new DeadState(_logger));
            }
        }
    }
}
