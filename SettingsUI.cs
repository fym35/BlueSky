using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;
using System.Threading;
using System.Windows;
using System.IO;
using System.Diagnostics;
using System.ServiceProcess;
using System.Net;
using System.Windows.Threading;

namespace BlueSkyNew
{
    public partial class SettingsUI : Form
    {
        public SettingsUI()
        {
            InitializeComponent();
            MCD_Refresh();
        }

        WebClient client;
        WebClient client1;

        private async void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog mcpck = new System.Windows.Forms.OpenFileDialog();
            mcpck.DefaultExt = ".appx";
            mcpck.Filter = "Appx File (*.appx)|*.appx|MSIX File (*.msix)|*.msix";
            mcpck.ShowDialog();
            string PCKPath = mcpck.FileName;
            if (PCKPath.Length != 0)
            {
                DisableAllButton();
                progressBar1.Value = 0;
                label5.Text = "Installing package...";
                PowerShell ps = PowerShell.Create();
                ps.AddCommand("Add-AppxPackage");
                ps.AddParameter("Path", PCKPath);
                ps.Streams.Error.DataAdded += this.Error_DataAdded;
                ps.Streams.Progress.DataAdded += this.Progress_DataAdded;
                await Task.Run(delegate ()
                {
                    ps.Invoke();
                });
                if (!ps.HadErrors)
                {
                    label5.Text = "Package installed successfully!";
                    EnableAllButton();
                }
                else
                {
                    label5.Text = "Package failed to install! Error code 0002";
                    EnableAllButton();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Operation cancelled!");
            }
        }

        private void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ErrorRecord> psdataCollection = (PSDataCollection<ErrorRecord>)sender;
            string error = psdataCollection[e.Index].FullyQualifiedErrorId;
        }

        private void Progress_DataAdded(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ProgressRecord> psdataCollection = (PSDataCollection<ProgressRecord>)sender;
            int prog = psdataCollection[e.Index].PercentComplete;
            if(prog>0)
            {
                G_progress = prog;
            }
        }

        int G_progress
        {
            get
            {
                return 0;
            }
            set
            {
                set_progress(value);
            }

        }
        private delegate void progresser(int arg);
        void set_progress(int arg)
        {
            if (progressBar1.InvokeRequired)
            {
                var d = new progresser(set_progress);
                progressBar1.Invoke(d, new object[] { arg });
            }
            else
            {
                progressBar1.Value = arg;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("Are you sure you want to uninstall Minecraft? This will ERASE your game data!", "WARNING", (MessageBoxButtons)MessageBoxButton.OKCancel);
            if(result==MessageBoxResult.OK)
            {
                DisableAllButton();
                progressBar1.Value = 0;
                label5.Text = "Uninstalling...";
                progressBar1.Value = 50;
                string uns = @"/C powershell -Command ""Get-AppxPackage Microsoft.MinecraftUWP | Remove-AppxPackage"" ";
                System.Diagnostics.Process uninstall = System.Diagnostics.Process.Start("CMD.exe", uns);
                uninstall.WaitForExit();
                int errCode = uninstall.ExitCode;
                if (errCode == 0)
                {
                        progressBar1.Value = 100;
                        label5.Text = "Package uninstalled successfully!";
                        EnableAllButton();
                }
                else
                {
                        progressBar1.Value = 100;
                        label5.Text = "Package failed to uninstall! Error code 0005";
                        EnableAllButton();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Operation cancelled!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Patching Xbox Live Session...";
            await Task.Run(delegate ()
            {
                System.Diagnostics.Process xblp = System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + "Assets/xblive_patch.EXE");
            });
            label5.Text = "Xbox Live Session patched successfully! Please restart your game session for change to take effect!";
            progressBar1.Value = 100;
            EnableAllButton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Checking process...";
            Process[] pname = Process.GetProcessesByName("Minecraft.Windows");
            if (pname.Length == 0)
            {
                progressBar1.Value = 50;
                label5.Text = "All checks passed! Disabling";
                Process[] processesByName = Process.GetProcessesByName("RuntimeBroker");
                for (int i = 0; i < processesByName.Length; i++)
                {
                    processesByName[i].Kill();
                }
                progressBar1.Value = 75;
                string disable = AppDomain.CurrentDomain.BaseDirectory + "Assets/DISABLE.reg";
                Process regeditProcess = Process.Start("regedit.exe", "/s \"" + disable + "\"");
                regeditProcess.WaitForExit();
                progressBar1.Value = 95;
                ServiceController serviceController = new ServiceController("ClipSVC");
                if (serviceController.Status != ServiceControllerStatus.Running)
                {
                    serviceController.Start();
                }
                progressBar1.Value = 100;
                label5.Text = "ClipSVC Exploit has been disabled successfully!";
                EnableAllButton();
            }
            else
            {
                progressBar1.Value = 100;
                label5.Text = "Minecraft still running! Please close your Minecraft session first before disable the exploit! Error code 0006";
                EnableAllButton();
            }
        }

        void MCD_Refresh()
        {
            try
            {
                Directory.CreateDirectory("C:\\BlueSky\\");
                WebClient Client = new WebClient();
                Client.DownloadFile("https://bluesky-webserver.000webhostapp.com/BLUESKY-VER-CHECK.bluesky", @"C:\BlueSky\blueskymcd.bluesky");
                string lateststable = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(2).Take(1).First();
                string latestbeta = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(5).Take(6).First();
                string lateststableinfo = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(1).Take(1).First();
                string latestbetainfo = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(4).Take(1).First();
                label3.Text = lateststableinfo;
                label4.Text = latestbetainfo;
            }
            catch (Exception ex)
            {
                label4.Text = "";
                label3.Text = "An error occur when trying to fetch Update Database! Error code 0007";
                button6.Enabled = false;
                button7.Enabled = false;
            }
        }

        private void SettingsUI_Load(object sender, EventArgs e)
        {
            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client1 = new WebClient();
            client1.DownloadProgressChanged += Client1_DownloadProgressChanged;
            client1.DownloadFileCompleted += Client1_DownloadFileCompleted;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Downloading latest stable...";
            button6.Enabled = false;
            button7.Enabled = false;
            string lateststable = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(2).Take(1).First();
            string latestbeta = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(5).Take(6).First();
            string url = lateststable;
            Uri uri = new Uri(url);
            client.DownloadFileAsync(uri, "C:\\BlueSky\\stable.appx");
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            label5.Text = "Download completed! The file are saved in C:\\BlueSky\\stable.appx";
            button6.Enabled = true;
            button7.Enabled = true;
            EnableAllButton();
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double receive = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = receive / total * 100;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }

        private void Client1_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            label5.Text = "Download completed! The file are saved in C:\\BlueSky\\beta.appx";
            button6.Enabled = true;
            button7.Enabled = true;
            EnableAllButton();
        }

        private void Client1_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Minimum = 0;
                double receive = double.Parse(e.BytesReceived.ToString());
                double total = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = receive / total * 100;
                progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            }));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Downloading latest beta...";
            button6.Enabled = false;
            button7.Enabled = false;
            string lateststable = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(2).Take(1).First();
            string latestbeta = File.ReadLines(@"C:\BlueSky\blueskymcd.bluesky").Skip(5).Take(6).First();
            string url = latestbeta;
            Uri uri = new Uri(url);
            client1.DownloadFileAsync(uri, "C:\\BlueSky\\beta.appx");
        }

        void DisableAllButton()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button8.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
            button11.Enabled = false;
        }

        void EnableAllButton()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button8.Enabled = true;
            button4.Enabled = true;
            button3.Enabled = true;
            button11.Enabled = true;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Enabling Developer Mode...";
            string DevOn = AppDomain.CurrentDomain.BaseDirectory + "Assets/DEVON.reg";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + DevOn + "\"");
            regeditProcess.WaitForExit();
            progressBar1.Value = 100;
            label5.Text = "Success!";
            EnableAllButton();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Disabling Developer Mode...";
            string DevOff = AppDomain.CurrentDomain.BaseDirectory + "Assets/DEVOFF.reg";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + DevOff + "\"");
            regeditProcess.WaitForExit();
            progressBar1.Value = 100;
            label5.Text = "Success!";
            EnableAllButton();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MessageBoxResult result = (MessageBoxResult)System.Windows.Forms.MessageBox.Show("Are you sure you want to reset Microsoft Store? This will wipe all of your launcher changes and uninstall tweaks made by BlueSky. Also please take a note that this feature sometimes might not works as expected! (Microsoft Store will open automatically after completed.)", "WARNING", (MessageBoxButtons)MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                DisableAllButton();
                progressBar1.Value = 0;
                label5.Text = "Resetting Microsoft Store...";
                System.Diagnostics.Process rst = System.Diagnostics.Process.Start("wsreset.exe");
                rst.WaitForExit();
                progressBar1.Value = 50;
                label5.Text = "Uninstalling changes...";
                string disable = AppDomain.CurrentDomain.BaseDirectory + "Assets/DISABLE.reg";
                Process regeditProcess = Process.Start("regedit.exe", "/s \"" + disable + "\"");
                regeditProcess.WaitForExit();
                ServiceController serviceController = new ServiceController("ClipSVC");
                if (serviceController.Status == ServiceControllerStatus.Stopped)
                {
                    serviceController.Start();
                }
                progressBar1.Value = 100;
                label5.Text = "Microsoft Store has been resetted successfully!";
                EnableAllButton();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Operation cancelled!");
            }
        }
    }
}
