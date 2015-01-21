namespace SampleSoC.Data.Core.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using SampleSoC.Domain.Core.Models.Base;

    /// <summary>
    /// Methods that provide Repository functionality.
    /// </summary>
    /// <typeparam name="T">A type that is derived from <see cref="IdentityModelBase"/></typeparam>
    public interface IRepository<T> where T : IdentityModelBase
    {
        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        T GetById(int id);

        /// <summary>
        /// Gets the entity by id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <param name="includeProperty">The include property.</param>
        /// <returns></returns>
        T GetById<TProperty>(int id, Expression<Func<T, TProperty>> includeProperty);

        /// <summary>
        /// Gets the entities by specification.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Fetch();

        /// <summary>
        /// Gets the entities by specification.
        /// </summary>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        IEnumerable<T> Fetch(Expression<Func<T, bool>> specification);

        /// <summary>
        /// Gets the entities by specification.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="specification">The specification.</param>
        /// <param name="includeProperty">The include property.</param>
        /// <returns></returns>
        IEnumerable<T> Fetch<TProperty>(Expression<Func<T, bool>> specification, Expression<Func<T, TProperty>> includeProperty);

        /// <summary>
        /// Fetches the specified order by expression.
        /// </summary>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="specification">The specification.</param>
        /// <returns></returns>
        IEnumerable<T> Fetch<TOrderKey>(Expression<Func<T, TOrderKey>> orderByExpression,
            Expression<Func<T, bool>> specification);

        /// <summary>
        /// Fetches the specified order by expression.
        /// </summary>
        /// <typeparam name="TOrderKey">The type of the order key.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <param name="specification">The specification.</param>
        /// <param name="includeProperty">The include property.</param>
        /// <returns></returns>
        IEnumerable<T> Fetch<TOrderKey, TProperty>(Expression<Func<T, TOrderKey>> orderByExpression,
            Expression<Func<T, bool>> specification, Expression<Func<T, TProperty>> includeProperty);

        /// <summary>
        /// Fetches entities by the specified specification.
        /// </summary>
        /// <typeparam name="TOrderKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="specification"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="totalRecordCount"></param>
        /// <returns></returns>
        IEnumerable<T> FetchPaged<TOrderKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> specification, Expression<Func<T, TOrderKey>> orderByExpression, out int totalRecordCount);

        /// <summary>
        /// Gets the recordcount based upon the given filterspecification.
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> specification);

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);
    }
}
