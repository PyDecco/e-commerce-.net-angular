using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore; //ORM Database (Relational, noSQL)
using Sensedia.Infrastructure.Factory;
//using Sensedia.Infrastructure.Factory;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("SensediaLocalSqlServer");
builder.Services.AddDbContext<SensediaContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", p =>
    {
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowOrigins");
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<SensediaContext>();
await context.Database.MigrateAsync();
var logger = services.GetRequiredService<ILogger<Program>>();
var logger2 = services.GetRequiredService<ILoggerFactory>();
try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context, logger2);
}
catch (Exception ex)
{
    logger.LogError(ex, "An error orccured during migration");
}

app.Run();
