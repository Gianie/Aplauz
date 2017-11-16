using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    class DynamicGreedyPlayer : Player
    {
        public DynamicGreedyPlayer(string name) : base(name)
        {
            type = "DynamicGreedy";
        }
        public DynamicGreedyPlayer(Player player) : base(player)
        {

        }

        public override string Entry(Board board)
        {
        //    Console.WriteLine("time for " + Name + " move. DynamicGreedyPlayer");
            return ChoseMove(board);
        }
        public void DeleteNone(List<Move> moves)
        {
            if (moves.Count > 1)
            {
                for (int i = 0; i < moves.Count; i++)
                {
                    if (moves[i].Shortcut == "n")
                    {
                        moves.Remove(moves[i]);
                    }
                }
            }
        }
        public void AddPosibleMoves(Board board, Move move, List<Move> moves)
        {
            List<Coin> coinsOnBoard = board.CoinsOnBoard;
            List<List<Mine>> minesOnBoard = board.MinesOnBoard;

            foreach (Move.PossibleMoves code in Enum.GetValues(typeof(Move.PossibleMoves)))
            {
                string moveCode = code.ToString();
                move = new Move(moveCode);
                if (Move.IsMovePossible(move, this, coinsOnBoard, minesOnBoard))
                {
                    moves.Add(new Move(moveCode));
                }
            }
            DeleteNone(moves);
        }

        public string ChoseMove(Board board)
        {
            Random random = new Random();
            List<Move> moves = new List<Move>();
            Move move = new Move();

            AddPosibleMoves(board, move, moves);

            if (this.CountAllMines() <= 3)
            {
                if (IsAnyMinePossibleToBuy(moves))
                {
                    move = GetAnyMine(moves);
                    return move.MoveCode;
                }
            }
            if (IsMineTwoOrThreePossibleToBuy(moves))
            {
                move = GetMineTwoOrThree(moves);
                return move.MoveCode;
            }

            string rand = moves[random.Next(moves.Count)].MoveCode;
            return rand;
        }

        public bool IsAnyMinePossibleToBuy(List<Move> moves)
        {
            bool result = false;

            foreach (Move eachMove in moves)
            {
                if (eachMove.Shortcut == "m")
                {
                    result = true;
                }
            }
            return result;
        }

        public Move GetAnyMine(List<Move> moves) //funkcja zwracajaca ruch ktory jest kupieniem pierwszje mozliwej kopalni
        {
            Move move = new Move();

            foreach (Move eachMove in moves)
            {
                if (eachMove.Shortcut == "m")
                {
                    move = eachMove;
                }
            }
            return move;
        }

        public bool IsMineTwoOrThreePossibleToBuy(List<Move> moves)
        {
            bool result = false;

            foreach (Move eachMove in moves)
            {
                if ((eachMove.Shortcut != "n") && (eachMove.MoveCode[1].ToString() == "2" || eachMove.MoveCode[1].ToString() == "3"))
                {
                    result = true;
                }
            }
            return result;
        }


        public Move GetMineTwoOrThree(List<Move> moves) //funkcja zwracajaca ruch ktory jest kupieniem pierwszje mozliwej kopalni
        {
            Move move = new Move();

            foreach (Move eachMove in moves)
            {
                if ((eachMove.Shortcut != "n") && (eachMove.MoveCode[1].ToString() == "2" || eachMove.MoveCode[1].ToString() == "3"))
                {
                    move = eachMove;
                }
            }
            return move;
        }

    }
}
