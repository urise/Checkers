using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Helpers;

namespace CheckersRules.Common
{
    public enum PieceType
    {
        Simple,
        King
    };

    public enum PieceColor
    {
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

        public Piece(char c)
        {
            Type = c.ToPieceType();
            Color = c.ToPieceColor();
        }

        
    }
}
