namespace DAL.Core
{
    using System;
    using System.Text;
    using DAL.Model.Entities;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Data.Common;
    using System.Data;

    public static class QueryBuilder
    {
        private static string selectCommandWithConditionTemplate = "SELECT {0} FROM {1} WHERE {2}";
        private static string selectCommandTemplate = "SELECT {0} FROM {1}";
        private static string deleteEntityCommandTemplate = "DELETE FROM {0} WHERE {1}";
        private static string deleteAllEntitiesCommandTemplate = "DELETE FROM {0}";
        private static string insertRecordCommadWithFieldsTemplate = "INSERT INTO {0} ({1}) VALUES ({2})";
        private static string insertRecordCommadTemplate = "INSERT INTO {0} VALUES ({1})";
        private static string UpdateRecordCommandWithConditionTemplate = "UPDATE {0} SET {1} WHERE {2}";
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
        /// Собирает и возвращает строку запроса удаления всех записей из таблицы tableName.
        /// </summary>
        /// <param name="tableName">Имя таблицы, откуда удалять записи.</param>
        /// <returns>Строка запроса.</returns>
        public static string GetDeleteAllRecordsCommand(string tableName)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(deleteAllEntitiesCommandTemplate, tableName);
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
            result.AppendFormat(insertRecordCommadWithFieldsTemplate, tableName, fields, values);
            return result.ToString();
        }

        public static string GetAddRecordCommand(string tableName, string values)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(insertRecordCommadTemplate, tableName, values);
            return result.ToString();
        }

        /// <summary>
        /// Собирает и возвращает строку запроса выборки из таблицы tableName с условием по формату conditionFormat.
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        /// <param name="fields">Поля выборки.</param>
        /// <param name="conditionFormat">Формат условия.</param>
        /// <param name="conditionArgs">Аргументы условия.</param>
        /// <returns>Строка запроса выборки с условием.</returns>
        public static string GetSelectRecordCommand(string tableName, string fields, string conditionFormat, params string[] conditionArgs)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(selectCommandWithConditionTemplate, fields, tableName, string.Format(conditionFormat, conditionArgs));
            return result.ToString();
        }

        /// <summary>
        /// Собирает и возвращает строку запроса выборки из таблицы tableName без условия.
        /// </summary>
        /// <param name="tableName">Имя таблицы.</param>
        /// <param name="fields">Поля выборки.</param>
        /// <returns>Строка запроса выборки без условия.</returns>
        public static string GetSelectRecordCommand(string tableName, string fields)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(selectCommandTemplate, fields, tableName);
            return result.ToString();
        }

        /// <summary>
        /// Собирает и возвращает строку запроса обновления записи из таблицы tableName.
        /// </summary>
        /// <param name="tableName">Название таблицы.</param>
        /// <param name="condition">Условия.</param>
        /// <param name="fieldsAndValues">Поля и их новые значения в формате "field1 = val1, field2 = 'val2',..."</param>
        /// <returns>Строка запроса обновления записи.</returns>
        public static string GetUpdateRecordCommand(string tableName, string condition, string fieldsAndValues)
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat(UpdateRecordCommandWithConditionTemplate, tableName, fieldsAndValues, condition);
            return result.ToString();
        }

        /// <summary>
        /// Вызывает хранимую процедуру procName и возвращает DbDataReader.
        /// </summary>
        /// <param name="connection">Используемое соединение к базе.</param>
        /// <param name="procName">Имя вызываемой процедуры.</param>
        /// <param name="parameters">Список параметров процедуры.</param>
        /// <returns>DbDataReader с результатом выполнения хранимой процедуры.</returns>
        public static DbDataReader GetStoredProcedureDataReader(DbConnection connection, string procName, params DbParameter[] parameters)
        {
            try
            {
                DbCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procName;

                foreach (var item in parameters)
                {
                    command.Parameters.Add(item);
                }

                return command.ExecuteReader();
            }
            catch(DbException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }


        /// <summary>
        /// Собирает имена свойств и значения объекта entity в строки, разделенные запятыми для дальнейшего использования в запросах.
        /// </summary>
        /// <typeparam name="T">Тип сущности, производный от DAL.Model.Entities.Entity.</typeparam>
        /// <param name="entity">Ссылка на объект.</param>
        /// <param name="tableFields">Строка с названиями свойств. Названия разделяются запятыми.</param>
        /// <param name="fieldValues"> Строка со значениями свойств. Значения разделяются запятыми. Если тип свойства является 
        /// сущностью БД, то берется ее ID. </param>
        /// <param name="getID">true если необходимо поле с ID добавить в строку запроса, иначе false.</param>
        //public static void GetEntityPropertiesAndValues<T>(T entity, out string tableFields, out string fieldValues, bool getID = false) where T : Entity
        //{
        //    tableFields = string.Empty;
        //    fieldValues = string.Empty;
        //    Dictionary<string, object> propertiesAndValues = new Dictionary<string, object>();
        //    PropertyInfo IDProperty = typeof(Entity).GetProperty(IDPropName);
        //    //TableFieldNameAttribute tblFldNameAttr = null;
        //    object value = null;
        //    foreach (var item in entity.GetType().GetProperties())
        //    {
        //     //   tblFldNameAttr = item.GetCustomAttribute<TableFieldNameAttribute>();

        //       // if(tblFldNameAttr == null || (!getID && item.Name == IDProperty.Name))// если аттрибут имени столбца не найден
        //        {
        //          //  continue;
        //        }

        //        value = null;

        //        // если тип свойства является сущностью бд, то взять ее ID иначе взять ее значение.
        //        value = item.PropertyType.IsSubclassOf(typeof(Entity)) ? (item.GetValue(entity) as Entity).ID : value = item.GetValue(entity);


        //        if (item.PropertyType == typeof(string) || item.PropertyType == typeof(bool) || item.PropertyType == typeof(FormattedDate))
        //        {
        //            value = string.Format("'{0}'", value);
        //        }

        //     //   propertiesAndValues.Add(tblFldNameAttr.FieldName, value);
        //    }

        //    StringBuilder fields = new StringBuilder();
        //    StringBuilder values = new StringBuilder();
        //    string delimiter = ",";
        //    int index = 1;

        //    foreach (var item in propertiesAndValues)
        //    {
        //        fields.Append(item.Key);
        //        values.Append(item.Value);

        //        if (index < propertiesAndValues.Count)
        //        {
        //            fields.Append(delimiter);
        //            values.Append(delimiter);
        //        }

        //        index++;
        //    }

        //    tableFields = fields.ToString();
        //    fieldValues = values.ToString();
        //}

        public static string GetValueList(params object[] args)
        {
            StringBuilder result = new StringBuilder();
            string delimiter = ",";
            int index = 1;

            foreach (var item in args)
            {
                if ((item is string) || (item is bool) || (item is FormattedDate))
                {
                    result.AppendFormat("'{0}'", item);
                }
                else
                {
                    result.Append(item);
                }

                if(index < args.Length)
                {
                    result.Append(delimiter);
                }

                index++;
            }

            return result.ToString();
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
