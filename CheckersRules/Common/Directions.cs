using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Interfaces;

namespace CheckersRules.Common
{
    public class Directions : IDirections
    {
        private readonly ReadOnlyCollection<Direction> _directions = new ReadOnlyCollection<Direction>(
            new List<Direction>
            {
                new Direction {DirectionX = -1, DirectionY = -1},
                new Direction {DirectionX = 1, DirectionY = -1},
                new Direction {DirectionX = -1, DirectionY = 1},
                new Direction {DirectionX = 1, DirectionY = 1}
            });

        public IEnumerable<IDirection> AllDirections()
        {
            return _directions.Select(r => (IDirection)r).AsEnumerable();
        }

        public IEnumerable<IDirection> SimpleMoveDirections(PieceColor color)
        {
            if (color == PieceColor.Empty) throw new Exception("There are no simple moves for empty color");
            var directionY = color == PieceColor.White ? 1 : -1;
            return _directions.Where(d => d.DirectionY == directionY).Select(r => (IDirection)r).AsEnumerable();;
        }
    }
}
