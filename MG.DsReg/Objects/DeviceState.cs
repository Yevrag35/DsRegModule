using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        [JsonConstructor]
        public DeviceState()
            : base()
        {
            base.AddSetter(this, x => x.AzureADJoined, x => this.AzureADJoined = base.ConvertToBool(x));
            base.AddSetter(this, x => x.DomainJoined, x => this.DomainJoined = base.ConvertToBool(x));
            base.AddSetter(this, x => x.DomainName, x => this.DomainName = x);
            base.AddSetter(this, x => x.EnterpriseJoined, x => this.EnterpriseJoined = base.ConvertToBool(x));
        }
    }
}
