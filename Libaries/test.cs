using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlueSky.Libaries;

namespace BlueSky.Libaries
{
    internal class test
    {
        public static void start()
        {
            STARTUPINFO si = new STARTUPINFO();
            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
            bool success = NativeMethods.CreateProcess("minecraft://", null,
                IntPtr.Zero, IntPtr.Zero, false,
                ProcessCreationFlags.CREATE_SUSPENDED,
                IntPtr.Zero, null, ref si, out pi);
            Process[] s = Process.GetProcessesByName("Minecraft.Windows");
            int sid = s.Length;
            int id = s[sid - 1].Id;
            sid -= 1;
            injector.Inject((uint)id, AppDomain.CurrentDomain.BaseDirectory + "\\Assets\\LaunchingAsset\\1164.dll");
            Thread.Sleep(200);

            NativeMethods.ResumeThread(pi.hThread);
        }
    }
}
