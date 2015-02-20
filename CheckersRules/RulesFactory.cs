using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Interfaces;

namespace CheckersRules
{
    public class RulesFactory
    {
        public IRules CreateCheckerRules(string positionStr, string currentColorStr)
        {
            IPosition position = new Position(positionStr, currentColorStr);
            IBoardGeometry boardGeometry = new BoardGeometry();
            IDirections directions = new Directions();
            return new Rules(position, boardGeometry, directions);
        }
    }
}
