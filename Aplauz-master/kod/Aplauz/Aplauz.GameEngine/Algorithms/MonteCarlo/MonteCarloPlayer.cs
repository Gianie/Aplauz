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

        public static int numberOfSimulations = 100;//Do testow//Do testow
        public static int numberOfDoneSimulations = 0;//Do testow//Do testow
        string time;
        public MonteCarloPlayer(string name) : base(name)
        {
            type = "MonteCarlo";
            time = System.DateTime.Now.ToString("ddMMyyyyHHmmss");
            if (numberOfDoneSimulations >= 10)//Do testow//Do testow
            {//Do testow//Do testow
                numberOfSimulations += 100;//Do testow//Do testow
                numberOfDoneSimulations = 0;//Do testow//Do testow
            }//Do testow//Do testow
            numberOfDoneSimulations++;
        }

        public override string Entry(Board board)
        {
            Console.WriteLine("time for " + Name + " move");
            
            string result = StartSimulations(board, numberOfSimulations);


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


            for ( int i=0; i < numberOfSimulations/2; i++)
            {
                MonteCarloBoard boardForSimulation = new MonteCarloBoard(board);
                string rand = moves[random.Next(moves.Count)].MoveCode;
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


            moves.Sort((s2, s1) => s1.result.CompareTo(s2.result));

            int bestOf = 3;
            if (moves.Count < 3)
                bestOf = moves.Count;

            for (int i = 0; i < numberOfSimulations / 2; i++)
            {
                MonteCarloBoard boardForSimulation = new MonteCarloBoard(board);
                string rand = moves[random.Next(bestOf)].MoveCode;
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
            for (int i = 0; i < moves.Count; i++)
            {
                moves[i].result = (double)moves[i].wins / (double)moves[i].trys;
            }  
            for (int i = 3; i < moves.Count; i++)
            {
                moves[i].result = 0;
            }
            string path = System.IO.Directory.GetCurrentDirectory() + "\\MonteCarloTests";
            System.IO.Directory.CreateDirectory(path);
            System.IO.Directory.CreateDirectory(path+"\\3Best");
            System.IO.Directory.CreateDirectory(path+"\\ChosenMove");
            System.IO.Directory.CreateDirectory(path);

            moves.Sort((s2, s1) => s1.result.CompareTo(s2.result));
            for (int i = 0; i < bestOf; i++)
            {
                File.AppendAllText(path + "\\3Best\\" + time + ".xls",numberOfSimulations +"\t"+ moves[i].MoveCode + "\t" + moves[i].trys + "\t" + moves[i].wins + "\t" + moves[i].result + Environment.NewLine);

            }


            File.AppendAllText(path + "\\3Best\\" + time + ".xls", Environment.NewLine);
            File.AppendAllText(path + "\\ChosenMove\\" + time + ".xls", numberOfSimulations + "\t" + moves[0].MoveCode + "\t" + moves[0].trys + "\t" + moves[0].wins + "\t" + moves[0].result + Environment.NewLine);


            Console.WriteLine("Ruch: " + moves[0].MoveCode + " Liczba prob: " + moves[0].trys + " liczba wygranych: " + moves[0].wins + " Szansa na wygrana " + moves[0].result);
            return moves[0].MoveCode;

            //for (int i = 0; i < bestOf; i++)
            //{
            //    File.AppendAllText(path+ "\\3Best" + time + ".txt", "Ruch: " + moves[i].MoveCode + " Liczba prob: " + moves[i].trys + " liczba wygranych: " + moves[i].wins + " Szansa na wygrana " + moves[i].result + Environment.NewLine);

            //}
            //File.AppendAllText(path + "\\3Best" + time + ".txt", Environment.NewLine);
            //File.AppendAllText(path + "\\ChosenMove" + time + ".txt", "Ruch: " + moves[0].MoveCode + " Liczba prob: " + moves[0].trys + " liczba wygranych: " + moves[0].wins + " Szansa na wygrana " + moves[0].result + Environment.NewLine);

            //moves.Sort((s2, s1) => s1.result.CompareTo(s2.result));
            //Console.WriteLine("Ruch: " + moves[0].MoveCode +" Liczba prob: " + moves[0].trys + " liczba wygranych: " + moves[0].wins + " Szansa na wygrana " + moves[0].result);
            //return moves[0].MoveCode;
        }
    }
}
