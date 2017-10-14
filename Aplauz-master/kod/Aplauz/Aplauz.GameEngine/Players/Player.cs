using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    public abstract class Player
    {
        private List<Coin> coins = new List<Coin>();
        public string name { get; }

        public int prestige { get; }

        public virtual List<Move> PossibleMoves { get; }

        public Player(string name)
        {
            this.name = name;
        }

       

        public virtual string Entry()
        {
            return "dunno what to write here";
        }

        public void AddCoin(Coin coin) //adds one coin of specific Color
        {
            coins.Add(coin);
        }

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
