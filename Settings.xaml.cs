using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Documents;
using Windows.Management.Deployment;
using System.Collections.ObjectModel;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.Windows;
using Microsoft.Win32;
using System.ComponentModel;

namespace BlueSkyRewrite
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        int DoingTask;
        public Settings()
        {
            SettingsAsync().GetAwaiter().GetResult();
        }
        public async Task SettingsAsync()
        {
            InitializeComponent();
            await ParseConfig();
        }
        public async Task ParseConfig()
        {
            string Config = @"C:\BlueSky\config.dev";
            //Privacy settings
            string _AllowCollectData = File.ReadLines(Config).Skip(2).Take(1).First();
            string _AllowCollectLog = File.ReadLines(Config).Skip(3).Take(1).First();
            string _AllowUsePCName = File.ReadLines(Config).Skip(4).Take(1).First();
            string _AllowCollectMarketplaceENT = File.ReadLines(Config).Skip(5).Take(1).First();
            if (_AllowCollectData == "allow_collect_data:1")
            {
                AllowCollectData.IsChecked = true;
            }
            if (_AllowCollectLog == "allow_collect_logs:1")
            {
                AllowCollectLog.IsChecked = true;
            }
            if (_AllowUsePCName == "allow_use_pc_username:1")
            {
                AllowUsePCName.IsChecked = true;
            }
            if (_AllowCollectMarketplaceENT == "allow_collect_marketplace_ent:1")
            {
                AllowCollectMarketplaceStuff.IsChecked = true;
            }
            //Advanced settings
            string _RevertAfterPlay = File.ReadLines(Config).Skip(7).Take(1).First();
            string _ForceClipSVC = File.ReadLines(Config).Skip(8).Take(1).First();
            string _ForceDefDLL = File.ReadLines(Config).Skip(9).Take(1).First();
            string _RememberInfo = File.ReadLines(Config).Skip(10).Take(1).First();
            if (_RevertAfterPlay == "revert:1")
            {
                RevertSystem.IsChecked = true;
            }
            if (_ForceClipSVC == "clipsvc:1")
            {
                UseSVC.IsChecked = true;
            }
            if (_ForceDefDLL == "force_default_dll:1")
            {
                ForceDefDLL.IsChecked = true;
            }
            if (_RememberInfo == "remember_info:1")
            {
                RememberInfo.IsChecked = true;
            }
        }
        void OnClosing(object sender, CancelEventArgs e)
        {
            if (DoingTask == 1)
            {
                e.Cancel = true;
                System.Windows.MessageBox.Show("Error: BlueSky is executing an action, please wait until it finishes.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                string Config = @"C:\BlueSky\config.dev";
                if (AllowCollectData.IsChecked == true)
                {
                    ChangeLine("allow_collect_data:1", Config, 3);
                }
                else
                {
                    ChangeLine("allow_collect_data:0", Config, 3);
                }
                if (AllowCollectLog.IsChecked == true)
                {
                    ChangeLine("allow_collect_logs:1", Config, 4);
                }
                else
                {
                    ChangeLine("allow_collect_logs:0", Config, 4);
                }
                if (AllowUsePCName.IsChecked == true)
                {
                    ChangeLine("allow_use_pc_username:1", Config, 5);
                }
                else
                {
                    ChangeLine("allow_use_pc_username:0", Config, 5);
                }
                if (AllowCollectMarketplaceStuff.IsChecked == true)
                {
                    ChangeLine("allow_collect_marketplace_ent:1", Config, 6);
                }
                else
                {
                    ChangeLine("allow_collect_marketplace_ent:0", Config, 6);
                }
                if (RevertSystem.IsChecked == true)
                {
                    ChangeLine("revert:1", Config, 8);
                }
                else
                {
                    ChangeLine("revert:0", Config, 8);
                }
                if (UseSVC.IsChecked == true)
                {
                    ChangeLine("clipsvc:1", Config, 9);
                }
                else
                {
                    ChangeLine("clipsvc:0", Config, 9);
                }
                if (ForceDefDLL.IsChecked == true)
                {
                    ChangeLine("force_default_dll:1", Config, 10);
                }
                else
                {
                    ChangeLine("force_default_dll:0", Config, 10);
                }
                if (RememberInfo.IsChecked == true)
                {
                    ChangeLine("remember_info:1", Config, 11);
                }
                else
                {
                    ChangeLine("remember_info:0", Config, 11);
                }
                e.Cancel = false;
            }
        }
        private async void InstallMC_Click(object sender, RoutedEventArgs e)
        {
            int GameInstalled = await CheckInstalled("Microsoft.MinecraftUWP");
            if (GameInstalled == 0)
            {
                System.Windows.MessageBox.Show("Error: Minecraft is already installed!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                System.Windows.Forms.OpenFileDialog MCAppx = new System.Windows.Forms.OpenFileDialog();
                MCAppx.DefaultExt = ".appx";
                MCAppx.Filter = "Appx File (*.appx)|*.appx";
                MCAppx.Title = "Please select Minecraft Appx file";
                MCAppx.ShowDialog();
                string AppxPath = MCAppx.FileName;
                if (AppxPath.Length == 0)
                {
                    System.Windows.MessageBox.Show("Error: Invalid file or no file was selected!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                else
                {
                    string AppxName = System.IO.Path.GetFileName(AppxPath);
                    MessageBoxResult result = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("Do you want to install " + AppxName + "?", "Confirmination", (MessageBoxButtons)MessageBoxButton.YesNo, MessageBoxIcon.Asterisk);
                    if (result == MessageBoxResult.Yes)
                    {
                        DoingTask = 1;
                        StatusString.Content = "Installing " + AppxName + "...";
                        PowerShell ps = PowerShell.Create();
                        ps.AddCommand("Add-AppxPackage");
                        ps.AddParameter("Path", AppxPath);
                        ps.Streams.Error.DataAdded += InstallationErrorData;
                        ps.Streams.Progress.DataAdded += InstallationProgressData;
                        await Task.Run(delegate ()
                        {
                            ps.Invoke();
                        });
                        if (!ps.HadErrors)
                        {
                            DoingTask = 0;
                            StatusString.Content = AppxName + " is installed successfully!";
                            System.Windows.MessageBox.Show("Minecraft is installed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            return;
                        }
                        else
                        {
                            DoingTask = 0;
                            StatusString.Content = AppxName + " is failed to installled successfully!";
                            System.Windows.MessageBox.Show("Minecraft failed to installed successfully!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Error: Operation Cancelled!", "Notice", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                }
            }
        }

        private void InstallationProgressData(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ProgressRecord> psdataCollection = (PSDataCollection<ProgressRecord>)sender;
            CurrentProgress = psdataCollection[e.Index].PercentComplete;
        }
        private int CurrentProgress
        {
            set
            {
                System.Windows.Application.Current.Dispatcher.Invoke(delegate ()
                {
                    ProgressiveBar.Value = (double)value;
                });
            }
        }
        private void InstallationErrorData(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ErrorRecord> psdataCollection = (PSDataCollection<ErrorRecord>)sender;
            string Error = psdataCollection[e.Index].FullyQualifiedErrorId;
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
        public static async Task<string> GetName(string package)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Get-AppxPackage");
            ps.AddParameter("Name", package);
            Collection<PSObject> results = null;
            await Task.Run(delegate
            {
                results = ps.Invoke();
            });
            if (results.Count == 0)
            {
                return null;
            }
            return (string)results[0].Members["PackageFullName"].Value;
        }

        private async void UninstallMC_Click(object sender, RoutedEventArgs e)
        {
            int GameInstalled = await CheckInstalled("Microsoft.MinecraftUWP");
            if (GameInstalled == 1)
            {
                System.Windows.MessageBox.Show("Error: Minecraft is not installed!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            else
            {
                MessageBoxResult result = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("Are you sure you want to uninstall Minecraft? This will permanently delete all of your worlds, addons, resources packs and skins which cannot be reverted after done!", "Confirmination", (MessageBoxButtons)MessageBoxButton.YesNo, MessageBoxIcon.Asterisk);
                if (result == MessageBoxResult.Yes)
                {
                    DoingTask = 1;
                    StatusString.Content = "Uninstalling...";
                    string MCFullName = await GetName("Microsoft.MinecraftUWP");
                    PowerShell ps = PowerShell.Create();
                    ps.AddCommand("Remove-AppxPackage");
                    ps.AddParameter("-Package", MCFullName);
                    ps.Streams.Error.DataAdded += InstallationErrorData;
                    ps.Streams.Progress.DataAdded += InstallationProgressData;
                    await Task.Run(delegate ()
                    {
                        ps.Invoke();
                    });
                    if (!ps.HadErrors)
                    {
                        DoingTask = 0;
                        StatusString.Content = "Minecraft is uninstalled successfully!";
                        System.Windows.MessageBox.Show("Minecraft is uninstalled successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        return;
                    }
                    else
                    {
                        DoingTask = 0;
                        StatusString.Content = "Minecraft is failed to uninstalled successfully!";
                        System.Windows.MessageBox.Show("Minecraft is failed to uninstalled successfully!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Error: Operation Cancelled!", "Notice", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    return;
                }
            }
        }
        static void ChangeLine(string Text, string FileName, int Line)
        {
            string[] ArrLine = File.ReadAllLines(FileName);
            ArrLine[Line - 1] = Text;
            File.WriteAllLines(FileName, ArrLine);
        }
        private void SaveAllChanges_Click(object sender, RoutedEventArgs e)
        {
            string Config = @"C:\BlueSky\config.dev";
            if (AllowCollectData.IsChecked == true)
            {
                ChangeLine("allow_collect_data:1", Config, 3);
            }
            else
            {
                ChangeLine("allow_collect_data:0", Config, 3);
            }
            if (AllowCollectLog.IsChecked == true)
            {
                ChangeLine("allow_collect_logs:1", Config, 4);
            }
            else
            {
                ChangeLine("allow_collect_logs:0", Config, 4);
            }
            if (AllowUsePCName.IsChecked == true)
            {
                ChangeLine("allow_use_pc_username:1", Config, 5);
            }
            else
            {
                ChangeLine("allow_use_pc_username:0", Config, 5);
            }
            if (AllowCollectMarketplaceStuff.IsChecked == true)
            {
                ChangeLine("allow_collect_marketplace_ent:1", Config, 6);
            }
            else
            {
                ChangeLine("allow_collect_marketplace_ent:0", Config, 6);
            }
            if (RevertSystem.IsChecked == true)
            {
                ChangeLine("revert:1", Config, 8);
            }
            else
            {
                ChangeLine("revert:0", Config, 8);
            }
            if (UseSVC.IsChecked == true)
            {
                ChangeLine("clipsvc:1", Config, 9);
            }
            else
            {
                ChangeLine("clipsvc:0", Config, 9);
            }
            if (ForceDefDLL.IsChecked == true)
            {
                ChangeLine("force_default_dll:1", Config, 10);
            }
            else
            {
                ChangeLine("force_default_dll:0", Config, 10);
            }
            if (RememberInfo.IsChecked == true)
            {
                ChangeLine("remember_info:1", Config, 11);
            }
            else
            {
                ChangeLine("remember_info:0", Config, 11);
            }
            System.Windows.MessageBox.Show("All changes has been saved!", "Saved", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
