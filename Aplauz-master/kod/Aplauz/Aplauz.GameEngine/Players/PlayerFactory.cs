using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;
using Aplauz.GameEngine.Utils;

namespace Aplauz.GameEngine
{
    //TODO
    public class PlayerFactory
    {
        private HashSet<Type> storedTypes = new HashSet<Type>
        {
            typeof(HumanPlayer),
            typeof(RandomPlayer),
            typeof(MonteCarloPlayer),
            typeof(MonteCarloUpgradePlayer),
            typeof(DynamicGreedyPlayer),
            typeof(NeuralNetworkPlayer)
        };
        public Player ClonePlayer(Player player)
        {

            if (player.GetType() == typeof(HumanPlayer))
            {
                HumanPlayer copy = new HumanPlayer(player);
                return copy;
            }

            else if (player.GetType() == typeof(RandomPlayer))
            {
                RandomPlayer copy = new RandomPlayer(player);
                return copy;
            }

            else if (player.GetType() == typeof(MonteCarloPlayer))
            {
                MonteCarloPlayer copy = new MonteCarloPlayer(player);
                return copy;
            }

            else if (player.GetType() == typeof(MonteCarloUpgradePlayer))
            {
                MonteCarloUpgradePlayer copy = new MonteCarloUpgradePlayer(player);
                return copy;
            }

            else if (player.GetType() == typeof(DynamicGreedyPlayer))
            {
                DynamicGreedyPlayer copy = new DynamicGreedyPlayer(player);
                return copy;
            }

            else if (player.GetType() == typeof(NeuralNetworkPlayer))
            {
                NeuralNetworkPlayer copy = new NeuralNetworkPlayer(player);
                return copy;
            }
            else
            {
                return null;
            }
        }
    }
}
