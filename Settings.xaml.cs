using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Forms;

namespace BlueSky
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Please wait...");
            string DevModeOff;
            DevModeOff = AppDomain.CurrentDomain.BaseDirectory + "DevMode/OffDev.reg";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + DevModeOff + "\"");
            regeditProcess.WaitForExit();
            Console.WriteLine("DEBUG: Sending data...");
            Console.WriteLine("Developer Mode is turned off!");
            System.Windows.Forms.MessageBox.Show("Developer Mode is turned off!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageboxresult = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("This will restore your PC and all changes made by the launcher to its own original state. But, if somethings goes wrong, this can cause a crictal damage. Please create a full backup of your system first before clicking OK, else click Cancel.", "WARNING", (MessageBoxButtons)MessageBoxButton.OKCancel);

            if (messageboxresult == MessageBoxResult.OK)
            {
                Console.WriteLine("Turning off User Account Control...");
                // disable uac first to prevent error
                string uacdisable = AppDomain.CurrentDomain.BaseDirectory + "UAC/disable.reg";
                Process regeditProcess = Process.Start("regedit.exe", "/s \"" + uacdisable + "\"");
                regeditProcess.WaitForExit();
                // start operation
                Console.WriteLine("Starting operation...");
                string sys32path = @"C:\Windows\System32\Windows.ApplicationModel.Store.dll";
                string wow64path = @"C:\Windows\SysWOW64\Windows.ApplicationModel.Store.dll";
                System.Diagnostics.Process res1 = System.Diagnostics.Process.Start("C:\\Windows\\System32\\sfc.exe" + sys32path);
                res1.WaitForExit();
                Console.WriteLine("Restored!");
                System.Diagnostics.Process res2 = System.Diagnostics.Process.Start("C:\\Windows\\System32\\sfc.exe" + wow64path);
                res2.WaitForExit();
                Console.WriteLine("Restored!");
                // re-enable uac because we completed our task
                Console.WriteLine("Finalizing the task...");
                string uacenable = AppDomain.CurrentDomain.BaseDirectory + "UAC/enable.reg";
                Process regeditProcess2 = Process.Start("regedit.exe", "/s \"" + uacenable + "\"");
                regeditProcess2.WaitForExit();
                Console.WriteLine("DEBUG: Sending data...");
                Console.WriteLine("Restore finished!");
                System.Windows.Forms.MessageBox.Show("Restore finished!");
            }
            if (messageboxresult == MessageBoxResult.Cancel)
            {
                System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ClipSVC\Parameters", true);

            //adding/editing a value 
            key.SetValue("ServiceDll", @"%SystemRoot%\System32\ClipSVC.dll", RegistryValueKind.ExpandString); //sets 'someData' in 'someValue' 

            key.Close();
            string clst1;
            clst1 = @"/C net start ""ClipSVC"" ";
            System.Diagnostics.Process.Start("cmd.exe", clst1);
            System.Windows.Forms.MessageBox.Show("ClipSVC exploit is disabled!");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".appx";
            dlg.Filter = "Appx File (*.appx)|*.appx|MSIX File (*.msix)|*.msix";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            string apath = dlg.FileName;
            MessageBoxResult messageboxresult = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("You Selected:" + apath, "Install?", (MessageBoxButtons)MessageBoxButton.OKCancel);

            if (messageboxresult == MessageBoxResult.OK)
            {
                System.Diagnostics.Process.Start("powershell", "Add-AppxPackage -Path " + apath);
                System.Windows.Forms.MessageBox.Show("Installed Minecraft!");
            }
            if (messageboxresult == MessageBoxResult.Cancel)
            {
                System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ClipSVC\Parameters", true);

            //adding/editing a value 
            key.SetValue("ServiceDll", @"%SystemRoot%\System32\ClipSVC.dll", RegistryValueKind.ExpandString); //sets 'someData' in 'someValue' 

            key.Close();
            MessageBoxResult messageboxresult = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("Are you sure you want to uninstall the game?", "ATTENTION", (MessageBoxButtons)MessageBoxButton.OKCancel);

            if (messageboxresult == MessageBoxResult.OK)
            {
                string clst;
                string pw;
                clst = @"/C net start ""ClipSVC"" ";
                pw = @"/C powershell -Command ""Get-AppxPackage Microsoft.MinecraftUWP | Remove-AppxPackage"" ";
                System.Diagnostics.Process.Start("cmd.exe", clst);
                System.Diagnostics.Process.Start("cmd.exe", pw);
                System.Windows.Forms.MessageBox.Show("MC is uninstalled!");
            }
            if (messageboxresult == MessageBoxResult.Cancel)
            {
                System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageboxresult = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("Before install please enable Developer Mode first else the install will fail. Also, under anyway, this still in WIP, so bug may occurs. I highly recommends you to use Appx install instead.", "WARNING", (MessageBoxButtons)MessageBoxButton.OKCancel);

            if (messageboxresult == MessageBoxResult.OK)
            {
                var messageresut = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = messageresut.ShowDialog();
                var path = messageresut.SelectedPath;
                System.Windows.Forms.MessageBox.Show("You Selected: " + path);
                System.Diagnostics.Process.Start("powershell", "Add-AppxPackage -Path " + path + "AppxManifest.xml -Register");

            }
            if (messageboxresult == MessageBoxResult.Cancel)
            {
                System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
            }
        }

        private void Button_Click9(object sender, RoutedEventArgs e)
        {
            string DevModeOn;
            DevModeOn = AppDomain.CurrentDomain.BaseDirectory + "DevMode/OnDev.reg";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + DevModeOn + "\"");
            regeditProcess.WaitForExit();
            System.Windows.Forms.MessageBox.Show("Developer Mode is turned on!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DevCheck dev1 = new DevCheck();
            dev1.Show();
        }
    }
}
