using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MG.DsReg
{
    public interface IDsRegResult : IJsonOutputter
    {
        DeviceDetails DeviceDetails { get; }
        DeviceState DeviceState { get; }
        DiagnosticData DiagnosticData { get; }
        NgcPrerequisiteCheck NgcPrerequisiteCheck { get; }
        TenantDetails TenantDetails { get; }
        SSOState SsoState { get; }
        UserState UserState { get; }
        WorkAccountCollection WorkAccounts { get; }

        int LineCount { get; }

        IEnumerable<string> GetClasses();

        string[] ToArray();
    }
}
