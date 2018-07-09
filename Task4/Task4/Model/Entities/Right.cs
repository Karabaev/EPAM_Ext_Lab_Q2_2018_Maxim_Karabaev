using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Привилегии (редактирование записей, пользователей, блокировка пользователей, доступ к разделам форума)
    /// </summary>
    public class Right : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Записать состояние сущности в базу
        /// </summary>
        public override void Write()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Считать состояние сущности из базы
        /// </summary>
        public override void Read()
        {
            throw new NotImplementedException();
        }
    }
}
