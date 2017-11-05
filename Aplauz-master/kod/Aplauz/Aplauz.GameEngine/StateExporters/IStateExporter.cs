using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.StateExporters
{
    interface IStateExporter
    {
        void ExportEndedGame(State finalState, int[] finalResults);
    }
}
