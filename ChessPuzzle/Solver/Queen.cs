using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPuzzle
{
    class Queen
    {
        #region Properties

        private int row;
        public int Row
        {
            get { return row; }
            set { row = value; SetDiagonal(); }
        }

        private int column;
        public int Column
        {
            get { return column; }
            set { column = value; SetDiagonal(); }
        }

        public int RowDiagonal { get; set; }
        public int ColumnDiagonal { get; set; }
        public bool IsSet { get; set; }

        #endregion //Properties

        #region PrivateMethods

        private void SetDiagonal()
        {
            var cd = row + column;
            if (cd > 1)
            {
                RowDiagonal = row - column; //This is used to check if left to right diagonal has already a queen.
                ColumnDiagonal = cd; //This is used to check if right to left diagonal has already a queen.
            }
        }

        #endregion //PrivateMethods
    }
}