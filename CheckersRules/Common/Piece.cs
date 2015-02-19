using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Common
{
    internal enum PieceType
    {
        Empty,
        Simple,
        King
    };

    internal enum PieceColor
    {
        Empty,
        White,
        Black
    }

    internal struct Piece
    {
        public PieceType Type;
        public PieceColor Color;
    }
}
