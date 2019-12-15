using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace MG.DsReg
{
    internal class DsRegResult : IDsRegResult
    {
        private List<string> strs;
        private WorkAccountCollection _was { get; } = new WorkAccountCollection();
        private DeviceDetails _dd;
        private DeviceState _ds;
        private DiagnosticData _diag;
        private NgcPrerequisiteCheck _npc;
        private TenantDetails _td;
        private SSOState _sso;
        private UserState _us;

        DeviceDetails IDsRegResult.DeviceDetails => _dd;
        DeviceState IDsRegResult.DeviceState => _ds;
        DiagnosticData IDsRegResult.DiagnosticData => _diag;
        NgcPrerequisiteCheck IDsRegResult.NgcPrerequisiteCheck => _npc;
        SSOState IDsRegResult.SsoState => _sso;
        TenantDetails IDsRegResult.TenantDetails => _td;
        UserState IDsRegResult.UserState => _us;
        WorkAccountCollection IDsRegResult.WorkAccounts => _was;

        int IDsRegResult.LineCount => strs.Count;

        //private DsRegParser Parser { get; set; }

        internal DsRegResult(IEnumerable<string> allLines)
        {
            strs = new List<string>(allLines);
            _was = new WorkAccountCollection();
            //this.Parser = new DsRegParser();
            this.PopulateClasses();
        }

        IEnumerable<string> IDsRegResult.GetClasses() => DsRegParser.ParseIntoClasses(strs);
        private void PopulateClasses()
        {
            IEnumerable<BaseDetail> bds = DsRegParser.ParseFrom(strs);
            foreach (BaseDetail bd in bds)
            {
                if (bd is DeviceDetails dd)
                    _dd = dd;

                else if (bd is DeviceState ds)
                    _ds = ds;

                else if (bd is DiagnosticData diag)
                    _diag = diag;

                else if (bd is NgcPrerequisiteCheck npc)
                    _npc = npc;

                else if (bd is SSOState sso)
                    _sso = sso;

                else if (bd is TenantDetails td)
                    _td = td;

                else if (bd is UserState us)
                    _us = us;

                else if (bd is WorkAccount wa)
                    _was.Add(wa);
            }
        }

        public string ToJson()
        {
            var serializer = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DefaultValueHandling = DefaultValueHandling.Ignore,
                FloatParseHandling = FloatParseHandling.Decimal,
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Include
            };
            serializer.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
            return this.ToJson(serializer);
        }

        public string ToJson(JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.SerializeObject(this, serializerSettings);
        }

        //public override string ToString() => string.Join(Environment.NewLine, strs);

        string[] IDsRegResult.ToArray() => strs.ToArray();
    }
}
