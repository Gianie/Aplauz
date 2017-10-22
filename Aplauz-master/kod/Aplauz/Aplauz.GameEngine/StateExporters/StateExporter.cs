using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplauz.GameEngine.StateExporters
{
    public class StateExporter:IStateExporter
    {
        public void ExportCurrentState(State state)
        {
            throw new NotImplementedException();
        }

        public void ExportCurrentStateWithPossibleMoves(State state, List<int> possibleMoves)
        {
            possibleMoves=new List<int>();
            foreach (var move in Enum.GetValues(typeof(Move.PossibleMoves)))
            {
                Move.IsMovePossible(move,)
            }
        }
    }
}
