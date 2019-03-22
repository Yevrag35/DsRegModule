using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.PowerShell.DsReg
{
    public class DeviceState : BaseDetail
    {
        public bool? DomainJoined { get; set; }
        public bool? AzureADJoined { get; set; }
        public bool? EnterpriseJoined { get; set; }

        public DeviceState() { }
    }
}
