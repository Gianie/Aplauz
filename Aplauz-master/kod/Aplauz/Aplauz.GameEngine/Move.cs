using Aplauz.GameEngine.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class Move
    {
        public string Name { get; set; }
        public string Shortcut { get; set; }
        public string MoveCode { get; set; }

        public Move(string moveCode)
        {
            if (moveCode[0] == 'c')
            {
                string suffix = moveCode.Substring(1);
                suffix = String.Concat(suffix.OrderBy(c => c));
                this.MoveCode = moveCode[0] + suffix;
            }
            else
            {
                this.MoveCode = moveCode;
            }         
            Shortcut = moveCode[0].ToString();
            if (Shortcut == Move.TakeCoins.Shortcut)
                Name = "Take Coins";
            else if (Shortcut == Move.TakeMine.Shortcut)
                Name = "Take Mine";
            else if (Shortcut == Move.None.Shortcut)
                Name = "None";
        }
        public Move()
        {
            this.MoveCode = "420";
        }

        public static Move TakeCoins = new Move()
        {
            Name = "Take Coins",
            Shortcut = "c",
            MoveCode = String.Empty

        };
        public static Move TakeMine = new Move()
        {
            Name = "Take Mine",
            Shortcut = "m",
            MoveCode = String.Empty
        };
        public static Move None = new Move()
        {
            Name = "None",
            Shortcut = "n",
            MoveCode = String.Empty
        };

        public static bool IsMovePossible(Move move, Player player, List<Coin> coins, List<List<Mine>> mines)
        {
            bool result = false;
            if (move.Shortcut == Move.TakeCoins.Shortcut)
            {
                string coinsCodes = move.MoveCode.Substring(1);
                result = canPlayerTakeCoins(coinsCodes, player, coins);
            }
            else if (move.Shortcut == Move.TakeMine.Shortcut)
            {
                string mineCode = move.MoveCode.Substring(1);
                result = canPlayerBuyMine(mineCode, player, mines);
            }
            else if (move.Shortcut == Move.None.Shortcut)
            {
                result = true;
            }
            return result;
        }

        private static bool canPlayerTakeCoins(string coinsCodes, Player player, List<Coin> coins)
        {
            bool result = true;

            if (coinsCodes.Length == 2) 
            {
                if (player.CountCoins(true) >= 8) //rule: player can't have 10 coins or more
                {
                    return false;
                }
                if (coins.Count(c => c.Color == coinsCodes.Substring(1)) <4) //rule: player cant take 2 coins of the same color
                {                                                                 // if there is less than 4 coins of that color on the board
                    return false;
                }
            }
            if (coinsCodes.Length == 3 && player.CountCoins(true) >= 7)
            {
                if (player.CountCoins(true) >= 7) //rule: player can't have 10 coins or more
                {
                    return false;
                }
                string[] codes = coinsCodes.ToCharArray().Select(c => c.ToString()).ToArray();
                foreach (var code in codes)
                {
                    if (coins.Count(c => c.Color == code) == 0)
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private static bool canPlayerBuyMine(string mineCode, Player player, List<List<Mine>> mines)
        {


            //                Regex regex = new Regex(@"^\d$");
            if (mineCode.Length != 2)
                return false;
            int level = Int32.Parse(mineCode[0].ToString());
            int number = Int32.Parse(mineCode[1].ToString());
            if (level >= 1 && level <= 3 && number >= 1 && number <= 4)
            {
                Mine chosenMine = mines[level - 1].ElementAtOrDefault(number - 1);
                if (chosenMine == null)
                    return false;
                if (chosenMine.Prices["r"] <= player.CountResources("r") &&
                    chosenMine.Prices["w"] <= player.CountResources("w") &&
                    chosenMine.Prices["k"] <= player.CountResources("k") &&
                    chosenMine.Prices["b"] <= player.CountResources("b") &&
                    chosenMine.Prices["g"] <= player.CountResources("g"))
                {
                    return true;
                }
            }
            return false;

        }

        public enum PossibleMoves
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
            n
        }

    }
}
