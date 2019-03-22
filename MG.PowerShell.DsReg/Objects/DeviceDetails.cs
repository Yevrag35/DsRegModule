using System;

namespace MG.PowerShell.DsReg
{
    public class DeviceDetails : BaseDetail
    {
        

        internal string deviceCertificateValidity { get; set; }

        public Guid? DeviceId { get; set; }
        public string Thumbprint { get; set; }
        public DateTime? DeviceCertificateValidityStart => base.ConvertTime(this.deviceCertificateValidity, 1);
        public DateTime? DeviceCertificateValidityEnd => base.ConvertTime(this.deviceCertificateValidity, 2);
        public Guid? KeyContainerId { get; set; }
        public bool? TpmProtected { get; set; }

        public DeviceDetails() { }
    }
}
