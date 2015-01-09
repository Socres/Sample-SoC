namespace SampleSoC.Data.Context
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public class SampleSoCContextConfiguration : DbConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SampleSoCContextConfiguration"/> class.
        /// </summary>
        public SampleSoCContextConfiguration()
        {
            SetDefaultConnectionFactory(new SqlConnectionFactory());
            SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
            SetDatabaseInitializer(new NullDatabaseInitializer<SampleSoCContext>());
        }
    }
}
