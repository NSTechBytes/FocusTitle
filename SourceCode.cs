using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Rainmeter;

namespace PluginFocusTitle
{
    internal class Measure
    {
        // Define the MeasureType enum for different title options
        enum MeasureType
        {
            WindowTitle,
            ApplicationTitle
        }

        private MeasureType Type = MeasureType.WindowTitle;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        internal void Reload(Rainmeter.API api, ref double maxValue)
        {
            string type = api.ReadString("Type", "");
            switch (type.ToLowerInvariant())
            {
                case "windowtitle":
                    Type = MeasureType.WindowTitle;
                    break;

                case "applicationtitle":
                    Type = MeasureType.ApplicationTitle;
                    break;

                default:
                    api.Log(API.LogType.Error, "FocusTitle.dll: Type=" + type + " not valid");
                    break;
            }
        }

        internal string GetString()
        {
            IntPtr hWnd = GetForegroundWindow();

            if (hWnd == IntPtr.Zero)
                return null;

            if (Type == MeasureType.WindowTitle)
            {
                int length = GetWindowTextLength(hWnd);
                StringBuilder windowText = new StringBuilder(length + 1);
                GetWindowText(hWnd, windowText, windowText.Capacity);
                return windowText.ToString();
            }
            else if (Type == MeasureType.ApplicationTitle)
            {
                uint processId;
                GetWindowThreadProcessId(hWnd, out processId);
                Process process = Process.GetProcessById((int)processId);
                return process.ProcessName;
            }

            return null;
        }

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
    }

    public static class Plugin
    {
        static IntPtr StringBuffer = IntPtr.Zero;

        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();

            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            measure.Reload(new Rainmeter.API(rm), ref maxValue);
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            // Update method is not needed for this plugin, returning 0.0.
            return 0.0;
        }

        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;

            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }

            string stringValue = measure.GetString();
            if (stringValue != null)
            {
                StringBuffer = Marshal.StringToHGlobalUni(stringValue);
            }

            return StringBuffer;
        }
    }
}
