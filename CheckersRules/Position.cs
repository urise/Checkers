using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules
{
    internal class Position
    {
        #region Private Members

        private const int BOARD_SIZE = 8;
        private Cell[] _board = new Cell[BOARD_SIZE * BOARD_SIZE];

        #endregion

        #region public Members

        public void SetPosition(string position, string currentColor)
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

        public IEnumerable<Cell> GetPossibleSimpleMoves(Cell cell)
        {
            var result = new List<Cell>();
            int direction = CurrentColor == PieceColor.White ? 1 : -1;


            if (cell.X > 1 && cell.Y < BOARD_SIZE)
                result.Add(GetCellByXY(cell.X - 1, cell.Y + direction));

            if (cell.X < BOARD_SIZE && cell.Y < BOARD_SIZE)
                result.Add(GetCellByXY(cell.X + 1, cell.Y + direction));

            return result;
        }

        public PieceColor CurrentColor { get; private set; }

        #endregion

        #region Set Position Methods

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

        #region public Methods

        

        private Cell GetCellByXY(int x, int y)
        {
            return _board[(y - 1)*BOARD_SIZE + x - 1];
        }

        #endregion
    }
}
