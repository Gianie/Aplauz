using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;


namespace Aplauz.GameEngine
{
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

        public List<Player> PopulatePlayers(string[] names, string[] playerTypes)
        {
            var result = new List<Player>();
            for (int i = 0; i < 4; i++)
            {
                Type type = CastNameToPlayerType(playerTypes[i]);
                ConstructorInfo ctor = type.GetConstructor(new[] { typeof(string) });
                object instance = ctor.Invoke(new object[] { names[i] });
                result.Add((Player)instance);
            }
            return result;
        }


        private Type CastNameToPlayerType(string type)
        {
            if (type == "Human")
            {
                return typeof(HumanPlayer);
            }
            else if (type == "Random")
            {
                return typeof(RandomPlayer);
            }
            else if (type == "Neural")
            {
                return typeof(NeuralNetworkPlayer);
            }
            else if (type == "Monte")
            {
                return typeof(MonteCarloPlayer);
            }
            else if (type == "Greedy")
            {
                return typeof(DynamicGreedyPlayer);
            }
            else if (type == "Upgrade")
            {
                return typeof(MonteCarloUpgradePlayer);
            }

            else
            {
                return null;
            }
        }
    }
}
