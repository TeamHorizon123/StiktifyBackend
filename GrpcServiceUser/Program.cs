using GrpcServiceUser.Data;
using GrpcServiceUser.Interface;
using GrpcServiceUser.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options
    => options.UseNpgsql(builder.Configuration["ConnectionStrings:Db"]));
// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddSingleton<IShopRepository, ShopRepository>();
builder.Services.AddSingleton<IShopRatingRepository, ShopRatingRepository>();
builder.Services.AddSingleton<IAddressRepository, AddressRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
app.MapGrpcService<ShopGrpcService>();
app.MapGrpcService<AddressGrpcService>();
app.MapGrpcService<ShopRatingGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
