namespace SampleSoC.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Domain.Core.Models.Base;

    /// <summary>
    /// The class for a repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : IdentityModelBase
    {
        private readonly DbContext _context;
        private readonly IDbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        public TEntity GetById(int id)
        {
            return GetById<object>(id, null);
        }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="id">The entity id.</param>
        /// <param name="includeProperty">The include property.</param>
        /// <returns></returns>
        public TEntity GetById<TProperty>(int id, Expression<Func<TEntity, TProperty>> includeProperty)
        {
            return GetQuery(e => e.Id.Equals(id), includeProperty).SingleOrDefault();
        }

        public IEnumerable<TEntity> Fetch()
        {
            return GetQuery(null).ToList();
        }

        /// <summary>
        /// Fetches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> specification)
        {
            return GetQuery(specification).ToList();
        }

        /// <summary>
        /// Fetches the specified specification.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="includeProperty">The include property.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch<TProperty>(Expression<Func<TEntity, bool>> specification, Expression<Func<TEntity, TProperty>> includeProperty)
        {
            return GetQuery(specification, includeProperty).ToList();
        }

        /// <summary>
        /// Fetches the specified order by expression.
        /// </summary>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderByExpression, Expression<Func<TEntity, bool>> specification)
        {
            var query = GetQuery(specification);

            return query.OrderBy(orderByExpression).ToList();
        }

        /// <summary>
        /// Fetches the specified order by expression.
        /// </summary>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="includeProperty">The include property.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch<TOrderKey, TProperty>(Expression<Func<TEntity, TOrderKey>> orderByExpression, Expression<Func<TEntity, bool>> specification, Expression<Func<TEntity, TProperty>> includeProperty)
        {
            var query = GetQuery(specification, includeProperty);

            return query.OrderBy(orderByExpression).ToList();
        }

        /// <summary>
        /// Fetches the paged.
        /// </summary>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="totalRecordCount">The total record count.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FetchPaged<TOrderKey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> specification, Expression<Func<TEntity, TOrderKey>> orderByExpression,
            out int totalRecordCount)
        {
            var query = GetQuery(specification);

            query = query.OrderBy(orderByExpression);

            totalRecordCount = GetCount(specification);

            var availablePageIndex = pageIndex;
            if ((availablePageIndex - 1) * pageSize > totalRecordCount)
            {
                availablePageIndex = 1;
            }

            return query.Skip((availablePageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Gets the recordcount based upon the given filterspecification.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<TEntity, bool>> specification)
        {
            return GetQuery(specification).Count();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntity entity)
        {
            DeleteFromDbSet(entity);
        }

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return GetQuery<object>(predicate, null);
        }

        private IQueryable<TEntity> GetQuery<TProperty>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TProperty>> includeProperty)
        {
            // Add the included properties
            var query = _dbSet.AsQueryable();
            if (includeProperty != null)
            {
                query = query.Include(includeProperty);
            }

            // Add the specification, when specified
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Return the actual query
            return query;
        }

        private void DeleteFromDbSet(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
    }
}
