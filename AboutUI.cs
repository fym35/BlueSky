using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace BlueSkyNew
{
    public partial class AboutUI : Form
    {
        public AboutUI()
        {
            InitializeComponent();
        }

        private void AboutUI_Load(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://youtube.com/c/ClickNin");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/YDPdw5eM2U");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ClickNinYT/BlueSky");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Process.Start("https://youtube.com/c/ClickNin");
        }
    }
}
