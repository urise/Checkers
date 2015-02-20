using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Interfaces;

namespace CheckersRules
{
    public class Rules: IRules
    {
        #region Private Members

        private readonly IPosition _position;
        private readonly IBoardGeometry _boardGeometry;
        private readonly IDirections _directions;

        #endregion

        #region Public Methods

        public Rules(IPosition position, IBoardGeometry boardGeometry, IDirections directions)
        {
            _position = position;
            _boardGeometry = boardGeometry;
            _directions = directions;
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

        private IEnumerable<ISquare> GetPossibleSimpleMoves(Cell cell)
        {
            return _boardGeometry.GetCellsByDirections(cell.Square, _directions.SimpleMoveDirections(cell.PieceColor), 1);
        }

        private void AddSimpleMoves(List<string> moves, Cell cell)
        {
            foreach (var square in GetPossibleSimpleMoves(cell))
            {
                var cellToMove = _position.GetCell(square);
                if (cellToMove.PieceType == PieceType.Empty)
                    moves.Add(cell + "-" + cellToMove);
            }
        }

        private void AddKingMoves(List<string> moves, Cell cell)
        {
            foreach (var direction in _directions.AllDirections())
            {
                var squares = _boardGeometry.GetCellsByDirection(cell.Square, direction, 0);
                foreach (var square in squares)
                {
                    var moveCell = _position.GetCell(square);
                    if (moveCell.PieceColor != PieceColor.Empty) break;
                    moves.Add(cell + "-" + moveCell);
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
            var color = cell.PieceColor;
            _position.SetColor(cell.Square, PieceColor.Empty);
            AddTakeMoves(moves, cell, new List<Square>(), string.Empty);
            _position.SetColor(cell.Square, color);
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

                var newPieceType = (cell.PieceType == PieceType.Simple &&
                                    _position.IsTurnToKingHorizontal(takeMove.CellToMove.Y)
                    ? PieceType.King
                    : cell.PieceType);
                var newPiece = new Piece(newPieceType, cell.PieceColor);
                var newCell = new Cell(takeMove.CellToMove, newPiece);

                AddTakeMoves(moves, newCell, newAlreadyTaken, path + "-" + takeMove.CellToMove.ToString());
            }
        }

        private List<TakeMove> GetSingleTakeMoves(Cell cell, List<Square> alreadyTaken)
        {
            int distance = cell.PieceType == PieceType.King ? 0 : 2;
            var result = new List<TakeMove>();
            foreach (var direction in _directions.AllDirections())
            {
                AddTakeMoves(result, cell, direction, distance, alreadyTaken);
            }
            
            return result;
        }

        private void AddTakeMoves(List<TakeMove> takeMoves, Cell cell, IDirection direction, int distance, List<Square> alreadyTaken)
        {
            var squares = _boardGeometry.GetCellsByDirection(cell.Square, direction, distance);
            bool isTaken = false;

            var cellTaken = new Square();
            foreach (var square in squares)
            {
                var moveCell = _position.GetCell(square);
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
