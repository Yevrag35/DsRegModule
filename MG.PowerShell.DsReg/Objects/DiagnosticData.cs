﻿using System;

namespace MG.PowerShell.DsReg
{
    public class DiagnosticData : BaseDetail
    {
        public bool? AadRecoveryNeeded { get; set; }
        public string KeySignTest { get; set; }

        public DiagnosticData() { }
    }
}
