using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class DsRegResult : BaseDetail
    {
        #region FIELDS/CONSTANTS
        private string[] _dsRegLines;

        #endregion

        #region PROPERTIES
        [JsonProperty("deviceDetails")]
        public DeviceDetails DeviceDetails { get; private set; }

        [JsonProperty("deviceState")]
        public DeviceState DeviceState { get; private set; }

        [JsonProperty("diagnosticData")]
        public DiagnosticData DiagnosticData { get; private set; }

        [JsonProperty("ngcPrerequisiteCheck")]
        public NgcPrerequisiteCheck NgcPrerequisiteCheck { get; private set; }

        [JsonProperty("ssoState")]
        public SSOState SsoState { get; private set; }

        [JsonProperty("tenantDetails")]
        public TenantDetails TenantDetails { get; private set; }

        [JsonProperty("userState")]
        public UserState UserState { get; private set; }

        [JsonProperty("workAccounts")]
        public WorkAccountCollection WorkAccounts { get; } = new WorkAccountCollection();

        #endregion

        #region CONSTRUCTORS
        public DsRegResult(IEnumerable<string> allLines)
        {
            if (allLines is string[] strArr)
                _dsRegLines = strArr;

            else
                _dsRegLines = allLines.ToArray();

            this.PopulateClasses();
        }

        #endregion

        #region PUBLIC METHODS


        #endregion

        #region BACKEND/PRIVATE METHODS
        private void PopulateClasses()
        {
            foreach (BaseDetail bd in DsRegParser.ParseFrom(_dsRegLines))
            {
                if (bd is DeviceDetails dd)
                    this.DeviceDetails = dd;

                else if (bd is DeviceState ds)
                    this.DeviceState = ds;

                else if (bd is DiagnosticData diag)
                    this.DiagnosticData = diag;

                else if (bd is NgcPrerequisiteCheck npc)
                    this.NgcPrerequisiteCheck = npc;

                else if (bd is SSOState sso)
                    this.SsoState = sso;

                else if (bd is TenantDetails td)
                    this.TenantDetails = td;

                else if (bd is UserState us)
                    this.UserState = us;

                else if (bd is WorkAccount wa)
                    this.WorkAccounts.Add(wa);
            }
        }

        #endregion
    }
}