namespace DAL.Model.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL.Model.Entities;

    public interface IBaseService<T>
    {
        T GetEntity<T>(uint id) where T : Entity, new();
        List<T> GetAllEntities<T>() where T : Entity, new();
        bool SaveEntity<T>(T entity) where T : Entity;
        bool RemoveEntity(uint id);
    }
}
