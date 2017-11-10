using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Algorithms.MonteCarlo
{
    class MonteCarloUpgradeMove : Move
    {
        public int wins;
        public int trys;
        public double result;

        public MonteCarloUpgradeMove(string moveCode)
        {
            this.MoveCode = moveCode;
            Shortcut = moveCode[0].ToString();
            if (Shortcut == MonteCarloUpgradeMove.TakeCoins.Shortcut)
                Name = "Take Coins";
            else if (Shortcut == MonteCarloUpgradeMove.TakeMine.Shortcut)
                Name = "Take Mine";
            else if (Shortcut == MonteCarloUpgradeMove.None.Shortcut)
                Name = "None";

            wins = 0;
            trys = 0;
            result = 0;
        }

        public MonteCarloUpgradeMove()
        {

        }
    }
}
