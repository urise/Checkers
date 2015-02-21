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
            const int boardSize = 8;
            
            IPosition position = new Position();
            position.SetPosition(positionStr, currentColorStr);
            
            IBoardGeometry boardGeometry = new BoardGeometry(boardSize);
            IDirections directions = new Directions();
            
            return new Rules(position, boardGeometry, directions);
        }
    }
}
