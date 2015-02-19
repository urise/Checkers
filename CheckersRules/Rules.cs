using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;

namespace CheckersRules
{
    public class Rules
    {
        #region Private Members

        private readonly Position _position = new Position();

        #endregion

        #region Public Methods

        public Rules(string position, string currentColor)
        {
            _position.SetPosition(position, currentColor);
        }

        public List<string> GetMoveList()
        {
            var result = GetTakeMoves();
            if (result.Count == 0) result = GetNoTakeMoves();
            return result;
        }

        #endregion

        #region Get No Take Moves

        private List<string> GetNoTakeMoves()
        {
            var result = new List<string>();
            foreach (var cell in _position.GetCurrentColorCells())
            {
                AddMoves(result, cell);
            }
            return result;
        }

        private void AddMoves(List<string> moves, Cell cell)
        {
            if (cell.PieceType == PieceType.King)
                AddKingMoves(moves, cell);
            else
                AddSimpleMoves(moves, cell);
        }

        private void AddSimpleMoves(List<string> moves, Cell cell)
        {
            foreach (var cellToMove in _position.GetPossibleSimpleMoves(cell))
            {
                if (cellToMove.PieceType == PieceType.Empty)
                    moves.Add(cell.ToString() + "-" + cellToMove.ToString());
            }
        }

        private void AddKingMoves(List<string> moves, Cell cell)
        {
            foreach (var direction in Constants.Directions)
            {
                var cells = _position.GetCellByDirection(cell, direction, 0);
                foreach (var moveCell in cells)
                {
                    if (moveCell.PieceColor != PieceColor.Empty) break;
                    moves.Add(cell.ToString() + "-" + moveCell.ToString());
                }
            }
        }

        #endregion

        #region Get Take Moves

        private List<string> GetTakeMoves()
        {
            var result = new List<string>();
            foreach (var cell in _position.GetCurrentColorCells())
            {
                AddTakeMoves(result, cell);
            }
            return result;
        }

        private void AddTakeMoves(List<string> moves, Cell cell)
        {
            _position.SetColor(cell.Square, PieceColor.Empty);
            AddTakeMoves(moves, cell, new List<Square>(), string.Empty);
            _position.SetColor(cell.Square, cell.PieceColor);
        }

        private void AddTakeMoves(List<string> moves, Cell cell, List<Square> alreadyTaken, string path)
        {
            var takeMoves = GetSingleTakeMoves(cell, alreadyTaken);
            if (takeMoves.Count == 0 && !string.IsNullOrEmpty(path))
            {
                moves.Add(path);
                return;
            }
            
            if (string.IsNullOrEmpty(path)) path = cell.ToString();

            foreach (var takeMove in takeMoves)
            {
                var newAlreadyTaken = new List<Square>(alreadyTaken);
                newAlreadyTaken.Add(takeMove.CellTaken);

                var newCell = new Cell { 
                    Square = takeMove.CellToMove, 
                    PieceColor = cell.PieceColor, 
                    PieceType = (cell.PieceType == PieceType.Simple && _position.IsTurnToKingHorizontal(takeMove.CellToMove.Y) 
                        ? PieceType.King : cell.PieceType)
                };

                AddTakeMoves(moves, newCell, newAlreadyTaken, path + "-" + takeMove.CellToMove.ToString());
            }
        }

        private List<TakeMove> GetSingleTakeMoves(Cell cell, List<Square> alreadyTaken)
        {
            int distance = cell.PieceType == PieceType.King ? 0 : 2;
            var result = new List<TakeMove>();
            foreach (var direction in Constants.Directions)
            {
                AddTakeMoves(result, cell, direction, distance, alreadyTaken);
            }
            
            return result;
        }

        private void AddTakeMoves(List<TakeMove> takeMoves, Cell cell, Direction direction, int distance, List<Square> alreadyTaken)
        {
            var cells = _position.GetCellByDirection(cell, direction, distance);
            bool isTaken = false;

            var cellTaken = new Square();
            foreach (var moveCell in cells)
            {
                if (isTaken)
                {
                    if (moveCell.PieceColor != PieceColor.Empty) break;
                    takeMoves.Add(new TakeMove {CellTaken = cellTaken, CellToMove = moveCell.Square});
                }
                else
                {
                    if (moveCell.IsOppositeColor(cell.PieceColor))
                    {
                        if (alreadyTaken.Contains(moveCell.Square)) break;
                        isTaken = true;
                        cellTaken = moveCell.Square;
                    }
                }
            }
        }

        #endregion
    }
}
