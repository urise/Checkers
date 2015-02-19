using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Common
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
        public Square Square;
        public Piece Piece;
        public PieceColor PieceColor;

        public int X
        {
            get { return Square.X; }
            set { Square.X = value; }
        }

        public int Y
        {
            get { return Square.Y; }
            set { Square.Y = value; }
        }

        public override string ToString()
        {
            return Square.ToString();
        }

        public bool IsOppositeColor(PieceColor color)
        {
            return PieceColor != PieceColor.Empty && PieceColor != color;
        }
    }
}
