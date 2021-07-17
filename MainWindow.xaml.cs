using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BlueSky
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Console.WriteLine("NEVER CLOSING THIS WINDOW!");
            InitializeComponent();
            Console.WriteLine("BlueSky Launcher \nBeta 8 Version 0.8b");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("DEBUG: ActionID 1");
            System.Windows.Forms.MessageBox.Show("BlueSky Launcher Beta 8 \nVersion 0.8b \nSpecial Release \n\nBy ClickNin, TinedPakGamer, santu, Frakod, kostya, LureCore and XenonyxBlaze.");
        }



        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            
        }
            

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("DEBUG: Settings.Window opened!");
            Settings setting1 = new Settings();
            setting1.Show();
        }

        public void Enable()
        {
            try
            {
                Process process = Process.Start(new ProcessStartInfo("cmd.exe", "/k takeown /f C:\\Windows\\System32\\ClipSVC.dll && icacls C:\\Windows\\System32\\ClipSVC.dll /grant *S-1-3-4:F /t /c /l"));
                if (!process.WaitForExit(5000))
                {
                    process.Kill();
                }
                if (!File.Exists("C:\\BlueSky\\ClipSVC.dll"))
                {
                    File.Move("C:\\Windows\\System32\\ClipSVC.dll", "C:\\BlueSky\\ClipSVC.dll");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occur when trying to activate patch!");
            }
            ServiceController serviceController = new ServiceController("ClipSVC");
            if (serviceController.CanStop)
            {
                serviceController.Stop();
            }
        }

        public void Disable()
        {
            ServiceController serviceController = new ServiceController("ClipSVC");
            if (serviceController.CanStop)
            {
                serviceController.Stop();
            }
            if (File.Exists("C:\\BlueSky\\ClipSVC.dll"))
            {
                File.Copy("C:\\BlueSky\\ClipSVC.dll", "C:\\Windows\\System32\\ClipSVC.dll", true);
                File.Delete("C:\\BlueSky\\ClipSVC.dll");
            }
            if (serviceController.Status != ServiceControllerStatus.Running)
            {
                serviceController.Start();
            }
        }
    }
}
