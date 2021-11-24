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
using System.Net;
using System.ServiceProcess;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.CodeDom.Compiler;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BlueSky.Libaries;

namespace BlueSky
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BS.launch(11, 4, 45, progressBar1, label2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BS.installmc(progressBar2, label8);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BS.uninstallmc(progressBar2, label8);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BS.desvc();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BS.FetchUpdate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BS.GetMCID();
        }
    }
}
