using Manager;
using Manager.Classes;
using Manager.Models;
using NLog;
using System.Diagnostics;
using System.Windows;

namespace GleamManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel VM = new ViewModel();
        Logger logger = LogManager.GetCurrentClassLogger();
        Manager.Classes.Manager Manager = new Manager.Classes.Manager();
        Process WorkingProcess { get; set; }
        Browser WorkingBrowser { get; set; }
        bool IsRunning { get; set; } = false;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
            VM.Browsers = new System.Collections.ObjectModel.ObservableCollection<Browser>(Filemanager.LoadBrowsers());
            StopBtn.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WorkingBrowser = (Browser)DG.SelectedItem;
                if(WorkingBrowser == null)
                {
                    return;
                }
                WorkingProcess = Manager.StartBrowser(WorkingBrowser);
                IsRunning = true;
                StopBtn.IsEnabled = true;
                StartBtn.IsEnabled = false;
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void StopBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Manager.StopBrowser(WorkingProcess, WorkingBrowser);
            IsRunning = false;
            StartBtn.IsEnabled = true;
            StopBtn.IsEnabled = false;
            WorkingBrowser = null;
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            Manager.NewBrowser();
            VM.Browsers = new System.Collections.ObjectModel.ObservableCollection<Browser>(Filemanager.LoadBrowsers());
            
        }

        private void Delete()
        {
            try
            {
                WorkingBrowser = (Browser)DG.SelectedItem;
            }
            catch (System.Exception e)
            {

                return;
            }
            if (IsRunning)
            {
                MessageBox.Show("Please stop browser first", "Error", MessageBoxButton.OK);
            }
            else
            {
                if(Filemanager.DeleteBrowser(WorkingBrowser))
                {
                    VM.Browsers.Remove(WorkingBrowser);
                }
            }
        }

        private void DG_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Delete:
                    Delete();
                    break;
            }
        }

        private void DG_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case System.Windows.Input.Key.Delete:
                    Delete();
                    break;
            }
        }
    }
}
