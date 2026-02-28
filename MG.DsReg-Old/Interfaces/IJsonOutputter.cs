using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    public interface IJsonOutputter
    {
        string ToJson();
        string ToJson(JsonSerializerSettings serializerSettings);
    }
}
