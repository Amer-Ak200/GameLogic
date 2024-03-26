using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Champion
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public string SpecialAbility { get; set; }

        // Constructor to initialize a new Champion
        public Champion(string name, int strength, string specialAbility)
        {
            Name = name;
            Strength = strength;
            SpecialAbility = specialAbility;
        }

        // Additional methods or properties related to the champion can be added here
    }

}
