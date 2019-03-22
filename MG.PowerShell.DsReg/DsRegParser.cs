using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MG.PowerShell.DsReg
{
    public class DsRegParser : IDsRegParser
    {
        private const string REGEX = @"^\s{1,}(.{1,})\s\:\s{1,}(.{1,})$";
        private const string CLASS_REGEX = @"^\|\s{1,}(\S{1,}\s\S{1,})";

        public DsRegParser() { }

        IEnumerable<BaseDetail> IDsRegParser.ParseFrom(string[] allLines)
        {
            var classes = this.ParseIntoClasses(allLines);

            var list = new List<BaseDetail>();
            string allOne = string.Join(Environment.NewLine, allLines);
            foreach (string s in classes.Where(x => !x.Contains("WorkAccount")))
            {
                var t = Type.GetType(string.Format("MG.PowerShell.DsReg.{0}", s));
                if (t != null)
                {
                    var newObj = Activator.CreateInstance(t) as BaseDetail;
                    list.Add(this.MatchTo(newObj, allOne));
                }
            }
            //foreach (string ws in classes.Where(x => x.Contains("WorkAccount")))
            //{
            //    var newWs = new WorkAccount();
            //    BaseDetail bd = 
            //}

            return list;
        }

        private List<string> ParseIntoClasses(string[] lines)
        {
            string allOne = string.Join(Environment.NewLine, lines);
            var classes = new List<string>();
            MatchCollection matchCol = Regex.Matches(allOne, CLASS_REGEX, RegexOptions.Multiline);
            foreach (Match m in matchCol)
            {
                classes.Add((m.Groups[1].Value as string).Replace(" ", string.Empty));
            }
            return classes;
        }

        private BaseDetail MatchTo(BaseDetail newObj, string allLines)
        {
            var allProps = newObj.GetType().GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).Where(
                    x => x.CanWrite);

            var dict = new List<KeyValuePair<string, string>>();
            MatchCollection matches = Regex.Matches(allLines, REGEX, RegexOptions.Multiline);
            foreach (Match m in matches)
            {
                dict.Add(new KeyValuePair<string, string>(m.Groups[1].Value as string, m.Groups[2].Value as string));
            }
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                string key = kvp.Key;
                string val = kvp.Value;
                PropertyInfo pi = null;
                try
                {
                    pi = allProps.Single(x => x.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase));
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
                if (pi.PropertyType.Equals(typeof(bool?)))
                {
                    if (val.Contains("YES"))
                        pi.SetValue(newObj, true);

                    else if (val.Contains("NO"))
                        pi.SetValue(newObj, false);
                }
                if (pi.PropertyType.Equals(typeof(int?)) && int.TryParse(val, out int number))
                {
                    pi.SetValue(newObj, number);
                }
                else if (pi.PropertyType.Equals(typeof(Guid?)) && Guid.TryParse(val, out Guid guid))
                {
                    pi.SetValue(newObj, guid);
                }
                else if (pi.PropertyType.Equals(typeof(DateTime?)) && DateTime.TryParse(val, out DateTime dt))
                {
                    pi.SetValue(newObj, dt);
                }
                else if (pi.PropertyType.Equals(typeof(Version)) && Version.TryParse(val, out Version vers))
                {
                    pi.SetValue(newObj, vers);
                }
                else if (pi.PropertyType.Equals(typeof(string)) && !string.IsNullOrEmpty(val))
                {
                    pi.SetValue(newObj, val);
                }
            }
            return newObj;
        }
    }
}
