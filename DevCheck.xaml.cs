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
using System.IO;
using Microsoft.Win32;

namespace BlueSky
{
    /// <summary>
    /// Interaction logic for DevCheck.xaml
    /// </summary>
    public partial class DevCheck : Window
    {
        public DevCheck()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developer Options is some set of advanced testing and experimenting features of BlueSky.");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (File.Exists(@"C:\BlueSky\DONTDELETE.txt"))
            { 
            string strCmdText;
            strCmdText = @"/C explorer.exe shell:appsFolder\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            }
            else
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ClipSVC\Parameters", true);

                //adding/editing a value 
                key.SetValue("ServiceDll", @"%SystemRoot%\System32\ClipSVC.dll" + new Random().Next(1, 1000), RegistryValueKind.ExpandString); //sets 'someData' in 'someValue' 

                key.Close();
                string path;
                path = @"C:\Windows\System32\Windows.ApplicationModel.Store.dll";
                System.Diagnostics.Process takeperm1 = System.Diagnostics.Process.Start("cmd.exe", @"/c takeown /f " + path + @" && icacls " + path + " /grant %username%:F");
                takeperm1.WaitForExit();
                string path2;
                path2 = @"C:\Windows\SysWOW64\Windows.ApplicationModel.Store.dll";
                System.Diagnostics.Process takeperm2 = System.Diagnostics.Process.Start("cmd.exe", @"/c takeown /f " + path2 + @" && icacls " + path2 + " /grant %username%:F");
                takeperm2.WaitForExit();
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "x86/Windows.ApplicationModel.Store.dll", "C:\\Windows\\SysWOW64\\Windows.ApplicationModel.Store.dll", true);
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "x64/Windows.ApplicationModel.Store.dll", "C:\\Windows\\System32\\Windows.ApplicationModel.Store.dll", true);
                string confirmf = @"C:\BlueSky\DONTDELETE.txt";
                Directory.CreateDirectory("C:\\BlueSky\\");
                File.Create(confirmf);
                string mc1;
                mc1 = @"/C explorer.exe shell:appsFolder\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App";
                System.Diagnostics.Process.Start("CMD.exe", mc1);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string strCmdText;
            strCmdText = @"/C net stop ""ClipSVC"" ";
            System.Diagnostics.Process killsvc = System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            killsvc.WaitForExit();
            string mc1;
            mc1 = @"/C explorer.exe shell:appsFolder\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App";
            System.Diagnostics.Process mcst = System.Diagnostics.Process.Start("CMD.exe", mc1);
            mcst.WaitForExit();
            var j = new ProcessMemoryReader();
            var processname = Process.Start("CMD.exe", @"/C explorer.exe shell:appsFolder\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App");
            j.ReadProcess = processname;
            j.OpenProcess();
            await Task.Delay(45000);
            string mc2 = @"/C taskkill /IM ""RuntimeBroker.exe"" /F";
            System.Diagnostics.Process kill1 = System.Diagnostics.Process.Start("CMD.exe", mc2);
            kill1.WaitForExit();
            RegistryKey key1 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ClipSVC\Parameters", true);

            //adding/editing a value 
            key1.SetValue("ServiceDll", @"%SystemRoot%\System32\ClipSVC.dll", RegistryValueKind.ExpandString); //sets 'someData' in 'someValue' 

            key1.Close();
            string clst1;
            clst1 = @"/C net start ""ClipSVC"" ";
            System.Diagnostics.Process.Start("cmd.exe", clst1);
            MessageBox.Show("Game started successfully!");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not impletemented!");
        }
    }
}
    
