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
            var result = new List<string>();

            foreach (var cell in _position.GetCurrentColorCells())
                result.AddRange(GetMoveList(cell, false));
            
            return result;
        }

        #endregion

        #region Get Moves

        private IEnumerable<string> GetMoveList(Cell cell, bool killOnly)
        {
            return cell.Piece == Piece.King ? GetKingMoveList(cell, killOnly) : GetSimpleMoveList(cell, killOnly);
        }

        private IEnumerable<string> GetSimpleMoveList(Cell cell, bool killOnly)
        {
            var result = new List<string>();
            foreach (var cellToMove in _position.GetPossibleSimpleMoves(cell))
            {
                if (cellToMove.Piece == Piece.Empty)
                    result.Add(cell.ToString() + "-" + cellToMove.ToString());
            }
            return result;
        }

        private IEnumerable<string> GetKingMoveList(Cell cell, bool killOnly)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
