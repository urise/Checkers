using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: checkers.exe <position> <color to move>");
                return;
            }
            string position = args[0];
            string colorToMove = args[1];
            var rulesFactory = new RulesFactory();
            var rules = rulesFactory.CreateCheckerRules(position, colorToMove);
            var moves = rules.GetMoveList();
            
            foreach(var move in moves)
            {
                Console.WriteLine(move);
            }
        }
    }
}
