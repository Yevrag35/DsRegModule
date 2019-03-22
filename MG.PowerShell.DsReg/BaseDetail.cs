using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.PowerShell.DsReg
{
    public abstract class BaseDetail
    {
        private const string REGEX_MULTI = @"^\[\s{1,}(.{1,})\s{1,}UTC\s{1,}\-\-\s{1,}(.{1,})\s{1,}UTC\s{1,}\]$";
        private const string REGEX_SINGLE = @"^(.{1,})\s{1,}UTC$";

        public DateTime? ConvertTime(string input, int groupNo)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            DateTime? dtVal = null;
            Match match = Regex.Match(input, REGEX_MULTI);
            if (match.Success && DateTime.TryParse(match.Groups[groupNo].Value as string, out DateTime dt))
            {
                var utcDt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                dtVal = utcDt;
            }
            return dtVal;
        }

        public DateTime? ConvertTime(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            DateTime? dtVal = null;
            Match match = Regex.Match(input, REGEX_SINGLE);
            if (match.Success && DateTime.TryParse(match.Groups[1].Value as string, out DateTime dt))
            {
                var utcDt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                dtVal = utcDt;
            }
            return dtVal;
        }
    }
}
