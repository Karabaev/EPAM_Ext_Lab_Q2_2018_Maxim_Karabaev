namespace DAL.Model.Repository
{
    using System.Collections.Generic;
    using DAL.Model.Entities;

    public interface IBaseRepository<T> 
    {
        T GetEntity<T>(uint id) where T : Entity;
        List<T> GetAllEntities<T>() where T : Entity, new();
        bool SaveEntity<T>(T entity) where T : Entity, new();
        int RemoveEntity(uint id);

        string TableName { get; }
    }
}
