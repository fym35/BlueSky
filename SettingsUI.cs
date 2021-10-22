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
        }

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
                    label5.Text = "Package failed to install! Error code PKG_INSTALL_FAILED";
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
                        label5.Text = "Package failed to uninstall! Error code PKG_UNINSTALL_FAILED";
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
                label5.Text = "Minecraft still running! Please close your Minecraft session first before disable the exploit! Error code MC_RUNNING";
                EnableAllButton();
            }
        }


        void DisableAllButton()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
            button11.Enabled = false;
        }

        void EnableAllButton()
        {
            button1.Enabled = true;
            button2.Enabled = true;
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
            DisableAllButton();
            progressBar1.Value = 0;
            label5.Text = "Disabling Developer Mode...";
            string DevOn = AppDomain.CurrentDomain.BaseDirectory + "Assets/DEVOFF.reg";
            Process regeditProcess = Process.Start("regedit.exe", "/s \"" + DevOn + "\"");
            regeditProcess.WaitForExit();
            progressBar1.Value = 100;
            label5.Text = "Success!";
            EnableAllButton();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            APITests apitest = new APITests();
            apitest.Show();
        }
    }
}
