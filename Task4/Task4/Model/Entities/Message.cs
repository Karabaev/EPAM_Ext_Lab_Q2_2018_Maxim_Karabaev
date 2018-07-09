using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Сообщение в теме
    /// </summary>
    public class Message : Entity
    {
        /// <summary>
        /// Кто создал 
        /// </summary>
        public User Creator { get; set; }
        /// <summary>
        /// Когда создана
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string Content { get; set; }
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
