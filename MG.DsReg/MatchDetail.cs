using System;
using System.Reflection;

namespace MG.DsReg
{
    public class MatchDetail
    {
        private static Type[] BOOLS => new Type[2] { typeof(bool), typeof(bool?) };
        private static Type[] INTS => new Type[2] { typeof(int), typeof(int?) };
        private static Type[] GUIDS => new Type[2] { typeof(Guid), typeof(Guid?) };
        //private static Type[] GUID

        public bool IsNullableType { get; }
        public string Key { get; }
        public object Value { get; }
        public PropertyInfo Property { get; }
        public Type PropertyType { get; }

        public MatchDetail(string key, string value, PropertyInfo pri)
        {
            this.Key = key.Trim();
            this.Property = pri;
            //this.IsNullableType = Nullable.GetUnderlyingType(pri.PropertyType) != null;
            Type check = Nullable.GetUnderlyingType(pri.PropertyType);
            if (check != null)
                this.PropertyType = check;

            else
                this.PropertyType = pri.PropertyType;

            this.Value = this.ConvertTo(value.Trim());
        }

        private object ConvertTo(string s)
        {
            object obj = null;
            if (!string.IsNullOrWhiteSpace(s))
            {
                if (this.PropertyType.Equals(typeof(string)))
                    return s;

                else if (this.PropertyType.Equals(typeof(bool)))
                {
                    if (s.IndexOf("YES", StringComparison.OrdinalIgnoreCase) >= 0)
                        return true;

                    else if (bool.TryParse(s, out bool outBool))
                        return outBool;

                    else
                        return false;
                }

                else if (this.PropertyType.Equals(typeof(int)) && int.TryParse(s, out int outInt))
                    return outInt;

                else if (this.PropertyType.Equals(typeof(Guid)) && Guid.TryParse(s, out Guid outGuid))
                    return outGuid;

                else if (this.PropertyType.Equals(typeof(DateTime)) && DateTime.TryParse(s, out DateTime outDate))
                    return outDate;

                else if (this.PropertyType.Equals(typeof(Version)) && Version.TryParse(s, out Version outVers))
                    return outVers;
            }

            return obj;
        }

        public void SetValue(object newObject)
        {
            var setAcc = this.Property.GetSetMethod(true);
            setAcc.Invoke(newObject, new object[1] { this.Value });
        }
    }
}
