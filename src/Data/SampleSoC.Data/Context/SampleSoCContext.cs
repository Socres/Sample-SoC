namespace SampleSoC.Data.Context
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using SampleSoC.Domain.Core.Models;

    [DbConfigurationType(typeof(SampleSoCContextConfiguration))]
    public class SampleSoCContext : DbContext
    {
        public DbSet<BlogPost> BlogPosts { get; set; }

        public DbSet<BlogComment> BlogComments { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleSoCContext"/> class.
        /// </summary>
        public SampleSoCContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new NullDatabaseInitializer<SampleSoCContext>());
        }

        /// <summary>
        /// Initializes the specified test data density.
        /// </summary>
        public void Initialize()
        {
            Database.SetInitializer(new SampleSoCContextDbInitializer());

            Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // We do not want Cascading deletes
            modelBuilder.Conventions
                .Remove<System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions
                .Remove<System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention>();

            AddCustomConfigurations(modelBuilder);
        }

        private void AddCustomConfigurations(DbModelBuilder modelBuilder)
        {
            foreach (var configuration in GetType().Assembly.GetTypes()
                .Where(t => !t.IsAbstract && IsEntityTypeConfiguration(t)).Select(Activator.CreateInstance))
            {
                modelBuilder.Configurations.Add((dynamic)configuration);
            }
        }

        private static bool IsEntityTypeConfiguration(Type type)
        {
            var result = false;
            if (type.IsGenericType &&
                typeof(EntityTypeConfiguration<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
            {
                result = true;
            }
            else
            {
                if (type.BaseType != null)
                {
                    result = IsEntityTypeConfiguration(type.BaseType);
                }
            }
            return result;
        }
    }
}
