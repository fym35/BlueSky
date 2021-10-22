using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlueSkyNew.API;

namespace BlueSkyNew
{
    public partial class APITests : Form
    {
        public APITests()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rAPI.notice("A hello from the API itself :)");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rAPI.installmc(progressBar1, label2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int test = rAPI.notice_ask("test", "Press ok for 1, press cancel for 2");
            if (test == 1)
            {
                rAPI.notice("you pressed ok"); 
            }
            else
            {
                rAPI.notice("you pressed cancel");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            rAPI.uninstallmc(progressBar1, label2);
        }
    }
}
