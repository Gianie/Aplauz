using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplauz.GameEngine.Players
{
    public class HumanPlayer : Player
    {
        private List<Coin> coins = new List<Coin>();

        private int Prestige;

        public HumanPlayer(string name) : base(name)
        {
        }
        

        public override List<Move> PossibleMoves { get; } = new List<Move>()
        {
            Move.TakeCoins,
            Move.TakeMine
        };

        public override string Entry()
        {
            Console.WriteLine("time for " +  Name + " move");
            foreach (var move in PossibleMoves)
            {
                Console.WriteLine(move.Shortcut + " for: " + move.Name );
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
