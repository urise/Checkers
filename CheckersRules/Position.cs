using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Interfaces;

namespace CheckersRules
{
    public class Position : IPosition
    {
        #region Private Members

        private const int BOARD_SIZE = 8;
        private Cell[] _board = new Cell[BOARD_SIZE * BOARD_SIZE];

        #endregion

        #region Public Members

        public PieceColor CurrentColor { get; private set; }

        public Position(string positionStr, string currentColorStr)
        {
            InitBoard();
            SetPosition(positionStr, currentColorStr);
        }

        private void InitBoard()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i] = new Cell();
            }
        }

        private void SetPosition(string position, string currentColor)
        {
            if (string.IsNullOrEmpty(position)) throw new Exception("Position cannot be empty");
            if (string.IsNullOrEmpty(currentColor)) throw new Exception("Current color cannot be empty");
            
            ClearPosition();
            CurrentColor = GetPieceColorByChar(currentColor[0]);

            var parts = position.Split(';');
            foreach (var part in parts)
                SetCell(part);
        }

        public IEnumerable<Cell> GetCurrentColorCells()
        {
            return _board.Where(r => r.PieceColor == CurrentColor);
        }

        public bool IsTurnToKingHorizontal(int horizontal)
        {
            return horizontal == (CurrentColor == PieceColor.White ? BOARD_SIZE : 1);
        }

        public void SetColor(Square square, PieceColor color)
        {
            int index = GetIndexByCoordinates(square);
            _board[index].PieceColor = color;
        }

        public Cell GetCell(ISquare square)
        {
            return GetCellByXY(square.X, square.Y);
        }

        #endregion

        #region Set Position Methods

        private void ClearPosition()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i].X = i % BOARD_SIZE + 1;
                _board[i].Y = i / BOARD_SIZE + 1;
                _board[i].PieceType = PieceType.Empty;
                _board[i].PieceColor = PieceColor.Empty;
            }
        }

        private void SetCell(string cellStr)
        {
            if (cellStr.Length != 3) throw new Exception("Wrong format: " + cellStr);
            int vertical = Constants.CoordinateCharacters.IndexOf(cellStr[1]);
            if (vertical == -1)
                throw new Exception("Wrong vertical char: " + cellStr);
            int horizontal = Convert.ToInt32(cellStr.Substring(2, 1)) - 1;
            if (horizontal < 0 || horizontal >= BOARD_SIZE)
                throw new Exception("Wrong horizontal char: " + cellStr);
            _board[horizontal * BOARD_SIZE + vertical].PieceType = GetPieceByChar(cellStr[0]);
            _board[horizontal*BOARD_SIZE + vertical].PieceColor = GetPieceColorByChar(cellStr[0]);
        }

        private PieceType GetPieceByChar(char c)
        {
            switch (c)
            {
                case 'w':
                case 'b': return PieceType.Simple;
                case 'W':
                case 'B': return PieceType.King;
                default:
                    throw new Exception("Wrong piece char: " + c);
            }
        }

        private PieceColor GetPieceColorByChar(char c)
        {
            switch (c)
            {
                case 'w':
                case 'W': return PieceColor.White;
                case 'b':
                case 'B': return PieceColor.Black;
                default:
                    throw new Exception("Wrong piece char: " + c);
            }
        }

        #endregion

        #region Auxilliary Methods

        private Cell GetCellByXY(int x, int y)
        {
            return _board[GetIndexByXY(x, y)];
        }

        private int GetIndexByXY(int x, int y)
        {
            return (y - 1)*BOARD_SIZE + x - 1;
        }

        private int GetIndexByCoordinates(Square square)
        {
            return GetIndexByXY(square.X, square.Y);
        }

        private bool IsLegalCoordinates(int x, int y)
        {
            return x >= 1 && x <= BOARD_SIZE && y >= 1 && y <= BOARD_SIZE;
        }

        #endregion
    }
}
