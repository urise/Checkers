using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;

namespace CheckersRules.Helpers
{
    public static class PieceColorHelper
    {
        public static PieceColor GetOppositeColor(this PieceColor color)
        {
            switch (color)
            {
                case PieceColor.Black:
                    return PieceColor.White;
                case PieceColor.White:
                    return PieceColor.Black;
                default:
                    return color;
            }
        }
    }
}
