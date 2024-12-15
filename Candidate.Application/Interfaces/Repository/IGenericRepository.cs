using Candidate.Application.Common.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidate.Application.Interfaces.Repository
{
    public interface IGenericRepository : ITransientService
    {
        #region Items Exists
        bool Exists<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class;

        #endregion

        #region Get Item Collection
        IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>>? filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "")
          where TEntity : class;
        #endregion

        #region Data Insertion
        Guid Insert<TEntity>(TEntity entity) where TEntity : class;

        bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        #endregion

        #region Data Updation
        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;

        void UpdateMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        #endregion
    }
}
