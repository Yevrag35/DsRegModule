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
        
        public NgcPrerequisiteCheck() { }
    }
}
