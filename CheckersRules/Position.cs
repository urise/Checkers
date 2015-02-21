using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Helpers;
using CheckersRules.Interfaces;

namespace CheckersRules
{
    public class Position : IPosition
    {
        #region Private Members

        private readonly List<Cell> _cells = new List<Cell>();

        #endregion

        #region Public Members

        public PieceColor CurrentColor { get; private set; }

        public void SetPosition(string position, string currentColor)
        {
            if (string.IsNullOrEmpty(position)) throw new Exception("Position cannot be empty");
            if (string.IsNullOrEmpty(currentColor)) throw new Exception("Current color cannot be empty");

            _cells.Clear();
            CurrentColor = currentColor[0].ToPieceColor();

            var parts = position.Split(';');
            foreach (var part in parts)
                SetCell(part);
        }

        public IEnumerable<Cell> GetCurrentColorCells()
        {
            return _cells.Where(r => r.PieceColor == CurrentColor);
        }

        public void SetColor(ISquare square, PieceColor color)
        {
            var cell = GetCell(square);
            cell.PieceColor = color;
        }

        public Cell GetCell(ISquare square)
        {
            return _cells.SingleOrDefault(c => c.Square.IsEqualTo(square));
        }

        public bool SquareIsEmpty(ISquare square)
        {
            return !_cells.Any(c => c.PieceColor != PieceColor.Empty && c.Square.IsEqualTo(square));
        }

        public bool SquareIsColor(ISquare square, PieceColor color)
        {
            var cell = GetCell(square);
            return cell != null && cell.PieceColor == color;
        }

        #endregion

        #region Set Position Methods

        private void SetCell(string cellStr)
        {
            if (cellStr.Length != 3) throw new Exception("Wrong format: " + cellStr);
            var cell = new Cell(cellStr);
            _cells.Add(cell);
        }

        #endregion
    }
}
