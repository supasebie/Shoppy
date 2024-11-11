using Core;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// <-----------------Services-------------------->
builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));

builder.Services.AddControllers();
// <-----------------Services-------------------->


// <-----------------APP-------------------->
var app = builder.Build();

app.MapControllers();

try
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
    await context.Database.MigrateAsync();
    await SeedStoreContext.SeedDatabase(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

app.Run();
// <-----------------APP-------------------->