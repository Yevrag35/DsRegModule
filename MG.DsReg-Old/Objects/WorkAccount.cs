using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class WorkAccount : BaseDetail
    {
        [JsonProperty("ngcSet")]
        public bool? NgcSet { get; private set; }

        [JsonProperty("workplaceDeviceId")]
        public Guid? WorkplaceDeviceId { get; private set; }

        [JsonProperty("workplaceIdp")]
        public string WorkplaceIdp { get; private set; }

        [JsonProperty("workplaceMdmUrl")]
        public string WorkplaceMdmUrl { get; private set; }

        [JsonProperty("workplaceSettingsUrl")]
        public string WorkplaceSettingsUrl { get; private set; }

        [JsonProperty("workplaceTenantId")]
        public Guid? WorkplaceTenantId { get; private set; }

        [JsonProperty("workplaceTenantName")]
        public string WorkplaceTenantName { get; private set; }

        [JsonProperty("workplaceThumbprint")]
        public string WorkplaceThumbprint { get; private set; }

        public WorkAccount() { }
    }
}
