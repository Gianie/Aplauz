﻿using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.Players
{
    public class Player
    {
        private List<Coin> Coins = new List<Coin>();
        private List<Mine> Mines = new List<Mine>();
        public bool IsWinner { get; set; }
        public string Name { get; }
        public int id;
        public string type { get; set; }

        public int Prestige
        {         
            get { return Mines.Sum(m => m.Prestige);}
            set {; }
        }

        public virtual List<Move> PossibleMoves { get; }

        public Player(string name)
        {
            this.Name = name;
        }

        public Player(Player player)
        {
            this.Coins = player.Coins.ConvertAll<Coin>(coin => new Coin(coin));
            this.Mines = player.Mines.ConvertAll<Mine>(mine => new Mine(mine));
            this.Name = player.Name;
            this.IsWinner = player.IsWinner;
            this.id = player.id;
            this.type = player.type;
            this.Prestige = player.Prestige;
        }

        public virtual string Entry(Board board)
        {
            return "dunno what to write here";
        }

        public virtual string Entry(State state)
        {
            return "dunno what to write here";
        }

        public void AddCoin(Coin coin) //adds one coin of specific Color
        {
            Coins.Add(coin);
        }

        public int CountCoins(string color) // counts Coins of specific Color
        {
            return Coins.Count(i => i.Color == color);
        }
        public int CountMines(string color)
        {
            return Mines.Count(m => m.Color == color);
        }

        public int CountResources(string color)
        {
            return CountCoins(color) + CountMines(color);
        }


        public int CountCoins(bool withGold) //counts all Coins
        {
            int count;
            count = CountCoins("w");
            count += CountCoins("b");
            count += CountCoins("g");
            count += CountCoins("k");
            count += CountCoins("r");
            if (withGold == true)
                count += CountCoins("gold");

            return count;
        }


        public void AddMine(Mine mine)
        {
            Mines.Add(mine);
        }

        public List<Coin> RemoveCoins(Dictionary<string,int> dicCoins)
        {
            List<Coin> resultList = new List<Coin>();
            foreach (var dicElement in dicCoins)
            {
                string code = dicElement.Key;
                for (int i = 0; i < dicElement.Value; i++)
                {
                    Coin coin = Coins.First(c => c.Color == code);
                    Coins.Remove(coin);
                    resultList.Add(coin);
                }
            }
            return resultList;
        }

        public string RandomMove(State state)
        {
            List<Coin> coinsOnBoard = state.CoinsOnBoard;
            List<List<Mine>> minesOnBoard = state.MinesOnBoard;

            Random random = new Random();
            List<Move> moves = new List<Move>();
            Move move = new Move();
            foreach (Move.PossibleMoves code in Enum.GetValues(typeof(Move.PossibleMoves)))
            {
                string moveCode = code.ToString();
                move = new Move(moveCode);
                if (Move.IsMovePossible(move, this, coinsOnBoard, minesOnBoard))
                {
                    moves.Add(new Move(moveCode));
                }
            }
            string rand = moves[random.Next(moves.Count)].MoveCode;
            return rand;
        }

    }
}
