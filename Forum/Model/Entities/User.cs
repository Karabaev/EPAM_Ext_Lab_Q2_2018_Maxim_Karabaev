namespace DAL.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
        public string PublicName { get; set; }
        /// <summary>
        /// Роль, определяет уровень доступа
        /// </summary>
        public Role UserRole { get; set; } = new Role();
        /// <summary>
        /// Заблокирован пользователь?
        /// </summary>
        public bool IsBanned { get; set; }
        public DateTime RegistrationDate { get; set; }
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
        public override string ToString()
        {
            return string.Format("ID: {0}, Name: {1}", ID, PublicName);
        }
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
