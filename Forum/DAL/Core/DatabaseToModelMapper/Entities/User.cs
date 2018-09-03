namespace DAL.Core.DatabaseToModelMapper.Entities
{
    using System;
    using System.Reflection;
    using System.Linq;
    using DAL.Core;
    public class User
    {
        static User()
        {
            Mapper = new DbMapper(typeof(User));
        }

        public User() { }
        public User(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public FormattedDate RegDate { get; set; }

        public static DbMapper Mapper;


        public static void ChangeTableName(string newTableName)
        {
            Mapper.TableName = newTableName;
        }

        public static void Remap(string propName, string newTableFieldName)
        {
            MapTrio mapTrio = Mapper.PropertyMapTrios.Where(pmt => pmt.Property.Name == propName).FirstOrDefault();
            try
            {
                mapTrio.TableFieldName = newTableFieldName;
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }  
        }
    }
}
