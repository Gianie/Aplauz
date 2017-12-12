using Aplauz.GameEngine.Algorithms.MonteCarlo;
using Aplauz.GameEngine.Players.MonteCarlo;
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
        string time;
        public MonteCarloPlayer(string name) : base(name)
        {
            type = "MonteCarlo";
            time = System.DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        public MonteCarloPlayer(Player player) : base(player)
        {

        }

        public override string Entry(Board board)
        {
           // Console.WriteLine("time for " + Name + " move. MonteCarloPlayer");
            int numberOfSimulations = 1000;
            string result = StartSimulations(board, numberOfSimulations);


            return result;
        }

        public void DeleteNone(List<MonteCarloMove> moves)
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

        public void AddMoves (List<MonteCarloMove> moves, Board board)
        {
            foreach (MonteCarloMove.PossibleMoves code in Enum.GetValues(typeof(MonteCarloMove.PossibleMoves)))
            {
                MonteCarloMove move = new MonteCarloMove();
                string moveCode = code.ToString();
                move = new MonteCarloMove(moveCode);
                if (Move.IsMovePossible(move, this, board.CoinsOnBoard, board.MinesOnBoard))
                {
                    moves.Add(new MonteCarloMove(moveCode));
                }
            }
            DeleteNone(moves);
        }

        public void TestMoves(int numberOfSimulations, Board board, List<MonteCarloMove> moves, int amountOfTestedMoves)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfSimulations / 2; i++)
            {
                MonteCarloBoard boardForSimulation = new MonteCarloBoard(board);
                string rand = moves[random.Next(amountOfTestedMoves)].MoveCode;
                int score = boardForSimulation.StartNewGame(this.Name, rand);
                foreach (MonteCarloMove findMoveCode in moves)
                {
                    if (findMoveCode.MoveCode == rand)
                    {
                        findMoveCode.wins += score;
                        findMoveCode.trys++;
                    }
                }
            }
        }

        public void writeToFile(int bestOf, List<MonteCarloMove> moves)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\MonteCarloTests";
            System.IO.Directory.CreateDirectory(path);

            for (int i = 0; i < bestOf; i++)
            {
                File.AppendAllText(path + "\\3Best" + time + ".txt", "Ruch: " + moves[i].MoveCode + " Liczba prob: " + moves[i].trys + " liczba wygranych: " + moves[i].wins + " Szansa na wygrana " + moves[i].result + Environment.NewLine);

            }
            File.AppendAllText(path + "\\3Best" + time + ".txt", Environment.NewLine);
            File.AppendAllText(path + "\\ChosenMove" + time + ".txt", "Ruch: " + moves[0].MoveCode + " Liczba prob: " + moves[0].trys + " liczba wygranych: " + moves[0].wins + " Szansa na wygrana " + moves[0].result + Environment.NewLine);    

        }

        public void setResult(List<MonteCarloMove> moves)
        {
            for (int i = 0; i < moves.Count; i++)
            {
                moves[i].result = (double)moves[i].wins / (double)moves[i].trys;
            }
        }

        public string StartSimulations(Board board, int numberOfSimulations)
        {
           

            Random random = new Random();
            List<MonteCarloMove> moves = new List<MonteCarloMove>();
            MonteCarloMove move = new MonteCarloMove();
            AddMoves(moves, board);
            int amountOfTestedMoves = moves.Count;
            TestMoves(numberOfSimulations, board, moves, amountOfTestedMoves);
            setResult(moves);
            moves.Sort((s2, s1) => s1.result.CompareTo(s2.result));

            int bestOf = 3;
            if (moves.Count < 3)
                bestOf = moves.Count;
            TestMoves(numberOfSimulations, board, moves, bestOf);
            setResult(moves);
            for (int i = bestOf; i < moves.Count; i++)
            {
                moves[i].result = 0;
            }
            moves.Sort((s2, s1) => s1.result.CompareTo(s2.result));
            Console.WriteLine("Ruch: " + moves[0].MoveCode + " Liczba prob: " + moves[0].trys + " liczba wygranych: " + moves[0].wins + " Szansa na wygrana " + moves[0].result);
            //  writeToFile(bestOf, moves);
            return moves[0].MoveCode;
        }
    }
}
