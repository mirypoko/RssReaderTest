using Database.EntityFramework;
using Domain.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Domain.Services.EntityFramework
{
    public sealed class GenericUnitOfWork : IGenericUnitOfWork
    {
        private ConcurrentDictionary<Type, object> _repositories = new ConcurrentDictionary<Type, object>();
        private ApplicationDbContext _dbContext;

        public GenericUnitOfWork()
        {
            _dbContext = new ApplicationDbContext();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (!_repositories.ContainsKey(typeof(TEntity)))
            {
                var repository = new GenericRepository<TEntity>(_dbContext);
                _repositories.TryAdd(typeof(TEntity), repository);
            }

            return (GenericRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        public Task<int> SaveChangesAsync()
        {
            _dbContext.SaveChanges();
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repositories = null;
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
