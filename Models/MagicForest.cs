using System;
using TheBraveHero.Models.Abilities;
using TheBraveHero.Models.Characters;

namespace TheBraveHero.Models
{
    class MagicForest
    {

        private Hero hero;
        private Beast beast;
        private int round = 1;
        private Random rdm = new Random();
        private bool turn;
        private bool gameOn;

        public MagicForest(Hero theHero, Beast theBeast)
        {
            this.hero = theHero;
            this.beast = theBeast;
        }

        internal void StartGame()
        {
            PrintGameIntroduction();
            PrintFightIntroduction();
            turn = IsHeroFirst();
            PrintInitialStats();
            Console.WriteLine("Are you ready start the fight?");
            RetreatFromBattle();
            PrintFirstAttacker();
            while (gameOn && round < 21 && hero.Life > 0 && beast.Life > 0)
            {
                Console.WriteLine("Round " + round + " starts!");
                SetHeroAbilities();
                int damage = playRound();
                PrintRoundStatistics(damage);
                ResetHeroAbilities();
                turn = !turn;
                round++;
                if (round < 21 && hero.Life > 0 && beast.Life > 0)
                {
                    Console.WriteLine("Continue?");
                    RetreatFromBattle();
                }
            }
            if (hero.Life <= 0)
            {
                PrintDefeat();
            }
            else if (beast.Life <= 0)
            {
                PrintVictory();
            }
            else if (round > 20)
            {
                PrintTie();
            }
        }


        private bool IsHeroFirst()
        {
            if (hero.Speed > beast.Speed)
            {
                return true;
            }
            else if (hero.Speed == beast.Speed)
            {
                return hero.Luck > beast.Luck;
            }
            else
            {
                return false;
            }
        }

        private void SetHeroAbilities()
        {
            foreach (SpecialAbility specialAbility in hero.SpecialAbilities)
            {
                if (1 + rdm.Next(100) <= specialAbility.ActivationChance)
                {
                    specialAbility.IsActive = true;
                }
            }
        }

        private void ResetHeroAbilities()
        {
            foreach (SpecialAbility specialAbility in hero.SpecialAbilities)
            {
                specialAbility.IsActive = false;
            }
        }

        private int InitiateAttack(int strength, int defence)
        {
            int damage = strength - defence;
            if (damage > 0 && damage <= 100)
            {
                return damage;
            }
            else if (damage > 100)
            {
                return 100;
            }
            else
            {
                return 0;
            }

        }

        private bool Defend(int luck)
        {
            return (1 + rdm.Next(100) <= luck);
        }

        private int playRound()
        {
            int damage;
            if (turn)
            {
                Console.WriteLine(hero.Name + " grabs his sword and attacks!");
                int initialStrength = hero.Strength;
                foreach (SpecialAbility specialAbility in hero.SpecialAbilities)
                {
                    if (specialAbility.IsActive && specialAbility.AbilityType.Equals(AbilityType.DAMAGE_INCREASE))
                    {
                        hero.Strength *= 2;
                    }
                }
                if (Defend(beast.Luck))
                {
                    Console.WriteLine(beast.Name + " dodges the attack!");
                    damage = 0;
                }
                else
                {
                    damage = InitiateAttack(hero.Strength, beast.Defence);
                }
                beast.Life -= damage;
                hero.Strength = initialStrength;
            }
            else
            {
                Console.WriteLine(beast.Name + " rapidly charges our hero and attacks!");
                if (Defend(hero.Luck))
                {
                    damage = 0;
                    Console.WriteLine(hero.Name + " dodges the attacks!");
                }
                else
                {
                    damage = InitiateAttack(beast.Strength, hero.Defence);
                }
                foreach (SpecialAbility specialAbility in hero.SpecialAbilities)
                {
                    if (specialAbility.IsActive && specialAbility.AbilityType.Equals(AbilityType.DEFENCE_INCREASE))
                    {
                        damage /= 2;
                    }
                }
            }
            return damage;
        }

        private void RetreatFromBattle()
        {
            Console.WriteLine("Type YES/NO");
            String answer = Console.ReadLine().ToUpper();
            switch (answer)
            {
                case "YES":
                    gameOn = true;
                    break;
                case "NO":
                    PrintDefeat();
                    gameOn = false;
                    break;
                default:
                    Console.WriteLine("Unfortunately... we could not match your answer! Let's try again!");
                    RetreatFromBattle();
                    break;
            }
        }

        private void PrintInitialStats()
        {
            Console.WriteLine("\t*** GAME STATS ***");
            Console.WriteLine("# " + hero.Name + "'s life: " + hero.Life);
            Console.WriteLine("# " + hero.Name + "'s strength: " + hero.Strength);
            Console.WriteLine("# " + hero.Name + "'s defence: " + hero.Defence);
            Console.WriteLine("# " + hero.Name + "'s speed: " + hero.Speed);
            Console.WriteLine("# " + hero.Name + "'s luck: " + hero.Luck);
            Console.WriteLine();
            Console.WriteLine("# " + beast.Name + "'s life: " + beast.Life);
            Console.WriteLine("# " + beast.Name + "'s strength: " + beast.Strength);
            Console.WriteLine("# " + beast.Name + "'s defence: " + beast.Defence);
            Console.WriteLine("# " + beast.Name + "'s speed: " + beast.Speed);
            Console.WriteLine("# " + beast.Name + "'s luck: " + beast.Luck);
            Console.WriteLine("\t*** GAME STATS ***");

        }

        private void PrintRoundStatistics(int damage)
        {
            foreach (SpecialAbility specialAbility in hero.SpecialAbilities)
            {
                if (specialAbility.IsActive &&
                        specialAbility.AbilityType.Equals(AbilityType.DAMAGE_INCREASE) &&
                        turn)
                {
                    Console.WriteLine("*** " + specialAbility.Name + " was active this round!");
                    Console.WriteLine("*** " + specialAbility.Description);
                }
                else if (specialAbility.IsActive &&
                      specialAbility.AbilityType.Equals(AbilityType.DEFENCE_INCREASE) &&
                      !turn)
                {
                    Console.WriteLine("*** " + specialAbility.Name + " was active this round!");
                    Console.WriteLine("*** " + specialAbility.Description);
                }
            }
            if (turn)
            {
                Console.WriteLine("# " + beast.Name + " suffers a damage of " + damage + "!");
                Console.WriteLine("# " + beast.Name + "'s remaining life is " + beast.Life + "!");
            }
            else
            {
                Console.WriteLine("# " + hero.Name + " suffers a damage of " + damage + "!");
                Console.WriteLine("# " + hero.Name + "'s remaining life is " + hero.Life + "!");
            }
            Console.WriteLine("Round " + round + " ends!");
        }

        private void PrintFirstAttacker()
        {
            if (turn && round == 1 && gameOn)
            {
                Console.WriteLine("***" + hero.Name + " is faster than " + beast.Name + " and strikes first!***");
            }
            else if (!turn && round == 1 && gameOn)
            {
                Console.WriteLine("***" + beast.Name + " is faster than " + hero.Name + " and strikes first!***");
            }
        }

        private void PrintFightIntroduction()
        {
            Console.WriteLine("___________________________________");
            Console.WriteLine("| Welcome to The Brave Hero game! |");
            Console.WriteLine("___________________________________");
            Console.WriteLine("The sun is rising, a beautiful day starts!");
            Console.WriteLine("Another adventure awaits just around the corner...");
            Console.WriteLine("Which hero will depart into the Magic Forest to fight with unbelievable monsters?");
        }

        private void PrintVictory()
        {
            Console.WriteLine("***VICTORY!***");
            Console.WriteLine(hero.Name + ", is victorious again!");
            Console.WriteLine("No beast is a match for our brave hero!");
        }

        private void PrintDefeat()
        {
            Console.WriteLine("***DEFEAT***");
            Console.WriteLine("Unfortunately the beast was too powerful and " + hero.Name + " flee away in pain.");
            Console.WriteLine("Surely, after recovering " + hero.Name + " will send the beast right away from where it came!");
            Console.WriteLine("But this adventure is for another day!");
        }

        private void PrintTie()
        {
            Console.WriteLine("***TIE***");
            Console.WriteLine("Seems like this beast is truly a match for " + hero.Name + "!");
            Console.WriteLine("Unfortunately, this fight is over, it has to have end!");
        }

        private void PrintGameIntroduction()
        {
            Console.WriteLine("___________________________________");
            Console.WriteLine("| Welcome to The Brave Hero game! |");
            Console.WriteLine("___________________________________");
            Console.WriteLine("The sun is rising, a beautiful day starts!");
            Console.WriteLine("Another adventure awaits just around the corner...");
            Console.WriteLine("Which hero will depart into the Magic Forest to fight with unbelievable monsters?");
        }

    }
}
