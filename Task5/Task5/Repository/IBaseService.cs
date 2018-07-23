using System;
using System.Collections.Generic;
using System.Text;

namespace Task5.Repository
{
    interface IBaseService
    {
        T GetEntity<T>(uint id) where T : Entity, new();
        List<T> GetAllEntities<T>() where T : Entity, new();
        bool SaveEntity<T>(T entity) where T: Entity;
        bool Remove(uint id);
    }
}
