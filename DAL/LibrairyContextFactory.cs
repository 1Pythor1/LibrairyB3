using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL
{    
    public class LibrairyContextFactory : IDesignTimeDbContextFactory<LibrairyContext>
    {
        public LibrairyContext CreateDbContext(string[] args)
        {            
            const string connectionString = "Server=localhost;Database=b3_librairy;User=root;Password=;";

            var optionsBuilder = new DbContextOptionsBuilder<LibrairyContext>();
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new LibrairyContext(optionsBuilder.Options);
        }
    }
}
