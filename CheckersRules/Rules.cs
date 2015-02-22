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
            if (cell.Type == PieceType.King)
                AddKingMoves(moves, cell);
            else
                AddSimpleMoves(moves, cell);
        }

        private IEnumerable<ISquare> GetPossibleSimpleMoves(Cell cell)
        {
            return _boardGeometry.GetCellsByDirections(cell, _directions.SimpleMoveDirections(cell.Color), 1);
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
                var squares = _boardGeometry.GetCellsByDirection(cell, direction, 0);
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
            AddTakeMoves(new FindTakeMovesParameter(cell), moves, cell, string.Empty);
        }

        private bool IsTurnToKingHorizontal(PieceColor color, int horizontal)
        {
            return horizontal == (color == PieceColor.White ? _boardGeometry.LastHorizontal() : _boardGeometry.FirstHorizontal());
        }

        private void AddTakeMoves(FindTakeMovesParameter takeMoveInfo, List<string> moves, Cell cell, string path)
        {
            var takeMoves = GetSingleTakeMoves(takeMoveInfo, cell);
            if (takeMoves.Count == 0 && !string.IsNullOrEmpty(path))
            {
                moves.Add(path);
                return;
            }
            
            if (string.IsNullOrEmpty(path)) path = cell.ToString();

            foreach (var takeMove in takeMoves)
            {
                takeMoveInfo.AddAlreadyTaken(takeMove.SquareTaken);

                var newPieceType = (cell.Type == PieceType.Simple &&
                                    IsTurnToKingHorizontal(_position.CurrentColor, takeMove.SquareToMove.Y)
                    ? PieceType.King
                    : cell.Type);
                var newPiece = new Piece(newPieceType, cell.Color);
                var newCell = new Cell(takeMove.SquareToMove, newPiece);

                AddTakeMoves(takeMoveInfo, moves, newCell, path + "-" + takeMove.SquareToMove);
                takeMoveInfo.RemoveLastAlreadyTaken();
            }
        }

        private List<TakeMove> GetSingleTakeMoves(FindTakeMovesParameter takeMoveInfo, Cell cell)
        {
            var result = new List<TakeMove>();
            foreach (var direction in _directions.AllDirections())
            {
                result.AddRange(AddTakeMoves(takeMoveInfo, cell, direction));
            }
            
            return result;
        }

        private IEnumerable<TakeMove> AddTakeMoves(FindTakeMovesParameter takeMoveInfo, Cell cell, IDirection direction)
        {
            int distance = cell.Type == PieceType.King ? 0 : 2;
            var squares = _boardGeometry.GetCellsByDirection(cell, direction, distance);
            bool isTaken = false;
            
            var result = new List<TakeMove>();

            ISquare cellTaken = null;
            foreach (var square in squares)
            {
                if (isTaken)
                {
                    if (!_position.SquareIsEmpty(square) && !square.IsEqualTo(takeMoveInfo.StartCell)) break;
                    result.Add(new TakeMove {SquareTaken = cellTaken, SquareToMove = square});
                }
                else
                {
                    if (_position.SquareIsColor(square, takeMoveInfo.StartCell.Color.GetOppositeColor()))
                    {
                        if (takeMoveInfo.IsAlreadyTaken(square)) break;
                        isTaken = true;
                        cellTaken = square;
                    }
                }
            }
            return result;
        }

        #endregion
    }
}
