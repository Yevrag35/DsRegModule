using System;

namespace MG.DsReg
{
    public class WorkAccount : BaseDetail
    {
        public bool? NgcSet { get; set; }
        public Guid? WorkplaceDeviceId { get; set; }
        public string WorkplaceIdp { get; set; }
        public string WorkplaceMdmUrl { get; set; }
        public string WorkplaceSettingsUrl { get; set; }
        public Guid? WorkplaceTenantId { get; set; }
        public string WorkplaceTenantName { get; set; }
        public string WorkplaceThumbprint { get; set; }

        public WorkAccount() { }
    }
}
