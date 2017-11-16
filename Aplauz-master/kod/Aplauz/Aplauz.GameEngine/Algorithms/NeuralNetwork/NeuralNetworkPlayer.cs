﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    class NeuralNetworkPlayer : Player
    {

        public NeuralNetworkPlayer(string name) : base(name)
        {
            type = "RandomPlayer";
        }
        public NeuralNetworkPlayer(Player player) : base(player)
        {

        }


        public override string Entry(Board board)
        {
            //    Console.WriteLine("time for " + Name + " move. RandomPlayer" );
            return NeuralNetworkMove(board);
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

        public string NeuralNetworkMove(Board board)
        {
//            List<Coin> coinsOnBoard = board.CoinsOnBoard;
//            List<List<Mine>> minesOnBoard = board.MinesOnBoard;
//
//            Random random = new Random();
//            List<Move> moves = new List<Move>();
//            Move move = new Move();
//            foreach (Move.PossibleMoves code in Enum.GetValues(typeof(Move.PossibleMoves)))
//            {
//                string moveCode = code.ToString();
//                move = new Move(moveCode);
//                if (Move.IsMovePossible(move, this, coinsOnBoard, minesOnBoard))
//                {
//                    moves.Add(new Move(moveCode));
//                }
//            }
//
//            DeleteNone(moves);
//            string rand = moves[random.Next(moves.Count)].MoveCode;
//            return rand;
                //TODO dodac lsite possiblemoves i stangry 
            int move = run_cmd("C:/Program Files/Python36/python.exe", "C:/Users/Ewa/PycharmProjects/SiecNeuronowaInzynierka/Network-in-runtime.py 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26 8,8,8,8,8,1,2,0,2,1,0,0,0,1,2,0,1,3,1,0,0,1,3,0,2,1,1,0,1,1,1,0,0,1,3,1,0,2,0,2,0,0,1,4,2,2,0,2,0,0,0,5,0,2,2,2,0,5,3,0,0,2,1,2,5,3,0,0,0,3,3,4,0,3,6,3,0,3,1,4,6,3,0,0,3,3,4,5,0,0,0,7,3,3,0,3,0,3,3,5,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
           // string a = (Move.PossibleMoves) move;
            return Enum.GetName(typeof(Move.PossibleMoves), move);
        }

        private int run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = cmd;//cmd is full path to python.exe
            start.Arguments = args;//args is path to .py file and any cmd line args
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    //Console.Write(result);
                    string Text = "Wybierz ruch: ";
                    int a = result.IndexOf(Text);
                    a += Text.Length;
                    string wynik = result.Substring(a, result.Length - a - 4);
                    int ruch = Int32.Parse(wynik);
                   // Console.WriteLine(wynik);
                    return ruch;
                }
            }
        }


    }
}
