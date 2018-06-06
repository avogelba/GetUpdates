using System;
using System.Windows.Data;

namespace GetUpdates
{
    //Used from: https://stackoverflow.com/questions/15467553/proper-datagrid-search-from-textbox-in-wpf-using-mvvm
    public class SearchValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string cellText = values[0] == null ? string.Empty : values[0].ToString();
            string searchText = values[1] as string;

            if (!string.IsNullOrEmpty(searchText) && !string.IsNullOrEmpty(cellText))
            {
                //return cellText.ToLower().StartsWith(searchText.ToLower());
                return cellText.ToLower().Contains(searchText.ToLower());
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
