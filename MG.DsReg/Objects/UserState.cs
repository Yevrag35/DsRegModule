using System;

namespace MG.DsReg
{
    public class UserState : BaseDetail
    {
        public string CanReset { get; set; }
        public Guid? NgcKeyId { get; set; }
        public bool? NgcSet { get; set; }
        public bool? WamDefaultSet { get; set; }
        public string WamDefaultAuthority { get; set; }
        public string WamDefaultId { get; set; }
        public string WamDefaultGUID { get; set; }
        public bool? WorkplaceJoined { get; set; }
        public int? WorkAccountCount { get; set; }

        public UserState() { }
    }
}
