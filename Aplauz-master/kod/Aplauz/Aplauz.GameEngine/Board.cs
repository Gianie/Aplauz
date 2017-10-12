using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class Board
    {
        List<Coin> coins = new List<Coin>();
        List<Coin> coinsLifted = new List<Coin>();
        List<Player> players = new List<Player>();
        List<Mine> mines = new List<Mine>();
        private int currentPlayer = 0;
        private int playersCount = 4;
        private int turn = 0;
       



        public void StartNewGame(string[] args)
        {
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
            Console.ReadLine();
            PopulatePlayers(4, args);

        }
        private void PopulateCoins()
        {
            for (int i = 0; i < 7; i++)
            {

                coins.Add(new Coin("blue"));
                coins.Add(new Coin("green"));
                coins.Add(new Coin("black"));
                coins.Add(new Coin("red"));
                coins.Add(new Coin("white"));
            }
            for (int i = 0; i < 5; i++)
            {
                coins.Add(new Coin("gold"));
            }
            //CountAllCoins();
        }

        private void PopulatePlayers(int quantity, string[] names)
        {
            for (int i = 0; i < quantity; i++)
            {
                Player p = new Player(names[i]);
                players.Add(p);
            }

        }
    }
}
