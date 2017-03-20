using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy.Creatures
{
    abstract class Unit : IComparable
    {
        public string Name { get; set; }
        protected int _health;
        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; }
        public abstract int Initiative { get; }

        public Unit(string name)
        {
            Name = name;
        }
        
        public int CompareTo(object unit)
        {
            return Initiative.CompareTo(((Unit)unit).Initiative);
        }

        public override string ToString()
        {
            return $"{Name} ({Health})";
        }
    }
}
