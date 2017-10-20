using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aplauz.GameEngine.Drawers;
using Aplauz.GameEngine.Mines;
using Aplauz.GameEngine.Players;


namespace Aplauz.GameEngine
{
    public class Board
    {
        List<Coin> coins = new List<Coin>();
        List<Player> players = new List<Player>();

        private int currentPlayer = 0;
        private int turn = 0;
        private IDrawer _drawer;

        public List<Mine> MinesPack { get; } = new List<Mine>();
        public List<List<Mine>> MinesOnBoard { get; } = new List<List<Mine>>();
        public List<Mine> MinesOnBoardLvl1 { get; } = new List<Mine>();
        public List<Mine> MinesOnBoardLvl2 { get; } = new List<Mine>();
        public List<Mine> MinesOnBoardLvl3 { get; } = new List<Mine>();


        public Board(string[] names)
        {

            _drawer = new Drawer();
            StartNewGame(names);
            
        }

        private void StartNewGame(string[] args)
        {
 
            PopulatePlayers(4, args);
            PopulateCoins();
            PopulateMines();
            RandomizeMissingMines();
            Console.BackgroundColor = ConsoleColor.Gray;
            foreach (var player in players)
            {
                Console.WriteLine(player.Name);
            }

            while (true)
            {
                foreach (var player in players)
                {
                    if (player.GetType() == typeof(HumanPlayer))
                    {
                        _drawer.Draw(players, coins, MinesOnBoard);
                    }
                    string moveCode = String.Empty;
                    bool movePossible = false;
                    while (!movePossible)
                    {
                        moveCode = player.Entry();
                        movePossible = isStringLegal(moveCode,player);
                        if (!movePossible)
                        {
                            Console.WriteLine("string is not legal");
                            continue;

                        }
                        movePossible = Move.IsMovePossible(moveCode, player, coins, MinesOnBoard);
                        if (!movePossible)
                        {
                            Console.WriteLine("Move is not possible. Sorry");
                            continue;
                        }
                        movePossible = true;
                    }
                    if (moveCode[0].ToString() == Move.TakeCoins.Shortcut)
                    {
                        string coinsCodes = moveCode.Substring(1);
                        GiveCoins(coinsCodes.ToCharArray().Select(c => c.ToString()).ToArray(), player); //Tak trzeba Janek jezeli chcemy miec tablicę stringów jednoelementowych :D

                    }
                    else if (moveCode[0].ToString() == Move.TakeMine.Shortcut)
                    {
                        string mineCode = moveCode.Substring(1);
                        GiveMine(mineCode,player);
                    }
                    RandomizeMissingMines();
                }
                turn++;
            }

        }

        private void RandomizeMissingMines()
        {
            int missingCountLvl1 = 4 - MinesOnBoardLvl1.Count;
            Random rnd = new Random();
            for (int i = 0; i < missingCountLvl1; i++)
            {
                var adequateMinesFromPack = MinesPack.Where(m => m.Level == 1).ToList();
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count-1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                MinesPack.Remove(randomizedMine);
                MinesOnBoardLvl1.Add(randomizedMine);
            }
            int missingCountLvl2 = 4 - MinesOnBoardLvl2.Count;
            for (int i = 0; i < missingCountLvl2; i++)
            {
                var adequateMinesFromPack = MinesPack.Where(m => m.Level == 2).ToList();
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                MinesPack.Remove(randomizedMine);
                MinesOnBoardLvl2.Add(randomizedMine);
            }
            int missingCountLvl3 = 4 - MinesOnBoardLvl3.Count;
            for (int i = 0; i < missingCountLvl3; i++)
            {
                var adequateMinesFromPack = MinesPack.Where(m => m.Level == 3).ToList();
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                MinesPack.Remove(randomizedMine);
                MinesOnBoardLvl3.Add(randomizedMine);
            }
        }

        private void PopulateMines()
        {
            MineFactory Mf = new MineFactory();
            MinesPack.AddRange(Mf.GetAllMines());
            MinesOnBoard.Add(MinesOnBoardLvl1);
            MinesOnBoard.Add(MinesOnBoardLvl2);
            MinesOnBoard.Add(MinesOnBoardLvl3);
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

        private void GiveMine(string code, Player player)
        {
            int level = Int32.Parse(code[0].ToString()) -1;
            int number = Int32.Parse(code[1].ToString()) - 1;
            Mine chosenMine = MinesOnBoard[level][number];
            if (chosenMine.Prices["r"] <= player.CountCoins("red") &&
                chosenMine.Prices["w"] <= player.CountCoins("white") &&
                chosenMine.Prices["k"] <= player.CountCoins("black") &&
                chosenMine.Prices["b"] <= player.CountCoins("blue") &&
                chosenMine.Prices["g"] <= player.CountCoins("green"))
            {
                if (MinesOnBoard[level].Remove(chosenMine))
                {
                    player.RemoveCoins(chosenMine.Prices);
                    player.AddMine(chosenMine);
                }
            }
            
            
        }

        public bool isStringLegal(string codes, Player player)
        {
            bool result = false;
            if (codes.Length > 4 || codes == "" || codes.Length < 1)
                return false;
            
            if (codes[0].ToString() != Move.TakeCoins.Shortcut && codes[0].ToString() != Move.TakeMine.Shortcut)
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
            if (codes[0].ToString() == Move.TakeMine.Shortcut)
            {
                codes = codes.Substring(1);
//                Regex regex = new Regex(@"^\d$");
                if (codes.Length != 2)
                    return false;
                int level = Int32.Parse(codes[0].ToString());
                int number = Int32.Parse(codes[1].ToString());
                if (level >= 1 && level <= 3 && number >= 1 && number <= 4)
                {
                    Mine chosenMine = MinesOnBoard[level - 1][number - 1];
                    if (chosenMine.Prices["r"] <= player.CountCoins("red") &&
                        chosenMine.Prices["w"] <= player.CountCoins("white") &&
                        chosenMine.Prices["k"] <= player.CountCoins("black") &&
                        chosenMine.Prices["b"] <= player.CountCoins("blue") && 
                        chosenMine.Prices["g"] <= player.CountCoins("green"))
                    {
                        return true;
                    }
                }
            }
            return result;

        }
    }
}
