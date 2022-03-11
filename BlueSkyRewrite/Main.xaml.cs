using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Management.Automation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace BlueSkyRewrite
{
    public partial class Main
    {
        string WindowsBuild;
        int DoingTask;
        //Config values
        int CollectData;
        int CollectLog;
        int UsePCName;
        int CollectENT;
        int Revert;
        int ForceDefDLL;
        int ClipSVC;
        //Global
        int extMC;
        public Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }
        private async Task MainAsync()
        {
            InitializeComponent();
            Log("Checking BlueSky config file...");
            await ConfigCheck();
            Log("Finished startup!");
            GetInformation();
            Log("Windows Build: " + WindowsBuild);
        }
        void OnClosing(object sender, CancelEventArgs e)
        {
            if (DoingTask == 1)
            {
                e.Cancel = true;
                System.Windows.MessageBox.Show("Error: To prevent certain errors from occuring, BlueSky will prevent you from closing the launcher if Minecraft is still running. Sorry for the inconvenience.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                e.Cancel = false;
            }
        }
        public async Task ConfigCheck()
        {
            if (!Directory.Exists(@"C:\BlueSky"))
            {
                Log("BlueSky's config directory not exist, creating...");
                Directory.CreateDirectory(@"C:\BlueSky");
            }
            if (!File.Exists(@"C:\BlueSky\config.dev"))
            {
                Log("Config file not exist, generating...");
                using (FileStream fs = File.Create(@"C:\BlueSky\config.dev"))
                {
                    //Main header
                    Byte[] HEADER = new UTF8Encoding(true).GetBytes("--START OF BLUESKY CONFIG FILE, PLEASE DO NOT MANUALLY EDIT THIS FILE!--\n");
                    fs.Write(HEADER, 0, HEADER.Length);
                    //Privacy settings
                    Byte[] PRIVACY_HEADER = new UTF8Encoding(true).GetBytes("--PRIVACY SETTINGS--\n");
                    fs.Write(PRIVACY_HEADER, 0, PRIVACY_HEADER.Length);
                    Byte[] PRIVACY_SETTINGS = new UTF8Encoding(true).GetBytes("allow_collect_data:0\nallow_collect_logs:0\nallow_use_pc_username:0\nallow_collect_marketplace_ent:0\n");
                    fs.Write(PRIVACY_SETTINGS, 0, PRIVACY_SETTINGS.Length);
                    //Advanced settings
                    Byte[] ADVANCED_HEADER = new UTF8Encoding(true).GetBytes("--ADVANCED SETTINGS--\n");
                    fs.Write(ADVANCED_HEADER, 0, ADVANCED_HEADER.Length);
                    Byte[] ADVANCED_SETTINGS = new UTF8Encoding(true).GetBytes("revert:1\nclipsvc:0\nforce_default_dll:0\nremember_info:1\n");
                    fs.Write(ADVANCED_SETTINGS, 0, ADVANCED_SETTINGS.Length);
                    //Bug report settings
                    Byte[] BUG_REPORT_HEADER = new UTF8Encoding(true).GetBytes("--BUG REPORT SETTINGS--\n");
                    fs.Write(BUG_REPORT_HEADER, 0, BUG_REPORT_HEADER.Length);
                    Byte[] BUG_REPORT_SETTINGS = new UTF8Encoding(true).GetBytes("identify:\ndiscord_tag:\nuuid:\nsave_info:1\n");
                    fs.Write(BUG_REPORT_SETTINGS, 0, BUG_REPORT_SETTINGS.Length);
                    //End header
                    Byte[] END_HEADER = new UTF8Encoding(true).GetBytes("--END OF CONFIG FILE--\n");
                    fs.Write(END_HEADER, 0, END_HEADER.Length);
                }
                Log("Config file generated!");
                Log("Finished config check!");
                return;
            }
            else
            {
                Log("Finished config check!");
                return;
            }
        }

        public void GetInformation()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            var UBR = registryKey.GetValue("UBR").ToString();
            var CurrentBuild = registryKey.GetValue("CurrentBuild").ToString();
            string version = CurrentBuild + "." + UBR;
            WindowsBuild = version;
            return;
        }
        public void Log(string message)
        {
            string time = DateTime.Now.ToString("h:mm:ss");
            Logger.Document.Blocks.Add(new Paragraph(new Run("[" + time + "] " + message)));
        }
        private void CreditsButton_Click(object sender, RoutedEventArgs e)
        {
            Credits credit = new Credits();
            credit.ShowDialog();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            DoingTask = 1;
            ProgressiveBar.IsIndeterminate = true;
            Log("Checking if Minecraft is installed...");
            StatusLabel.Content = "Initializing...";
            int Installed = await CheckInstalled("Microsoft.MinecraftUWP");
            if (Installed == 1)
            {
                DoingTask = 0;
                ProgressiveBar.IsIndeterminate = false;
                StatusLabel.Content = "Failed to launch!";
                Log("Error: Minecraft is not installed!");
                System.Windows.MessageBox.Show("Error: Minecraft is not installed!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                string MCVersion = await GetPackageVersion("Microsoft.MinecraftUWP");
                Log("Minecraft for Windows version " + MCVersion + " is installed");
                Log("Checking config file...");
                StatusLabel.Content = "Checking config...";
                await InitialConfigCheck();
                if (ForceDefDLL == 0 && ClipSVC == 0)
                {
                    StatusLabel.Content = "Checking DLL for your current Windows build...";
                    Log("Checking DLL available...");
                    DoingTask = 0;
                    ProgressiveBar.IsIndeterminate = false;
                    return;
                }
                else if (ForceDefDLL == 1 && ClipSVC == 0)
                {
                    StatusLabel.Content = "Checking DLL for your current Windows build...";
                    Log("Checking DLL available...");
                    DoingTask = 0;
                    ProgressiveBar.IsIndeterminate = false;
                    return;
                }
                else if(ClipSVC == 1)
                {
                    StatusLabel.Content = "Applying Anti Tracking...";
                    Log("Running Anti Tracking patches");
                    AntiTelemetryPatch();
                    StatusLabel.Content = "Patching ClipSVC...";
                    Log("Running ClipSVC patches");
                    ClipSVCPatch();
                    StatusLabel.Content = "Launching Protocol...";
                    Log("Launching Protocol");
                    Process.Start("minecraft://");
                    StatusLabel.Content = "Initializing...";
                    Log("Waiting for game to initilize...");
                    await Task.Delay(40);
                    StatusLabel.Content = "Killing...";
                    Log("Killing...");
                    await Task.Run(delegate ()
                    {
                        Process[] processesByName = Process.GetProcessesByName("RuntimeBroker");
                        for (int i = 0; i < processesByName.Length; i++)
                        {
                            processesByName[i].Kill();
                        }
                    });
                    StatusLabel.Content = "Done! Enjoy the game!";
                    Log("Finished!");
                    Process mc = Process.GetProcessesByName("Minecraft.Windows")[0];
                    mc.WaitForExit();
                    mc.Exited += ProcessEnded;
                }
            }
        }
        private static void ProcessEnded(object sender, EventArgs e)
        {
            Process process = sender as Process;
            if (process != null)
            {
                extMC = process.ExitCode;
            }
        }
        public async void ClipSVCPatch()
        {
            await Task.Run(delegate ()
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\ClipSVC\\Parameters", true);
                registryKey.SetValue("ServiceDll", "%SystemRoot%\\System32\\ClipSVC.dll" + new Random().Next(1, 1000).ToString(), RegistryValueKind.ExpandString);
                registryKey.Close();
            });
        }
        public void AntiTelemetryPatch()
        {
            RegistryKey key;
            key = Registry.LocalMachine.CreateSubKey($"SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection");
            key?.SetValue("AllowTelemetry", 0x00000000, RegistryValueKind.DWord);
            RegistryKey key1;
            key1 = Registry.LocalMachine.CreateSubKey($"SOFTWARE\\Policies\\Microsoft\\Windows\\System");
            key1?.SetValue("PublishUserActivities", 0x00000000, RegistryValueKind.DWord);
            RegistryKey key2;
            key2 = Registry.CurrentUser.CreateSubKey($"Software\\Policies\\Microsoft\\Windows\\EdgeUI");
            key2?.SetValue("DisableMFUTracking", 0x00000001, RegistryValueKind.DWord);
            key2 = Registry.LocalMachine.CreateSubKey($"SOFTWARE\\Policies\\Microsoft\\Windows\\EdgeUI");
            key2?.SetValue("DisableMFUTracking", 0x00000001, RegistryValueKind.DWord);
        }
        public async Task InitialConfigCheck()
        {
            string Config = @"C:\BlueSky\config.dev";
            string _AllowCollectData = File.ReadLines(Config).Skip(2).Take(1).First();
            string _AllowCollectLog = File.ReadLines(Config).Skip(3).Take(1).First();
            string _AllowUsePCName = File.ReadLines(Config).Skip(4).Take(1).First();
            string _AllowCollectMarketplaceENT = File.ReadLines(Config).Skip(5).Take(1).First();
            string _RevertAfterPlay = File.ReadLines(Config).Skip(7).Take(1).First();
            string _ForceClipSVC = File.ReadLines(Config).Skip(8).Take(1).First();
            string _ForceDefDLL = File.ReadLines(Config).Skip(9).Take(1).First();
            if (_AllowCollectData == "allow_collect_data:1")
            {
                CollectData = 1;
            }
            else
            {
                CollectData = 0;
            }
            if (_AllowCollectLog == "allow_collect_logs:1")
            {
                CollectLog = 1;
            }
            else
            {
                CollectLog = 0;
            }
            if (_AllowUsePCName == "allow_use_pc_username:1")
            {
                UsePCName = 1;
            }
            else
            {
                UsePCName = 0;
            }
            if (_AllowCollectMarketplaceENT == "allow_collect_marketplace_ent:1")
            {
                CollectENT = 1;
            }
            else
            {
                CollectENT = 0;
            }
            if (_RevertAfterPlay == "revert:1")
            {
                Revert = 1;
            }
            else
            {
                Revert = 0;
            }
            if (_ForceClipSVC == "clipsvc:1")
            {
                ClipSVC = 1;
            }
            else
            {
                ClipSVC = 0;
            }
            if (_ForceDefDLL == "force_default_dll:1")
            {
                ForceDefDLL = 1;
            }
            else
            {
                ForceDefDLL = 0;
            }
            Log("Done config check!");
        }
        public static async Task<string> GetPackageVersion(string PackageFamilyName)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-AppxPackage");
            ps.AddParameter("Name", PackageFamilyName);
            Collection<PSObject> results = null;
            await Task.Run(delegate
            {
                results = ps.Invoke();
            });
            if (results.Count == 0)
            {
                return "";
            }
            return (string)results[0].Members["Version"].Value;
        }
        public static async Task<int> CheckInstalled(string PackageFamilyName)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-AppxPackage");
            ps.AddParameter("Name", PackageFamilyName);
            Collection<PSObject> results = null;
            await Task.Run(delegate
            {
                results = ps.Invoke();
            });
            if (results.Count == 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
