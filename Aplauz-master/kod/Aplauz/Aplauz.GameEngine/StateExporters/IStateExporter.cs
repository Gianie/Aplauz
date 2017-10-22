using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.StateExporters
{
    interface IStateExporter
    {
        void ExportCurrentState(State state);

        void ExportCurrentStateWithPossibleMoves(State state, List<int> possibleMoves);

    }
}
