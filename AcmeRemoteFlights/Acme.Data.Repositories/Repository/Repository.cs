using System.Linq;
using System.Data.Entity;
using Acme.Data.Repositories.Entities;
using System.Collections.Generic;

namespace Acme.Data.Repositories.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _Entity;
        private readonly AcmeDBEntities _AcmeEntities;

        public Repository(AcmeDBEntities entities)
        {
            _Entity = entities.Set<TEntity>();
            _AcmeEntities = entities;
        }

        public void Add(TEntity entity)
        {
            _Entity.Add(entity);
        }

        IList<TEntity> IRepository<TEntity>.GetAll()
        {
            return _Entity.AsQueryable().ToList();
        }

        public void Delete(TEntity entity)
        {
            _Entity.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _Entity.Attach(entity);
            _AcmeEntities.Entry(entity).State = EntityState.Modified;
        }

        public TEntity GetByKey(int key)
        {
            return _Entity.Find(key);
        }
    }
}
