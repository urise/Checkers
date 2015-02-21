using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Helpers;
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
                if (_position.SquareIsEmpty(square))
                    moves.Add(cell + "-" + square);
            }
        }

        private void AddKingMoves(List<string> moves, Cell cell)
        {
            foreach (var direction in _directions.AllDirections())
            {
                var squares = _boardGeometry.GetCellsByDirection(cell.Square, direction, 0);
                foreach (var square in squares)
                {
                    if (!_position.SquareIsEmpty(square)) break;
                    moves.Add(cell + "-" + square);
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
            AddTakeMoves(moves, cell, new List<ISquare>(), string.Empty);
            _position.SetColor(cell.Square, color);
        }

        private bool IsTurnToKingHorizontal(PieceColor color, int horizontal)
        {
            return horizontal == (color == PieceColor.White ? _boardGeometry.LastHorizontal() : _boardGeometry.FirstHorizontal());
        }

        private void AddTakeMoves(List<string> moves, Cell cell, List<ISquare> alreadyTaken, string path)
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
                var newAlreadyTaken = new List<ISquare>(alreadyTaken);
                newAlreadyTaken.Add(takeMove.CellTaken);

                var newPieceType = (cell.PieceType == PieceType.Simple &&
                                    IsTurnToKingHorizontal(_position.CurrentColor, takeMove.CellToMove.Y)
                    ? PieceType.King
                    : cell.PieceType);
                var newPiece = new Piece(newPieceType, cell.PieceColor);
                var newCell = new Cell(takeMove.CellToMove, newPiece);

                AddTakeMoves(moves, newCell, newAlreadyTaken, path + "-" + takeMove.CellToMove.ToString());
            }
        }

        private List<TakeMove> GetSingleTakeMoves(Cell cell, List<ISquare> alreadyTaken)
        {
            int distance = cell.PieceType == PieceType.King ? 0 : 2;
            var result = new List<TakeMove>();
            foreach (var direction in _directions.AllDirections())
            {
                AddTakeMoves(result, cell, direction, distance, alreadyTaken);
            }
            
            return result;
        }

        private void AddTakeMoves(List<TakeMove> takeMoves, Cell cell, IDirection direction, int distance, List<ISquare> alreadyTaken)
        {
            var squares = _boardGeometry.GetCellsByDirection(cell.Square, direction, distance);
            bool isTaken = false;

            ISquare cellTaken = new Square();
            foreach (var square in squares)
            {
                if (isTaken)
                {
                    if (!_position.SquareIsEmpty(square)) break;
                    takeMoves.Add(new TakeMove {CellTaken = cellTaken, CellToMove = square});
                }
                else
                {
                    if (_position.SquareIsColor(square, _position.CurrentColor.GetOppositeColor()))
                    {
                        if (alreadyTaken.Any(s => s.IsEqualTo(square))) break;
                        isTaken = true;
                        cellTaken = square;
                    }
                }
            }
        }

        #endregion
    }
}
