using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Common
{
    public enum PieceType
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

    public class Piece
    {
        public PieceType Type { get; set; }
        public PieceColor Color { get; set; }

        public Piece() {}

        public Piece(PieceType type, PieceColor color)
        {
            Type = type;
            Color = color;
        }
    }
}
