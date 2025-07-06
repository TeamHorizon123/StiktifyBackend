using Grpc.Core;
using GrpcServiceProduct.Data;
using GrpcServiceProduct.External;
using GrpcServiceProduct.External.IExternal;
using GrpcServiceProduct.Interfaces;
using GrpcServiceProduct.Order;
using GrpcServiceProduct.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration["ConnectionStrings:Db"]));
builder.Services.AddGrpc();

var productService = builder.Configuration["ConnectionStrings:GrpcServiceOrder"]!;
builder.Services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(o
    => o.Address = new Uri(productService))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductOptionRepository, ProductOptionRepository>();
builder.Services.AddScoped<IProductRatingRepository, ProductRatingRepository>();
builder.Services.AddScoped<ICategorySizeRepository, CategorySizeRepository>();
builder.Services.AddScoped<IProductItemRepository, ProductItemRepository>();
builder.Services.AddScoped<IProductVarriantRepository, ProductVarriantRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
app.MapGrpcService<CategoryGrpcService>();
app.MapGrpcService<ProductGrpcService>();
app.MapGrpcService<ProductOptionGrpcServie>();
app.MapGrpcService<ProductRatingGrpcService>();
app.MapGrpcService<CategorySizeGrpcSevice>();
app.MapGrpcService<ProductItemGrpcService>();
app.MapGrpcService<ProductVarriantGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
