using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersRules.Common;
using CheckersRules.Interfaces;

namespace CheckersRules
{
    public class FindTakeMovesParameter
    {
        public Cell StartCell { get; private set; }
        private List<ISquare> _alreadyTaken = new List<ISquare>();

        public FindTakeMovesParameter(Cell startCell)
        {
            StartCell = startCell;
        }

        public bool IsAlreadyTaken(ISquare square)
        {
            return _alreadyTaken.Any(s => s.IsEqualTo(square));
        }

        public void AddAlreadyTaken(ISquare square)
        {
            _alreadyTaken.Add(square);
        }

        public void RemoveLastAlreadyTaken()
        {
            _alreadyTaken.RemoveAt(_alreadyTaken.Count - 1);
        }
    }
}
