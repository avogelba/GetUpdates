using System.Diagnostics;
using System.Windows;

namespace GetUpdates
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        //public AboutBox(Window parent) : this()
        //    {
        //        this.Owner = parent;
        //    }
    
            private void hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
            {
                if (e.Uri != null && string.IsNullOrEmpty(e.Uri.OriginalString) == false)
                {
                    string uri = e.Uri.AbsoluteUri;
                    Process.Start(new ProcessStartInfo(uri));
    
                    e.Handled = true;
                }
            }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
