using System;

namespace MG.DsReg
{
    public class TenantDetails : BaseDetail
    {
        public string AccessTokenUrl { get; set; }
        public string AuthCodeUrl { get; set; }
        public Version DeviceManagementSrvVer { get; set; }
        public string DeviceManagementSrvUrl { get; set; }
        public string DeviceManagementSrvId { get; set; }
        public string Idp { get; set; }
        public Version JoinSrvVersion { get; set; }
        public string JoinSrvUrl { get; set; }
        public string JoinSrvId { get; set; }
        public Version KeySrvVersion { get; set; }
        public string KeySrvUrl { get; set; }
        public string KeySrvId { get; set; }
        public string MdmUrl { get; set; }
        public string MdmTouUrl { get; set; }
        public string MdmComplianceUrl { get; set; }
        public string SettingsUrl { get; set; }
        public string TenantName { get; set; }
        public Guid? TenantId { get; set; }
        public Version WebAuthNSrvVersion { get; set; }
        public string WebAuthNSrvUrl { get; set; }
        public string WebAuthNSrvId { get; set; }

        public TenantDetails() { }
    }
}
