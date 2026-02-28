using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DiagnosticData : BaseDetail
    {
        [JsonProperty("aadRecoveryEnabled")]
        public bool? AadRecoveryEnabled { get; set; }

        [JsonProperty("keySignTest")]
        public string KeySignTest { get; set; }

        public DiagnosticData() { }
    }
}
