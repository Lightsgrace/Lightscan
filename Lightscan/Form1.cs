using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightscan
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.Initialize(richTextBox1, this);
        }

        private bool IsLinked(string folder)
        {
            if (folder.Contains("Documents and Settings") || folder.Contains("Application Data")) return false;
            return true;
        }

        private IEnumerable<string> GetFiles(string folder, string filter, bool recursive)
        {
            string[] found = null;
            
            if (IsLinked(folder))
                try
                {
                    found = Directory.GetFiles(folder, filter);
                }
                catch 
                {
                    
                }

            if (found != null)
                foreach (var x in found)
                    if (File.Exists(x))
                        yield return x;

            if (!recursive) yield break;
            {
                found = null;

                if (IsLinked(folder))
                    try
                    {
                        found = Directory.GetDirectories(folder);
                    }
                    catch
                    {
                        
                    }

                if (found == null) yield break;

                foreach (var x in found)
                foreach (var y in GetFiles(x, filter, true))
                    if (File.Exists(y))
                        yield return y;
            }
        }

        private string GetMD5(FileInfo fi)
        {
            using (var md5 = MD5.Create())
            {                
                using (var stream = fi.OpenRead())
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
                }                
            }
        }

        private async void btnScanNow_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();

            await Task.Run(() =>
            {
                Parallel.ForEach(GetFiles(txtPath.Text, "*.exe", true), file =>
                {
                    try
                    {
                        if (file.ToLower().EndsWith("exe"))
                        {
                            var fileInfo = new FileInfo(file);
                            Log.Write($"Hash: {GetMD5(fileInfo)} - File: {fileInfo.Name}");
                        }
                    }
                    catch { }
                });
            });

            //var t = new Thread(delegate ()
            //{
            //    Parallel.ForEach(GetFiles(txtPath.Text, "*.exe", true), file => {
            //        var fileInfo = new FileInfo(file);
            //        Log.Write($"Hash: {GetMD5(fileInfo)} - File: {fileInfo.Name}");
            //    });
            //});
            //t.IsBackground = true;
            //t.Start();

            Log.Write($"Processed after {sw.Elapsed} seconds", Color.Green);
            sw.Stop();
        }

        private void btnProcessScan_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcesses())
            {
                try
                {
                    var fullFileName = process.MainModule.FileName;
                    Log.Write($"File Name: {Path.GetFileName(fullFileName)}", Color.Green);
                }
                catch (Exception ex)
                {
                    Log.Write($"Process Name: {process.ProcessName} - {ex.Message}", Color.Red);
                }
            }
        }
    }
}
