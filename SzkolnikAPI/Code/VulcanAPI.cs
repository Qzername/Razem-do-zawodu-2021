using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Lekcjobot.Code
{
    public static class VulcanAPI
    {
        public static string runScript(string script, string arguments = "")
        {
            ProcessStartInfo start = new ProcessStartInfo();
            //start.StandardInputEncoding = Encoding.UTF8;
            start.StandardOutputEncoding = Encoding.UTF8;
            start.FileName = @"C:\Users\huber\AppData\Local\Microsoft\WindowsApps\PythonSoftwareFoundation.Python.3.8_qbz5n2kfra8p0\python.exe";
            start.Arguments = string.Format("{0} {1}", script, arguments);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
