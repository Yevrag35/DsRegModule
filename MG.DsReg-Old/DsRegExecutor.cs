using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    public class DsRegExecutor
    {
        private const string DSREG_EXE = "dsregcmd.exe";

        public string DsRegExe { get; }

        public DsRegExecutor() => DsRegExe = this.GetExePath();

        private string GetExePath()
        {
            string system32 = Environment.GetFolderPath(Environment.SpecialFolder.System);
            return Path.Combine(system32, DSREG_EXE);
        }

        private ProcessStartInfo NewProcessInfo(DsRegArgument argument)
        {
            return new ProcessStartInfo
            {
                Arguments = string.Format("/{0}", argument.ToString().ToLower()),
                CreateNoWindow = true,
                FileName = this.DsRegExe,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
        }

        public DsRegResult GetStatus()
        {
            var output = new List<string>();
            ProcessStartInfo psi = this.NewProcessInfo(DsRegArgument.Status);
            using (var proc = new Process
            {
                StartInfo = psi
            })
            {
                proc.Start();
                while (!proc.StandardOutput.EndOfStream)
                {
                    output.Add(proc.StandardOutput.ReadLine());
                }
                string errLine = proc.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(errLine))
                {
                    throw new Exception(errLine);
                }

                return new DsRegResult(output);
            }
        }

        //public DsRegResult GetStatus()
        //{
        //    DsRegResult allLines = this.GetStatus();
        //    return allLines;
        //}

        //public static DsRegExecutor NewExecutor() => new DsRegExecutor();
    }
}
