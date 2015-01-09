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
            return GetById(id, string.Empty);
        }

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="includeProperties">The include properties.</param>
        public TEntity GetById(int id, string includeProperties)
        {
            return GetQuery(e => e.Id.Equals(id), includeProperties).SingleOrDefault();
        }

        public IEnumerable<TEntity> Fetch()
        {
            return GetQuery(null, string.Empty).ToList();
        }

        /// <summary>
        /// Fetches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> specification)
        {
            return GetQuery(specification, string.Empty).ToList();
        }

        /// <summary>
        /// Fetches the specified specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch(Expression<Func<TEntity, bool>> specification, string includeProperties)
        {
            return GetQuery(specification, includeProperties).ToList();
        }

        /// <summary>
        /// Fetches the specified order by expression.
        /// </summary>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Fetch<TOrderKey>(Expression<Func<TEntity, TOrderKey>> orderByExpression, Expression<Func<TEntity, bool>> specification, string includeProperties)
        {
            var query = GetQuery(specification, includeProperties);

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
            var query = GetQuery(specification, string.Empty);

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
            return GetQuery(specification, string.Empty).Count();
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

        /// <summary>
        /// Deletes the entity specified by the id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            DeleteFromDbSet(entity);
        }

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>> predicate, string includeProperties)
        {
            // Add the included properties
            var query = _dbSet.AsQueryable();
            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

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
