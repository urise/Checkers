using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Interfaces;

namespace CheckersRules.Common
{
    public class Square: ISquare
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Square() { }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Square operator +(Square square, IDirection direction)
        {
            return new Square(square.X + direction.DirectionX, square.Y + direction.DirectionY);
        }

        public override string ToString()
        {
            return Constants.CoordinateCharacters[X - 1] + Y.ToString();
        }
    }
}
