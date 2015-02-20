using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;

namespace CheckersRules.Interfaces
{
    public interface IDirections
    {
        IEnumerable<IDirection> AllDirections();
        IEnumerable<IDirection> SimpleMoveDirections(PieceColor color);
    }

}
