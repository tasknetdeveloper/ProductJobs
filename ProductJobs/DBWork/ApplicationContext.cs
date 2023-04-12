using Microsoft.EntityFrameworkCore;
using Model;
namespace DBWork
{
    
    public class ApplicationContext : DbContext
    {       
        public DbSet<Product>? Product { get; set; } = null;
        public DbSet<OrderItem>? Order { get; set; } = null;
       
        private string connection = "";
        private int maxRetryDelay = 30;
        public ApplicationContext(string connection,int maxRetryDelay)
        {
            this.maxRetryDelay = maxRetryDelay;
            this.connection = connection;
            Database.EnsureCreated();           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(this.connection,

                ServerVersion.AutoDetect(this.connection),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(this.maxRetryDelay),
                    errorNumbersToAdd: null));
        }
    }
}
