using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace BlueSkyRewrite
{
    /// <summary>
    /// Interaction logic for Credits.xaml
    /// </summary>
    public partial class Credits 
    {
        public Credits()
        {
            InitializeComponent();
        }

        private void JoinDiscord_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://discord.gg/cSvj4YFUSn");
        }
    }
}
