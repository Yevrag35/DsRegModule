using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    public class JsonMapping
    {
        private PropertyInfo PropertyInfo { get; }
        public string PropertyName => this.PropertyInfo.Name;
        public string JsonPropertyName { get; }

        private JsonMapping(PropertyInfo propertyInfo, JsonPropertyAttribute foundAttribute)
        {
            this.PropertyInfo = propertyInfo;
            if (!string.IsNullOrEmpty(foundAttribute.PropertyName))
                this.JsonPropertyName = foundAttribute.PropertyName;

            else
                this.JsonPropertyName = propertyInfo.Name;   
        }

        internal MatchDetail ToMatchDetail(Match regexMatch)
        {
            MatchDetail md = null;
            if (regexMatch.Success)
            {
                string key = regexMatch.Groups[1].Value.Trim();
                string value = regexMatch.Groups[2].Value.Trim();
                md = new MatchDetail(key, value, this.PropertyInfo);
            }
            return md;
        }
        public MatchDetail ToMatchDetail(string key, string value) => new MatchDetail(key, value, this.PropertyInfo);

        private static JsonMapping Construct(PropertyInfo pi)
        {
            JsonMapping mapping = null;
            JsonPropertyAttribute att = pi.GetCustomAttribute<JsonPropertyAttribute>();
            if (att != null)
            {
                mapping = new JsonMapping(pi, att);
            }
            return mapping;
        }

        public static IEnumerable<JsonMapping> MappingFromObject(object fromThis)
        {
            Type fromThisType = fromThis.GetType();
            return MappingFromType(fromThisType);
        }
        public static List<JsonMapping> MappingFromType(Type fromThisType)
        {
            PropertyInfo[] propInfos = fromThisType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var list = new List<JsonMapping>(propInfos.Length);

            foreach (PropertyInfo pi in propInfos)
            {
                JsonMapping mapping = Construct(pi);
                if (mapping != null)
                    list.Add(mapping);
            }
            return list;
        }
    }
}