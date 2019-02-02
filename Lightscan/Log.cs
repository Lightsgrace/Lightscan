using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lightscan
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class Log
    {
        private const int WM_VSCROLL = 277;
        private const int SB_PAGEBOTTOM = 7;
        private static Form _parent;
        private static RichTextBox _rtbLogWindow;
        private static string _dateFormat;
        private static bool Initialized;
        public static void Initialize(RichTextBox rtbLogWindow = null, Form parent = null, string dateFormat = "HH:mm:ss", bool IsWeb = false)
        {
            _parent = parent;
            _rtbLogWindow = rtbLogWindow;
            _dateFormat = dateFormat;

            Initialized = true;
        }

        public static void Clear() => _parent.Invoke(new Action(() => { _rtbLogWindow.Clear(); }));

        public static void Write(string message, [CallerMemberName] string callingMethod = "", string agentName = "")
        {
            Write(message, Color.Black, callingMethod, agentName);
        }

        private static void WriteToTextLog(string Time, string message, [CallerMemberName] string callingMethod = "")
        {
            var path = Environment.CurrentDirectory;
            
            var di = new DirectoryInfo(path + "\\Logs\\" + DateTime.Now.ToString("yyyy-MMMM"));
            if (!di.Exists)
                di.Create();

            var LogFile = new FileInfo(path + "\\Logs\\" + DateTime.Now.ToString("yyyy-MMMM") + "\\" + DateTime.Now.ToString("yyyy.MM.dd") + ".txt");
            var sw = LogFile.Exists ? LogFile.AppendText() : LogFile.CreateText();

            sw.WriteLine(Time + " " + "[" + callingMethod + "] " + message);
            sw.Flush();
            sw.Close();
        }

        public static void Write(string message, Color c, [CallerMemberName] string callingMethod = "", string agentName = "", bool emailed = false)
        {
            var Time = "[" + DateTime.Now.ToString(_dateFormat) + "] ";
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(Time);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{message}");

            if (!Initialized)
                return;

            try
            {
                InternalWrite(c, message);
                
                try
                {
                    WriteToTextLog(Time, message, callingMethod);
                }
                catch
                {
                    // We dont mind if this fails - we have logged to DB.
                }
            }
            catch (Exception ex)
            {
                WriteToTextLog(Time, ex.Message, callingMethod);                
            }
        }
        
        private static void InternalWrite(Color color, string message)
        {
            try
            {   
                if (_parent == null || _rtbLogWindow == null) return;

                _parent.Invoke
                (
                    new Action(() =>
                    {
                        var rtb = _rtbLogWindow;

                        rtb.SuspendLayout();

                        // We remove the top 1000 lines from the text box when we reach 2000 lines
                        // We are only doing this update @ 2000 lines to prevent flickering
                        // Flickering is not 100% removed but it is reduced to an acceptable level.

                        if (rtb?.Lines.Length > 2000)
                        {
                            rtb.Select(0, rtb.GetFirstCharIndexFromLine(rtb.Lines.Length - 1000));
                            rtb.SelectedText = "";
                        }

                        rtb.SelectionStart = rtb.Text.Length;
                        rtb.SelectionLength = 0;


                        rtb.SelectionColor = Color.Gray;
                        rtb.AppendText("[" + DateTime.Now.ToString(_dateFormat) + "] ");

                        rtb.SelectionColor = color;
                        rtb.AppendText($"{message}\r");

                        rtb.ClearUndo();

                        rtb.ResumeLayout(false);

                        ScrollToBottom(rtb);
                    })
                );
            }
            catch
            {
                // ignored
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public static void ScrollToBottom(RichTextBox MyRichTextBox)
        {
            SendMessage(MyRichTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
        }
    }
}