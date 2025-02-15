using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Smart.DAL.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[]? includes);

        Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<TEntity> RecoverAsync(TEntity entity);

        Task<TEntity> RemoveAsync(TEntity entity);
    }
}
