using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    public interface IJsonOutputter
    {
        string ToJson(Formatting asFormat, bool includeType);
    }
}
