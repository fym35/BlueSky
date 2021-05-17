using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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
            Console.WriteLine("BlueSky Launcher \nBeta 7 Version 0.7a");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("DEBUG: ActionID 1");
            System.Windows.Forms.MessageBox.Show("BlueSky Launcher \nBeta 7 - Version 0.7a \n\nCREDITS \nClickNin: Main developer \nTech Archives: Concept, Ideas and GUI Design \nTinedPakGamer: Method and concepts \nsantu: Powershell code helper \nGhostDelective, Razer: Concept, Ideas \n\nABOUT \nBlueSky Launcher is a cracked Minecraft for Windows 10 launcher that's aiming for a user-friendly, easy to use and feature-rich launcher. This has made with love and support from the community. This has made because Minecraft Windows 10 Edition is fucking expensive but peoples want it's own rich-multiplayer. \n\nABOUT PERMISSIONS \nBlueSky Launcher is a a free product, non soldable. Please ask for our permissions first before trying to distribute the launcher to anywhere expect My and TinedPakGamer's Discord Server in order to prevent virus, malware or anything that peoples can inject into the program. This actions will both make users safe since no one can distribute harmful version of BlueSky. If you're going to make a video have content that included or related to BlueSky, please give us credits first. \n\nABOUT DECOMPILATION \nYou are not and will never allowed to decompile this launcher or any launcher that made by who supported this launcher to complete source code and claiming it as your own. We are not allowing that to happens. Please use this launcher in the way it has been designed to. If you come to BlueSky just for steal it, then bye.");
        }



        private async void Button_Click1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Initilizing...");
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\ClipSVC\Parameters", true);

            //adding/editing a value 
            key.SetValue("ServiceDll", @"%SystemRoot%\System32\ClipSVC.dll" + new Random().Next(1, 1000), RegistryValueKind.ExpandString); //sets 'someData' in 'someValue' 

            key.Close();
            Console.WriteLine("Running setup...");
            string strCmdText;
            string mc1;
            string mc2;
            strCmdText = @"/C net stop ""ClipSVC"" ";
            mc1 = @"/C explorer.exe shell:appsFolder\Microsoft.MinecraftUWP_8wekyb3d8bbwe!App";
            mc2 = @"/C taskkill /IM ""RuntimeBroker.exe"" /F";
            Console.WriteLine("Initilizing progress complete! Disabling ClipSVC...");
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            Console.WriteLine("Starting Minecraft...");
            System.Diagnostics.Process.Start("CMD.exe", mc1);
            Console.WriteLine("Countdown: 45 seconds");
            await Task.Delay(45000);
            Console.WriteLine("Countdown is over!");
            System.Diagnostics.Process.Start("CMD.exe", mc2);
            Console.WriteLine("Minecraft now started and fully playable! If Minecraft still in trial mode or stucking in Loading phases, try to start the game again.");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("DEBUG: Settings.Window opened!");
            Settings setting1 = new Settings();
            setting1.Show();
        }
    }
}
