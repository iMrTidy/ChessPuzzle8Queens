using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessPuzzle.UCs
{
    class UCChessRowVM
    {
        private ObservableCollection<UCChessCellVM> chessRow;
        public ObservableCollection<UCChessCellVM> ChessRow
        {
            get { return chessRow; }
            set { chessRow = value; }
        }

        public UCChessRowVM()
        {
            ChessRow = new ObservableCollection<UCChessCellVM>();
            for (int c = 0; c < 8; c++)
            {
                ChessRow.Add(new UCChessCellVM { Column = c + 1 });
            }
        }
    }
}
