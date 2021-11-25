using System;
using System.Windows.Forms;
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
            //BS.launch(int winver, int method, int timeout, System.Windows.Forms.ProgressBar pbar, System.Windows.Forms.Label label);
            BS.launch(11, 5, 45, progressBar1, label2);
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
