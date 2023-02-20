using Core.Entities;
using Microsoft.Extensions.Logging;
using Sensedia.Infrastructure.Factory;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(SensediaContext context, ILoggerFactory loggerFactory)
        {
            await SeedContext(context, loggerFactory);
        }

        private static async Task SeedContext(SensediaContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    await BuildFactoryFake.GenerateBuildFactoryProductBrand(context, loggerFactory);
                    var brandsData = File.ReadAllText($"../Infrastructure/Data/SeedData/{nameof(ProductBrand)}.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    context.ProductBrands.AddRange(brands);
                    if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    await BuildFactoryFake.GenerateBuildFactoryProductType(context, loggerFactory);
                    var typeData = File.ReadAllText($"../Infrastructure/Data/SeedData/{nameof(ProductType)}.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);
                    context.ProductTypes.AddRange(types);
                    if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
                }
                if (!context.Products.Any())
                {
                    await BuildFactoryFake.GenerateBuildFactoryProduct(context, loggerFactory);
                    var productsData = File.ReadAllText($"../Infrastructure/Data/SeedData/{nameof(Product)}.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    context.Products.AddRange(products);
                    if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
