using MeetUp.DBEntityModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUp.DBRepositories
{
    class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IHasId
    {
        protected DbContext Context;
        protected DbSet<TEntity> DBSet;

        public EFGenericRepository(DbContext context)
        {
            Context = context;
            DBSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DBSet.ToList();
        }

        public IEnumerable<TEntity> Get(string include)
        {
            return DBSet.Include(include).ToList();
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return DBSet.Where(predicate).ToList();
        }

        public TEntity FindById(int id)
        {
            return DBSet.Find(id);
        }

        public void Create(TEntity item)
        {
            DBSet.Add(item);
            Context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            //Context.Entry(item).State = EntityState.Modified;
            //Context.SaveChanges();

            var result = DBSet.SingleOrDefault(e => e.Id == item.Id); ;
            if (result != null)
            {
                Context.Entry(result).CurrentValues.SetValues(item);
                Context.SaveChanges();
            }
        }
        public void Remove(TEntity item)
        {

            DBSet.Remove(item);
            Context.SaveChanges();
        }
    }
}
