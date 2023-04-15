﻿namespace MiniGameFramework.Models.Items
{
    public class AttackItem : Item
    {
        public AttackItem(string name, string description, int damage, int range)
               : base(name, description)
        {
            Damage = damage;
            Range = range;
        }

        public int Damage { get; set; }
        public int Range { get; set; }
    }
}
