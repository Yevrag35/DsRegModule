using System;

namespace MG.DsReg
{
    public class NgcPrerequisiteCheck : BaseDetail
    {
        public bool? IsDeviceJoined { get; set; }
        public bool? IsUserAzureAD { get; set; }
        public bool? PolicyEnabled { get; set; }
        public bool? PostLogonEnabled { get; set; }
        public bool? DeviceEligible { get; set; }
        public bool? SessionIsNotRemote { get; set; }
        public string CertEnrollment { get; set; }
        public string PreReqResult { get; set; }

        public NgcPrerequisiteCheck() { }
    }
}
