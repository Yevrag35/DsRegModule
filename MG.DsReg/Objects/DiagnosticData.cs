using System;

namespace MG.DsReg
{
    public class DiagnosticData : BaseDetail
    {
        public bool? AadRecoveryNeeded { get; set; }
        public string KeySignTest { get; set; }

        public DiagnosticData() { }
    }
}
