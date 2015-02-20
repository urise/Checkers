using System.Collections.Generic;
using CheckersRules.Common;

namespace CheckersRules.Interfaces
{
    public interface IPosition
    {
        IEnumerable<Cell> GetCurrentColorCells();
        PieceColor CurrentColor { get; }
        bool IsTurnToKingHorizontal(int horizontal);
        void SetColor(Square square, PieceColor color);
        Cell GetCell(ISquare square);
    }
}