using MG.DsReg;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace MG.PowerShell.DsReg
{
    [Cmdlet(VerbsCommon.Get, "DsRegStatus")]
    public class GetDsRegStatus : PSCmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        [ValidateSet("DeviceDetails", "DeviceState", "DiagnosticData", "NgcPrerequisiteCheck", "SsoState", "TenantDetails", "UserState", "WorkAccount")]
        public string Display { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter AsJson { get; set; }

        protected override void ProcessRecord()
        {
            IDsRegExecutor exec = DsRegExecutor.NewExecutor();
            IDsRegResult result = exec.GetStatus();
            object writtenOut = null;

            if (this.MyInvocation.BoundParameters.ContainsKey("Display"))
            {
                IJsonOutputter retObj = null;
                switch (this.Display)
                {
                    case "DeviceDetails":
                        retObj = result.DeviceDetails;
                        break;

                    case "DeviceState":
                        retObj = result.DeviceState;
                        break;

                    case "DiagnosticData":
                        retObj = result.DiagnosticData;
                        break;

                    case "NgcPrerequisiteCheck":
                        retObj = result.NgcPrerequisiteCheck;
                        break;

                    case "SsoState":
                        retObj = result.SsoState;
                        break;

                    case "TenantDetails":
                        retObj = result.TenantDetails;
                        break;

                    case "UserState":
                        retObj = result.UserState;
                        break;

                    case "WorkAccount":
                        retObj = result.WorkAccounts;
                        break;
                }

                writtenOut = this.MyInvocation.BoundParameters.ContainsKey("AsJson") ? 
                    retObj.ToJson(Formatting.Indented, false) : (object)retObj;
            }
            else
            {
                writtenOut = result;
            }
            WriteObject(writtenOut, false);
        }
    }
}
