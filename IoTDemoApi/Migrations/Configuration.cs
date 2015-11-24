namespace IoTDemoApi.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Runtime.Remoting.Messaging;

    internal sealed class Configuration : DbMigrationsConfiguration<IoTDemoApi.DataAccess.SparkEnvironmentDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        public static bool SuspendExecutionStrategy
        {
            get
            {
                return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
            }
            set
            {
                CallContext.LogicalSetData("SuspendExecutionStrategy", value);
            }
        }

        protected override void Seed(IoTDemoApi.DataAccess.SparkEnvironmentDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
    //public class MyConfiguration : DbConfiguration
    //{
    //    public MyConfiguration()
    //    {
    //        this.SetExecutionStrategy("System.Data.SqlClient", () => SuspendExecutionStrategy
    //          ? (IDbExecutionStrategy)new DefaultExecutionStrategy()
    //          : new SqlAzureExecutionStrategy());
    //    }

    //    public static bool SuspendExecutionStrategy
    //    {
    //        get
    //        {
    //            return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
    //        }
    //        set
    //        {
    //            CallContext.LogicalSetData("SuspendExecutionStrategy", value);
    //        }
    //    }
    //}
}
