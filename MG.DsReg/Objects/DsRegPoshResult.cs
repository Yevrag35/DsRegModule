using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MG.DsReg
{
    public class DsRegPoshResult
    {
        private DsRegResult _res;

        #region PUBLIC PROPERTIES
        public bool AdfsEnterpriseJoined => _res.DeviceState.EnterpriseJoined;
        public Guid? AzureTenantId => _res.TenantDetails.TenantId;
        public string AzureTenantName => _res.TenantDetails.TenantName;
        public bool AzureADJoined => _res.DeviceState.AzureADJoined;
        public string DeviceCertificateThumbprint => _res.DeviceDetails.Thumbprint;
        public DateTime? DeviceCertificateValidityEnd => _res.DeviceDetails.DeviceCertificateValidityEnd;
        public DateTime? DeviceCertificateValidityStart => _res.DeviceDetails.DeviceCertificateValidityStart;
        public Guid? DeviceId => _res.DeviceDetails.DeviceId;
        public bool DeviceTpmProtected => _res.DeviceDetails.TpmProtected.HasValue && _res.DeviceDetails.TpmProtected.Value;
        public DiagnosticData DiagnosticDetails => _res.DiagnosticData;
        public bool DomainJoined => _res.DeviceState.DomainJoined;
        public string DomainName => _res.DeviceState.DomainName;
        public bool HybridAzureADJoined => _res.DeviceState.AzureADJoined && _res.DeviceState.DomainJoined;
        public Guid? KeyContainerId => _res.DeviceDetails.KeyContainerId;
        public NgcPrerequisiteCheck NgcPrerequisiteCheck => _res.NgcPrerequisiteCheck;
        public SSOState SsoState => _res.SsoState;
        public TenantDetails TenantDetails => _res.TenantDetails;
        public UserState UserState => _res.UserState;
        public WorkAccountCollection WorkAccounts => _res.WorkAccounts;

        #endregion

        public DsRegPoshResult(DsRegResult dsRegResult) => _res = dsRegResult;

        public string ToJson() => _res.ToJson();
    }
}
