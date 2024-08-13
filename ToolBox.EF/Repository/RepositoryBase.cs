using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ToolBox.EF.Repository
{
    public abstract class RepositoryBase(DbContext context)
    {
        protected readonly DbContext _context = context;
    }

    public abstract class RepositoryBase<TEntity>(DbContext context) : RepositoryBase(context), IRepository<TEntity> 
        where TEntity : class
    {
        protected DbSet<TEntity> Entities => _context.Set<TEntity>();

        public virtual IEnumerable<TEntity> Find()
        {
            return Entities;
        }

        public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        {
            return Entities.Where(predicate);
        }

        public virtual int Count()
        {
            return Entities.Count();
        }

        public virtual int Count(Func<TEntity, bool> predicate)
        {
            return Entities.Where(predicate).Count();
        }

        public virtual TEntity? FindOne(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual TEntity? FindOne(Func<TEntity, bool> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }

        public bool Any(Func<TEntity, bool> predicate)
        {
            return Entities.Any(predicate);
        }

        public virtual TEntity Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Add(entity);
            entry.State = EntityState.Added;
            _context.SaveChanges();
            return entry.Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Update(entity);
            entry.State = EntityState.Modified;
            _context.SaveChanges();
            return entry.Entity;
        }

        public virtual TEntity Remove(TEntity entity)
        {
            EntityEntry<TEntity> entry = _context.Remove(entity);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
            return entry.Entity;
        }
    }
}
