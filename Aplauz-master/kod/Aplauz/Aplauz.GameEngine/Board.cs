using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aplauz.GameEngine.Drawers;
using Aplauz.GameEngine.Players;


namespace Aplauz.GameEngine
{
    public class Board
    {
        List<Coin> coins = new List<Coin>();
        List<Coin> coinsLifted = new List<Coin>();
        List<Player> players = new List<Player>();
        List<Mine> mines = new List<Mine>();
        private int currentPlayer = 0;
        private int turn = 0;
        private IDrawer _drawer;


        public Board(string[] names)
        {

            _drawer = new Drawer();
            StartNewGame(names);
            
        }

        private void StartNewGame(string[] args)
        {
 
            PopulatePlayers(4, args);
            PopulateCoins();

            foreach (var player in players)
            {
                Console.WriteLine(player.name);
            }
           // Console.WriteLine(players[0].Entry());

            while (true)
            {
                foreach (var player in players)
                {
                    if (player.GetType() == typeof(HumanPlayer))
                    {
                        _drawer.Draw(players, coins);
                    }
                    string moveCode = String.Empty;
                    bool legal = false;
                    while (!legal)
                    {
                        moveCode = player.Entry();
                        legal = IsMoveLegal(moveCode);
                        if (!legal)
                        {
                            Console.WriteLine("move not legal");
                        }
                    }
                    //check if move is legal
                    if (moveCode[0].ToString() == Move.TakeCoins.Shortcut)
                    {
                        string coinsCodes = moveCode.Substring(1);
                        GiveCoins(Regex.Split(coinsCodes, string.Empty), player);

                    }
                    else if (moveCode == Move.DrawBoard.Shortcut)
                    {
                        
                    }
                    
                }

            }

            _drawer.Draw(players,coins);

            Console.ReadLine();
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
                HumanPlayer p = new HumanPlayer(names[i]);
                players.Add(p);
            }

        }

        private bool GiveCoins(string[] codes, Player player)
        {
            bool result=true;
            foreach (var code in codes)
            {
                Coin coin = coins.FirstOrDefault(c => c.Code == code);
                if (coins.Remove(coin) && coin != null)
                {
                    player.AddCoin(coin);
                }
                else if (coin == null)
                {
                    result = false;
                }
            }
            return result;
        }

        public bool IsMoveLegal(string codes)
        {
            bool result = false;
            if (codes.Length > 4 || codes == "" || codes.Length < 1)
                return false;
            
            if (codes[0].ToString() != Move.TakeCoins.Shortcut && codes[0].ToString() != Move.TakeMine.Shortcut &&
                codes[0].ToString() != Move.TakeTrader.Shortcut)
                return false;

            if (codes[0].ToString() == Move.TakeCoins.Shortcut)
            {
                codes = codes.Substring(1);
                if (codes.Length <= 3)
                {
                    foreach (var code in codes)
                    {
                        if (code != 'b' && code != 'k' && code != 'w' && code != 'r' && code != 'g')
                            return false;
                    }
                    if (codes.Length == 2 && codes[0] == codes[1])
                        return true;
                    if (codes.Length == 3 && codes[0] != codes[1] && codes[0] != codes[2] && codes[1] != codes[2])
                        return true;

                }
            }
            return result;

        }

        private void LiftCoin(string color)
        {

            Coin c = coins.FirstOrDefault(i => i.Color == color);
            if (coins.Remove(c))
                coinsLifted.Add(c);

            if (coinsLifted.Count() == 3)
            {
                //foreach (var item in coinButtons)
                //{
                //    item.Value.IsEnabled = false;
                //}
                //TakeCoins.IsEnabled = true;
            }
            else if (coinsLifted.Count() == 2)
            {
                if (coinsLifted[0].Color == coinsLifted[1].Color)
                {
                    //foreach (var item in coinButtons)
                    //{
                    //    item.Value.IsEnabled = false;
                    //}
                    //TakeCoins.IsEnabled = true;
                }
                else
                {
                    //string color0 = coinsLifted[0].Color;
                    //string color1 = coinsLifted[1].Color;
                    //coinButtons[color0].IsEnabled = false;
                    //coinButtons[color1].IsEnabled = false;
                }
            }
        }

        private void TakeLiftedCoins()
        {
            foreach (Coin item in coinsLifted)
            {
                Coin c = item;
                //coinsLifted.Remove(c);
                players[currentPlayer].AddCoin(c);
            }
            coinsLifted.Clear();

            //foreach (var item in coinButtons)
            //{
            //    item.Value.IsEnabled = true;
            //}
            //TakeCoins.IsEnabled = false;

            //CountAllCoins();
            NextTurn();
        }

        private void NextTurn() // switches turn to next player
        {
            if (currentPlayer < players.Count - 1)
            {
                //borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Black;
                currentPlayer++;

            }
            else
            {
                //borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Black;
                currentPlayer = 0;
                turn++;
            }
            //labelKolej.Content = players[currentPlayer].name;
           // borders[currentPlayer].BorderBrush = System.Windows.Media.Brushes.Red;
        }


    }
}
