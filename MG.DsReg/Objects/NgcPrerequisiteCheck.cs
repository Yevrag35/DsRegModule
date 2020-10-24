using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class NgcPrerequisiteCheck : BaseDetail
    {
        [JsonProperty("certEnrollment")]
        public string CertEnrollment { get; set; }

        [JsonProperty("deviceEligible")]
        public bool? DeviceEligible { get; set; }

        [JsonProperty("isDeviceJoined")]
        public bool? IsDeviceJoined { get; set; }

        [JsonProperty("isUserAzureAD")]
        public bool? IsUserAzureAD { get; set; }

        [JsonProperty("policyEnabled")]
        public bool? PolicyEnabled { get; set; }

        [JsonProperty("postLogonEnabled")]
        public bool? PostLogonEnabled { get; set; }

        [JsonProperty("preReqResult")]
        public string PreReqResult { get; set; }

        [JsonProperty("sessionIsNotRemote")]
        public bool? SessionIsNotRemote { get; set; }
        
        [JsonConstructor]
        public NgcPrerequisiteCheck()
            : base(8)
        {
            base.AddSetter(this, x => x.CertEnrollment, x => this.CertEnrollment = x);
            base.AddSetter(this, x => x.DeviceEligible, x => this.DeviceEligible = ConvertToBool(x));
            base.AddSetter(this, x => x.IsDeviceJoined, x => this.IsDeviceJoined = ConvertToBool(x));
            base.AddSetter(this, x => x.IsUserAzureAD, x => this.IsUserAzureAD = ConvertToBool(x));
            base.AddSetter(this, x => x.PolicyEnabled, x => this.PolicyEnabled = ConvertToBool(x));
            base.AddSetter(this, x => PostLogonEnabled, x => this.PostLogonEnabled = ConvertToBool(x));
            base.AddSetter(this, x => x.PreReqResult, x => this.PreReqResult = x);
            base.AddSetter(this, x => x.SessionIsNotRemote, x => this.SessionIsNotRemote = ConvertToBool(x));
        }
    }
}
