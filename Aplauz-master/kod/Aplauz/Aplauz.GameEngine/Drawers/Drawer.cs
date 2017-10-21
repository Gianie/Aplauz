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
            Console.Write(coins.Count(c => c.Color == "w") + " ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(coins.Count(c => c.Color=="b") + " ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(coins.Count(c => c.Color == "g") + " ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(coins.Count(c => c.Color == "r") + " ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(coins.Count(c => c.Color == "k") + " ");
 
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
                Console.Write(player.CountCoins("w") + " ");
                Console.ForegroundColor=ConsoleColor.Blue;
                Console.Write(player.CountCoins("b") + " ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(player.CountCoins("g") + " ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(player.CountCoins("r") + " ");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(player.CountCoins("k") + " ");

                Console.ForegroundColor=ConsoleColor.Gray;

                Console.BackgroundColor = ConsoleColor.White;
                Console.Write(player.CountMines("w"));
                Console.BackgroundColor=ConsoleColor.Gray;
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(player.CountMines("b"));
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(player.CountMines("g"));
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(player.CountMines("r"));
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(player.CountMines("k"));

                Console.ResetColor();
            }

        }
    }
}
