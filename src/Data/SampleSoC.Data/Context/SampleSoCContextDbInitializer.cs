namespace SampleSoC.Data.Context
{
    using System.Data.Entity;
    using SampleSoC.Data.Migrations;

    /// <summary>
    /// Implements the Code First Migrations to update the database to the latest version. 
    /// </summary>
    internal class SampleSoCContextDbInitializer : MigrateDatabaseToLatestVersion<SampleSoCContext, MigrationConfiguration>
    {
    }
}
