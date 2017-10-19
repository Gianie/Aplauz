﻿using Aplauz.GameEngine.Players;
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


        public static Move TakeCoins = new Move()
        {
            Name = "Take Coins",
            Shortcut = "c"

        };
        public static Move TakeMine = new Move()
        {
            Name = "Take Mine",
            Shortcut = "m"

        };

        public static bool IsMovePossible(string moveCode, Player player, List<Coin> coins)
        {
            bool result = false;
            if (moveCode[0].ToString() == Move.TakeCoins.Shortcut)
            {
                string coinsCodes = moveCode.Substring(1);
                result = canPlayerTakeCoins(coinsCodes, player, coins);
            }
            else if (moveCode[0].ToString() == Move.TakeMine.Shortcut)
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
                string cos = coinsCodes.Substring(1);
                if (coins.Count(c => c.Code == coinsCodes.Substring(1)) <2)
                {
                    result = false;
                }
            }
            if (coinsCodes.Length == 3)
            {
                string[] codes = coinsCodes.ToCharArray().Select(c => c.ToString()).ToArray();
                foreach (var code in codes)
                {
                    if (coins.Count(c => c.Code == code) == 0)
                    {
                        result = false;
                        break;
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
