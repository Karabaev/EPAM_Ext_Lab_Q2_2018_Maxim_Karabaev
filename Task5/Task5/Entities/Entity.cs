using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    /// <summary>
    /// Базовый класс для всех записей базы данных
    /// </summary>
    public abstract class Entity
    {
        public uint? ID { get; set; }

        public Entity() {ID = null;}

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
        public abstract void Reinitialization(Entity other);
    }
}
