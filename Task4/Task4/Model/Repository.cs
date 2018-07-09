using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Пользователи
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// Роли пользователей
        /// </summary>
        public List<Role> Roles { get; set; }
        /// <summary>
        /// Возможные права ролей
        /// </summary>
        public List<Right> Rights { get; set; }
        /// <summary>
        /// Разделы форума
        /// </summary>
        public List<Section> Sections { get; set; }
        /// <summary>
        /// Темы форума
        /// </summary>
        public List<Topic> Topics { get; set; }
        /// <summary>
        /// Сохранить все данные в базе
        /// </summary>
        public void WriteAll() { }
        /// <summary>
        /// Загрузить все данные из базы
        /// </summary>
        public void ReadAll() { }
        /// <summary>
        /// Сохранить одну записть в базе
        /// </summary>
        /// <param name="entity">Сущность, состояние которой необходимо сохранить.</param>
        public void Write(IEntity entity) { }
        /// <summary>
        /// Считать из базы состояние ранее загруженной сущности.
        /// </summary>
        /// <param name="entity">Сущность, состояние которой необходимо пересчитать</param>
        public void Read(IEntity entity) 
        {
            if (entity.ID == null)
                throw new Exceptions.NullIDException();
        }
        /// <summary>
        /// Считать из базы сущность
        /// </summary>
        /// <param name="id">ID записи, которую необходимо считать</param>
        /// <param name="entity">Ссылка, в которую будет произведено считывание из базы</param>
        public void Read(uint id, IEntity entity) { }

    }
}
