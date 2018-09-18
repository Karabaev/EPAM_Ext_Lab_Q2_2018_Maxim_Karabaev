namespace DAL.Model.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL.Model.Entities;

    public interface IBaseService<T> where T : Entity
    {
        T GetEntity(int id);
        List<T> GetAllEntities();
        bool SaveEntity(T entity);
        int RemoveEntity(int id);
    }
}
