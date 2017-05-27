using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ChessPuzzle.UCs
{
    class UCChessCellVM : BaseVM
    {
        private int row;
        public int Row
        {
            get { return row; }
            set { row = value; NotifyPropertyChanged("IsEven"); }
        }
        private int column;
        public int Column
        {
            get { return column; }
            set { column = value; }
        }
        public bool IsEven { get { return (Row + Column) % 2 == 0; } private set { } }
        public string Content { get; set; }
    }

    [ValueConversion(typeof(bool), typeof(Brush))]
    public class IsEvenToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var res = (bool)value ? Brushes.White : Brushes.Gray;
            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
