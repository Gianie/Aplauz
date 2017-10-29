﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aplauz.GameEngine.Drawers;
using Aplauz.GameEngine.Mines;
using Aplauz.GameEngine.Players;
using Aplauz.GameEngine.StateExporters;


namespace Aplauz.GameEngine
{

    public class Board
    {



        protected int currentPlayer = 0;
        protected int turn = 0;
        private IDrawer _drawer;
        private IStateExporter _stateExporter;

        protected State state = new State();



        public List<Coin> CoinsOnBoard { get; } = new List<Coin>();
        public List<Player> players { get; } = new List<Player>();
        public List<Mine> MinesPack { get; } = new List<Mine>();
        public List<List<Mine>> MinesOnBoard { get; } = new List<List<Mine>>();
        public List<Mine> MinesOnBoardLvl1 { get; } = new List<Mine>();
        public List<Mine> MinesOnBoardLvl2 { get; } = new List<Mine>();
        public List<Mine> MinesOnBoardLvl3 { get; } = new List<Mine>();


        public Board(string[] names)
        {

            _drawer = new Drawer();
            _stateExporter = new StateExporter();
            StartNewGame(names);
            
        }

        public Board(Board board)
        {
            currentPlayer = board.currentPlayer;
            turn = board.turn;
            _drawer = board._drawer;

            CoinsOnBoard = board.CoinsOnBoard.ConvertAll(coin => new Coin(coin));
           // players = board.players.ConvertAll(player => new Player(player));
            MinesPack = board.MinesPack.ConvertAll(mine =>new Mine(mine));
            MinesOnBoard = board.MinesOnBoard.ConvertAll(mine => new List<Mine>(mine));
            MinesOnBoardLvl1 = board.MinesOnBoardLvl1.ConvertAll(mine => new Mine(mine));
            MinesOnBoardLvl2 = board.MinesOnBoardLvl2.ConvertAll(mine => new Mine(mine));
            MinesOnBoardLvl3 = board.MinesOnBoardLvl3.ConvertAll(mine => new Mine(mine));
        }

        



        private void StartNewGame(string[] args)
        {

            // PopulatePlayers(4, args);
            //   PopulateThreePlusOne(4, args);
              PopulateRandomPlayers(4, args);
           // PopulateWithMonteCarlo(4, args);
            PopulateCoins();
            PopulateMines();
            RandomizeMissingMines();
            Console.BackgroundColor = ConsoleColor.Gray;
            foreach (var player in players)
            {
                Console.WriteLine(player.Name);
            }
            Move move = new Move();
            while (players.All(p=>p.Prestige<15))
            {
                foreach (var player in players)
                {
                    state.Update(CoinsOnBoard, players, MinesPack, MinesOnBoard);

                    if (player.GetType() == typeof(HumanPlayer))
                    {
                        _drawer.Draw(players, CoinsOnBoard, MinesOnBoard);
                    }

                    bool movePossible = false;
                    while (!movePossible)
                    {
                        string moveCode;
                        if (player.type == "MonteCarlo")
                        {
                            moveCode = player.Entry(this);
                        }
                        else
                        {
                            moveCode = player.Entry(state);
                        }
                        movePossible = isStringLegal(moveCode);

                        if (!movePossible)
                        {
                            Console.WriteLine("string is not legal");
                            continue;

                        }
                        move = new Move(moveCode);
                        movePossible = Move.IsMovePossible(move, player, CoinsOnBoard, MinesOnBoard);
                        if (!movePossible)
                        {
                            Console.WriteLine("Move is not possible. Sorry");
                            continue;
                        }
                        movePossible = true;
                    }
                    if (move.Shortcut == Move.TakeCoins.Shortcut)
                    {
                        string coinsCodes = move.MoveCode.Substring(1);
                        GiveCoins(coinsCodes.ToCharArray().Select(c => c.ToString()).ToArray(), player); //Tak trzeba Janek jezeli chcemy miec tablicę stringów jednoelementowych :D

                    }
                    else if (move.Shortcut == Move.TakeMine.Shortcut)
                    {
                        string mineCode = move.MoveCode.Substring(1);
                        GiveMine(mineCode,player);
                    }

                    else if (move.Shortcut == Move.None.Shortcut)
                    {
                        Console.WriteLine("Ten chujek nic nie robi. Sorry");
                    }
                    RandomizeMissingMines();
                }               
                turn++;
            }
            SetWinner();
            _stateExporter.ExportEndedGame(state);
            Console.ReadKey();
        }

        protected void SetWinner()
        {
            players.First(p => p.Prestige == players.Max(p1 => p1.Prestige)).IsWinner = true;

            foreach (Player player in players)
            {
                if (player.IsWinner == true)
                    Console.WriteLine("Player " + player.Name + " won and got " + player.Prestige + " points.");
            }
        }

        protected void RandomizeMissingMines()
        {
            int missingCountLvl1 = 4 - MinesOnBoardLvl1.Count;
            Random rnd = new Random();
            for (int i = 0; i < missingCountLvl1; i++)
            {
                var adequateMinesFromPack = MinesPack.Where(m => m.Level == 1).ToList();
                if (adequateMinesFromPack.Count <= 0) continue;
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                MinesPack.Remove(randomizedMine);
                MinesOnBoardLvl1.Add(randomizedMine);
            }
            int missingCountLvl2 = 4 - MinesOnBoardLvl2.Count;
            for (int i = 0; i < missingCountLvl2; i++)
            {
                var adequateMinesFromPack = MinesPack.Where(m => m.Level == 2).ToList();
                if (adequateMinesFromPack.Count <= 0) continue;
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                MinesPack.Remove(randomizedMine);
                MinesOnBoardLvl2.Add(randomizedMine);
            }
            int missingCountLvl3 = 4 - MinesOnBoardLvl3.Count;
            for (int i = 0; i < missingCountLvl3; i++)
            {
                var adequateMinesFromPack = MinesPack.Where(m => m.Level == 3).ToList();
                if (adequateMinesFromPack.Count <= 0) continue;
                int randomizedNumber = rnd.Next(0, adequateMinesFromPack.Count - 1);
                Mine randomizedMine = adequateMinesFromPack[randomizedNumber];
                MinesPack.Remove(randomizedMine);
                MinesOnBoardLvl3.Add(randomizedMine);
            }
        }

        protected void PopulateMines()
        {
            MineFactory Mf = new MineFactory();
            MinesPack.AddRange(Mf.GetAllMines());
            MinesOnBoard.Add(MinesOnBoardLvl1);
            MinesOnBoard.Add(MinesOnBoardLvl2);
            MinesOnBoard.Add(MinesOnBoardLvl3);
        }
        protected void PopulateCoins()
        {
            for (int i = 0; i < 7; i++)
            {

                CoinsOnBoard.Add(new Coin("b"));
                CoinsOnBoard.Add(new Coin("g"));
                CoinsOnBoard.Add(new Coin("k"));
                CoinsOnBoard.Add(new Coin("r"));
                CoinsOnBoard.Add(new Coin("w"));
            }
            for (int i = 0; i < 5; i++)
            {
                CoinsOnBoard.Add(new Coin("gold"));
            }
        }

        protected void PopulatePlayers(int quantity, string[] names)
        {
            for (int i = 0; i < quantity; i++)
            {
                HumanPlayer p = new HumanPlayer(names[i]);
                players.Add(p);
            }
        }

        protected void PopulateThreePlusOne(int quantity, string[] names)
        {
            for (int i = 0; i < 3; i++)
            {
                RandomPlayer randomPlayer = new RandomPlayer(names[i]);
                players.Add(randomPlayer);
            }
            HumanPlayer humanPlayer = new HumanPlayer(names[3]);
            players.Add(humanPlayer);
        }

        protected void PopulateRandomPlayers(int quantity, string[] names)
        {
            for (int i = 0; i < quantity; i++)
            {
                RandomPlayer p = new RandomPlayer(names[i]);
                players.Add(p);
            }
        }

        protected void PopulateWithMonteCarlo(int quantity, string[] names)
        {
            for (int i = 0; i < 3; i++)
            {
                RandomPlayer randomPlayer = new RandomPlayer(names[i]);
                randomPlayer.id = i;
                players.Add(randomPlayer);
            }
            MonteCarloPlayer monteCarloPlayer = new MonteCarloPlayer(names[3]);
            monteCarloPlayer.id = 3;
            players.Add(monteCarloPlayer);
        }

        protected bool GiveCoins(string[] codes, Player player)
        {
            bool result=true;
            foreach (var code in codes)
            {
                Coin coin = CoinsOnBoard.FirstOrDefault(c => c.Color == code);
                if (CoinsOnBoard.Remove(coin) && coin != null)
                {
                    player.AddCoin(coin);
                }
                else if (coin == null)
                {
                    result = false;
                }
            }
            return result;
        }

        protected void GiveMine(string code, Player player)
        {
            int level = Int32.Parse(code[0].ToString()) -1;
            int number = Int32.Parse(code[1].ToString()) - 1;
            Mine chosenMine = MinesOnBoard[level][number];
            if (chosenMine.Prices["w"] <= player.CountResources("w") &&
                chosenMine.Prices["b"] <= player.CountResources("b") &&
                chosenMine.Prices["g"] <= player.CountResources("g") &&
                chosenMine.Prices["r"] <= player.CountResources("r") &&
                chosenMine.Prices["k"] <= player.CountResources("k"))
            {
                if (MinesOnBoard[level].Remove(chosenMine))
                {
                    Dictionary<string,int> coinsToRemove = new Dictionary<string, int>();

                    coinsToRemove.Add("w", chosenMine.Prices["w"] - player.CountMines("w"));
                    coinsToRemove.Add("b", chosenMine.Prices["b"] - player.CountMines("b"));
                    coinsToRemove.Add("g", chosenMine.Prices["g"] - player.CountMines("g"));
                    coinsToRemove.Add("r", chosenMine.Prices["r"] - player.CountMines("r"));
                    coinsToRemove.Add("k", chosenMine.Prices["k"] - player.CountMines("k"));

                    List<Coin> removedCoins = player.RemoveCoins(coinsToRemove);
                    CoinsOnBoard.AddRange(removedCoins);
                    player.AddMine(chosenMine);
                }
            }
            
            
        }

        public bool isStringLegal(string codes)
        {
            bool result = false;
            if (codes.Length > 4 || codes == "" || codes.Length < 1)
                return false;
            
            if (codes[0].ToString() != Move.TakeCoins.Shortcut && codes[0].ToString() != Move.TakeMine.Shortcut && codes[0].ToString() != Move.None.Shortcut)
                return false;

            string suffix = codes.Substring(1);
            suffix = string.Concat(suffix.OrderBy(c => c));
            codes = codes[0] + suffix;
            return Enum.IsDefined(typeof(Move.PossibleMoves), codes) || result;
        }
    }
}
