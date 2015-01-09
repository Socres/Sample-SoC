namespace SampleSoC.Data
{
    using System;
    using SampleSoC.Data.Context;
    using SampleSoC.Data.Core.Interfaces;
    using SampleSoC.Data.Repositories;
    using SampleSoC.Domain.Core.Models;
    using SampleSoC.Domain.Core.Models.Base;

    /// <summary>
    /// Implements the Unit of Work pattern.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SampleSoCContext Context { get; set; }
        private IRepository<BlogPost> _blogPosts;
        private IRepository<BlogComment> _blogComments;

        /// <summary>
        /// Gets the blog posts.
        /// </summary>
        public IRepository<BlogPost> BlogPosts
        {
            get { return GetOrCreateRepository(ref _blogPosts); }
        }

        /// <summary>
        /// Gets the blog comments.
        /// </summary>
        public IRepository<BlogComment> BlogComments
        {
            get { return GetOrCreateRepository(ref _blogComments); }
        }

        /// <summary>
        /// Constructor for UnitOfWork
        /// </summary>
        public UnitOfWork()
        {
            Context = new SampleSoCContext();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        ~UnitOfWork()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Gets the or create repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="repository">The repository.</param>
        /// <returns></returns>
        private IRepository<TEntity> GetOrCreateRepository<TEntity>(ref IRepository<TEntity> repository)
            where TEntity : IdentityModelBase
        {
            return repository ?? (repository = new Repository<TEntity>(Context));
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public virtual void Save()
        {
            Context.SaveChanges();
        }

        /// <summary>
        /// Initializes this Database.
        /// </summary>
        public void Initialize()
        {
            Context.Initialize();
        }
    }
}