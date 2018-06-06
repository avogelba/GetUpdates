using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
//using WUApiInterop;
using WUApiLib;

namespace GetUpdates
{
    #region class
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region definitionen
        
        ListCollectionView collectionView;
        private ObservableCollection<Update> _Updates = new ObservableCollection<Update>();
        public ObservableCollection<Update> Updates
        {
            get { return _Updates; }
            set { _Updates = value; }
        }
        #endregion
        #region main
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                GetUpdates();
            }
            catch (Exception ex)
            { }
            collectionView = new ListCollectionView(Updates);

            myDataGrid.ItemsSource = collectionView;
            
        }
        #endregion
        #region helpers

        //replaced by Regex version
        public string getKB_Substring(string pTitle)
        {
            var lStartIndex = pTitle.IndexOf("(");
            var stop = pTitle.IndexOf(")");
            return pTitle.Substring(lStartIndex + 1, stop -lStartIndex - 1);
        }


        public string getKB_RegEx(string pTitle)
        {
            string lSerachPattern = @"(((\w{2}\d+) ?))";
            string lInputString = pTitle;
            string lResultString = string.Empty;
            Regex lRegex = new Regex(lSerachPattern, RegexOptions.Multiline);
            MatchCollection lMatchCollection = lRegex.Matches(lInputString);

            if (lMatchCollection.Count > 0)
            {
                foreach (Match match in lMatchCollection)
                    lResultString = match.Value;
            }
            if (lResultString.StartsWith("KB",true, CultureInfo.InvariantCulture))
                return lResultString; //normalfall, KB in runden klammen: Windows-Tool zum Entfernen bösartiger Software x64 - Mai 2018 (KB890830)
            else
                return getKB_RegEx2(pTitle); //Spezialfall, KB ohne runde Klammern: Definitionsupdate für Windows Defender Antivirus – KB2267602 (Definition 1.267.1832.0)
        }

        public string getKB_RegEx2(string pTitle)
        {
            string lSerachPattern = @"KB\d+ ?";
            string lInputString = pTitle.ToUpper();
            string lResultString = string.Empty;
            Regex lRegex = new Regex(lSerachPattern, RegexOptions.Multiline);
            MatchCollection lMatchCollection = lRegex.Matches(lInputString);

            if (lMatchCollection.Count > 0)
            {
                foreach (Match match in lMatchCollection)
                    lResultString = match.Value;
            }
            return lResultString;
        }

        public void GetUpdates()
        {
            var updateSession = new UpdateSession();
            var updateSearcher = updateSession.CreateUpdateSearcher();
            var count = updateSearcher.GetTotalHistoryCount();
            var history = updateSearcher.QueryHistory(0, count);
            
            string lKB;
            for (int i = 0; i < count; i++)
            {

                //lKB = getKB_Substring(history[i].Title);
                try
                {
                    lKB = getKB_RegEx(history[i].Title);
                }
                catch (Exception exL)
                {
                    lKB = string.Empty;
                }

                if (!String.IsNullOrEmpty(history[i].Title))
                {
                    try
                    {
                        Updates.Add(new Update()
                        {

                            ID = i + 1,
                            KB = lKB,
                            Title = history[i].Title,
                            Date = history[i].Date,
                            ClientApplicationID = history[i].ClientApplicationID,
                            HResult = history[i].HResult,
                            Description = history[i].Description,
                            ServiceID = history[i].ServiceID,
                            SupportUrl = history[i].SupportUrl,
                            //Auskommentierte Zeilen gaben teilweise Exceptions, da sie null sein können:
                            //UnmappedResultCode = history[i].UnmappedResultCode,
                            //UninstallationNotes = history[i].UninstallationNotes,
                            //Special WUA
                            //Operation = history[i].Operation,
                            //ResultCode = history[i].ResultCode,
                            //serverSelection = history[i].ServerSelection,
                            //UninstallationSteps = history[i].UninstallationSteps,
                            //UpdateIdentity = history[i].UpdateIdentity
                        });
                    }
                    catch (Exception exI)
                    {

                    }
                }
                statusText3.Text = "Updates gesammt gefunden: " + count + ". Davon sind "+ Updates.Count+" sinvolle Einträge.";
            }

           
        }


        #endregion

        #region buttons
        /// <summary>
        /// To apply the filter to the DataGrid
        /// </summary>
        /// uses SearchBox and statusText2
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void applyFilterButton_Pressed(object sender, RoutedEventArgs e)
        {
            try
            {
                collectionView.Filter = (filtered) =>
                {
                    Update lUpdate = filtered as Update;
                    if (lUpdate.Title.ToLower().Contains(SearchBox.Text.ToLower()))
                        return true;
                    return false;
                };
            }
            catch (Exception eX) { }
            statusText2.Text = "Filter gesetzt, Items gefunden: "+ (collectionView.Count-1);

        }
        /// <summary>
        /// remove filter from DataGrid
        /// </summary>
        /// uses statusText2
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void removeFilterButton_Pressed(object sender, RoutedEventArgs e)
        {
            collectionView.Filter = null;
            statusText2.Text = "Filter entfernt: "+ (collectionView.Count-1);
        }
        /// <summary>
        /// Export DataGrid content to a CSV-File
        /// </summary>
        /// Uses statusText2
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportButton_Pressed(object sender, RoutedEventArgs e)
        {
            string lFilename;
            
            SaveFileDialog lSFDialog = new SaveFileDialog();
            //Give a default filename, but can be changed:
            lSFDialog.FileName = "GetUpdates"+ Environment.MachineName+"_"+ DateTime.Now.ToString("_yyyy-MM-dd_HH_mm_ss"); 
            lSFDialog.DefaultExt = ".csv"; 
            lSFDialog.Filter = "Comma Seperated Values (.csv)|*.csv"; // Filter files by extension

            // Show save file dialog box
            var result = lSFDialog.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                lFilename = lSFDialog.FileName;
            }
            else
            {
                statusText2.Text = "Export abgebrochen...";
                return;
            }


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string seperator = ListSeperators.Instance.cListSeparator;
            foreach (Update item in Updates)
            {
                sb.AppendLine(
                    item.ID.ToString() + seperator +
                    item.KB + seperator +
                    item.Date.ToString() + seperator +
                    item.Title + seperator +
                    item.ClientApplicationID + seperator +
                    item.HResult + seperator +
                    item.Description + seperator +
                    item.ServiceID);
                //sb.AppendLine(item.ToString());

            }

            StreamWriter file = new StreamWriter(lFilename,false, Encoding.UTF8);
            file.WriteLine(sb.ToString());
            statusText2.Text = "Exported erfolgreich...";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings settingsWND = new Settings();
            //über Mutterfenster Legen:
            settingsWND.Left = this.Left + (this.Width - this.ActualWidth) / 2;
            settingsWND.Top = this.Top + (this.Height - this.ActualHeight) / 2;
            settingsWND.Owner = this;
            settingsWND.Show();
            //this.Close();
        }

        #endregion

        private void aboutBTN_Click(object sender, RoutedEventArgs e)
        {
            About aboutWND = new About();
            //über Mutterfenster Legen:
            aboutWND.Left = this.Left + (this.Width - this.ActualWidth) / 2;
            aboutWND.Top = this.Top + (this.Height - this.ActualHeight) / 2;
            aboutWND.Owner = this;
            aboutWND.Show();
        }
    }
#endregion
}
