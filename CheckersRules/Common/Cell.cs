using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Interfaces;

namespace CheckersRules.Common
{
    public class Cell: ISquare, IPiece
    {
        private ISquare Square { get; set; }
        private IPiece Piece { get; set; }

        public int X
        {
            get { return Square.X; }
        }

        public int Y
        {
            get { return Square.Y; }
        }

        public PieceColor Color
        {
            get { return Piece.Color; }
        }

        public PieceType Type
        {
            get { return Piece.Type; }
        }

        public Cell(ISquare square, IPiece piece)
        {
            Square = square;
            Piece = piece;
        }

        public Cell(string cellStr)
        {
            Square = new Square(cellStr.Substring(1));
            Piece = new Piece(cellStr[0]);
        }

        public override string ToString()
        {
            return Square.ToString();
        }

        public bool IsOppositeColor(PieceColor color)
        {
            return Piece.Color != color;
        }

        public bool IsEqualTo(ISquare square)
        {
            return Square.IsEqualTo(square);
        }
    }
}
