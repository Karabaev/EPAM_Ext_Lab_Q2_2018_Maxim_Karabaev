namespace DAL.Core
{
    using System;
    using System.Reflection;

    public class MapTrio
    {
        public MapTrio(PropertyInfo prop, string tblFieldName, object val = null)
        {
            this.Property = prop;
            this.TableFieldName = tblFieldName;
            this.Value = val;
        }

        /// <summary>
        /// Хранит тип и имя свойства.
        /// </summary>
        public PropertyInfo Property { get; set; }

        /// <summary>
        /// Хранит название столбца из таблицы.
        /// </summary>
        public string TableFieldName { get; set; }

        /// <summary>
        /// Хранит значение.
        /// </summary>
        public object Value { get; set; }

        public override string ToString()
        {
            return string.Format("Property: {0}/ TableFieldName: {1}/ Value: {2}", Property, TableFieldName, Value);
        }
    }
}
