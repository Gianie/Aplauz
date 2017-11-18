using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplauz.GameEngine.Players;

namespace Aplauz.GameEngine.StateExporters
{
    class StateExporter:IStateExporter
    {
        private int bestResult;

        public void ExportEndedGame(State finalState, int[] finalResults)
        {
            bestResult = GetMaxResult(finalResults);
            var time = System.DateTime.Now.ToString("ddMMyyyyHHmmss");
            Random rnd = new Random();
            using (TextWriter writer = new StreamWriter("..\\..\\Exports\\"+ time +"_" + rnd.Next(1000,9999) + ".csv"))
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
                    foreach (var playerRecord in MapSpecificPlayer(state.Players[state.LastMovedPlayerIndex]))
                    {
                        csv.WriteRecord(playerRecord);
                    }
                    csv.WriteRecord(state.LastMove);

                    string mark = GetMark(bestResult, finalResults[state.LastMovedPlayerIndex]).ToString();

                    string dot = ".";
                    string comma = ",";
                    if (mark.Contains(comma))
                    {
                        mark = mark.Replace(comma, dot);
                    }
                    csv.WriteField(mark);

                    csv.NextRecord();
                }

                writer.Flush();
            }
        }

        public void ExportCurrentState(State state)
        {

            using (TextWriter writer = new StreamWriter("..\\..\\Exports\\" +  "current.csv"))
            {

                var csv = new CsvWriter(writer);


                foreach (var coinNumber in MapCoins(state.CoinsOnBoard))
                {
                    csv.WriteRecord(coinNumber);
                }
                foreach (var mineList in state.MinesOnBoard)
                {
                    var records = MapMines(mineList, 4);
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
                writer.Flush();
            }
        }

        private int GetMaxResult(int[] finalResults)
        {
            int bestResult=0;
            foreach (var result in finalResults)
            {
                if (result > bestResult)
                    bestResult = result;
            }
            return bestResult;
        }

        private float GetMark(int bestResult, int result)
        {
            return (float) result / bestResult;
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

        private int[] MapSpecificPlayer(Player player)
        {
            return player.ToIntArray();
        }
    }
}
