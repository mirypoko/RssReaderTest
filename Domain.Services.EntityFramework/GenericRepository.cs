using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.EntityFramework
{
    public sealed class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public Task<int> Count()
        {
            return _entities.CountAsync();
        }

        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(params TEntity[] entities)
        {
            foreach(var entity in entities)
            {
                Add(entity);
            }
        }

        public async void Remove(object id)
        {
            var entity = await _entities.FindAsync(id);
            _entities.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(params TEntity[] entities)
        {
            _entities.RemoveRange(entities);
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _entities.FindAsync(keyValues);
        }

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FirstOrDefaultReversAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Reverse().FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.FirstAsync(predicate);
        }

        public Task<TEntity> FirstReversAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Reverse().FirstAsync(predicate);
        }

        public Task<List<TEntity>> ToListAsync()
        {
            return _entities.ToListAsync();
        }

        public Task<List<TEntity>> ToListAsync(int offset)
        {
            return _entities.Skip(offset).ToListAsync();
        }

        public Task<List<TEntity>> ToListAsync(int offset, int limit)
        {
            return _entities.Skip(offset).Take(limit).ToListAsync();
        }

        public Task<List<TEntity>> ToListReversAsync()
        {
            return _entities.Reverse().ToListAsync();
        }

        public Task<List<TEntity>> ToListReversAsync(int offset)
        {
            return _entities.Reverse().Skip(offset).ToListAsync();
        }

        public Task<List<TEntity>> ToListReversAsync(int offset, int limit)
        {
            return _entities.Reverse().Skip(offset).Take(limit).ToListAsync();
        }

        public Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToListAsync();
        }

        public Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, int offset)
        {
            return _entities.Where(predicate).Skip(offset).ToListAsync();
        }

        public Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, int offset, int limit)
        {
            return _entities.Where(predicate).Skip(offset).Take(limit).ToListAsync();
        }

        public Task<List<TEntity>> SearchReversAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).Reverse().ToListAsync();
        }

        public Task<List<TEntity>> SearchReversAsync(Expression<Func<TEntity, bool>> predicate, int offset)
        {
            return _entities.Where(predicate).Reverse().Skip(offset).ToListAsync();
        }

        public Task<List<TEntity>> SearchReversAsync(Expression<Func<TEntity, bool>> predicate, int offset, int limit)
        {
            return _entities.Where(predicate).Reverse().Skip(offset).Take(limit).ToListAsync();
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleAsync(predicate);
        }

        public Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefaultAsync(predicate);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.Entry(entities).State = EntityState.Modified;
        }
    }
}
