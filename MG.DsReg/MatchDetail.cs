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

        //public void SetValue(object newObject)
        //{

        //    if (this.Property.PropertyType.Equals(typeof(bool?)))
        //    {
        //        if (this.Value.Contains("YES"))
        //            this.Property.SetValue(newObject, true);

        //        else if (this.Value.Contains("NO"))
        //            this.Property.SetValue(newObject, false);
        //    }
        //    if (this.Property.PropertyType.Equals(typeof(int?)) && int.TryParse(this.Value, out int number))
        //    {
        //        this.Property.SetValue(newObject, number);
        //    }
        //    else if (this.Property.PropertyType.Equals(typeof(Guid?)) && Guid.TryParse(this.Value, out Guid guid))
        //    {
        //        this.Property.SetValue(newObject, guid);
        //    }
        //    else if (this.Property.PropertyType.Equals(typeof(DateTime?)) && DateTime.TryParse(this.Value, out DateTime dt))
        //    {
        //        this.Property.SetValue(newObject, dt);
        //    }
        //    else if (this.Property.PropertyType.Equals(typeof(Version)) && Version.TryParse(this.Value, out Version vers))
        //    {
        //        this.Property.SetValue(newObject, vers);
        //    }
        //    else if (this.Property.PropertyType.Equals(typeof(string)) && !string.IsNullOrEmpty(this.Value))
        //    {
        //        this.Property.SetValue(newObject, this.Value);
        //    }
        //}

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
