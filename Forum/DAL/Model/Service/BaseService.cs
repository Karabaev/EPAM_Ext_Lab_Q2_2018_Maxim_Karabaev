﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model.Entities;

namespace DAL.Model.Service
{
    public class BaseService<T> : IBaseService<T>
    {
        public virtual List<T> GetAllEntities<T>() where T : Entity, new()
        {
            throw new NotImplementedException();
        }

        public virtual T GetEntity<T>(uint id) where T : Entity, new()
        {
            throw new NotImplementedException();
        }

        public virtual bool RemoveEntity(uint id)
        {
            throw new NotImplementedException();
        }

        public virtual bool SaveEntity<T>(T entity) where T : Entity
        {
            throw new NotImplementedException();
        }
    }
}