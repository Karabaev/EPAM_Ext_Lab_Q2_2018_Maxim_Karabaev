using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Базовый класс для всех записей базы данных
    /// </summary>
    public abstract class Entity : IEntity
    {
        public uint? ID { get; set; }

        public Entity() {ID = null;}

        public abstract void Write();
        public abstract void Read();
    }
}
