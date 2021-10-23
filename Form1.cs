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
using BlueSkyNew.API;

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

        private void button1_Click(object sender, EventArgs e)
        {
            rAPI.launch(10, 2, 45, progressBar1, label1);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            SettingsUI settings = new SettingsUI();
            settings.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
