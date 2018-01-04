using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IGenericReadRepository<TEntity> where TEntity : class
    {
        Task<int> Count();
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultReversAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstReversAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> ToListAsync();
        Task<List<TEntity>> ToListAsync(int offset);
        Task<List<TEntity>> ToListAsync(int offset, int limit);
        Task<List<TEntity>> ToListReversAsync();
        Task<List<TEntity>> ToListReversAsync(int offset);
        Task<List<TEntity>> ToListReversAsync(int offset, int limit);
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, int offset);
        Task<List<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate, int offset, int limit);
        Task<List<TEntity>> SearchReversAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> SearchReversAsync(Expression<Func<TEntity, bool>> predicate, int offset);
        Task<List<TEntity>> SearchReversAsync(Expression<Func<TEntity, bool>> predicate, int offset, int limit);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
