namespace DAL.Model.Entities
{
    using System;
    using DAL.Core;

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
        public FormattedDate RegistrationDate { get; set; }
        /// <summary>
        /// Почта.
        /// </summary>
        public string Email { get; set; }

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
        /// <param name="email">Электронная почта пользователя.</param>
        public User(int id, string login, string pass, string publicName, Role role, bool isBanned, FormattedDate regDate, string email)
        {
            base.ID = id;
            this.Login = login;
            this.PasswordHash = pass;
            this.PublicName = publicName;
            this.UserRole = role;
            this.IsBanned = isBanned;
            this.RegistrationDate = regDate;
            this.Email = email;
        }

        /// <summary>
        /// Сравнивает объект с другим.
        /// </summary>
        /// <param name="obj">Другой объект.</param>
        /// <returns>true если объекты равны, иначе false.</returns>
        public override bool Equals(object obj) // todo pn убрать из проверки LikeAs и сравнить поля.
        {
            User other = obj as User;

            if (other == null)
            {
                return false;
            }

            return (base.ID == other.ID) && this.LikeAs(other);
        }

        /// <summary>
        /// Сравнивает сущность с другой. Сравниваются все свойства, кроме ID.
        /// </summary>
        /// <param name="entity">Другая сущность.</param>
        /// <returns>true если объекты подобны, иначе false.</returns>
        public override bool LikeAs(Entity entity)
        {
            User other = entity as User;

            if(other == null)
            {
                return false;
            }

            return  (this.Login == other.Login) &&
                    (this.PasswordHash == other.PasswordHash) &&
                    this.UserRole.LikeAs(other.UserRole) &&
                    (this.IsBanned == other.IsBanned) &&
                    (this.PublicName == other.PublicName) &&
                    (this.Email == other.Email);
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
            return string.Format("ID: {0}, Login: {1}, PasswordHash: {2}, PublicName: {3}, UserRole {4}, IsBanned: {5}, RegDate: {6}" +
                "Email: {7}", ID, this.Login, this.PasswordHash, this.PublicName, this.UserRole, this.IsBanned, this.RegistrationDate,
                this.Email);
        }

        /// <summary>
        /// Переинициализация объекта полями другого объекта (кроме ID).
        /// </summary>
        /// <param name="other">Другой объект сущности.</param>
        public override void Reinitialization(Entity other)
        {
            if (!(other is User newUser))
            {
                return;
            }

            this.Login = newUser.Login;
            this.PasswordHash = newUser.PasswordHash;
            this.PublicName = newUser.PublicName;
            this.UserRole = newUser.UserRole;
            this.IsBanned = newUser.IsBanned;
            this.RegistrationDate = newUser.RegistrationDate;
            this.Email = newUser.Email;
        }
    }
}
