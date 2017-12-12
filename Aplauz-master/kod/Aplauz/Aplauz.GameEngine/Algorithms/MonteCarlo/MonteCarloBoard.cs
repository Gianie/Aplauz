using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players.MonteCarlo
{
    class MonteCarloBoard : Board
    {
        public List<RandomPlayer> Players { get; } = new List<RandomPlayer>();
        public MonteCarloBoard(string[] names, string[] types) : base ( names, types)
        {

        }


        public MonteCarloBoard(Board board) : base(board)
        {
            Players = board.Players.ConvertAll(player => new RandomPlayer(player));
        }
        public int StartNewGame(string simulationPlayer, string moveCode)
        {
            int turn = 0;
            Move move = new Move();
            int simulationTurn = 0;
            int wasAlready = 0; // zmienna mowiaca czy gracz dla ktorego robimy symulacje wykonal już ruch
            while (Players.All(p => p.Prestige < 15))
            {
                foreach (var player in Players)
                {
                    if (simulationTurn == 0 && wasAlready == 0 && player.Name != simulationPlayer)
                        continue;
                    if ((simulationTurn > 0) || (player.Name != simulationPlayer))
                    {
                        moveCode = player.RandomMove(this);
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
                if(turn > 60)
                {
                    return 0;
                }

            }
            SetWinner();

            foreach (Player player in Players)
            {
                if (player.Name == simulationPlayer)
                {
                    if (player.IsWinner)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }

        protected new void SetWinner()
        {
            Players.First(p => p.Prestige == Players.Max(p1 => p1.Prestige)).IsWinner = true;

        }
    }
}
