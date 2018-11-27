using System;
using System.Collections;
using System.Collections.Generic;

namespace DAB3_2del1.RepoAndUnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Trying to fulfill all CRUD operation (Create(Create), Read(GetAll/GetByID/Find), Update(Find), Delete(Delete)
        // According to Mosh Hamedani it isn't nescessary with an Update- or Save-method.


        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

        void Delete(TEntity entity);
        TEntity GetByID(int id);
        void Create(TEntity entity);


    }
}