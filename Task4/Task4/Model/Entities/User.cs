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
