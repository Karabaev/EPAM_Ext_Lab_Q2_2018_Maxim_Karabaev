namespace DAL.Model.Repository
{
    using System.Collections.Generic;
    using DAL.Model.Entities;

    public interface IBaseRepository<T> 
    {
        T GetEntity<T>(int? id) where T : Entity;
        List<T> GetAllEntities<T>() where T : Entity;
        List<T> GetAllEntities<T>(int count) where T : Entity;
        bool SaveEntity<T>(T entity) where T : Entity;
        int RemoveEntity(int? id);
        int RemoveAllEntities();
        string TableName { get; }
    }
}
