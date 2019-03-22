using System;

namespace MG.PowerShell.DsReg
{
    public class WorkAccount : BaseDetail
    {
        public Guid? WorkplaceDeviceId { get; set; }
        public string WorkplaceThumbprint { get; set; }
        public string WorkplaceIdp { get; set; }
        public Guid? WorkplaceTenantId { get; set; }
        public string WorkplaceTenantName { get; set; }
        public string WorkplaceMdmUrl { get; set; }
        public string WorkplaceSettingsUrl { get; set; }
        public bool? NgcSet { get; set; }

        public WorkAccount() { }
    }
}
