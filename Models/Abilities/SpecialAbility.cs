using System;
using System.Collections.Generic;
using System.Text;

namespace TheBraveHero.Models.Abilities
{
    class SpecialAbility
    {
        private string name;
        private int activationChance;
        private string description;
        private AbilityType abilityType;
        private bool isActive;

        public SpecialAbility(string theName, int theActivationChance, string theDescription, AbilityType theAbilityType, bool theIsActive)
        {
            this.name = theName;
            this.activationChance = theActivationChance;
            this.description = theDescription;
            this.abilityType = theAbilityType;
            this.isActive = theIsActive;
        }

        public string Name { get => name; set => name = value; }
        public int ActivationChance { get => activationChance; set => activationChance = value; }
        public string Description { get => description; set => description = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        internal AbilityType AbilityType { get => abilityType; set => abilityType = value; }
    }
}
