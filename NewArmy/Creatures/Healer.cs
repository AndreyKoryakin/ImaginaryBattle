using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy.Creatures
{
    class Healer : Unit, ICanHeal
    {
        public int _maxHealth = 3;
        public int RecoverPoints = 2;
        public int RecoverUnits = 1;
        public int _initiative = 5;
        
        public Healer(string name)
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

        public void Heal(List<Unit> army)
        {
            Random r = new Random();
            Unit u;
            int points = RecoverPoints, inc, health;

            for (int i = 0; i < RecoverUnits && points > 0; i++)
            {
                var units = army.
                    Where(unit => (unit.Health < unit.MaxHealth) && (unit.Health > 0) && !(unit is ITower))
                    .ToList();
                if (units.Count > 0)
                {
                    u = units[r.Next(units.Count)];
                }
                else
                {
                    Logger.Log($"{this}: No units to heal");
                    break;
                }     

                health = u.Health;
                inc = health + points;
                u.Health += points;
                points = inc - u.Health;
                Logger.Log($"{Name} healed his {u.Name} ({health} => {u.Health})");
            }
        }

        public override string ToString()
        {
            return $"{Name} ({Health})";
        }
    }
}
