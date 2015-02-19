using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Common
{
    internal struct Square
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return Constants.CoordinateCharacters[X - 1] + Y.ToString();
        }
    }
}
