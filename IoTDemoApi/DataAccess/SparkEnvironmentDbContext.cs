using IoTDemoApi.Migrations;
using IoTDemoApi.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IoTDemoApi.DataAccess
{
    public class SparkEnvironmentDbContext : DbContext
    {
        public DbSet<SparkEnvironmentData> EnvironmentData { get; set; }

        public DbSet<IfTttEventTriggered> IfTttEvents { get; set; }

        public SparkEnvironmentDbContext() : base("SparkEnvironmentDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SparkEnvironmentDbContext, Configuration>());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
