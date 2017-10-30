using Aplauz.GameEngine.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Utils;

namespace Aplauz.GameEngine
{
    public class State
    {
        public List<Coin> CoinsOnBoard { get; set; } = new List<Coin>();
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Mine> MinesPack { get; set; } = new List<Mine>();
        public List<List<Mine>> MinesOnBoard { get; set; } = new List<List<Mine>>();

        public List<State> HistoryStates { get; set; } = new List<State>();

        public void Update(List<Coin> coinsOnBoard, List<Player> players, List<Mine> MinesPack, List<List<Mine>> MinesOnBoard)
        {
            State copy = new State(this);
            HistoryStates.Add(copy);
            this.CoinsOnBoard = coinsOnBoard;
            this.Players = players;       
            this.MinesPack = MinesPack;
            this.MinesOnBoard = MinesOnBoard;
            
        }

        public State()
        {
            
        }

        private State(State state = null)
        {

            this.CoinsOnBoard = state.CoinsOnBoard.ConvertAll<Coin>(coin => new Coin(coin));
            this.MinesPack = state.MinesPack.ConvertAll<Mine>(mine => new Mine(mine));
            this.MinesOnBoard = new List<List<Mine>>();
            foreach(var list in state.MinesOnBoard)
            {
                this.MinesOnBoard.Add(list.ConvertAll<Mine>(mine => new Mine(mine)));
            }
            this.Players.Clear();
            PlayerFactory pf = new PlayerFactory();
            foreach (var player in state.Players)
            {
                this.Players.Add(pf.ClonePlayer(player));
            }

        }
    }

   
}
