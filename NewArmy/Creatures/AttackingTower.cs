using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy.Creatures
{
    class AttackingTower : Unit, ITower, ICanAttack
    {
        public int _maxHealth = 30;
        public int _initiative = 1;
        public int Power = 20;

        public AttackingTower(string name)
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

        public void Attack(Unit u)
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
