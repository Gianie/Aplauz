using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    class RandomPlayer : Player
    {
        private List<Coin> coins = new List<Coin>();

        private int Prestige;

        public RandomPlayer(string name) : base(name)
        {
        }

        public override string Entry()
        {
            Console.WriteLine("time for " + Name + " move");
            foreach (var move in PossibleMoves)
            {
                Console.WriteLine(move.Shortcut + " for: " + move.Name);
            }
            string selectedMove;
            selectedMove = RandomMove();
            return selectedMove;
            }
        public string RandomMove()
        {
            return String.Empty;
        }
    }
}
