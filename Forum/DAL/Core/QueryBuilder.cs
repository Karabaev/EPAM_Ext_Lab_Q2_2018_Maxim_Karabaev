namespace DAL.Core
{
    using System;
    using System.Text;
    using DAL.Model.Entities;
    using System.Collections.Generic;
    using System.Reflection;

    public static class QueryBuilder
    {
        private static string selectCommandWithConditionTemplate = "SELECT {0} FROM {1} WHERE {2}";
        private static string deleteEntityCommandTemplate = "DELETE FROM {0} WHERE {1}";
        private static string insertRecordCommadTemplate = "INSERT INTO {0} ({1}) VALUES ({2})";
        private const string IDPropName = "ID";
        private const string ErrorString = "Error";
        /// <summary>
        /// Собирает и возвращает строку запроса удаления записи из таблицы tableName с условием.
        /// </summary>
        /// <param name="tableName">Имя таблицы, откуда удалять запись(и).</param>
        /// <param name="conditionFormat">Формат условия.</param>
        /// /// <param name="conditionArgs">Параметры для условия.</param>
        /// <returns>Строка запроса.</returns>
        public static string GetDeleteRecordCommand(string tableName, string conditionFormat, params string[] conditionArgs)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(deleteEntityCommandTemplate, tableName, string.Format(conditionFormat, conditionArgs));
            return result.ToString();
        }

        /// <summary>
        /// Собирает и возвращает строку запроса добавления записи в таблицу tableName.
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        /// <param name="fields">Список полей через запятую.</param>
        /// <param name="values">Список значений через запятую.</param>
        /// <returns>Строка запроса.</returns>
        public static string GetAddRecordCommand(string tableName, string fields, string values)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(insertRecordCommadTemplate, tableName, fields, values);
            return result.ToString();
        }

        public static string GetSelectRecordCommand(string tableName, string fields, string conditionFormat, params string[] conditionArgs)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(selectCommandWithConditionTemplate, fields, tableName, string.Format(conditionFormat, conditionArgs));
            return result.ToString();
        }

        /// <summary>
        /// Собирает имена свойств и значения объекта entity в строки, разделенные запятыми для дальнейшего использования в запросах.
        /// </summary>
        /// <typeparam name="T">Тип сущности, производный от DAL.Model.Entities.Entity.</typeparam>
        /// <param name="entity">Ссылка на объект.</param>
        /// <param name="tableFields">Строка с названиями свойств. Названия разделяются запятыми.</param>
        /// <param name="fieldValues"> Строка со значениями свойств. Значения разделяются запятыми. Если тип свойства является 
        /// сущностью БД, то берется ее ID. </param>
        public static void GetEntityPropertiesAndValues<T>(T entity, out string tableFields, out string fieldValues) where T : Entity
        {
            tableFields = string.Empty;
            fieldValues = string.Empty;
            Dictionary<string, object> propertiesAndValues = new Dictionary<string, object>();
            PropertyInfo IDProperty = typeof(Entity).GetProperty(IDPropName);

            foreach (var item in entity.GetType().GetProperties())
            {
                propertiesAndValues.Add(item.Name, item.PropertyType.IsSubclassOf(typeof(Entity)) ? IDProperty.GetValue(entity) :
                                                                                                    item.GetValue(entity));
            }

            StringBuilder fields = new StringBuilder();
            StringBuilder values = new StringBuilder();
            string delimiter = ",";
            int index = 1;

            foreach (var item in propertiesAndValues)
            {
                fields.Append(item.Key);
                values.Append(item.Value);

                if (index < propertiesAndValues.Count)
                {
                    fields.Append(delimiter);
                    values.Append(delimiter);
                }

                index++;
            }

            tableFields = fields.ToString();
            fieldValues = values.ToString();
        }

        /// <summary>
        /// Собирает свойства сущности БД entity в строку. Имена свойств в строке разделяются запятыми.
        /// </summary>
        /// <typeparam name="T">Тип сущности, производный от DAL.Model.Entities.Entity.</typeparam>
        /// <param name="entity">Ссылка на объект сущности, сам объект в памяти не важен.</param>
        /// <returns>Строка с полями таблицы сущности. Поля разделяются запятыми.</returns>
        public static string GetEntityProperties(Type entityType)
        {
            if(!entityType.IsSubclassOf(typeof(Entity)))
            {
                return ErrorString;
            }

            StringBuilder fields = new StringBuilder();
            string delimiter = ",";
            int index = 1;
            PropertyInfo[] properties = entityType.GetProperties();

            foreach (var item in properties)
            {
                fields.Append(item.Name);

                if (index < properties.Length)
                {
                    fields.Append(delimiter);
                }

                index++;
            }

            return fields.ToString();
        }
    }
}
