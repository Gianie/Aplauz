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

        public override string  Entry(List<Coin> coinsOnBoard, List<List<Mine>> minesOnBoard)
        {
            Console.WriteLine("time for " + Name + " move");
            return RandomMove(coinsOnBoard, minesOnBoard);
        }

        public string RandomMove(List<Coin> coinsOnBoard, List<List<Mine>> minesOnBoard)
        {
            Random random = new Random();
            List<Move> moves = new List<Move>();
            Move move = new Move();
            foreach (Move.PossibleMoves code in Enum.GetValues(typeof(Move.PossibleMoves)))
            {
                string moveCode = code.ToString();
                move = new Move(moveCode);
                if( Move.IsMovePossible(move, this, coinsOnBoard, minesOnBoard))
                {
                    moves.Add(new Move(moveCode));
                }
            }
            string rand = moves[random.Next(moves.Count)].MoveCode;
            return rand;
        }
    }
}
