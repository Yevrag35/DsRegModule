using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MG.DsReg
{
    public static class DsRegParser2
    {
        private const string PIPE = "|";
        private const string WORK_ACCOUNT = "| Work Account";

        private readonly static Dictionary<string, Type> TypeDictionary = new Dictionary<string, Type>(StringComparer.CurrentCultureIgnoreCase)
        {
            { "| Device State", typeof(DeviceState) },
            { "| Device Details", typeof(DeviceDetails) },
            { "| Tenant Details", typeof(TenantDetails) },
            { "| User State", typeof(UserState) },
            { "| SSO State", typeof(SSOState) },
            { WORK_ACCOUNT , typeof(WorkAccount) },
            { "| Diagnostic Data", typeof(DiagnosticData) },
            { "| Ngc Prerequisite Check", typeof(NgcPrerequisiteCheck) }
        };

        public static void ParseFromSingleLine(string allOneLine)
        {

        }

        public static List<(int, Type)> GetAllHeaders(string[] lines)
        {
            var list = new List<(int, Type)>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith(PIPE))
                {
                    if (line.StartsWith(WORK_ACCOUNT))
                    {
                        list.Add((i, TypeDictionary[WORK_ACCOUNT]));
                    }
                    else
                    {
                        line = line.Trim().Substring(0, 71).Trim();
                        Type matchingType = TypeDictionary[line];

                        list.Add((i, matchingType));
                    }
                }
            }
            return list;
        }
    }
}
