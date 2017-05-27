using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPuzzle.UCs
{
    class UCChessBoardVM
    {
        private ObservableCollection<UCChessRowVM> chessBoard;
        public ObservableCollection<UCChessRowVM> ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        public UCChessBoardVM()
        {
            ChessBoard = new ObservableCollection<UCChessRowVM>();
            for (int r = 0; r < 8; r++)
            {
                var row = new UCChessRowVM();
                for (int rNum = 0; rNum < row.ChessRow.Count; rNum++)
                {
                    row.ChessRow.ElementAt(rNum).Row = r + 1;
                }
                ChessBoard.Add(row);
            }
        }
    }
}
