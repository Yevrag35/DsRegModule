using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.PowerShell.DsReg
{
    public class DsRegExecutor : IDsRegExecutor
    {
        private const string DSREG_EXE = "dsregcmd.exe";
        private readonly IDsRegParser _parser;

        IDsRegParser IDsRegExecutor.Parser => _parser;
        public string DsRegExe { get; }

        public DsRegExecutor()
        {
            DsRegExe = this.GetExePath();
            _parser = new DsRegParser();
        }

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

        private string[] GetStatus()
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
                var errLine = proc.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(errLine))
                {
                    throw new Exception(errLine);
                }

                return output.ToArray();
            }
        }

        private BaseDetail FilterInto(IEnumerable<BaseDetail> objs, Type type)
        {
            BaseDetail retVal = null;
            foreach (BaseDetail o in objs)
            {
                if (o.GetType().Equals(type))
                {
                    retVal = o;
                }
            }
            return retVal;
        }

        IEnumerable<BaseDetail> IDsRegExecutor.GetAllStatus()
        {
            string[] allLines = this.GetStatus();
            IEnumerable<BaseDetail> retObjs = _parser.ParseFrom(allLines);
            return retObjs;
        }

        BaseDetail IDsRegExecutor.GetSpecific(Type type)
        {
            IEnumerable<BaseDetail> allObjs = ((IDsRegExecutor)this).GetAllStatus();
            return this.FilterInto(allObjs, type);
        }
    }
}
