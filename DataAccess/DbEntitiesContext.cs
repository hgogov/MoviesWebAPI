namespace DataAccess
{
    using DatabaseStructure.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DbEntitiesContext : DbContext
    {
        // Your context has been configured to use a 'DbEntitiesContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccess.DbEntitiesContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DbEntitiesContext' 
        // connection string in the application configuration file.
        public DbEntitiesContext()
            : base("name=DbEntitiesContext")
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}