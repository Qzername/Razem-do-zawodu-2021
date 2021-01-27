using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Lekcjobot.Code.Models;

namespace Lekcjobot.Code.VulcanAPIIO
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

        public static Lesson[] createLessons(string input)
        {
            List<Lesson> list = new List<Lesson>();
            List<string> table = new List<string>(input.Split('|'));
            table.RemoveAt(table.Count - 1);
            foreach (string line in table)
            {
                string[] elements = line.Split('!');
                list.Add(new Lesson(elements[0], elements[1], Convert.ToDateTime(elements[2]), Convert.ToDateTime(elements[3])));
            }

            return list.ToArray();
        }
    }
}
