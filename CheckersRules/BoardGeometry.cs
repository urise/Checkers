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

        public BoardGeometry()
        {
            BoardSize = DEFAULT_BOARD_SIZE;
        }

        public IEnumerable<Square> GetCellsByDirection(ISquare square, IDirection direction, int distance)
        {
            int x = square.X + direction.DirectionX;
            int y = square.Y + direction.DirectionY;
            int n = 0;
            while (IsLegalCoordinates(x, y) && (n < distance || distance == 0))
            {
                yield return new Square(x, y);
                x += direction.DirectionX;
                y += direction.DirectionY;
                n++;
            }
        }

        private bool IsLegalCoordinates(int x, int y)
        {
            return x >= 1 && x <= BoardSize && y >= 1 && y <= BoardSize;
        }
    }
}
