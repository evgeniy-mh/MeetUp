using MeetUp.DB;
using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MeetUp.DBRepositories
{
    class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IHasId
    {
        protected MeetUpContext Context;
        protected DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DBSet { get { return _dbSet; } }

        public EFGenericRepository(MeetUpContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            _dbSet = Context.Set<TEntity>();
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> Get(string include)
        {
            _dbSet = Context.Set<TEntity>();
            return _dbSet.Include(include).ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            _dbSet = Context.Set<TEntity>();
            return _dbSet.Where(predicate).ToList();
        }

        public TEntity FindById(int id)
        {
            _dbSet = Context.Set<TEntity>();
            return _dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            _dbSet = Context.Set<TEntity>();
            _dbSet.Add(item);
            Context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            //Context.Entry(item).State = EntityState.Modified;
            //Context.SaveChanges();

            _dbSet = Context.Set<TEntity>();
            var result = _dbSet.SingleOrDefault(e => e.Id == item.Id); ;
            if (result != null)
            {
                Context.Entry(result).CurrentValues.SetValues(item);
                Context.SaveChanges();
            }
        }
        public void Remove(TEntity item)
        {
            _dbSet = Context.Set<TEntity>();
            var i = _dbSet.Find(item.Id);
            _dbSet.Remove(i);
            Context.SaveChanges();
        }
    }
}
