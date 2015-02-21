using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;

namespace CheckersRules.Interfaces
{
    public interface IPiece
    {
        PieceType Type { get; }
        PieceColor Color { get; }
    }
}
