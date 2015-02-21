﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules.Interfaces
{
    public interface ISquare
    {
        int X { get; set; }
        int Y { get; set; }
        bool IsEqualTo(ISquare square);
    }
}
