using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;

namespace Aplauz.GameEngine.StateExporters
{
    class StateExporter:IStateExporter
    {
        public void ExportEndedGame(State finalState)
        {
            using (TextWriter writer = new StreamWriter("..\\..\\Exports\\state.csv"))
            {

                var csv = new CsvWriter(writer);
                foreach (var state in finalState.HistoryStates)
                {
                    foreach (var coinNumber in MapCoins(state.CoinsOnBoard))
                    {
                        csv.WriteRecord(coinNumber);
                    }
                    foreach (var mineList in state.MinesOnBoard)
                    {
                        var records = MapMines(mineList,4);
                        foreach (var record in records)
                        {
                            csv.WriteRecord(record);
                        }
                    }
                    foreach (var playerRecord in MapPlayers(state.Players))
                    {
                        csv.WriteRecord(playerRecord);
                    }

                    csv.NextRecord();
                }

                writer.Flush();
            }
        }

        private int[] MapCoins(List<Coin> _coins)
        {
            int[] counts = new int[5];
            counts[0]=(_coins.Count(c => c.Color == "w"));
            counts[1]=(_coins.Count(c => c.Color == "b"));
            counts[2]=(_coins.Count(c => c.Color == "g"));
            counts[3]=(_coins.Count(c => c.Color == "r"));
            counts[4]=(_coins.Count(c => c.Color == "k"));
            return counts;
        }

        private int[] MapMines(List<Mine> _mines, int numberOfMines)
        {
            List<int> result = new List<int>();

            foreach (var mine in _mines)
            {
                result.AddRange(mine.ToIntArray());
            }
            int numberOfChars = numberOfMines * 8;
            if (result.Count < numberOfChars)
            {
                int missingChars = numberOfChars - result.Count;
                List<int> ToAdd = new List<int>();
                for (int i = 0; i < missingChars; i++)
                {
                    ToAdd.Add(0);
                }
                result.AddRange(ToAdd);
            }
            return result.ToArray();

        }

        private int[] MapPlayers(List<Player> _players)
        {
            List<int> result = new List<int>();

            foreach (var player in _players)
            {
                result.AddRange(player.ToIntArray());
            }
            return result.ToArray();

        }
    }
}
