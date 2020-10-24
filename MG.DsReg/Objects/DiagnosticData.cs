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

        [JsonConstructor]
        public DiagnosticData()
            : base(2)
        {
            base.AddSetter(this, x => x.AadRecoveryEnabled, x => this.AadRecoveryEnabled = ConvertToBool(x));
            base.AddSetter(this, x => x.KeySignTest, x => this.KeySignTest = x);
        }
    }
}
