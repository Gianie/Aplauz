using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;

namespace Aplauz.GameEngine.Drawers
{
    interface IDrawer
    {
        void Draw(List<Player> players, List<Coin> coins);
    }
}
