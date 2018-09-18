namespace DAL.Model.Repository
{
    using System.Collections.Generic;
    using DAL.Model.Entities;

    public interface IBaseRepository<T> where T : Entity
    {
        T GetEntity(int? id);
        List<T> GetAllEntities();
        List<T> GetAllEntities(int count);
        bool SaveEntity(T entity);
        bool UpdateEntity(T entity);
        int RemoveEntity(int? id);
        int RemoveAllEntities();
        string TableName { get; }
    }
}
