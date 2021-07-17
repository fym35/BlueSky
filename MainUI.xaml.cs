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

namespace BlueSky
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        public MainUI()
        {
            InitializeComponent();
        }

        private void aboutbtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("BlueSky Launcher Beta 8 \nVersion 0.8b \nSpecial Release \n\nBy ClickNin, TinedPakGamer, santu, Frakod, kostya, LureCore and XenonyxBlaze.");
        }

        private void settingsbtn_Click(object sender, RoutedEventArgs e)
        {
            NewSettings settings = new NewSettings();
            settings.Show();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
