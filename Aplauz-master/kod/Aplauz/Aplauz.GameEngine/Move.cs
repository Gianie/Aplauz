using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static Move TakeTrader = new Move()
        {
            Name = "Take Trader",
            Shortcut = "t"
        };

        public static Move DrawBoard = new Move()
        {
            Name = "Draw Board",
            Shortcut = "d"

        };
    }
}
