﻿using Aplauz.GameEngine.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    public class State
    {
        public List<Coin> CoinsOnBoard { get; set; } = new List<Coin>();
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Mine> MinesPack { get; set; } = new List<Mine>();
        public List<List<Mine>> MinesOnBoard { get; set; } = new List<List<Mine>>();

        public void Update(List<Coin> coinsOnBoard, List<Player> players, List<Mine> MinesPack, List<List<Mine>> MinesOnBoard)
        {
            this.CoinsOnBoard = coinsOnBoard;
            this.Players = players;
            this.MinesPack = MinesPack;
            this.MinesOnBoard = MinesOnBoard;
        }
    }

   
}
