using Candidate.Application.Interfaces.Repository;
using Candidate.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Candidate.Infrastructure.Implementation.Repository
{
    public class GenericRepository(ApplicationDbContext dbContext) : IGenericRepository
    {
        public bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {

            ArgumentNullException.ThrowIfNull(entityList);

            dbContext.Set<TEntity>().AddRange(entityList);

            dbContext.SaveChanges();

            const bool flag = true;

            return flag;
        }

        public bool Exists<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class
        {
            return filter != null && dbContext.Set<TEntity>().Any(filter);
        }

        public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "") where TEntity : class
        {
            var query = dbContext.Set<TEntity>().AsNoTracking();

            if (filter != null) query = query.Where(filter);

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : ApplyDefaultOrdering(query);
        }

        public Guid Insert<TEntity>(TEntity entity) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);

            dbContext.Set<TEntity>().Add(entity);

            dbContext.SaveChanges();

            var entityType = typeof(TEntity);

            var idProperty = entityType.GetProperty("Id");

            if (idProperty == null || idProperty.PropertyType != typeof(Guid))
            {
                return Guid.Empty;
            }

            return (Guid)idProperty.GetValue(entity)!;
        }

        public void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entityToUpdate);

            dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            dbContext.SaveChanges();
        }

        public void UpdateMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entityList);

            dbContext.Entry(entityList).State = EntityState.Modified;

            dbContext.SaveChanges();
        }

        private IOrderedQueryable<TEntity> ApplyDefaultOrdering<TEntity>(IQueryable<TEntity> query) where TEntity : class
        {
            var entityType = typeof(TEntity);

            var createdAtProperty = entityType.GetProperty("CreatedAt");

            if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
            {
                return query.OrderByDescending(e => EF.Property<DateTime>(e, "CreatedAt"));
            }

            var registeredDateProperty = entityType.GetProperty("RegisteredDate");

            if (registeredDateProperty != null && registeredDateProperty.PropertyType == typeof(DateTime))
            {
                return query.OrderByDescending(e => EF.Property<DateTime>(e, "RegisteredDate"));
            }

            var firstProperty = entityType.GetProperties().FirstOrDefault();

            if (firstProperty != null)
            {
                return query.OrderByDescending(e => EF.Property<object>(e, firstProperty.Name));
            }

            return query.OrderByDescending(e => e);
        }
    }
}
