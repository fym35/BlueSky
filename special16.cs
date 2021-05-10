using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlueSky
{
   public partial class DevCheck : Window
    {
        Process h=new Process();
        IntPtr address = IntPtr.Zero;
        string customCode = "b000";

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
        class ProcessMemoryReader
        {

            public ProcessMemoryReader()
            {
            }

            /// <summary>	
            /// Process from which to read		
            /// </summary>
            public Process ReadProcess
            {
                get
                {
                    return m_ReadProcess;
                }
                set
                {
                    m_ReadProcess = value;
                }
            }
            public int bise=0;
         public   long baseAddress = 0;

            private Process m_ReadProcess = null;

            private IntPtr m_hProcess = IntPtr.Zero;
            public byte[] jump(long origin, long destination)
            {
               
                if (destination > origin)
                {
                    var j = (int)(destination - origin);
                    var i = BitConverter.GetBytes(j).ToList();
                  
                    return i.ToArray();
                }
                var iA = BitConverter.GetBytes((int)(origin-destination)).ToList();
                bool added = false;
                for(int v = 0; v < iA.Count; ++v)
                {
                    iA[v] = (byte)(255 - iA[v]);
                    if (!added)
                    {
                        if (iA[v] != 255)
                        {
                            iA[v] += 1;
                            added = true;
                        }
                        else
                            iA[v] = 0;
                    }
                }
                return iA.ToArray();
                
                

            }
            public void OpenProcess()
            {
                //			m_hProcess = ProcessMemoryReaderApi.OpenProcess(ProcessMemoryReaderApi.PROCESS_VM_READ, 1, (uint)m_ReadProcess.Id);
                ProcessMemoryReaderApi.ProcessAccessType access;
                access = ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_READ
                    | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_WRITE
                    | ProcessMemoryReaderApi.ProcessAccessType.PROCESS_VM_OPERATION;
                m_hProcess = ProcessMemoryReaderApi.OpenProcess((uint)access, 1, (uint)m_ReadProcess.Id);
                //baseAddress = m_ReadProcess.MainModule.BaseAddress.ToInt64();
            }

            public void CloseHandle()
            {
                try
                {
                    int iRetValue;
                    iRetValue = ProcessMemoryReaderApi.CloseHandle(m_hProcess);
                    if (iRetValue == 0)
                    {
                        throw new Exception("CloseHandle failed");
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message, "error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            public IntPtr CreateMemory(int size)
            {
                IntPtr f = (IntPtr)(baseAddress+ 0xDE3A38);
            var s=   ProcessMemoryReaderApi.VirtualAllocEx(m_hProcess, out f, (uint)size, (uint)(ProcessMemoryReaderApi.flAllocationType.MEM_COMMIT|ProcessMemoryReaderApi.flAllocationType.MEM_RESERVE), (uint)ProcessMemoryReaderApi.flProtect.PAGE_EXECUTE_READWRITE);
                return f;
            
            }
            public void WriteProcessSpecial(long offset, byte[] bytesToWrite, out int bytesWritten)
            {
                IntPtr MemoryAddress = (IntPtr)(baseAddress + offset);
                IntPtr ptrBytesWritten;
                ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, MemoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out ptrBytesWritten);

                bytesWritten = ptrBytesWritten.ToInt32();
            }

            public byte[] ReadProcessMemory(IntPtr MemoryAddress, uint bytesToRead, out int bytesRead)
            {
                byte[] buffer = new byte[bytesToRead];

                IntPtr ptrBytesRead;
                ProcessMemoryReaderApi.ReadProcessMemory(m_hProcess, MemoryAddress, buffer, bytesToRead, out ptrBytesRead);

                bytesRead = ptrBytesRead.ToInt32();

                return buffer;
            }

            public void WriteProcessMemory(IntPtr MemoryAddress, byte[] bytesToWrite, out int bytesWritten)
            {
                IntPtr ptrBytesWritten;
                ProcessMemoryReaderApi.WriteProcessMemory(m_hProcess, MemoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out ptrBytesWritten);

                bytesWritten = ptrBytesWritten.ToInt32();
            }


            /// <summary>
            /// ProcessMemoryReader is a class that enables direct reading a process memory
            /// </summary>
            class ProcessMemoryReaderApi
            {
                // constants information can be found in <winnt.h>
                [Flags]
                public enum ProcessAccessType
                {
                    PROCESS_TERMINATE = (0x0001),
                    PROCESS_CREATE_THREAD = (0x0002),
                    PROCESS_SET_SESSIONID = (0x0004),
                    PROCESS_VM_OPERATION = (0x0008),
                    PROCESS_VM_READ = (0x0010),
                    PROCESS_VM_WRITE = (0x0020),
                    PROCESS_DUP_HANDLE = (0x0040),
                    PROCESS_CREATE_PROCESS = (0x0080),
                    PROCESS_SET_QUOTA = (0x0100),
                    PROCESS_SET_INFORMATION = (0x0200),
                    PROCESS_QUERY_INFORMATION = (0x0400)
                }
                public enum flAllocationType
                {
                    MEM_COMMIT=0x00001000,
                    MEM_RESET_UNDO=0x1000000,
                    MEM_RESERVE=0x00002000,
                    MEM_RESET=0x00080000,
                        MEM_TOP_DOWN=0x00100000
                }
                public enum flProtect
                {
                    PAGE_EXECUTE=0x10,
                        PAGE_EXECUTE_READ=0x20,
                        PAGE_EXECUTE_READWRITE=0x40,
                        PAGE_EXECUTE_WRITECOPY=0x80,
                        PAGE_NOACCESS=0x01,
                        PAGE_READONLY=0x02,
                        PAGE_READWRITE=0x04,
                        PAGE_WRITECOPY=0x08,
                        PAGE_TARGETS_INVALID=0x40000000,
                        PAGE_TARGETS_NO_UPDATE=0x40000000
                }
                // function declarations are found in the MSDN and in <winbase.h> 

                //		HANDLE OpenProcess(
                //			DWORD dwDesiredAccess,  // access flag
                //			BOOL bInheritHandle,    // handle inheritance option
                //			DWORD dwProcessId       // process identifier

                //			);
                [DllImport("Kernel32.dll")]
                public static extern IntPtr VirtualAllocEx(IntPtr hProcess, out IntPtr lpAddress, UInt32 dwSize, UInt32 flAllocationType,UInt32 flProtect);
                [DllImport("kernel32.dll")]
                public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);

                //		BOOL CloseHandle(
                //			HANDLE hObject   // handle to object
                //			);
                [DllImport("kernel32.dll")]
                public static extern Int32 CloseHandle(IntPtr hObject);

                //		BOOL ReadProcessMemory(
                //			HANDLE hProcess,              // handle to the process
                //			LPCVOID lpBaseAddress,        // base of memory area
                //			LPVOID lpBuffer,              // data buffer
                //			SIZE_T nSize,                 // number of bytes to read
                //			SIZE_T * lpNumberOfBytesRead  // number of bytes read
                //			);
                [DllImport("kernel32.dll")]
                public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);

                //		BOOL WriteProcessMemory(
                //			HANDLE hProcess,                // handle to process
                //			LPVOID lpBaseAddress,           // base of memory area
                //			LPCVOID lpBuffer,               // data buffer
                //			SIZE_T nSize,                   // count of bytes to write
                //			SIZE_T * lpNumberOfBytesWritten // count of bytes written
                //			);
                [DllImport("kernel32.dll")]
                public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);


            }
        }

        void start()
        {
            

//            var whitelist = Process.GetProcessesByName("RuntimeBroker");

            //            File.AppendAllText(pt, "Fetched Brokers\n");
            //          Enable();

            //        File.AppendAllText(pt, "Hack Enabled\n");
            try
            {
                Process.Start("minecraft://");
                File.AppendAllText("", "Process started\n");

            }
            catch (Exception e)
            {
                File.AppendAllText("", "Error came in launching game\n" + e.Message + "\t" + e.StackTrace);
            }

             h = new Process();
            try
            {
                h = Process.GetProcessesByName("Minecraft.Windows")[0];
            }

            
            catch (Exception e)
            {
                File.AppendAllText("", "Error came in retrieving game\n" + e.Message + "\t" + e.StackTrace);
                throw e;
            }
            ProcessMemoryReader reader = new ProcessMemoryReader();
            reader.ReadProcess = h;
            reader.OpenProcess();
            int i = 0;
            reader.WriteProcessSpecial(0xDbf767, new byte[] { 0xeb }, out i);
            PerformanceCounter PC = new PerformanceCounter();
           PC.CategoryName = "Process";
            PC.CounterName = "Working Set - Private";

            PC.InstanceName = "Minecraft.Windows";
           int memsize = 0;
            if (!h.HasExited)
              memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024);

           while (memsize < 40000)
           {

               Task.Delay(100).Wait();
                if (h.HasExited)
                    return;

                memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024);

          }
            PC.Close();
            PC.Dispose();
            memsize = 0;
            reader.WriteProcessSpecial(0xDbf767, new byte[] { 0x78 }, out i);
            var custom = StringToByteArray(customCode);
            reader.WriteProcessSpecial(0xDE3A3A,custom, out i);
            reader.CloseHandle();
            if(i==0)
                File.AppendAllText("", "Error in writing jump\n");

            while (!h.HasExited)
            {
                Task.Delay(100).Wait();
            }
            Application.Current.Dispatcher.Invoke((Action)(() => {

                this.Show();
            
            }));
        }


    }
}
