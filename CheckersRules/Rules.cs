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

        #region Get Moves

        private List<string> GetNoTakeMoves()
        {
            var result = new List<string>();
            foreach (var cell in _position.GetCurrentColorCells())
            {
                AddMoves(result, cell, false);
            }
            return result;
        }

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
            _position.SetColor(cell.Coordinates, PieceColor.Empty);
            AddTakeMoves(moves, cell, new List<Coordinates>(), string.Empty);
            _position.SetColor(cell.Coordinates, cell.PieceColor);
        }

        private void AddTakeMoves(List<string> moves, Cell cell, List<Coordinates> alreadyTaken, string path)
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
                var newAlreadyTaken = new List<Coordinates>(alreadyTaken);
                newAlreadyTaken.Add(takeMove.CellTaken);

                var newCell = new Cell { 
                    Coordinates = takeMove.CellToMove, 
                    PieceColor = cell.PieceColor, 
                    Piece = (cell.Piece == Piece.Simple && _position.IsTurnToKingHorizontal(takeMove.CellToMove.Y) 
                        ? Piece.King : cell.Piece)
                };

                AddTakeMoves(moves, newCell, newAlreadyTaken, path + "-" + takeMove.CellToMove.ToString());
            }
        }

        private List<TakeMove> GetSingleTakeMoves(Cell cell, List<Coordinates> alreadyTaken)
        {
            int distance = cell.Piece == Piece.King ? 8 : 2;
            var result = new List<TakeMove>();
            foreach (var direction in Constants.Directions)
            {
                AddTakeMoves(result, cell, direction, distance, alreadyTaken);
            }
            
            return result;
        }

        private void AddTakeMoves(List<TakeMove> takeMoves, Cell cell, Direction direction, int distance, List<Coordinates> alreadyTaken)
        {
            var cells = _position.GetCellByDirection(cell, direction, distance);
            bool isTaken = false;

            var cellTaken = new Coordinates();
            foreach (var moveCell in cells)
            {
                if (isTaken)
                {
                    if (moveCell.PieceColor != PieceColor.Empty) break;
                    takeMoves.Add(new TakeMove {CellTaken = cellTaken, CellToMove = moveCell.Coordinates});
                }
                else
                {
                    if (moveCell.IsOppositeColor(cell.PieceColor))
                    {
                        if (alreadyTaken.Contains(moveCell.Coordinates)) break;
                        isTaken = true;
                        cellTaken = moveCell.Coordinates;
                    }
                }
            }
        }

        private void AddMoves(List<string> moves, Cell cell, bool killOnly)
        {
            if (cell.Piece == Piece.King)
                AddKingMoves(moves, cell, killOnly);
            else
                AddSimpleMoves(moves, cell, killOnly);
        }

        private void AddSimpleMoves(List<string> moves, Cell cell, bool killOnly)
        {
            foreach (var cellToMove in _position.GetPossibleSimpleMoves(cell))
            {
                if (cellToMove.Piece == Piece.Empty)
                    moves.Add(cell.ToString() + "-" + cellToMove.ToString());
            }
        }

        private void AddKingMoves(List<string> moves, Cell cell, bool killOnly)
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
    }
}
