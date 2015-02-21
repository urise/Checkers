using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Helpers;
using CheckersRules.Interfaces;

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

    public class Piece: IPiece
    {
        public PieceType Type { get; private set; }
        public PieceColor Color { get; private set; }

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
