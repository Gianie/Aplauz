using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        List<Coin> coinsLifted = new List<Coin>();
        List<Player> players = new List<Player>();
        List<Mine> minesPack = new List<Mine>();
        List<Mine> minesOnBoardLvl1 = new List<Mine>();
        List<Mine> minesOnBoardLvl2 = new List<Mine>();
        List<Mine> minesOnBoardLvl3 = new List<Mine>();
        private List<List<Mine>> minesOnBoard=new List<List<Mine>>();
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
                        _drawer.Draw(players, coins, minesOnBoard);
                    }
                    string moveCode = String.Empty;
                    bool legal = false;
                    while (!legal)
                    {
                        moveCode = player.Entry();
                        legal = IsMoveLegal(moveCode,player);
                        if (!legal)
                        {
                            Console.WriteLine("move not legal");
                        }
                    }
                    if (moveCode[0].ToString() == Move.TakeCoins.Shortcut)
                    {
                        string coinsCodes = moveCode.Substring(1);
                        GiveCoins(Regex.Split(coinsCodes, string.Empty), player);

                    }
                    else if (moveCode[0].ToString() == Move.TakeMine.Shortcut)
                    {
                        string mineCode = moveCode.Substring(1);
                        GiveMine(mineCode,player);
                    }
                    RandomizeMissingMines();
                }

            }

        }

        private void RandomizeMissingMines()
        {
            int missingCountLvl1 = 4 - minesOnBoardLvl1.Count;
            Random rnd = new Random();
            for (int i = 0; i < missingCountLvl1; i++)
            {
                var adequateMinesFromPack = minesPack.Where(m => m.Level == 1).ToList();
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count-1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                minesPack.Remove(randomizedMine);
                minesOnBoardLvl1.Add(randomizedMine);
            }
            int missingCountLvl2 = 4 - minesOnBoardLvl2.Count;
            for (int i = 0; i < missingCountLvl2; i++)
            {
                var adequateMinesFromPack = minesPack.Where(m => m.Level == 2).ToList();
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                minesPack.Remove(randomizedMine);
                minesOnBoardLvl2.Add(randomizedMine);
            }
            int missingCountLvl3 = 4 - minesOnBoardLvl3.Count;
            for (int i = 0; i < missingCountLvl3; i++)
            {
                var adequateMinesFromPack = minesPack.Where(m => m.Level == 3).ToList();
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                minesPack.Remove(randomizedMine);
                minesOnBoardLvl3.Add(randomizedMine);
            }
        }

        private void PopulateMines()
        {
            MineFactory Mf = new MineFactory();
            minesPack = Mf.GetAllMines();
            minesOnBoard.Add(minesOnBoardLvl1);
            minesOnBoard.Add(minesOnBoardLvl2);
            minesOnBoard.Add(minesOnBoardLvl3);
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
            Mine chosenMine = minesOnBoard[level][number];
            if (chosenMine.Prices["r"] <= player.CountCoins("red") &&
                chosenMine.Prices["w"] <= player.CountCoins("white") &&
                chosenMine.Prices["k"] <= player.CountCoins("black") &&
                chosenMine.Prices["b"] <= player.CountCoins("blue") &&
                chosenMine.Prices["g"] <= player.CountCoins("green"))
            {
                if (minesOnBoard[level].Remove(chosenMine))
                {
                    player.RemoveCoins(chosenMine.Prices);
                    player.AddMine(chosenMine);
                }
            }
            
            
        }

        public bool IsMoveLegal(string codes, Player player)
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
            if (codes[0].ToString() == Move.TakeMine.Shortcut)
            {
                codes = codes.Substring(1);
                Regex regex = new Regex(@"^\d$");
                if (codes.Length != 2)
                    return false;
                int level = Int32.Parse(codes[0].ToString());
                int number = Int32.Parse(codes[1].ToString());
                if (level >= 1 && level <= 3 && number >= 1 && number <= 4)
                {
                    Mine chosenMine = minesOnBoard[level - 1][number - 1];
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

        enum PossibleMoves
        {
            crr,
            cww,
            cbb,
            ckk,
            cgg,
            cbgk,
            cbgr,
            cbgw,
            cbkr,
            cbkw,
            cbrw,
            cgkr,
            cgkw,
            cgrw,
            ckrw,
            m11,
            m12,
            m13,
            m14,
            m21,
            m22,
            m23,
            m24,
            m31,
            m32,
            m33,
            m34,
        }
    

    }
}
