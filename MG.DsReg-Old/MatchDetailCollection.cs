using System;
using System.Collections.Generic;
using System.Reflection;

namespace MG.DsReg
{
    public class MatchDetailCollection : List<MatchDetail>
    {
        public MatchDetailCollection() : base() { }
        public MatchDetailCollection(IEnumerable<MatchDetail> details)
            : base(details) { }

        public MatchDetailCollection(int capacity)
            : base(capacity) { }

        public void Add(string key, string value, PropertyInfo pi) =>
            base.Add(new MatchDetail(key, value, pi));
    }
}
