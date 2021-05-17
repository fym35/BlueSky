using System;
using System.Collections.Generic;
using System.IO;
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
using System.IO;

namespace BlueSky
{
    /// <summary>
    /// Interaction logic for Timer.xaml
    /// </summary>
    public partial class Timer : Window
    {
        public Timer()
        {
            InitializeComponent();
            if (File.Exists("C:\\BlueSky\\timer.bluesky"))
            {
                string startuptm = File.ReadLines("C:\\BlueSky\\timer.bluesky").Take(1).First();
                string delaytm = File.ReadLines("C:\\BlueSky\\timer.bluesky").Skip(1).Take(1).First();
                startup.Content = startuptm;
                delayed.Content = delaytm;
                st1.Value = (double)startup.Content;
                st2.Value = (double)delayed.Content;
            }
            else
            {
                Directory.CreateDirectory("C:\\BlueSky\\");
                File.Create("C:\\BlueSky\\timer.bluesky");
            }
        }

        private void St1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startup.Content = st1.Value;
            delayed.Content = st2.Value;
        }

        private void St2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startup.Content = st1.Value;
            delayed.Content = st2.Value;
        }
    }
}
