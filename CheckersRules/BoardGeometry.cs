using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Interfaces;

namespace CheckersRules
{
    public class BoardGeometry: IBoardGeometry
    {
        private const int DEFAULT_BOARD_SIZE = 8;
        private int BoardSize { get; set; }

        #region Public Methods

        public BoardGeometry()
        {
            BoardSize = DEFAULT_BOARD_SIZE;
        }

        public BoardGeometry(int boardSize)
        {
            BoardSize = boardSize;
        }

        public IEnumerable<ISquare> GetCellsByDirection(ISquare square, IDirection direction, int distance)
        {
            ISquare newSquare = AddDirection(square, direction);
            int n = 0;
            while (IsLegal(newSquare) && (n < distance || distance == 0))
            {
                yield return newSquare;
                newSquare = AddDirection(newSquare, direction);
                n++;
            }
        }

        public IEnumerable<ISquare> GetCellsByDirections(ISquare square, IEnumerable<IDirection> directions, int distance)
        {
            var result = new List<ISquare>();
            foreach (var direction in directions)
            {
                result.AddRange(GetCellsByDirection(square, direction, distance));
            }
            return result;
        }

        public int FirstHorizontal()
        {
            return 1;
        }

        public int LastHorizontal()
        {
            return BoardSize;
        }

        #endregion

        #region Private Methods

        private bool IsLegal(ISquare square)
        {
            return square.X >= 1 && square.X <= BoardSize && square.Y >= 1 && square.Y <= BoardSize;
        }

        private ISquare AddDirection(ISquare square, IDirection direction)
        {
            return new Square(square.X + direction.DirectionX, square.Y + direction.DirectionY);
        }

        #endregion
    }
}
