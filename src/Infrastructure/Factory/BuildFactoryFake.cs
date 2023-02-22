using Bogus;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Sensedia.Infrastructure.Factory
{
    public class BuildFactoryFake
    {
        private static List<Product> fakerProductList = new List<Product>();
        private static List<ProductBrand> fakerProductBrandList = new List<ProductBrand>();
        private static List<ProductType> fakerProductTypeList = new List<ProductType>();
        private const string PATH_SEED = @"../Infrastructure/Data/SeedData";
        private static string FILE_JSON_PRODUCT = $@"{PATH_SEED}/{nameof(Product)}.json";
        private static string FILE_JSON_PRODUCT_BRANDS = $@"{PATH_SEED}/{nameof(ProductBrand)}.json";
        private static string FILE_JSON_PRODUCT_TYPE = $@"{PATH_SEED}/{nameof(ProductType)}.json";


        public static async Task BuildFactoryAsync(SensediaContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!Directory.Exists(PATH_SEED))
                {
                    Directory.CreateDirectory(PATH_SEED);
                }
            }
            catch (System.Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                logger.LogError(ex.Message);
            }
        }

        public async static Task GenerateBuildFactoryProductBrand(SensediaContext context, ILoggerFactory loggerFactory)
        {
            Task.Run(() =>
            {

                try
                {
                    if (!context.ProductBrands.Any())
                    {

                        if (File.Exists(FILE_JSON_PRODUCT_BRANDS))
                        {
                            File.Delete(FILE_JSON_PRODUCT_BRANDS);
                        }
                        Randomizer.Seed = new Random(2675309);

                        var productBrandIds = 1;
                        var productBrand = new Faker<ProductBrand>("pt_BR")
                            .RuleFor(p => p.Name, p => p.Commerce.Product())
                            .Generate(100);

                        fakerProductBrandList.AddRange(productBrand);

                        using var file = File.CreateText(FILE_JSON_PRODUCT_BRANDS);
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, fakerProductBrandList);


                    }

                }
                catch (System.Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                    logger.LogError(ex.Message);
                }

            }).Wait();
        }


        public async static Task GenerateBuildFactoryProductType(SensediaContext context, ILoggerFactory loggerFactory)
        {
            Task.Run(() =>
            {

                try
                {
                    if (!context.ProductTypes.Any())
                    {

                        if (File.Exists(FILE_JSON_PRODUCT_TYPE))
                        {
                            File.Delete(FILE_JSON_PRODUCT_TYPE);
                        }
                        Randomizer.Seed = new Random(2675309);

                        var productTypeIds = 1;
                        var productType = new Faker<ProductType>("pt_BR")
                            .RuleFor(p => p.Name, p => p.Commerce.Product())
                            .Generate(100);

                        fakerProductTypeList.AddRange(productType);

                        using var file = File.CreateText(FILE_JSON_PRODUCT_TYPE);
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, fakerProductTypeList);


                    }

                }
                catch (System.Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                    logger.LogError(ex.Message);
                }

            }).Wait();
        }

        public async static Task GenerateBuildFactoryProduct(SensediaContext context, ILoggerFactory loggerFactory)
        {
            Task.Run(() =>
            {

                try
                {
                    if (!context.Products.Any())
                    {

                        if (File.Exists(FILE_JSON_PRODUCT))
                        {
                            File.Delete(FILE_JSON_PRODUCT);
                        }
                        Randomizer.Seed = new Random(2675309);
                        var idsProductBrand = context.ProductBrands.Select(x => x.Id).ToArray();
                        var idsProductType = context.ProductTypes.Select(x => x.Id).ToArray();
                        var productIds = 1;
                        var productList = new Faker<Product>("pt_BR")
                            .RuleFor(p => p.Name, p => p.Commerce.Product())
                            .RuleFor(p => p.Description, p => $"{p.Commerce.ProductName()} {p.Commerce.Ean8()}")
                            .RuleFor(p => p.Price, p => p.Random.Decimal(10, 150))
                            .RuleFor(p => p.PictureUrl, p => p.Image.LoremFlickrUrl())
                            .RuleFor(p => p.ProductBrandId, p => idsProductBrand[new Random().Next(idsProductBrand.Count())])
                            .RuleFor(p => p.ProductTypeId, p => idsProductType[new Random().Next(idsProductType.Count())])
                            .Generate(100);


                        fakerProductList.AddRange(productList);

                        using var file = File.CreateText(FILE_JSON_PRODUCT);
                        var serializer = new JsonSerializer();
                        serializer.Serialize(file, fakerProductList);


                    }

                }
                catch (System.Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<BuildFactoryFake>();
                    logger.LogError(ex.Message);
                }

            }).Wait();
        }
    }
}
