using Microsoft.EntityFrameworkCore;

namespace Alinta.Model
{
    public class Context: DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public Context(DbContextOptions<Context> options): base(options)
        {

        }
    }

}
