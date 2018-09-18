using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model.Entities;

namespace DAL.Model.Service
{
    public class BaseService<T> : IBaseService<T> where T : Entity
    {
        public virtual List<T> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public virtual T GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public virtual int RemoveEntity(int id)
        {
            throw new NotImplementedException();
        }

        public virtual bool SaveEntity(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
