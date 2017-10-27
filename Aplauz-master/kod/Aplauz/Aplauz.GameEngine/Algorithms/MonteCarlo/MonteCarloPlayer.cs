using Aplauz.GameEngine.Algorithms.MonteCarlo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{

    class MonteCarloPlayer : Player
    {
        public MonteCarloPlayer(string name) : base(name)
        {
            type = "MonteCarlo";
        }

        //public override string Entry(State state)
        //{
        //    int numberOfSimulations = 100;
        //    string result = StartSimulations(state, numberOfSimulations);
        //    Console.WriteLine("time for " + Name + " move");

        //    return result;
        //}

        public override string Entry(Board board)
        {
            int numberOfSimulations = 100;
            string result = StartSimulations(board, numberOfSimulations);
            Console.WriteLine("time for " + Name + " move");

            return result;
        }



        public string StartSimulations(Board board, int numberOfSimulations)
        {
           

            Random random = new Random();
            List<MonteCarloMove> moves = new List<MonteCarloMove>();
            MonteCarloMove move = new MonteCarloMove();
            foreach (MonteCarloMove.PossibleMoves code in Enum.GetValues(typeof(MonteCarloMove.PossibleMoves)))
            {
                string moveCode = code.ToString();
                move = new MonteCarloMove(moveCode);
                if (Move.IsMovePossible(move, this, board.CoinsOnBoard, board.MinesOnBoard))
                {
                    moves.Add(new MonteCarloMove(moveCode));
                }
            }


            for ( int i=0; i < numberOfSimulations; i++)
            {

                //  Board boardForSimulation = new Board(board); // tutaj trzeba zrobic konstruktor kopiujacy
                string rand = moves[random.Next(moves.Count)].MoveCode;
                int score = 0;
                //score = MonteCarlo.MonteCarloBoard(board);
                foreach (MonteCarloMove findMoveCode in moves)
                {
                    if (findMoveCode.MoveCode == rand)
                    {
                        findMoveCode.wins += score;
                        findMoveCode.trys++;
                    }
                }
            }

            moves.Sort((s2, s1) => s1.wins.CompareTo(s2.wins));

            return moves[0].MoveCode;
        }
    }
}
