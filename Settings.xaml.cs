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
using System.Management.Automation;
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
            System.Windows.Forms.MessageBox.Show("Not implemented");
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

            
            //Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            string apath = dlg.FileName;
            if (apath == "")
            {
                System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
            }
            else
            {
                MessageBoxResult messageboxresult = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("You Selected:" + " " + apath, "Install?", (MessageBoxButtons)MessageBoxButton.OKCancel);

                if (messageboxresult == MessageBoxResult.OK)
                {
                    System.Diagnostics.Process installmc = System.Diagnostics.Process.Start("powershell", "Add-AppxPackage -Path " + apath);
                    installmc.WaitForExit();
                    int exitcode = installmc.ExitCode;
                    if (exitcode == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Minecraft installed successfully!");
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Minecraft failed to install, possibly due to unsupported Windows version, corrupted or damaged APPX package. Please download Minecraft package from Minecraft Downloader and try again.");
                    }
                }
                if (messageboxresult == MessageBoxResult.Cancel)
                {
                    System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
                }
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
                System.Diagnostics.Process stvc = System.Diagnostics.Process.Start("cmd.exe", clst);
                stvc.WaitForExit();
                System.Diagnostics.Process unsmc = System.Diagnostics.Process.Start("cmd.exe", pw);
                unsmc.WaitForExit();
                int exitcode = unsmc.ExitCode;
                if (exitcode == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Minecraft is uninstalled successfully!");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Minecraft is failed to uninstall.");
                }
            }
            if (messageboxresult == MessageBoxResult.Cancel)
            {
                System.Windows.Forms.MessageBox.Show("The operation is cancelled by user!");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Not implemented!");
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

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            MCDownloader mcd1 = new MCDownloader();
            mcd1.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Timer tm = new Timer();
            tm.Show();
        }
    }
}
