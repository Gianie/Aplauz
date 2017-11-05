using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players.MonteCarlo
{
    class MonteCarloBoard : Board
    {
        public MonteCarloBoard(string[] names) : base ( names)
        {

        }


        public MonteCarloBoard(Board board) : base(board)
        {
        }
        public int StartNewGame(string simulationPlayer, string moveCode)
        {
            int turn = 0;
            Move move = new Move();
            int simulationTurn = 0;
            int wasAlready = 0; // zmienna mowiaca czy racz ld aktoreog robimy symulacje wykonal juz ruch
            while (Players.All(p => p.Prestige < 15))
            {
                if (simulationTurn >= 1000)
                    return 0;
                simulationTurn++;
                foreach (var player in Players)
                {
                    state.Update(CoinsOnBoard, Players, MinesPack, MinesOnBoard, (int)Enum.Parse(typeof(Move.PossibleMoves), move.MoveCode),currentPlayer);
                    if (simulationTurn == 1 && wasAlready == 0)
                         continue;
                    if (simulationTurn > 1)
                        {
                        moveCode = player.RandomMove(this);
                        }

                        move = new Move(moveCode);
                        wasAlready = 1;
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
                    simulationTurn++;
                }
                turn++;
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
