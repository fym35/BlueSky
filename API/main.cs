using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Windows;
using System.Windows.Forms;

namespace BlueSkyNew.API
{
    /*
    This file contain the whole BlueSky API. To use, simplity reference to this class and you are all set!
    */
    public class rAPI {
        static System.Windows.Forms.ProgressBar pbarm;
        static System.Windows.Forms.Label labelm;
        public static void notice(string msg)
        {
            System.Windows.Forms.MessageBox.Show(msg);
        }
                                  
        public static async void installmc(System.Windows.Forms.ProgressBar pbar, System.Windows.Forms.Label label)
        {
            pbarm = pbar;
            labelm = label;
            System.Windows.Forms.OpenFileDialog mcpck = new System.Windows.Forms.OpenFileDialog();
            mcpck.DefaultExt = ".appx";
            mcpck.Filter = "Appx File (*.appx)|*.appx|MSIX File (*.msix)|*.msix";
            mcpck.ShowDialog();
            string PCKPath = mcpck.FileName;
            if (PCKPath.Length != 0)
            {
                pbar.Value = 0;
                label.Text = "Installing package...";
                PowerShell ps = PowerShell.Create();
                ps.AddCommand("Add-AppxPackage");
                ps.AddParameter("Path", PCKPath);
                ps.Streams.Error.DataAdded += Error_DataAdded;
                ps.Streams.Progress.DataAdded += Progress_DataAdded;
                await Task.Run(delegate ()
                {
                    ps.Invoke();
                });
                if (!ps.HadErrors)
                {
                    label.Text = "Package installed successfully!";
                }
                else
                {
                    label.Text = "Package failed to install! Error code 0002";
                }
            }
            else
            {
                notice("Operation cancelled!");
            }
        }

        private static void Error_DataAdded(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ErrorRecord> psdataCollection = (PSDataCollection<ErrorRecord>)sender;
            string error = psdataCollection[e.Index].FullyQualifiedErrorId;
        }

        private static void Progress_DataAdded(object sender, DataAddedEventArgs e)
        {
            PSDataCollection<ProgressRecord> psdataCollection = (PSDataCollection<ProgressRecord>)sender;
            int prog = psdataCollection[e.Index].PercentComplete;
            if (prog > 0)
            {
                G_progress = prog;
            }
        }

        static int G_progress
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
        static void set_progress(int arg)
        {
            if (pbarm.InvokeRequired)
            {
                var d = new progresser(set_progress);
                pbarm.Invoke(d, new object[] { arg });
            }
            else
            {
                pbarm.Value = arg;
            }
        }

        public static async void installmcspec(string appx, System.Windows.Forms.ProgressBar pbar, System.Windows.Forms.Label label)
        {
            pbarm = pbar;
            labelm = label;
            string PCKPath = appx;
            if (PCKPath.Length != 0)
            {
                pbar.Value = 0;
                label.Text = "Installing package...";
                PowerShell ps = PowerShell.Create();
                ps.AddCommand("Add-AppxPackage");
                ps.AddParameter("Path", PCKPath);
                ps.Streams.Error.DataAdded += Error_DataAdded;
                ps.Streams.Progress.DataAdded += Progress_DataAdded;
                await Task.Run(delegate ()
                {
                    ps.Invoke();
                });
                if (!ps.HadErrors)
                {
                    label.Text = "Package installed successfully!";
                }
                else
                {
                    label.Text = "Package failed to install! Error code 0002";
                }
            }
            else
            {
                notice("Operation cancelled!");
            }
        }

        public static int notice_ask(string title, string msg)
        {
            MessageBoxResult result = (MessageBoxResult)System.Windows.Forms.MessageBox.Show(msg, title, (MessageBoxButtons)MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
