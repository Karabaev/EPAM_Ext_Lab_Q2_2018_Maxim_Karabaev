using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Интерфейс для всех записей базы данных
    /// </summary>
    public interface IEntity
    {
        uint? ID { get; set; }

        void Write();
        void Read();
    }
}
