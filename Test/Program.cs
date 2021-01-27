using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Test
{
    class Program
    {

        public static void run_cmd(string cmd)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\huber\AppData\Local\Microsoft\WindowsApps\PythonSoftwareFoundation.Python.3.8_qbz5n2kfra8p0\python.exe";
            start.Arguments = cmd;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }

        static void Main(string[] args)
        {
            run_cmd("./Python/main.py");
            Console.ReadKey();
        }
    }
}