using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Core.Entities;

namespace Infrastructure.Data
{
    public class SensediaContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public SensediaContext(DbContextOptions<SensediaContext> options) : base(options)
        {

        }
    }
}
