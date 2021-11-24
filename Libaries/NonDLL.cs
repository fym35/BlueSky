using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using BlueSky.Libaries;
using Windows.UI.Xaml;

namespace BlueSky.Libaries
{

    //This code is from M Centers 3.3 and has been optimized for BlueSky. Credit to TinedPakGamer

    public class NonDLL
    {
        object Previous { get; set; }
        public static void inti()
        {
            TrialManage.initialize();
        }

        public static void TrialHack()
        {
            try
            {
                Process[] s = Process.GetProcessesByName("Minecraft.Windows");
                int sid = s.Length;
                //while (sid > 0)
                //{
                //    int ids = s[sid - 1].Id;
                //    sid -= 1;
                //}
                int id = s[sid - 1].Id;
                sid -= 1;
                var z = Process.GetProcessById(id);
                TrialManage.e += TrialManage_e;
                TrialManage.trialcodecompleted += TrialManage_trialcodecompleted;

                TrialManage.RemoveTrial(z);
            }
            catch (ArgumentException ze)
            {
                if (ze.Message.Contains("is not running."))
                {
                    BS.notice("Minecraft is not running! Error Code: MC_NOT_RUNNING");
                }
            }
        }

        public static void TrialManage_trialcodecompleted(object sender, RoutedEventArgs e)
        {
            TrialManage.e -= TrialManage_e;
            TrialManage.trialcodecompleted -= TrialManage_trialcodecompleted;
        }
        static string time()
        {
            bool pm = false;
            var tim = DateTime.Now;
            var hour = tim.Hour;
            if (hour > 12)
            {
                hour -= 12;
                pm = true;
            }
            var min = tim.Minute;
            var sec = tim.Second;
            if (pm)
                return hour.ToString() + ":" + min.ToString() + ":" + sec.ToString() + " pm";
            return hour.ToString() + ":" + min.ToString() + ":" + sec.ToString() + " am";
        }
        public static void IAPHack()
        {
            var g = "Minecraft.Windows".ToArray();


            string s = "";
            int i = 0;
            while (i < g.Length)
            {
                if (g[i] == ':')
                    break;
                s += g[i];
                i++;

            }
            s = s.Replace(" ", "");
            try
            {
                var z = Process.GetProcessById(int.Parse(s));
                TrialManage.pe += TrialManage_ep;
                TrialManage.purchasecodecompleted += TrialManage_purchasecodecompleted;

                TrialManage.RemovePurchase(z);
            }
            catch (ArgumentException ze)
            {
                if (ze.Message.Contains("is not running."))
                {
                    BS.notice("Minecraft is not running! Error Code: MC_NOT_RUNNING");
                }
            }
        }

        public static void TrialManage_purchasecodecompleted(object sender, RoutedEventArgs e)
        {
            TrialManage.pe -= TrialManage_ep;
            TrialManage.purchasecodecompleted -= TrialManage_purchasecodecompleted;

        }
        
        List<int> processedids;
        BackgroundWorker autoTrial;
        bool autotrialCancelmode = false;
        bool cancelled = false;
        string processname = "";
        Run report;

        public static void TrialManage_e(object sender, RoutedEventArgs e)
        {
            var g = sender as string[];
            if (g[0] == "wroteold")
            {
                BS.notice("Removed old trial!");
                return;
            }
            BS.notice("Removed new trial!");
            return;
        }
        public static void TrialManage_ep(object sender, RoutedEventArgs e)
        {
            var g = sender as string[];
            if (g[0] == "wroteold")
            {
                BS.notice("Removed old purchase!");
                return;
            }
            BS.notice("Removed new trial!");
            return;
        }
        List<Process> GetProcesses(string name)
        {
            List<Process> output = new List<Process>();
            var arg = Process.GetProcessesByName(name);
            foreach (var item in arg)
            {
                if (!processedids.Contains(item.Id))
                    output.Add(item);
            }
            return output;
        }

    }
}