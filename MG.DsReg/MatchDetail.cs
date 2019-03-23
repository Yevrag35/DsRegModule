using System;
using System.Reflection;

namespace MG.DsReg
{
    public class MatchDetail
    {
        public string Key { get; }
        public string Value { get; }
        public PropertyInfo Property { get; }

        internal MatchDetail(string key, string value, PropertyInfo pri)
        {
            this.Key = key.Trim();
            this.Value = value.Trim();
            this.Property = pri;
        }

        public void SetValue(object newObject)
        {
            if (this.Property.PropertyType.Equals(typeof(bool?)))
            {
                if (this.Value.Contains("YES"))
                    this.Property.SetValue(newObject, true);

                else if (this.Value.Contains("NO"))
                    this.Property.SetValue(newObject, false);
            }
            if (this.Property.PropertyType.Equals(typeof(int?)) && int.TryParse(this.Value, out int number))
            {
                this.Property.SetValue(newObject, number);
            }
            else if (this.Property.PropertyType.Equals(typeof(Guid?)) && Guid.TryParse(this.Value, out Guid guid))
            {
                this.Property.SetValue(newObject, guid);
            }
            else if (this.Property.PropertyType.Equals(typeof(DateTime?)) && DateTime.TryParse(this.Value, out DateTime dt))
            {
                this.Property.SetValue(newObject, dt);
            }
            else if (this.Property.PropertyType.Equals(typeof(Version)) && Version.TryParse(this.Value, out Version vers))
            {
                this.Property.SetValue(newObject, vers);
            }
            else if (this.Property.PropertyType.Equals(typeof(string)) && !string.IsNullOrEmpty(this.Value))
            {
                this.Property.SetValue(newObject, this.Value);
            }
        }
    }
}
