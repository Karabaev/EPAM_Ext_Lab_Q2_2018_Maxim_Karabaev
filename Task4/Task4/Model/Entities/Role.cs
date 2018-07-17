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
    }
}
