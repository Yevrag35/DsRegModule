using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DeviceState : BaseDetail
    {
        [JsonProperty("azureADJoined")]
        public bool AzureADJoined { get; set; }

        [JsonProperty("domainJoined")]
        public bool DomainJoined { get; set; }

        [JsonProperty("domainName")]
        public string DomainName { get; set; }

        [JsonProperty("enterpriseJoined")]
        public bool EnterpriseJoined { get; set; }

        public DeviceState() { }
    }
}
