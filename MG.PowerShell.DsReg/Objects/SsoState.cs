using System;

namespace MG.PowerShell.DsReg
{
    public class SsoState : BaseDetail
    {
        internal string azureAdPrtUpdateTime { get; set; }
        internal string azureadprtExpiryTime { get; set; }

        public bool? AzureAdPrt { get; set; }
        public DateTime? AzureAdPrtUpdateTime => base.ConvertTime(azureAdPrtUpdateTime);
        public DateTime? AzureAdPrtExpiryTime => base.ConvertTime(azureadprtExpiryTime);
        public string AzureAdPrtAuthority { get; set; }
        public bool? EnterprisePrt { get; set; }
        public string EnterprisePrtAuthority { get; set; }

        public SsoState() { }
    }
}
