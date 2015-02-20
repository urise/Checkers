using System.Collections.Generic;
using CheckersRules.Common;

namespace CheckersRules.Interfaces
{
    public interface IPosition
    {
        void SetPosition(string position, string currentColor);
        IEnumerable<Cell> GetCurrentColorCells();
        IEnumerable<Cell> GetPossibleSimpleMoves(Cell cell);
        PieceColor CurrentColor { get; }
        IEnumerable<Cell> GetCellByDirection(Cell cell, IDirection direction, int distance);
        bool IsTurnToKingHorizontal(int horizontal);
        void SetColor(Square square, PieceColor color);
    }
}