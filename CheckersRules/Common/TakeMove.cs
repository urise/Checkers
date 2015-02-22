using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Interfaces;

namespace CheckersRules.Common
{
    public struct TakeMove
    {
        public ISquare SquareToMove;
        public ISquare SquareTaken;
    }
}
