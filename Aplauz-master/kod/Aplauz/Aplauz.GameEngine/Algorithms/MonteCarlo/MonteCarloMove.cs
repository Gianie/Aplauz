using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Algorithms.MonteCarlo
{
    class MonteCarloMove : Move
    {
        public int wins;
        public int trys;

        public MonteCarloMove(string moveCode)
        {
            this.MoveCode = moveCode;
            Shortcut = moveCode[0].ToString();
            if (Shortcut == MonteCarloMove.TakeCoins.Shortcut)
                Name = "Take Coins";
            else if (Shortcut == MonteCarloMove.TakeMine.Shortcut)
                Name = "Take Mine";
            else if (Shortcut == MonteCarloMove.None.Shortcut)
                Name = "None";

            wins = 0;
            trys = 0;
        }

        public MonteCarloMove()
        {

        }
    }
}
