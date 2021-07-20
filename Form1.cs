using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management.Automation;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;

namespace BlueSkyNew
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AboutUI about = new AboutUI();
            about.Show();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            progressBar1.Value = 0;
            label1.Text = "Preparing services...";
            progressBar1.Value = 35;
            await Prepare();
            label1.Text = "Preparing for protocol...";
            progressBar1.Value = 40;
            await GetRidService();
            label1.Text = "Launching protocol..";
            progressBar1.Value = 50;
            await LaunchProtocol();
            label1.Text = "Killing...";
            progressBar1.Value = 85;
            await KillRB();
            await Task.Delay(15000);
            progressBar1.Value = 100;
            label1.Text = "Success! Enjoy the game!";
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }

        public async Task Prepare()
        {
            await Task.Run(delegate ()
            {
                string enable = AppDomain.CurrentDomain.BaseDirectory + "Assets/ENABLE.reg";
                Process regeditProcess = Process.Start("regedit.exe", "/s \"" + enable + "\"");
                regeditProcess.WaitForExit();
            });
        }

        public async Task GetRidService()
        {
            await Task.Run(delegate ()
            {
                ServiceController serviceController = new ServiceController("ClipSVC");
                if (serviceController.Status != ServiceControllerStatus.Stopped)
                {
                    serviceController.Stop();
                }
            });
        }

        public async Task LaunchProtocol()
        {
            await Task.Run(delegate ()
            {
                Process.Start("minecraft://");
                for (int num = 20; num != 0; num--)
                {
                    Task.Delay(1000).Wait();
                }
            });
        }

        public async Task KillRB()
        {
            await Task.Run(delegate ()
            {
                Process[] processesByName = Process.GetProcessesByName("RuntimeBroker");
					for (int i = 0; i < processesByName.Length; i++)
					{
						processesByName[i].Kill();
					}
            });
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SettingsUI settings = new SettingsUI();
            settings.Show();
        }
    }
}
