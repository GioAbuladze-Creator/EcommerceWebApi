using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.DAL.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
    {
        public ProductsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EcommerceLatest;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true"); 

            return new ProductsDbContext(optionsBuilder.Options);
        }
    }
}
