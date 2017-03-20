using NewArmy.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewArmy
{
    class Program
    {
        static Random r = new Random();
        static int armyLength = 7;
        static int actionsNumber = 100;
        static int actionsNumberChoose = 100;

        static void Main(string[] args)
        {
            Logger.Clear();
            Logger.File = false;

            List<Unit> army1 = GetArmy(armyLength, "#army1"),
                army2 = GetArmy(armyLength, "#army2");

            GetArmyInfo(new List<Unit>[] { army1, army2 });
            Battle(army1, army2);
            GetArmyInfo(new List<Unit>[] { army1, army2 });

            if (army1.Count > army2.Count)
                Logger.Log("First army won!");
            else if (army1.Count < army2.Count)
                Logger.Log("Second army won!");
            else
                Logger.Log("There's no winner :(");

            Console.ReadKey();
        }

        public static List<Unit> GetArmy(int armyLength, string postfix = "")
        {
            List<Unit> army = new List<Unit>();
            for (int i = 0; i < armyLength; i++)
            {
                switch (r.Next(7))
                {
                    case 0:
                        army.Add(new Warrior($"Warrior{i}{postfix}"));
                        break;
                    case 1:
                        army.Add(new Cavalry($"Cavalry{i}{postfix}"));
                        break;
                    case 2:
                        army.Add(new Archer($"Archer{i}{postfix}"));
                        break;
                    case 3:
                        army.Add(new CavalryArcher($"CavalryArcher{i}{postfix}"));
                        break;
                    case 4:
                        army.Add(new Healer($"Healer{i}{postfix}"));
                        break;
                    case 5:
                        army.Add(new HealingTower($"HealingTower{i}{postfix}"));
                        break;
                    case 6:
                        army.Add(new AttackingTower($"AttackingTower{i}{postfix}"));
                        break;
                }
            }

            return army;
        }
        
        public static void GetArmyInfo(List<Unit>[] armies, string message = "")
        {
            if (message != "")
                Logger.Log(message);

            for (int i = 0; i < armies.Length; i++)
            {
                Logger.Log($"*** Army {i + 1} ***");
                for (int j = 0; j < armies[i].Count; j++)
                    Logger.Log($"{j + 1}\t{armies[i][j]}");
                Logger.Log("");
            }
            Logger.Log("\n");
        }

        public static void ClearDeath(List<Unit>[] armies)
        {
            for (int i = 0; i < armies.Length; i++)
                for (int j = 0; j < armies[i].Count; j++)
                    if (armies[i][j].Health <= 0)
                    {
                        armies[i].Remove(armies[i][j]);
                        break;
                    }
        }

        public static void Battle(List<Unit> army1, List<Unit> army2)
        {
            List<Unit> Sequence = new List<Unit>();
            int i, k, // iterator
                j = 0; // We should stop battle if healers will heal continuously
            Random r = new Random();
            Unit u1, u2;
            
            for(j = 0; j < actionsNumber && army1.Count > 0 && army2.Count > 0; j++)
            {
                // Create a stack of all of the units from both armies
                ClearDeath(new List<Unit>[] { army1, army2 });
                Sequence.AddRange(army1);
                Sequence.AddRange(army2);
                ClearDeath(new List<Unit>[] { Sequence });
                // Sort by ascending initiative
                Sequence.Sort();
                GetArmyInfo(new List<Unit>[] { Sequence }, $"\nSorted #{j}");

                for (i = 0; i < Sequence.Count; i++)
                {
                    // Get the first unit
                    u1 = Sequence[i];
                    if (u1.Health <= 0)
                        continue;

                    // Get the second unit
                    k = 0;
                    do
                    {
                        u2 = Sequence[r.Next(Sequence.Count)];
                        k++;
                    }
                    while ((k < actionsNumberChoose) && (((u2.Health <= 0) || (u1 == u2) || (army1.Contains(u1) && army1.Contains(u2)) || (army2.Contains(u1) && army2.Contains(u2)))));

                    if (k == actionsNumberChoose)
                    {
                        Logger.Log($"{u1} can't find a rival");
                        break;
                    }

                    Act(ref u1, ref u2, army1.Contains(u1) ? army1 : army2);
                }

                ClearDeath(new List<Unit>[] { army1, army2 });
                Sequence.Clear();
            }
        }
       
        public static void Act(ref Unit u1, ref Unit u2, List<Unit> army)
        {
            if (u1 is ICanAttack)
                ((ICanAttack)u1).Attack(u2);
            else if (u1 is ICanHeal)
                ((ICanHeal)u1).Heal(army);
        }
    }
}
