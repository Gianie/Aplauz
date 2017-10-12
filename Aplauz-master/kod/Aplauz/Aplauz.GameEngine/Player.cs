using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class Player
    {
        private List<Coin> coins = new List<Coin>();
        public string name { get; }

        private int prestige;

        public Player(string name)
        {
            this.name = name;
        }

        public void AddCoin(Coin coin) //adds one coin of specific color
        {
            coins.Add(coin);
        }

        //public void AddCoin(string color, int quantity) //adds multiple coins of specific color
        //{

        //}

        //public Coin RemoveCoin(string color)
        //{

        //}

        public int CountCoins(string color) // counts coins of specific color
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
