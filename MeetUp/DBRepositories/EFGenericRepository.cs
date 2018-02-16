using MeetUp.DB;
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
        protected DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DBSet { get { return _dbSet; } }

        /*public EFGenericRepository(DbContext context)
        {
            Context = context;
            _dbSet = context.Set<TEntity>();
        }*/

        public IEnumerable<TEntity> GetAll()
        {
            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                return _dbSet.ToList();
            }
        }

        public IEnumerable<TEntity> Get(string include)
        {
            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                return _dbSet.Include(include).ToList();
            }
        }

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                return _dbSet.Where(predicate).ToList();
            }
        }

        public TEntity FindById(int id)
        {
            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                return _dbSet.Find(id);
            }
        }



        public void Create(TEntity item)
        {
            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                _dbSet.Add(item);
                Context.SaveChanges();
            }
        }
        public void Update(TEntity item)
        {
            //Context.Entry(item).State = EntityState.Modified;
            //Context.SaveChanges();

            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                var result = _dbSet.SingleOrDefault(e => e.Id == item.Id); ;
                if (result != null)
                {
                    Context.Entry(result).CurrentValues.SetValues(item);
                    Context.SaveChanges();
                }
            }
        }
        public void Remove(TEntity item)
        {
            using (Context = new MeetUpContext())
            {
                _dbSet = Context.Set<TEntity>();
                _dbSet.Remove(item);
                Context.SaveChanges();
            }
        }
    }
}
