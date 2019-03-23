using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MG.DsReg
{
    public class DeviceState : BaseDetail
    {
        public bool? AzureADJoined { get; set; }
        public bool? DomainJoined { get; set; }
        public string DomainName { get; set; }
        public bool? EnterpriseJoined { get; set; }

        public DeviceState() { }
    }
}
