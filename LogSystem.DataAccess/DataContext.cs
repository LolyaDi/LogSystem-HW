namespace LogSystem.DataAccess
{
    using LogSystem.Models;
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Migrations.Configuration>());
        }
        
        public virtual DbSet<Visitor> Visitors { get; set; }
    }
}