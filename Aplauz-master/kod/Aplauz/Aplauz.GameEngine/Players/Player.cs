using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    public abstract class Player
    {
        private List<Coin> coins = new List<Coin>();
        private List<Mine> mines = new List<Mine>();
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
            var count = coins.Count(i => i.Color == color);
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

        public void AddMine(Mine mine)
        {
            mines.Add(mine);
        }

        public List<Coin> RemoveCoins(Dictionary<string,int> dicCoins)
        {
            List<Coin> resultList = new List<Coin>();
            foreach (var dicElement in dicCoins)
            {
                string code = dicElement.Key;
                for (int i = 0; i < dicElement.Value; i++)
                {
                    Coin coin = coins.First(c => c.Code == code);
                    coins.Remove(coin);
                    resultList.Add(coin);
                }
            }
            return resultList;
        }

    }
}
