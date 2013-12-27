using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules
{
    public enum Piece
    {
        Empty,
        Simple,
        King
    };

    public enum PieceColor
    {
        Empty,
        White,
        Black
    }

    public struct Cell
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

    public class Constants
    {
        public const string CoordinateCharacters = "abcdefgh";
    }
}
