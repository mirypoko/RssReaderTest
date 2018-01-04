using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface IGenericRepository<TEntity> : IGenericReadRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(params TEntity[] entities);
        void Remove(object id);
        void Remove(TEntity entity);
        void RemoveRange(params TEntity[] entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
    }
}
