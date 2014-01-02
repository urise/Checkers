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
            int distance = cell.Piece == Piece.King ? 8 : 2;
            var takeMoves = new List<TakeMove>();
            
            AddTakeMoves(takeMoves, cell, -1, -1, distance);
            AddTakeMoves(takeMoves, cell, -1,  1, distance);
            AddTakeMoves(takeMoves, cell,  1, -1, distance);
            AddTakeMoves(takeMoves, cell,  1,  1, distance);

            foreach (var takeMove in takeMoves)
            {
                moves.Add(cell.ToString() + "-" + takeMove.CellToMove.ToString());
            }
        }

        private void AddTakeMoves(List<TakeMove> takeMoves, Cell cell, int dirX, int dirY, int distance)
        {
            var cells = _position.GetCellByDirection(cell, dirX, dirY, distance);
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
                        isTaken = true;
                        cellTaken = cell.Coordinates;
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
