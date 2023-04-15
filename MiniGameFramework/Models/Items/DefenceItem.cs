﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniGameFramework.Models.Items
{
    public class DefenceItem : Item
    {
        public DefenceItem(string name, string description, int reduceDamage)
                : base(name, description)
        {
            reduceDamage = ReduceDamage;
        }
        public int ReduceDamage { get; set; }
    }
}
