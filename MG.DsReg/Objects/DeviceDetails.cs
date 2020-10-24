using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DeviceDetails : BaseDetail
    {
        [JsonProperty("deviceCertificateValidity")]
        internal string _deviceCertificateValidity { get; set; }

        [JsonProperty("deviceId")]
        public Guid? DeviceId { get; private set; }

        [JsonIgnore]
        public DateTimeOffset? DeviceCertificateValidityStart => base.ConvertTime(this._deviceCertificateValidity, 1);

        [JsonIgnore]
        public DateTimeOffset? DeviceCertificateValidityEnd => base.ConvertTime(this._deviceCertificateValidity, 2);

        [JsonProperty("keyContainerId")]
        public Guid? KeyContainerId { get; set; }

        [JsonProperty("thumbprint")]
        public string Thumbprint { get; set; }

        [JsonProperty("tpmProtected")]
        public bool? TpmProtected { get; set; }

        [JsonConstructor]
        public DeviceDetails()
            : base(5)
        {
            base.AddSetter(this, x => x._deviceCertificateValidity, x => _deviceCertificateValidity = x);
            base.AddSetter(this, x => x.DeviceId, x => base.ConvertGuid(x));
            base.AddSetter(this, x => x.KeyContainerId, x => base.ConvertGuid(x));
            base.AddSetter(this, x => x.Thumbprint, x => this.Thumbprint = x);
            base.AddSetter(this, x => x.TpmProtected, x => this.TpmProtected = base.ConvertToBool(x));
        }
    }
}
