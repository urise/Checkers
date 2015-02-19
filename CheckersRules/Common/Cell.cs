using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Common
{
    public class Cell
    {
        public Square Square { get; private set; }
        public Piece Piece { get; private set; }

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

        public PieceColor PieceColor
        {
            get { return Piece.Color; }
            set { Piece.Color = value; }
        }

        public PieceType PieceType
        {
            get { return Piece.Type; }
            set { Piece.Type = value; }
        }

        public Cell()
        {
            Square = new Square();
            Piece = new Piece();
        }

        public Cell(Square square, Piece piece)
        {
            Square = square;
            Piece = piece;
        }

        public override string ToString()
        {
            return Square.ToString();
        }

        public bool IsOppositeColor(PieceColor color)
        {
            return Piece.Color != PieceColor.Empty && Piece.Color != color;
        }
    }
}
