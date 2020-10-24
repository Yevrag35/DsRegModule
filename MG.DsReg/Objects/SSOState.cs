using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SSOState : BaseDetail
    {
        //[JsonProperty("azureAdPrtUpdateTime")]
        internal string azureAdPrtUpdateTime { get; set; }

        //[JsonProperty("azureadprtExpiryTime")]
        internal string azureadprtExpiryTime { get; set; }

        [JsonProperty("azureAdPrt")]
        public bool? AzureAdPrimaryRefreshToken { get; set; }

        [JsonProperty("azureAdPrtAuthority")]
        public string AzureAdPrimaryRefreshTokenAuthority { get; set; }

        //[JsonIgnore]
        [JsonProperty("azureadprtExpiryTime")]
        public DateTimeOffset? AzureAdPrimaryRefreshTokenExpiryTime { get; set; }// => base.ConvertTime(azureadprtExpiryTime);

        //[JsonIgnore]
        [JsonProperty("azureAdPrtUpdateTime")]
        public DateTimeOffset? AzureAdPrimaryRefreshTokenUpdateTime { get; set; }//=> base.ConvertTime(azureAdPrtUpdateTime);

        [JsonProperty("enterprisePrt")]
        public bool? EnterprisePrimaryRefreshToken { get; set; }

        [JsonProperty("enterprisePrtAuthority")]
        public string EnterprisePrimaryRefreshTokenAuthority { get; set; }

        [JsonConstructor]
        public SSOState()
            : base(6)
        {
            base.AddSetter(this, x => x.AzureAdPrimaryRefreshToken, x => this.AzureAdPrimaryRefreshToken = base.ConvertToBool(x));
            base.AddSetter(this, x => x.AzureAdPrimaryRefreshTokenAuthority, x => this.AzureAdPrimaryRefreshTokenAuthority = x);
            base.AddSetter(this, x => x.AzureAdPrimaryRefreshTokenExpiryTime, x => this.AzureAdPrimaryRefreshTokenExpiryTime = base.ConvertTime(x));
            base.AddSetter(this, x => x.AzureAdPrimaryRefreshTokenUpdateTime, x => this.AzureAdPrimaryRefreshTokenUpdateTime = base.ConvertTime(x));
            base.AddSetter(this, x => x.EnterprisePrimaryRefreshToken, x => this.EnterprisePrimaryRefreshToken = base.ConvertToBool(x));
            base.AddSetter(this, x => x.EnterprisePrimaryRefreshTokenAuthority, x => this.EnterprisePrimaryRefreshTokenAuthority = x);
        }
    }
}
