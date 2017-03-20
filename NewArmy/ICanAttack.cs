using NewArmy.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy
{
    interface ICanAttack
    {
        void Attack(Unit w);
    }
}
