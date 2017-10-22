using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;

namespace Aplauz.GameEngine.StateExporters
{
    public class State
    {
        public List<List<Mine>> MinesOnBoard { get; } = new List<List<Mine>>();
        public List<Coin> Coins = new List<Coin>();
        public List<Player> Players = new List<Player>();
    }
}
