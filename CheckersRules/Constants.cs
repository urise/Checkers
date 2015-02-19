using System;
using System.Collections.Generic;

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

    internal struct Square
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return Constants.CoordinateCharacters[X - 1] + Y.ToString();
        }
    }

    internal struct Direction
    {
        public int DirectionX;
        public int DirectionY;
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

    internal struct TakeMove
    {
        public Square CellToMove;
        public Square CellTaken;
    }

    internal class Constants
    {
        public const string CoordinateCharacters = "abcdefgh";

        public static IEnumerable<Direction> Directions = new List<Direction>
            {
                new Direction {DirectionX = -1, DirectionY = -1},
                new Direction {DirectionX = 1, DirectionY = -1},
                new Direction {DirectionX = -1, DirectionY = 1},
                new Direction {DirectionX = 1, DirectionY = 1}
            };
    }
}
