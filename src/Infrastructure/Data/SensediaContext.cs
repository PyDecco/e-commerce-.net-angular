using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Core.Entities;

namespace Infrastructure.Data
{
    public class SensediaContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands{ get; set; }
        public DbSet<ProductType> ProductType  { get; set; }
        public SensediaContext(DbContextOptions<SensediaContext> options) : base(options)
        {

        }
    }
}
