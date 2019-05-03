using System;

namespace MG.DsReg
{
    public class SSOState : BaseDetail
    {
        internal string azureAdPrtUpdateTime { get; set; }
        internal string azureadprtExpiryTime { get; set; }

        public bool? AzureAdPrt { get; set; }
        public string AzureAdPrtAuthority { get; set; }
        public DateTime? AzureAdPrtExpiryTime => base.ConvertTime(azureadprtExpiryTime);
        public DateTime? AzureAdPrtUpdateTime => base.ConvertTime(azureAdPrtUpdateTime);
        public bool? EnterprisePrt { get; set; }
        public string EnterprisePrtAuthority { get; set; }

        public SSOState() { }
    }
}
