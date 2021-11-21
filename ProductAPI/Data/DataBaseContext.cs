using Microsoft.EntityFrameworkCore;
using ProductAPI.Model;

namespace ProductAPI.Data
{
    public class DataBaseContext :  DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
