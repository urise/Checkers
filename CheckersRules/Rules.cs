using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersRules
{
    public class Rules
    {
        #region Private Members

        private const int BOARD_SIZE = 8;

        private enum Cell
        {
            BlackKing = -2,
            BlackSimple = -1,
            Empty = 0,
            WhiteSimple = 1,
            WhiteKing = 2
        };

        private enum MoveOrder
        {
            Black = -1,
            White = 1
        }

        private MoveOrder _moveOrder ;
        private readonly Cell[,] _board = new Cell[BOARD_SIZE, BOARD_SIZE];

        #endregion

        #region Public Methods

        public Rules(string position)
        {
            SetPosition(position);
        }

        public List<string> GetMoveList()
        {
            var result = new List<string>();
            for (int i = 0; i < BOARD_SIZE; i++)
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (PieceOfCurrentColor(i+1, j+1))
                        result.AddRange(GetMoveList(i, j, false));
                }
            return result;
        }

        #endregion

        #region Set Position Methods

        private void SetMoveOrder(string moveOrder)
        {
            switch (moveOrder)
            {
                case "w":
                    _moveOrder = MoveOrder.White;
                    break;
                case "b":
                    _moveOrder = MoveOrder.Black;
                    break;
                default: 
                    throw new Exception("Wront move order format");
            }
        }

        private void ClearPosition()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
                for (int j = 0; j < BOARD_SIZE; j++)
                    _board[i, j] = Cell.Empty;
        }

        private Cell GetCellByChar(char c)
        {
            switch (c)
            {
                case 'w': return Cell.WhiteSimple;
                case 'W': return Cell.WhiteKing;
                case 'b': return Cell.BlackSimple;
                case 'B': return Cell.BlackKing;
                default: 
                    throw new Exception("Wrong piece char: " + c);
            }
        }

        private void SetCell(string cellStr)
        {
            if (cellStr.Length != 3) throw new Exception("Wrong format: " + cellStr);
            Cell piece = GetCellByChar(cellStr[0]);
            int vertical = "abcdefgh".IndexOf(cellStr[1]);
            if (vertical == -1)
                throw new Exception("Wrong vertical char: " + cellStr);
            int horizontal = Convert.ToInt32(cellStr.Substring(2,1)) - 1;
            if (horizontal < 1 || horizontal > 8)
                throw new Exception("Wrong horizontal char: " + cellStr);
            _board[vertical, horizontal] = piece;
        }

        private void SetPosition(string position)
        {
            var parts = position.Split(' ');
            if (parts.Length == 0) throw new Exception("Position cannot be empty");
            ClearPosition();
            SetMoveOrder(parts[0]);
            for (int i = 1; i < parts.Length; i++)
                SetCell(parts[i]);
        }

        #endregion

        #region Get Moves

        private IEnumerable<string> GetMoveList(int x, int y, bool killOnly)
        {
            return IsKing(x, y) ? GetKingMoveList(x, y, killOnly) : GetSimpleMoveList(x, y, killOnly);
        }

        private IEnumerable<string> GetSimpleMoveList(int x, int y, bool killOnly)
        {
            var result = new List<string>();

            return result;
        }

        private void AddSimpleMoveListByDirection(List<string> moves, int x, int y, bool killOnly, int dirX, int dirY)
        {
            int x1 = x + dirX, y1 = y + dirY;
            if (IsLegalCoordinates(x1, y1))
                moves.Add(CoordinatesToString(x1, y1));
        }

        private string CoordinatesToString(int x1, int y1)
        {
            return "abcdefgh"[x1] + y1.ToString();
        }

        private IEnumerable<string> GetKingMoveList(int i, int j, bool killOnly)
        {
            throw new NotImplementedException();
        }

        private bool IsLegalCoordinates(int x, int y)
        {
            return x >= 1 && x <= BOARD_SIZE && y >= 1 && y <= BOARD_SIZE;
        }

        private bool PieceOfCurrentColor(int i, int j)
        {
            return _board[i, j] > 0 && _moveOrder > 0 || _board[i, j] < 0 && _moveOrder < 0;
        }

        private bool IsKing(int i, int j)
        {
            return _board[i, j] == Cell.WhiteKing || _board[i, j] == Cell.BlackKing;
        }

        #endregion
    }
}
