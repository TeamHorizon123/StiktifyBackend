using GrpcServiceProduct.Data;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration["ConnectionStrings:Db"]));
builder.Services.AddGrpc();

builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IProductOptionRepository, ProductOptionRepository>();
builder.Services.AddSingleton<IProductRatingRepository, ProductRatingRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
app.MapGrpcService<CategoryGrpcService>();
app.MapGrpcService<ProductGrpcService>();
app.MapGrpcService<ProductOptionGrpcServie>();
app.MapGrpcService<ProductRatingGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
