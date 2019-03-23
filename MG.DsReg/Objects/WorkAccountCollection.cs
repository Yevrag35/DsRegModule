using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MG.DsReg
{
    public class WorkAccountCollection : List<WorkAccount>, IJsonOutputter
    {
        public WorkAccountCollection() : base() { }
        public WorkAccountCollection(IEnumerable<WorkAccount> accounts)
            : base(accounts) { }

        public WorkAccountCollection(int capacity)
            : base(capacity) { }

        string IJsonOutputter.ToJson(Formatting asFormat, bool includeType)
        {
            var serializer = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Include,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };
            if (includeType)
                serializer.TypeNameHandling = TypeNameHandling.Objects;

            string jsonStr = JsonConvert.SerializeObject(this, asFormat, serializer);
            return jsonStr;
        }
    }
}
