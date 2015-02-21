using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;

namespace CheckersRules.Interfaces
{
    public interface IBoardGeometry
    {
        IEnumerable<ISquare> GetCellsByDirection(ISquare square, IDirection direction, int distance);
        IEnumerable<ISquare> GetCellsByDirections(ISquare square, IEnumerable<IDirection> directions, int distance);
        int FirstHorizontal();
        int LastHorizontal();
    }
}
