using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy.Creatures
{
    class Cavalry : Warrior
    {
        public new int _maxHealth = 10;
        public new int _initiative = 3;
        public new int Power = 7;
       
        public Cavalry(string name)
            : base(name)
        {
            Health = MaxHealth;
        }

        public override int MaxHealth
        { get { return _maxHealth; } }

        public override int Health
        {
            get { return _health; }
            set { _health = value > MaxHealth ? MaxHealth : value; }
        }

        public override int Initiative
        {
            get { return _initiative; }
        }

        public new void Attack(Unit u)
        {
            int health = u.Health;
            u.Health -= Power;
            Logger.Log($"{Name} attacked {u.Name} ({health} => {u.Health} pts.)");
        }

        public override string ToString()
        {
            return $"{Name} ({Health})";
        }
    }
}
