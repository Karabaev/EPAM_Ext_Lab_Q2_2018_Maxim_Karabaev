using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Хэш пароля
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Роль, определяет уровень доступа
        /// </summary>
        public Role UserRole { get; set; }
        /// <summary>
        /// Заблокирован пользователь?
        /// </summary>
        public bool IsBanned { get; set; }
    }
}
