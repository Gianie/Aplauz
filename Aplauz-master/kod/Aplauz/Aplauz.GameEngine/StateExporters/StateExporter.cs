using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                    csv.NextRecord();
                }
                //csv.WriteRecord(MapCoins(finalState.CoinsOnBoard)[1]);
                
                //foreach (var state in finalState.HistoryStates)
                //{
                //    csv.NextRecord(state.CoinsOnBoard);
                //}
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
    }
}
