using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplauz.GameEngine.Players
{
    public class HumanPlayer : Player
    {
        private List<Coin> coins = new List<Coin>();
        public string name { get; }

        private int prestige;

        public HumanPlayer(string name) : base(name)
        {
            this.name = name;
        }
        

        public override List<Move> PossibleMoves { get; } = new List<Move>()
        {
            Move.TakeCoins,
            Move.TakeMine,
            Move.TakeTrader,
            Move.DrawBoard
        };

        public override string Entry()
        {
            Console.WriteLine("time for " +  name + " move");
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
                //take mine
                return String.Empty;
            }
            else if (PossibleMoves.FirstOrDefault(m => m.Name == "Take Trader").Shortcut == selectedMove)
            {
                //take trader
                return String.Empty;
            }
            else if (PossibleMoves.FirstOrDefault(m => m.Name == "Draw Board").Shortcut == selectedMove)
            {
                return Move.DrawBoard.Shortcut;
            }
            else
            {
                return String.Empty;
            }

        }

        //public HumanPlayer(string name)
        //{
        //    this.name = name;
        //}

        public void AddCoin(Coin coin) //adds one coin of specific Color
        {
            coins.Add(coin);
        }

        //public void AddCoin(string Color, int quantity) //adds multiple coins of specific Color
        //{

        //}

        //public Coin RemoveCoin(string Color)
        //{

        //}

        public int CountCoins(string color) // counts coins of specific Color
        {
            int count = coins.Where(i => i.Color == color).Count();
            return count;
        }

        public int CountCoins(bool withGold) //counts all coins
        {
            int count;
            count = CountCoins("white");
            count += CountCoins("blue");
            count += CountCoins("green");
            count += CountCoins("black");
            count += CountCoins("red");
            if (withGold == true)
                count += CountCoins("gold");

            return count;
        }


    }
}
