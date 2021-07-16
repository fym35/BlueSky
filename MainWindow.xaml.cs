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

        //BlueSky Language File Parser

        //Main UI
        string abouttext_l1 = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string abouttext_l2 = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(41).Take(42).First();
        string playdb = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(5).Take(6).First();
        string settingb = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(6).Take(7).First();
        string aboutb = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(7).Take(8).First();

        //Setting UI
        string deveb = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(12).Take(13).First();
        string bssettinglabel = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(11).Take(12).First();
        string disdev = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(13).Take(14).First();
        string enadev = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(14).Take(15).First();
        string disbsmode = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string disclipsvc = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string unsmc = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string insmc = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string insmcextracted = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string mcdownloader = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();

        //Developer UI
        string aboutdb = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(7).Take(8).First();
        string devlabel = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(24).Take(25).First();
        string playb1 = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(25).Take(26).First();
        string playb2 = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(26).Take(27).First();
        string playb3 = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(27).Take(28).First();

        //Minecraft Downloader
        string mcdlabel = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(31).Take(32).First();
        string lststab = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(37).Take(38).First();
        string lstbeta = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(38).Take(39).First();
        string label1t = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(32).Take(33).First();
        string label2t = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(33).Take(34).First();
        string label3t = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(34).Take(35).First();
        string label4t = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(35).Take(36).First();
        string label5t = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(36).Take(37).First();
        string downstable = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(39).Take(40).First();
        string downbeta = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(40).Take(41).First();

        //Dialogues
        string notimplt = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(50).Take(51).First();
        string csvcdis = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(51).Take(52).First();
        string appxseldialog = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(52).Take(53).First();
        string msixseldialog = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(53).Take(54).First();
        string opencancelled = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(54).Take(55).First();
        string mcinssuccess = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(55).Take(56).First();
        string mcinsfailed = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(56).Take(57).First();
        string youselectedtxt = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(57).Take(58).First();
        string insmcquestion = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(58).Take(59).First();
        string areusuretounsmcquestion = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(59).Take(60).First();
        string unsmcquestion = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(60).Take(61).First();
        string mcunssuccess = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(61).Take(62).First();
        string mcunsfailed = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(62).Take(63).First();
        string devmodeturnedon = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(63).Take(64).First();
        string devmodeturnedoff = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(49).Take(50).First();
        string devsettingabout = File.ReadLines(AppDomain.CurrentDomain.BaseDirectory + "\\languages\\en-us.bslang").Skip(64).Take(65).First();

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
