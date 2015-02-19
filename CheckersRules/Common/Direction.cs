using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Interfaces;

namespace CheckersRules.Common
{
    public struct Direction: IDirection
    {
        public int DirectionX { get; set; }
        public int DirectionY { get; set; }
    }
}
