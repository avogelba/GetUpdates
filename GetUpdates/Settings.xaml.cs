using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GetUpdates
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            var sepIndex = Array.FindIndex(ListSeperators.Instance.lSeperators, index => index == (ListSeperators.Instance.cListSeparator));
            sepereatorLB.ItemsSource = ListSeperators.Instance.lSeperators;
            sepereatorLB.SelectedIndex = sepIndex;
            seperatorLBL.Content = "System Standard: " + ListSeperators.Instance.lListSeparator;
            curSeperatorLBL.Content = "Momentan gewählt: " + ListSeperators.Instance.cListSeparator;
        }

        private void seperatorBTN_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var lIndex = sepereatorLB.SelectedIndex;
                ListSeperators.Instance.cListSeparator = ListSeperators.Instance.lSeperators[lIndex];
            }
            catch (Exception eX)
            {

            }
            curSeperatorLBL.Content = "Momentan gewählt: " + ListSeperators.Instance.cListSeparator;

        }

        private void schliessenBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
