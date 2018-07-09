using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Уровень доступа (админ, модератор, пользователь, гость)
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список прав
        /// </summary>
        public List<Right> Rights { get; set; }
        /// <summary>
        /// Список пользователей, относящихся к роли
        /// </summary>
        public List<User> Users { get; set; }

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
