using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;

namespace CheckersRules.Helpers
{
    public static class ConversionHelper
    {
        public static PieceType ToPieceType(this char c)
        {
            switch (c)
            {
                case 'w':
                case 'b': return PieceType.Simple;
                case 'W':
                case 'B': return PieceType.King;
                default:
                    throw new Exception("Wrong piece char: " + c);
            }
        }

        public static PieceColor ToPieceColor(this char c)
        {
            switch (c)
            {
                case 'w':
                case 'W': return PieceColor.White;
                case 'b':
                case 'B': return PieceColor.Black;
                default:
                    throw new Exception("Wrong piece char: " + c);
            }
        }
    }
}
