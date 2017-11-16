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

    class MonteCarloUpgradePlayer : Player
    {
        string time;
        int howDeep = 5;
        int numberOfSimulations = 1200;
        public MonteCarloUpgradePlayer(string name) : base(name)
        {
            type = "MonteCarloUpgrade";
            time = System.DateTime.Now.ToString("ddMMyyyyHHmmss");
        }

        public MonteCarloUpgradePlayer(Player player) : base(player)
        {

        }

        public override string Entry(Board board)
        {
            Console.WriteLine("time for " + Name + " move. UpgradeMonteCarloPlayer");
            
            string result = StartSimulations(board, numberOfSimulations);


            return result;
        }

        public void DeleteNone(List<MonteCarloUpgradeMove> moves)
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

        public void AddMoves(List<MonteCarloUpgradeMove> moves, Board board)
        {
            foreach (MonteCarloUpgradeMove.PossibleMoves code in Enum.GetValues(typeof(MonteCarloUpgradeMove.PossibleMoves)))
            {
                MonteCarloUpgradeMove move = new MonteCarloUpgradeMove();
                string moveCode = code.ToString();
                move = new MonteCarloUpgradeMove(moveCode);
                if (Move.IsMovePossible(move, this, board.CoinsOnBoard, board.MinesOnBoard))
                {
                    moves.Add(new MonteCarloUpgradeMove(moveCode));
                }
            }
            DeleteNone(moves);
        }

        public void TestMoves(int numberOfSimulations, Board board, List<MonteCarloUpgradeMove> moves, int amountOfTestedMoves)
        {
            Random random = new Random();
            for (int i = 0; i < numberOfSimulations / 2; i++)
            {
                MonteCarloUpgradeBoard boardForSimulation = new MonteCarloUpgradeBoard(board);
                string rand = moves[random.Next(amountOfTestedMoves)].MoveCode;
                double score = boardForSimulation.StartNewGame(this, rand, howDeep);
                foreach (MonteCarloUpgradeMove findMoveCode in moves)
                {
                    if (findMoveCode.MoveCode == rand)
                    {
                        findMoveCode.rate += score;
                        findMoveCode.trys++;
                    }
                }
            }
        }

        public void writeToFile(int bestOf, List<MonteCarloUpgradeMove> moves)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\MonteCarloTests";
            System.IO.Directory.CreateDirectory(path);

            for (int i = 0; i < bestOf; i++)
            {
                File.AppendAllText(path + "\\3Best" + time + ".txt", "Ruch: " + moves[i].MoveCode + " Liczba prob: " + moves[i].trys + " Ocena: " + moves[i].rate + " Ocena/ilosc: " + moves[i].result + Environment.NewLine);

            }
            File.AppendAllText(path + "\\3Best" + time + ".txt", Environment.NewLine);
            File.AppendAllText(path + "\\ChosenMove" + time + ".txt", "Ruch: " + moves[0].MoveCode + " Liczba prob: " + moves[0].trys + " Ocena: " + moves[0].rate + " Ocena/ilosc: " + moves[0].result + Environment.NewLine);
        }

        public void setResult(List<MonteCarloUpgradeMove> moves)
        {
            for (int i = 0; i < moves.Count; i++)
            {
                moves[i].result = (double)moves[i].rate / (double)moves[i].trys;
            }
        }

        public string StartSimulations(Board board, int numberOfSimulations)
        {


            Random random = new Random();
            List<MonteCarloUpgradeMove> moves = new List<MonteCarloUpgradeMove>();
            MonteCarloUpgradeMove move = new MonteCarloUpgradeMove();
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
            Console.WriteLine("Ruch: " + moves[0].MoveCode + " Liczba prob: " + moves[0].trys + " łączna liczba punktów: " + moves[0].rate + " Ocena " + moves[0].result);
            //  writeToFile(bestOf, moves);
            return moves[0].MoveCode;
        }
    }
}
