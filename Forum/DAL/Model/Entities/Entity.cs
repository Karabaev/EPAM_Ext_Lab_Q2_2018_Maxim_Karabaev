﻿namespace DAL.Model.Entities
{
    /// <summary>
    /// Базовый класс для всех записей базы данных
    /// </summary>
    public abstract class Entity
    {
        public uint? ID { get; protected set; }

        public abstract override bool Equals(object obj);
        public abstract override int GetHashCode();
        public abstract void Reinitialization(Entity other); // надо будет сделать возврщаемое значение bool, чтоб отслеживать ошибки в методе
    }
}
