using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MG.DsReg
{
    [Serializable]
    public class WorkAccountCollection : IReadOnlyList<WorkAccount>, IJsonOutputter
    {
        private SortedList<string, WorkAccount> _list;

        public WorkAccount this[string tenantName] => _list[tenantName];
        public WorkAccount this[int index]
        {
            get
            {
                if (index >= 0)
                    return _list.Values[index];

                else
                {
                    int goHere = _list.Count + index;
                    return goHere >= 0 ? _list.Values[goHere] : null;
                }
            }
        }

        public int Count => _list.Count;

        public WorkAccountCollection()
        {
            _list = new SortedList<string, WorkAccount>(StringComparer.CurrentCultureIgnoreCase);
        }
        public WorkAccountCollection(int capacity)
        {
            _list = new SortedList<string, WorkAccount>(capacity, StringComparer.CurrentCultureIgnoreCase);
        }

        internal void Add(WorkAccount wa)
        {
            if (_list.ContainsKey(wa.WorkplaceTenantName))
                return;

            _list.Add(wa.WorkplaceTenantName, wa);
        }

        public IEnumerator<WorkAccount> GetEnumerator()
        {
            return _list.Values.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public string ToJson()
        {
            var serializer = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };
            serializer.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            return this.ToJson(serializer);
        }
        public string ToJson(JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.SerializeObject(_list.Values, serializerSettings);
        }
    }
}
