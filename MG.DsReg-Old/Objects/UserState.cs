using Newtonsoft.Json;
using System;

namespace MG.DsReg
{
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class UserState : BaseDetail
    {
        [JsonProperty("canReset")]
        public string CanReset { get; private set; }

        [JsonProperty("ngcKeyId")]
        public Guid? NgcKeyId { get; private set; }

        [JsonProperty("ngcSet")]
        public bool? NgcSet { get; private set; }

        [JsonProperty("wamDefaultSet")]
        public bool? WamDefaultSet { get; private set; }

        [JsonProperty("wamDefaultAuthority")]
        public string WamDefaultAuthority { get; private set; }

        [JsonProperty("wamDefaultID")]
        public string WamDefaultId { get; private set; }

        [JsonProperty("wamDefaultGUID")]
        public string WamDefaultGUID { get; private set; }

        [JsonProperty("workplaceJoined")]
        public bool WorkplaceJoined { get; private set; }

        [JsonProperty("workAccountCount")]
        public int WorkAccountCount { get; private set; }

        public UserState() { }
    }
}
