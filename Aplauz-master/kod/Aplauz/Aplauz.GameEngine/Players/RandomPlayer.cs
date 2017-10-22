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
            return RandomMove();
        }

        public string RandomMove()
        {
            List<Move> moves = new List<Move>();

            return String.Empty;
        }
    }
}
