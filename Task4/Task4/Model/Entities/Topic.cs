using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Тема на форуме
    /// </summary>
    public class Topic : Entity
    {
        /// <summary>
        /// заголовок темы
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// Сообщения в теме
        /// </summary>
        public List<Message> Messages { get; set; }
        /// <summary>
        /// Кем опубликована тема
        /// </summary>
        public User Creator { get; set; }
        /// <summary>
        /// Когда опубликована тема
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Ссылка на тему
        /// </summary>
        public string Link { get; set; }

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
