using System;
using System.Collections.Generic;
using TheBraveHero.Models.Abilities;
using TheBraveHero.Models.Characters;

namespace TheBraveHero.Data
{
    class GetCharacters
    {
        private static Random rdm = new Random();

        public static Hero GetHero()
        {
            List<SpecialAbility> specialAbilities = new List<SpecialAbility>();
            specialAbilities.Add(new SpecialAbility("Dragon's Force", 10, "Hero strength doubles!", AbilityType.DAMAGE_INCREASE, false));
            specialAbilities.Add(new SpecialAbility("Magic Shield", 20, "Next attack damage will be reduced by half!", AbilityType.DEFENCE_INCREASE, false));
            return new Hero("Carl",
                (65 + rdm.Next(95 - 65 + 1)),
                (60 + rdm.Next(70 - 60 + 1)),
                (40 + rdm.Next(50 - 40 + 1)),
                (40 + rdm.Next(50 - 40 + 1)),
                (10 + rdm.Next(30 - 10 + 1)),
                specialAbilities
                );
        }

        public static Beast GetBeast()
        {
            List<Beast> beasts = new List<Beast>();
            return new Beast("Beast",
                    (55 + rdm.Next(80 - 55 + 1)),
                    (50 + rdm.Next(80 - 50 + 1)),
                    (35 + rdm.Next(55 - 35 + 1)),
                    (40 + rdm.Next(60 - 40 + 1)),
                    (25 + rdm.Next(40 - 25 + 1)));
        }
    }
}
