using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessPuzzle.UCs;
using System.Collections.ObjectModel;

namespace ChessPuzzle
{
    class MainWindowVM : BaseVM
    {
        public ChessPuzzleSolver Solver { get; set; }

        private ObservableCollection<UCChessBoardVM> chessBoards;
        public ObservableCollection<UCChessBoardVM> ChessBoards
        {
            get { return chessBoards; }
            set { chessBoards = value; NotifyPropertyChanged(); }
        }

        public MainWindowVM()
        {
            ChessBoards = new ObservableCollection<UCChessBoardVM>();
            Solver = new ChessPuzzleSolver();
            Solver.Solve();
            SetQueens();
        }

        private void SetQueens()
        {
            var count = 0;
            foreach (var solution in Solver.Solutions)
            {
                count++;
                var chessBoard = new UCChessBoardVM();
                foreach (var item in solution)
                {
                    chessBoard.ChessBoard.ElementAt(item.Row - 1).ChessRow.ElementAt(item.Column - 1).Content = Constants.QUEEN_SYMBOL;
                }
                ChessBoards.Add(chessBoard);
                if (count > 100)
                {
                    break;
                }
            }
        }
    }
}
