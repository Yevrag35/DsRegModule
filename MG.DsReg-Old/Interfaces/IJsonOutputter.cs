using Newtonsoft.Json;

namespace MG.DsReg
{
    public interface IJsonOutputter
    {
        string ToJson();
        string ToJson(JsonSerializerSettings serializerSettings);
    }
}
