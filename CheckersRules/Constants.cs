using System;
using System.Collections.Generic;
using CheckersRules.Common;

namespace CheckersRules
{
    public class Constants
    {
        public const string CoordinateCharacters = "abcdefgh";

        public static IEnumerable<Direction> Directions = new List<Direction>
            {
                new Direction {DirectionX = -1, DirectionY = -1},
                new Direction {DirectionX = 1, DirectionY = -1},
                new Direction {DirectionX = -1, DirectionY = 1},
                new Direction {DirectionX = 1, DirectionY = 1}
            };
    }
}
