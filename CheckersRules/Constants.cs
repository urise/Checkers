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

    internal struct Coordinates
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return Constants.CoordinateCharacters[X - 1] + Y.ToString();
        }
    }

    internal struct Cell
    {
        public Coordinates Coordinates;
        public Piece Piece;
        public PieceColor PieceColor;

        public int X
        {
            get { return Coordinates.X; }
            set { Coordinates.X = value; }
        }

        public int Y
        {
            get { return Coordinates.Y; }
            set { Coordinates.Y = value; }
        }

        public override string ToString()
        {
            return Coordinates.ToString();
        }

        public bool IsOppositeColor(PieceColor color)
        {
            return PieceColor != PieceColor.Empty && PieceColor != color;
        }
    }

    internal struct TakeMove
    {
        public Coordinates CellToMove;
        public Coordinates CellTaken;
    }

    internal class Constants
    {
        public const string CoordinateCharacters = "abcdefgh";
    }
}
