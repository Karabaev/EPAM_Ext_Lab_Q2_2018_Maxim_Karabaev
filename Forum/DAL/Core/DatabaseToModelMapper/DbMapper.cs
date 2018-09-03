namespace DAL.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;

    public class DbMapper
    {
        public DbMapper(Type type)
        {
            TableName = string.Format("{0}s", type.Name);
            EntityType = type;
            PropertyMapTrios = new List<MapTrio>();
            foreach (var item in EntityType.GetProperties())
            {
                PropertyMapTrios.Add(new MapTrio(item, item.Name));
            }
        }

        public string TableName { get; set; }
        public Type EntityType { get; private set; }
        public List<MapTrio> PropertyMapTrios { get; set;}



        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("Table: {0}\nType: {1}\n", TableName, EntityType);
            foreach (var item in PropertyMapTrios)
            {
                result.AppendFormat("{0}\n", item);
            }
            return result.ToString();
        }
    }
}
