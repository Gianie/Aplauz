using Aplauz.GameEngine.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine
{
    static class ResultExport
    {

        public static void WinnersAndPointsToFile(List<Player> players, string path, string fileName)
        {
            string winnerAlghoritm = "";

            foreach (Player player in players)
            {
                File.AppendAllText(path + "\\" + fileName + ".xls", player.Prestige + "\t");
                if (player.IsWinner == true)
                {
                    winnerAlghoritm = player.type;
                }
            }
            File.AppendAllText(path + "\\" + fileName + ".xls", Environment.NewLine);
            File.AppendAllText(path + "\\Winners" + fileName + ".xls", winnerAlghoritm + Environment.NewLine);
        }


        public static void CardsToFile(List<Player> players, string path, string fileName)
        {

            foreach (Player player in players)
            {
                File.AppendAllText(path + "\\Cards" + fileName + ".xls", player.CountMinesByLevel(1) + "\t" + player.CountMinesByLevel(2) + "\t" + player.CountMinesByLevel(3) +"\t" + "\t");
            }
            File.AppendAllText(path + "\\Cards" + fileName + ".xls", Environment.NewLine);
        }

        public static void GameResultToFiles(List<Player> players)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "\\Results";
            System.IO.Directory.CreateDirectory(path);
            string fileName = "";
            foreach (Player player in players)
            {
                fileName += player.type;
            }
            WinnersAndPointsToFile(players, path, fileName);
            CardsToFile(players, path, fileName);

        }
    }
}
