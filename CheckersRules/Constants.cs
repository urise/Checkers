using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules
{
    internal enum Piece
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

    internal struct Cell
    {
        public int X;
        public int Y;
        public Piece Piece;
        public PieceColor PieceColor;

        public override string ToString()
        {
            return Constants.CoordinateCharacters[X - 1] + Y.ToString();
        }
    }

    internal class Constants
    {
        public const string CoordinateCharacters = "abcdefgh";
    }
}
