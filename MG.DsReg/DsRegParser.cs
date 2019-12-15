using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    public static class DsRegParser
    {
        private const string REGEX = @"^\s{1,}(.{1,})\s\:(.{1,}|$)$";
        private const string CLASS_REGEX = @"^\|\s{1,}(\S{1,}\s\S{1,}(?:\s\S{1,}|\s))";
        private static IgnoreCase IC = new IgnoreCase();

        
        private static BaseDetail MatchTo(BaseDetail newObj, string allLine)
        {
            foreach (MatchDetail md in MatchToKeyValuePairs(allLine, newObj.GetType()))
            {
                md.SetValue(newObj);
            }
            return newObj;
        }
        private static IEnumerable<MatchDetail> MatchToKeyValuePairs(string allLine, Type matchTo)
        {
            List<JsonMapping> mappings = JsonMapping.MappingFromType(matchTo);
            MatchCollection matches = Regex.Matches(allLine, REGEX, RegexOptions.Multiline);

            foreach (Match regexMatch in matches)
            {
                string key = regexMatch.Groups[1].Value.Trim();
                JsonMapping foundMapping = mappings.Find(x => x.JsonPropertyName.Equals(key, StringComparison.CurrentCultureIgnoreCase));
                if (foundMapping != null)
                {
                    yield return foundMapping.ToMatchDetail(key, regexMatch.Groups[2].Value.Trim());
                }
            }
        }
        private static IEnumerable<WorkAccount> MatchToWorkAccounts(string allOne, int howMany)
        {
            var list = new WorkAccountCollection(howMany);
            var matchDets = MatchToKeyValuePairs(allOne, typeof(WorkAccount)).ToList();
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

        internal static IEnumerable<BaseDetail> ParseFrom(IEnumerable<string> allLines)
        {
            IEnumerable<string> classes = ParseIntoClasses(allLines);
            string allOne = string.Join(Environment.NewLine, allLines);

            return ParseFromNotWA(classes, allOne);   
        }

        private static IEnumerable<BaseDetail> ParseFromNotWA(IEnumerable<string> classes, string allOne)
        {
            foreach (string s in classes)
            {
                if (!s.Contains("WorkAccount"))
                {
                    var t = Type.GetType(string.Format("MG.DsReg.{0}", s));
                    if (t != null)
                    {
                        var newObj = Activator.CreateInstance(t) as BaseDetail;
                        yield return MatchTo(newObj, allOne);
                    }
                }
                else
                {
                    int count = classes.Count(x => x.Contains("WorkAccount"));
                    foreach (BaseDetail bd in MatchToWorkAccounts(allOne, count))
                    {
                        yield return bd;
                    }
                }
            }
        }
        public static DsRegResult ParseFromText(IEnumerable<string> allLines) => new DsRegResult(allLines);
        internal static IEnumerable<string> ParseIntoClasses(IEnumerable<string> lines)
        {
            string allOne = string.Join(Environment.NewLine, lines);
            MatchCollection matchCol = Regex.Matches(allOne, CLASS_REGEX, RegexOptions.Multiline);
            foreach (Match m in matchCol)
            {
                yield return (m.Groups[1].Value as string).Replace(" ", string.Empty);
            }
        }

        #region PRIVATE CLASS - IGNORE CASE

        private class IgnoreCase : IEqualityComparer<string>
        {
            public bool Equals(string x, string y) => x.Equals(y, StringComparison.CurrentCultureIgnoreCase);
            public int GetHashCode(string obj) => obj.GetHashCode();
        }

        #endregion
    }
}
