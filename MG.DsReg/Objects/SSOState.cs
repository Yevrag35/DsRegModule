using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SSOState : BaseDetail
    {
        [JsonProperty("azureAdPrtUpdateTime")]
        internal string azureAdPrtUpdateTime { get; set; }

        [JsonProperty("azureadprtExpiryTime")]
        internal string azureadprtExpiryTime { get; set; }

        [JsonProperty("azureAdPrt")]
        public bool? AzureAdPrimaryRefreshToken { get; set; }

        [JsonProperty("azureAdPrtAuthority")]
        public string AzureAdPrimaryRefreshTokenAuthority { get; set; }

        [JsonIgnore]
        public DateTimeOffset? AzureAdPrimaryRefreshTokenExpiryTime => base.ConvertTime(azureadprtExpiryTime);

        [JsonIgnore]
        public DateTimeOffset? AzureAdPrimaryRefreshTokenUpdateTime => base.ConvertTime(azureAdPrtUpdateTime);

        [JsonProperty("enterprisePrt")]
        public bool? EnterprisePrimaryRefreshToken { get; set; }

        [JsonProperty("enterprisePrtAuthority")]
        public string EnterprisePrimaryRefreshTokenAuthority { get; set; }

        public SSOState() { }
    }
}
