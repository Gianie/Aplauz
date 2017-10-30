using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    class RandomPlayer : Player
    {

        public RandomPlayer(string name) : base(name)
        {
        }

        public RandomPlayer(Player player) : base(player)
        {
        }

        public override string  Entry(Board board)
        {
            Console.WriteLine("time for " + Name + " move");
            return RandomMove(board);
        }

        public string RandomMove(Board board)
        {
            List<Coin> coinsOnBoard = board.CoinsOnBoard;
            List<List<Mine>> minesOnBoard = board.MinesOnBoard;

            Random random = new Random();
            List<Move> moves = new List<Move>();
            Move move = new Move();
            foreach (Move.PossibleMoves code in Enum.GetValues(typeof(Move.PossibleMoves)))
            {
                string moveCode = code.ToString();
                move = new Move(moveCode);
                if (Move.IsMovePossible(move, this, coinsOnBoard, minesOnBoard))
                {
                    moves.Add(new Move(moveCode));
                }
            }
            string rand = moves[random.Next(moves.Count)].MoveCode;
            return rand;
        }
    }
}
