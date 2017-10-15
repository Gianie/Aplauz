using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;

namespace Aplauz.GameEngine.Drawers
{
    public class Drawer:IDrawer
    {
        public void Draw(List<Player> players, List<Coin> coins, List<List<Mine>> mines)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("Board: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(coins.Count(c => c.Color == "white") + " ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(coins.Count(c => c.Color=="blue") + " ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(coins.Count(c => c.Color == "green") + " ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(coins.Count(c => c.Color == "red") + " ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(coins.Count(c => c.Color == "black") + " ");
 
            Console.ResetColor();

            for (int i=0;i<3;i++ )
            {
                for(int j=0;j<4;j++)
                {
                    Console.Write((i+1).ToString() + (j+1).ToString() + ": " + mines[i][j] + " ");
                }
                Console.WriteLine();
            }

            foreach (var player in players)
            {
                Console.WriteLine("");
                Console.WriteLine(player.Name + " P: " + player.Prestige);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(player.CountCoins("white") + " ");
                Console.ForegroundColor=ConsoleColor.Blue;
                Console.Write(player.CountCoins("blue") + " ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(player.CountCoins("green") + " ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(player.CountCoins("red") + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(player.CountCoins("black") + " ");

                Console.ResetColor();
            }

        }
    }
}
