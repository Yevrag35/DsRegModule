using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    internal class DsRegParser
    {
        private const string REGEX = @"^\s{1,}(.{1,})\s\:(.{1,}|$)$";
        private const string CLASS_REGEX = @"^\|\s{1,}(\S{1,}\s\S{1,}(?:\s\S{1,}|\s))";

        internal DsRegParser() { }

        internal IEnumerable<BaseDetail> ParseFrom(IEnumerable<string> allLines)
        {
            IEnumerable<string> classes = this.ParseIntoClasses(allLines);

            var list = new List<BaseDetail>();
            string allOne = string.Join(Environment.NewLine, allLines);
            foreach (string s in classes.Where(x => !x.Contains("WorkAccount")))
            {
                var t = Type.GetType(string.Format("MG.DsReg.{0}", s));
                if (t != null)
                {
                    var newObj = Activator.CreateInstance(t) as BaseDetail;
                    list.Add(this.MatchTo(newObj, allOne));
                }
            }
            int count = classes.Count(x => x.Contains("WorkAccount"));
            if (count > 0)
                list.AddRange(this.MatchToWorkAccounts(allOne, count));

            return list;
        }

        internal IEnumerable<string>ParseIntoClasses(IEnumerable<string> lines)
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

        private MatchDetailCollection MatchToKeyValuePairs(string allLines, Type matchTo)
        {
            var allProps = matchTo.GetProperties(
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).Where(
                    x => x.CanWrite);

            IEnumerable<string> allLowerProps = allProps.Select(x => x.Name.ToLower());

            var dict = new MatchDetailCollection();
            MatchCollection matches = Regex.Matches(allLines, REGEX, RegexOptions.Multiline);

            for (int c = 0; c < matches.Count; c++)
            {
                Match m = matches[c];
                string k = m.Groups[1].Value.Trim();
                string v = m.Groups[2].Value.Trim();

                if (allLowerProps.Contains(k.ToLower()))
                {
                    string propName = allLowerProps.Single(x => x == k.ToLower());
                    PropertyInfo pi = allProps.Single(x => x.Name.Equals(propName, StringComparison.CurrentCultureIgnoreCase));
                    dict.Add(k, v, pi);
                }
            }
            return dict;
        }

        private BaseDetail MatchTo(BaseDetail newObj, string allLines)
        {
            MatchDetailCollection dict = this.MatchToKeyValuePairs(allLines, newObj.GetType());

            for (int i = 0; i < dict.Count; i++)
            {
                MatchDetail kvp = dict[i];
                kvp.SetValue(newObj);
            }
            return newObj;
        }

        private WorkAccountCollection MatchToWorkAccounts(string allOne, int howMany)
        {
            var list = new WorkAccountCollection(howMany);
            MatchDetailCollection matchDets = this.MatchToKeyValuePairs(allOne, typeof(WorkAccount));
            var listOfMatches = new List<MatchDetailCollection>(howMany);
            
            for (int i = 0; i < howMany; i++)
            {
                listOfMatches.Add(new MatchDetailCollection(matchDets.GetRange(i * 8, 8)));
            }
            for (int l = 0; l < listOfMatches.Count; l++)
            {
                MatchDetailCollection li = listOfMatches[l];
                var wa = new WorkAccount();
                for (int m = 0; m < li.Count; m++)
                {
                    MatchDetail md = li[m];
                    md.SetValue(wa);
                }
                list.Add(wa);
            }
            return list;
        }
    }
}
