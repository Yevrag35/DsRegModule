using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class TenantDetails : BaseDetail
    {
        [JsonProperty("accessTokenUrl")]
        public string AccessTokenUrl { get; set; }

        [JsonProperty("authCoreUrl")]
        public string AuthCodeUrl { get; set; }

        [JsonProperty("deviceManagementSrvVer")]
        public Version DeviceManagementSrvVer { get; set; }

        [JsonProperty("deviceManagementSrvUrl")]
        public string DeviceManagementSrvUrl { get; set; }

        [JsonProperty("deviceManagementSrvId")]
        public string DeviceManagementSrvId { get; set; }

        [JsonProperty("idp")]
        public string Idp { get; set; }

        [JsonProperty("joinSrvVersion")]
        public Version JoinSrvVersion { get; set; }

        [JsonProperty("joinSrvUrl")]
        public string JoinSrvUrl { get; set; }

        [JsonProperty("joinSrvId")]
        public string JoinSrvId { get; set; }

        [JsonProperty("keySrvVersion")]
        public Version KeySrvVersion { get; set; }

        [JsonProperty("keySrvUrl")]
        public string KeySrvUrl { get; set; }

        [JsonProperty("keySrvId")]
        public string KeySrvId { get; set; }

        [JsonProperty("mdmUrl")]
        public string MdmUrl { get; set; }

        [JsonProperty("mdmTouUrl")]
        public string MdmTouUrl { get; set; }

        [JsonProperty("MdmComplianceUrl")]
        public string MdmComplianceUrl { get; set; }

        [JsonProperty("settingsUrl")]
        public string SettingsUrl { get; set; }

        [JsonProperty("tenantName")]
        public string TenantName { get; set; }

        [JsonProperty("tenantId")]
        public Guid? TenantId { get; set; }

        [JsonProperty("webAuthNSrvVersion")]
        public Version WebAuthNSrvVersion { get; set; }

        [JsonProperty("webAuthNSrvUrl")]
        public string WebAuthNSrvUrl { get; set; }

        [JsonProperty("webAuthNSrvId")]
        public string WebAuthNSrvId { get; set; }

        [JsonConstructor]
        public TenantDetails()
            : base(21)
        {

        }
    }
}
