using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Repository
{ 
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    abstract public class Repository : IBaseService
    {
        public abstract T GetEntity<T>(uint id) where T : Entity, new();
        public abstract List<T> GetAllEntities<T>() where T : Entity, new();
        public abstract bool SaveEntity<T>(T entity) where T : Entity;
        public abstract bool Remove(uint id);

       // protected Database DB { get; set; } = new Database();
    }
}
