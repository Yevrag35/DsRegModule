using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    public abstract class BaseDetail : IJsonOutputter
    {
        private const string REGEX_MULTI = @"^\s{0,1}\[\s{1,}(.{1,})\s{1,}UTC\s{1,}\-\-\s{1,}(.{1,})\s{1,}UTC\s{1,}\]$";
        private const string REGEX_SINGLE = @"^\s{0,1}(.{1,})\s{1,}UTC$";

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

        public string ToJson(Formatting asFormat, bool includeTypes)
        {
            var serializer = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Include,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            if (includeTypes)
                serializer.TypeNameHandling = TypeNameHandling.Objects;

            string jsonStr = JsonConvert.SerializeObject(this, asFormat, serializer);
            return jsonStr;
        }
    }
}
