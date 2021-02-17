using System;
using TheBraveHero.Data;
using TheBraveHero.Models;

namespace TheBraveHero
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                MagicForest magicForest = new MagicForest(GetCharacters.GetHero(), GetCharacters.GetBeast());
                magicForest.StartGame();
            } while (PlayAgain());
        }

        private static bool PlayAgain()
        {
            Console.WriteLine("Do you want to start another adventure? Yes/No");
            string answer = Console.ReadLine().ToUpper();
            switch (answer)
            {
                case "YES":
                    return true;
                case "NO":
                    Console.WriteLine("Thank you for playing!");
                    return false;
                default:
                    Console.WriteLine("Unfortunately... we could not match your answer! Let's try again!");
                    PlayAgain();
                    break;
            }
            return false;
        }
    }
}
