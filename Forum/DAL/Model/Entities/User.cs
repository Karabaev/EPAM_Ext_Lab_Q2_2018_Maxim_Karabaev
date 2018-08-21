namespace DAL.Model.Entities
{
    using System;

    /// <summary>
    /// Пользователь.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Логин.
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Хэш пароля.
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Публичное имя пользователя.
        /// </summary>
        public string PublicName { get; set; }
        /// <summary>
        /// Роль, определяет уровень доступа.
        /// </summary>
        public Role UserRole { get; set; }
        /// <summary>
        /// Заблокирован пользователь?
        /// </summary>
        public bool IsBanned { get; set; }
        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Констуктор инициализации свойств объекта, включая ID.
        /// </summary>
        /// <param name="id">ID пользователя.</param>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="pass">Хэш пароля пользователя.</param>
        /// <param name="publicName">ПУбличное имя пользователя.</param>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="isBanned">Статус блокировки пользователя.</param>
        /// <param name="regDate">Дата регистрации пользователя.</param>
        public User(uint? id, string login, string pass, string publicName, Role role, bool isBanned, DateTime regDate)
        {
            base.ID = id;
            this.Login = login;
            this.PasswordHash = pass;
            this.PublicName = publicName;
            this.UserRole = role;
            this.IsBanned = isBanned;
            this.RegistrationDate = regDate;
        }

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Другой объект.</param>
        /// <returns>true если объекты равны, иначе false.</returns>
        public override bool Equals(object obj)
        {
            User other = obj as User;
            if (other == null) return false;
            return (Login == other.Login) && 
                    (PasswordHash == other.PasswordHash) && 
                    UserRole.Equals(other.UserRole) && 
                    (IsBanned == other.IsBanned) &&
                    (PublicName == other.PublicName);
        }

        /// <summary>
        /// Возвращает хэш код объекта.
        /// </summary>
        /// <returns>Хэш код объекта.</returns>
        public override int GetHashCode()
        {
            int result = 0;
            try
            {
                result += ID.GetHashCode() + Login.GetHashCode() + PasswordHash.GetHashCode() + PublicName.GetHashCode() + UserRole.GetHashCode() + IsBanned.GetHashCode();
            }
            catch(StackOverflowException ex)
            {
                Console.WriteLine(ex.Message);
            }         
            return result;
        }

        /// <summary>
        /// Возвращает строку для отображения сущности на экране.
        /// </summary>
        /// <returns>Строка, ID и PublicName.</returns>
        public override string ToString()
        {
            return string.Format("ID: {0}, Name: {1}", ID, PublicName);
        }

        /// <summary>
        /// Переинициализация объекта полями другого объекта (кроме ID).
        /// </summary>
        /// <param name="other">Другой объект сущности.</param>
        public override void Reinitialization(Entity other)
        {
            User newUser = other as User;
            if (newUser == null)
                return;
            Login = newUser.Login;
            PasswordHash = newUser.PasswordHash;
            PublicName = newUser.PublicName;
            UserRole = newUser.UserRole;
            IsBanned = newUser.IsBanned;
        }
    }
}
