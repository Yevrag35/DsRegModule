using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MG.DsReg
{
    [Serializable]
    public class WorkAccountCollection : IEnumerable<WorkAccount>, IJsonOutputter
    {
        private List<WorkAccount> _list;

        public WorkAccount this[int index] => _list[index];

        public int Count => _list.Count;

        public WorkAccountCollection() => _list = new List<WorkAccount>();
        public WorkAccountCollection(int capacity) => _list = new List<WorkAccount>(capacity);

        internal void Add(WorkAccount wa) => _list.Add(wa);

        public IEnumerator<WorkAccount> GetEnumerator() => ((IEnumerable<WorkAccount>)_list).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<WorkAccount>)_list).GetEnumerator();

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
            return JsonConvert.SerializeObject(this, serializerSettings);
        }
    }
}
