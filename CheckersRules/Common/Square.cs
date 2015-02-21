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
        private const string COORDINATE_CHARACTERS = "abcdefgh";
        private const string COORDINATE_NUMBERS = "12345678";
        public int X { get; set; }
        public int Y { get; set; }

        public Square() { }

        public Square(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Square(string str)
        {
            X = COORDINATE_CHARACTERS.IndexOf(str[0]) + 1;
            if (X == 0)
                throw new Exception("Wrong vertical char: " + str);
            Y = COORDINATE_NUMBERS.IndexOf(str[1]) + 1;
            if (Y == 0)
                throw new Exception("Wrong horizontal char: " + str);
        }

        public bool IsEqualTo(ISquare square)
        {
            return X == square.X && Y == square.Y;
        }

        public override string ToString()
        {
            return COORDINATE_CHARACTERS[X - 1] + Y.ToString();
        }
    }
}
