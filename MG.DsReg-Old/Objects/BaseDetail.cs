using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class BaseDetail : IJsonOutputter
    {
        private const string REGEX_MULTI = @"^\s{0,1}\[\s{1,}(.{1,})\s{1,}UTC\s{1,}\-\-\s{1,}(.{1,})\s{1,}UTC\s{1,}\]$";
        private const string REGEX_SINGLE = @"^\s{0,1}(.{1,})\s{1,}UTC$";

        protected private DateTimeOffset? ConvertTime(string input, int groupNo)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            DateTimeOffset? dtVal = null;
            Match match = Regex.Match(input, REGEX_MULTI);
            if (match.Success && DateTime.TryParse(match.Groups[groupNo].Value, out DateTime dt))
            {
                var utcDt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(utcDt);
                var utc = new DateTimeOffset(dt, offset);
                dtVal = utc;
            }
            return dtVal;
        }

        protected private DateTimeOffset? ConvertTime(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            DateTimeOffset? dtVal = null;
            Match match = Regex.Match(input, REGEX_SINGLE);
            if (match.Success && DateTime.TryParse(match.Groups[1].Value, out DateTime dt))
            {
                var utcDt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(utcDt);
                var utc = new DateTimeOffset(dt, offset);
                dtVal = utc;
            }
            return dtVal;
        }
        public string ToJson()
        {
            var serializer = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Include,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };
            serializer.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            serializer.Converters.Add(new VersionConverter());
            return this.ToJson(serializer);
        }

        public string ToJson(JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.SerializeObject(this, serializerSettings);
        }
    }
}
