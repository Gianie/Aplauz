using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplauz.GameEngine.Players
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name) : base(name)
        {
            type = "Human";
        }


        public override List<Move> PossibleMoves { get; } = new List<Move>()
        {
            Move.TakeCoins,
            Move.TakeMine
        };

        public override string Entry(Board board)
        {
            List<Coin> coinsOnBoard = board.CoinsOnBoard;
            List<List<Mine>> minesOnBoard = board.MinesOnBoard;
            
            Console.WriteLine("time for " + Name + " move");
            foreach (var move in PossibleMoves)
            {
                Console.WriteLine(move.Shortcut + " for: " + move.Name);
            }
            string selectedMove;
            selectedMove = Console.ReadLine();

            if (PossibleMoves.FirstOrDefault(m => m.Name == "Take Coins").Shortcut == selectedMove)
            {
                string message = selectedMove + Console.ReadLine();
                return message;
            }
            else if (PossibleMoves.FirstOrDefault(m => m.Name == "Take Mine").Shortcut == selectedMove)
            {
                string message = selectedMove + Console.ReadLine();
                return message;
            }
            else
            {
                return String.Empty;
            }

        }


    }
}
