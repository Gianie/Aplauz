﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;

namespace Aplauz.GameEngine.Drawers
{
    public class Drawer:IDrawer
    {
        public void Draw(List<Player> players, List<Coin> coins)
        {
            Console.Write("Board: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(coins.Count(c=>c.Color=="blue") + " ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(coins.Count(c => c.Color == "red") + " ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(coins.Count(c => c.Color == "black") + " ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(coins.Count(c => c.Color == "white") + " ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(coins.Count(c => c.Color == "green") + " ");
            Console.ResetColor();
            foreach (var player in players)
            {
                Console.WriteLine("");
                Console.WriteLine(player.name + " P: " + player.prestige);
                Console.ForegroundColor=ConsoleColor.Blue;
                Console.Write(player.CountCoins("blue") + " ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(player.CountCoins("red") + " ");
                Console.BackgroundColor=ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(player.CountCoins("black") + " ");
                Console.BackgroundColor=ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(player.CountCoins("white") + " ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(player.CountCoins("green") + " ");
                Console.ResetColor();
            }

        }
    }
}