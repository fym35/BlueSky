using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Management.Automation;
using System.Windows.Forms;

namespace BlueSky
{
    /// <summary>
    /// Interaction logic for NewSettings.xaml
    /// </summary>
    public partial class NewSettings : Window
    {
        public NewSettings()
        {
            InitializeComponent();
        }

        private void installmcbtn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog installmc = new System.Windows.Forms.OpenFileDialog();
            installmc.DefaultExt = ".appx";
            installmc.Filter = "Appx File (*.appx)|*.appx|MSIX File (*.msix)|*.msix";
            installmc.ShowDialog();

            string pckpath = installmc.FileName;
            if (pckpath != null)
            {
                progresslab.Content = "Installing...";
                DisableAllBtn();
                var installer = PowerShell.Create();
                installer.AddCommand("Add-AppxPackage");
                installer.AddParameter("Path", installmc);
                installer.Streams.Error.DataAdded += this.Error_DataAdded;
                installer.Streams.Progress.DataAdded += this.Progress_DataAdded;
                installer.Invoke();
                if(installer.HadErrors==false)
                {
                    EnableAllBtn();
                    progresslab.Content = "Minecraft has been installed successfully!";
                }
                else
                {
                    EnableAllBtn();
                    progresslab.Content = "Minecraft has been failed to installed successfully!";
                }
            } 
            else
            {
                System.Windows.Forms.MessageBox.Show("Operation as been cancelled!");
            }
        }

        private async void Progress_DataAdded(object sender, DataAddedEventArgs e)
        {
            await Dispatcher.Invoke(async () =>
            {
                PSDataCollection<ProgressRecord> psdataCollection = (PSDataCollection<ProgressRecord>)sender;
                int progress = psdataCollection[e.Index].PercentComplete;
                progressbar.Value = progress;
            });
        }
        private async void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ErrorRecord> psdataCollection = (PSDataCollection<ErrorRecord>)sender;
            string error = psdataCollection[e.Index].FullyQualifiedErrorId;
        }

        void DisableAllBtn()
        {
            installmcbtn.IsEnabled = false;
            uninstallmcbtn.IsEnabled = false;
            wipealldatabtn.IsEnabled = false;
            wiperscpackbtn.IsEnabled = false;
            wipeallbhpckbtn.IsEnabled = false;
            wipeallsettingsbtn.IsEnabled = false;
            offlicensebtn.IsEnabled = false;
            disclipsvc.IsEnabled = false;
            restorewindowsbtn.IsEnabled = false;
        }

        void EnableAllBtn()
        {
            installmcbtn.IsEnabled = true;
            uninstallmcbtn.IsEnabled = true;
            wipealldatabtn.IsEnabled = true;
            wiperscpackbtn.IsEnabled = true;
            wipeallbhpckbtn.IsEnabled = true;
            wipeallsettingsbtn.IsEnabled = true;
            offlicensebtn.IsEnabled = true;
            disclipsvc.IsEnabled = true;
            restorewindowsbtn.IsEnabled = true;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
