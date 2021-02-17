using System.Collections.Generic;
using TheBraveHero.Models.Abilities;

namespace TheBraveHero.Models.Characters
{
    abstract class Character
    {
        private string name;
        private int life;
        private int strength;
        private int defence;
        private int speed;
        private int luck;

        public Character(string theName, int theLife, int theStrength, int theDefence, int theSpeed, int theLuck)
        {
            this.Name = theName;
            this.Life = theLife;
            this.Strength = theStrength;
            this.Defence = theDefence;
            this.Speed = theSpeed;
            this.Luck = theLuck;
        }

        public string Name { get => name; set => name = value; }
        public int Life { get => life; set => life = value; }
        public int Strength { get => strength; set => strength = value; }
        public int Defence { get => defence; set => defence = value; }
        public int Speed { get => speed; set => speed = value; }
        public int Luck { get => luck; set => luck = value; }
    }

    class Hero : Character
    {
        private List<SpecialAbility> specialAbilities;

        public Hero(string theName, int theLife, int theStrength, int theDefence, int theSpeed, int theLuck, List<SpecialAbility> theSpecialAbilities) :
            base(theName, theLife, theStrength, theDefence, theSpeed, theLuck)
        {
            this.SpecialAbilities = theSpecialAbilities;
        }

        public List<SpecialAbility> SpecialAbilities { get => specialAbilities; set => specialAbilities = value; }
    }

    class Beast : Character
    {
        public Beast(string beastName, int beastLife, int beastStrength, int beastDefence, int beastSpeed, int beastLuck) :
            base(beastName, beastLife, beastStrength, beastDefence, beastSpeed, beastLuck)
        { }
    }
}
