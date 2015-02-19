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

        public override string ToString()
        {
            return Constants.CoordinateCharacters[X - 1] + Y.ToString();
        }
    }
}
