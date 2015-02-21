using System.Collections.Generic;
using CheckersRules.Common;

namespace CheckersRules.Interfaces
{
    public interface IPosition
    {
        void SetPosition(string position, string currentColor);
        IEnumerable<Cell> GetCurrentColorCells();
        PieceColor CurrentColor { get; }
        void SetColor(Square square, PieceColor color);
        Cell GetCell(ISquare square);
    }
}