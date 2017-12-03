using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players.MonteCarlo
{
    class MonteCarloUpgradeBoard : Board
    {
        public List<DynamicGreedyPlayer> Players { get; } = new List<DynamicGreedyPlayer>();
        public MonteCarloUpgradeBoard(string[] names, string[] playerTypes) : base(names, playerTypes)
        {

        }


        public MonteCarloUpgradeBoard(Board board) : base(board)
        {
            Players = board.Players.ConvertAll(player => new DynamicGreedyPlayer(player));
        }
        public Player SetPlayer(string simulationPlayer, Player myPlayer)
        {
            foreach (Player player in Players)
            {
                if (player.Name == simulationPlayer)
                {
                    myPlayer = player;
                }
            }
            return myPlayer;
        }

        public void StartSimulationMoves(string moveCode, string simulationPlayer, int howDeep)
        {
            Move move = new Move();
            int simulationTurn = 0;
            int wasAlready = 0; // zmienna mowiaca czy racz ld aktoreog robimy symulacje wykonal juz ruch
            while (Players.All(p => p.Prestige < 15))
            {
                foreach (var player in Players)
                {          
                    if (simulationTurn ==0 && wasAlready == 0 && player.Name!=simulationPlayer)
                        continue;
                    if ((simulationTurn > 0)||(player.Name != simulationPlayer))
                    {
                        moveCode = player.ChoseMove(this);
                    }
                    wasAlready = 1;
                    move = new Move(moveCode);

                    if (move.Shortcut == Move.TakeCoins.Shortcut)
                    {
                        string coinsCodes = move.MoveCode.Substring(1);
                        GiveCoins(coinsCodes.ToCharArray().Select(c => c.ToString()).ToArray(), player);

                    }
                    else if (move.Shortcut == Move.TakeMine.Shortcut)
                    {
                        string mineCode = move.MoveCode.Substring(1);
                        GiveMine(mineCode, player);
                    }

                    else if (move.Shortcut == Move.None.Shortcut)
                    {
                    }
                    RandomizeMissingMines();
                }
                simulationTurn++;
                turn++;
                if (simulationTurn ==howDeep)
                    break;
            }
        }

        public double RateMove(string moveCode, Player newPlayer, Player oldPlayer)
        {
            double rate = 30;
            bool isWinnerNow = true;
            foreach (Player player in Players)
            {
                if ((turn < 10 ) || ((newPlayer.Name != player.Name) && (newPlayer.Prestige <= player.Prestige)))
                {
                    rate = 10;
                    isWinnerNow = false;
                }
            }

            rate = rate * (double)((double)(newPlayer.Prestige + 1)/ (double)15)* (double)((double)(newPlayer.Prestige + 1));

            

            rate += newPlayer.CountAllMines();
            if ((oldPlayer.CountAllMines() <= 2) && (moveCode[0] == 'm') && (moveCode[1] == '1'))
            {
                rate += 25;
            }

            if ((newPlayer.Prestige>=15)  && isWinnerNow)
            {
                rate = 100000000;
            }

            return rate;
        }

        public double StartNewGame(Player simulationPlayer, string moveCode, int howDeep)
        {
            Player oldPlayer = new Player(simulationPlayer);
            Player newPlayer = new Player();
            StartSimulationMoves(moveCode, simulationPlayer.Name, howDeep);
            
            newPlayer = SetPlayer(simulationPlayer.Name, newPlayer);
            double result = RateMove(moveCode, newPlayer, oldPlayer);

            return result;
        }
    }
}
