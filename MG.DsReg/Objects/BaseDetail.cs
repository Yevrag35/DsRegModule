using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public abstract class BaseDetail : IJsonOutputter
    {
        private const string REGEX_MULTI = @"^\s{0,1}\[\s{1,}(.{1,})\s{1,}UTC\s{1,}\-\-\s{1,}(.{1,})\s{1,}UTC\s{1,}\]$";
        private const string REGEX_SINGLE = @"^\s{0,1}(.{1,})\s{1,}UTC$";
        private const char SEMICOLON = (char)58;

        protected Dictionary<string, Action<string>> Setters { get; }

        public BaseDetail(int capacity)
        {
            this.Setters = new Dictionary<string, Action<string>>(capacity, StringComparer.CurrentCultureIgnoreCase);
        }

        protected void AddSetter<TClass, TProp>(TClass obj, Expression<Func<TClass, TProp>> expression, Action<string> action)
            where TClass : BaseDetail
        {
            if (TryGetInfoFromExpression(expression, out MemberExpression memEx))
            {
                JsonPropertyAttribute jsonPropAtt = memEx.Member.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonPropAtt == null || string.IsNullOrWhiteSpace(jsonPropAtt.PropertyName))
                    return;

                this.Setters.Add(jsonPropAtt.PropertyName, action);
            }

        }

        protected Guid? ConvertGuid(string guid)
        {
            Guid? maybe = null;
            if (Guid.TryParse(guid, out Guid gotcha))
            {
                maybe = gotcha;
            }
            return maybe;
        }
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

        [Obsolete]
        private string GetValue(string line)
        {
            try
            {
                return line.Substring(line.IndexOf(SEMICOLON) + 1).Trim();
            }
            catch
            {
                return null;
            }
        }
        [Obsolete]
        private T GetValue<T>(string line, Func<string, T> func)
        {
            string strVal = this.GetValue(line);
            if (!string.IsNullOrWhiteSpace(strVal))
            {
                try
                {
                    return func(strVal);
                }
                catch
                {
                    return default;
                }
            }
            else
                return default;
        }
        [Obsolete]
        protected bool MatchesString<TClass, TProp>(TClass obj, string line, Expression<Func<TClass, TProp>> expression)
            where TClass : BaseDetail
        {
            if (TryGetInfoFromExpression(expression, out MemberExpression member))
            {
                return StringComparer.CurrentCultureIgnoreCase.Equals(line.Substring(0, line.IndexOf(SEMICOLON)).Trim(), member.Member.Name);
            }
            else
                return false;
        }
        [Obsolete]
        protected void SetValue<TClass, TProp>(TClass obj, string line, Func<string, TProp> func, Action<TProp> action)
            where TClass : BaseDetail
        {
            TProp value = this.GetValue(line, func);
            action(value);
        }
        [Obsolete]
        protected void SetValue(string line, Action<string> action)
        {
            string value = this.GetValue(line);
            action(value);
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

        public virtual void ApplyLines(string[] properties)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                (string, string) kvp = this.GetKeyAndValue(properties[i]);
                this.Setters[kvp.Item1](kvp.Item2);
            }
        }
        protected bool? ConvertToBool(string yesNo)
        {
            if (string.IsNullOrWhiteSpace(yesNo))
                return null;

            if (yesNo.IndexOf("YES", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
            else
                return false;
        }
        private (string, string) GetKeyAndValue(string line)
        {
            string key = line.Substring(0, line.IndexOf(SEMICOLON)).Trim();
            string value = line.Substring(line.IndexOf(SEMICOLON) + 1).Trim();
            return (key, value);
        }
        private static bool TryGetInfoFromExpression<TClass, TProp>(Expression<Func<TClass, TProp>> expression, out MemberExpression memberExp)
        {
            memberExp = null;
            if (expression.Body is MemberExpression memEx)
            {
                memberExp = memEx;
            }
            else if (expression.Body is UnaryExpression unEx && unEx.Operand is MemberExpression unExMem)
            {
                memberExp = unExMem;
            }
            return memberExp != null;
        }
    }
}
