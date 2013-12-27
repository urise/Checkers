using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules
{
    public class Position
    {
        #region Private Members

        private const int BOARD_SIZE = 8;
        private Cell[] _board = new Cell[BOARD_SIZE * BOARD_SIZE];

        #endregion

        #region Public Members

        public void SetPosition(string position)
        {
            var parts = position.Split(' ');
            if (parts.Length == 0) throw new Exception("Position cannot be empty");
            ClearPosition();
            SetMoveOrder(parts[0]);
            for (int i = 1; i < parts.Length; i++)
                SetCell(parts[i]);
        }

        public PieceColor CurrentColor { get; private set; }

        #endregion

        #region Set Position Methods

        private void SetMoveOrder(string moveOrder)
        {
            switch (moveOrder)
            {
                case "w":
                    CurrentColor = PieceColor.White;
                    break;
                case "b":
                    CurrentColor = PieceColor.Black;
                    break;
                default:
                    throw new Exception("Wrong move order format");
            }
        }

        private void ClearPosition()
        {
            for (int i = 0; i < _board.Length; i++)
            {
                _board[i].X = i % BOARD_SIZE + 1;
                _board[i].Y = i / BOARD_SIZE + 1;
                _board[i].Piece = Piece.Empty;
                _board[i].PieceColor = PieceColor.Empty;
            }
        }

        private Piece GetPieceByChar(char c)
        {
            switch (c)
            {
                case 'w':
                case 'b': return Piece.Simple;
                case 'W': 
                case 'B': return Piece.King;
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

        private void SetCell(string cellStr)
        {
            if (cellStr.Length != 3) throw new Exception("Wrong format: " + cellStr);
            int vertical = Constants.CoordinateCharacters.IndexOf(cellStr[1]);
            if (vertical == -1)
                throw new Exception("Wrong vertical char: " + cellStr);
            int horizontal = Convert.ToInt32(cellStr.Substring(2, 1)) - 1;
            if (horizontal < 1 || horizontal > 8)
                throw new Exception("Wrong horizontal char: " + cellStr);
            _board[horizontal * BOARD_SIZE + vertical].Piece = GetPieceByChar(cellStr[0]);
            _board[horizontal*BOARD_SIZE + vertical].PieceColor = GetPieceColorByChar(cellStr[0]);
        }

        #endregion

        #region Public Methods

        public IEnumerable<Cell> GetCurrentColorCells()
        {
            return _board.Where(r => r.PieceColor == CurrentColor);
        }

        public IEnumerable<Cell> GetPossibleSimpleMoves(Cell cell)
        {
            var result = new List<Cell>();

            if (cell.X > 1 && cell.Y < BOARD_SIZE)
                result.Add(GetCellByXY(cell.X - 1, cell.Y + 1));

            if (cell.X < BOARD_SIZE && cell.Y < BOARD_SIZE)
                result.Add(GetCellByXY(cell.X + 1, cell.Y + 1));

            return result;
        }

        private Cell GetCellByXY(int x, int y)
        {
            return _board[(y - 1)*BOARD_SIZE + x - 1];
        }

        #endregion
    }
}
