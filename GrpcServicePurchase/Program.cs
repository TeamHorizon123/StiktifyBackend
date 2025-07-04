using GrpcServicePurchase.Data;
using GrpcServicePurchase.Interfaces;
using GrpcServicePurchase.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option
    => option.UseNpgsql(builder.Configuration["ConnectionStrings:Db"]));
builder.Services.AddGrpc();
builder.Services.AddLogging();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
// Configure the HTTP request pipeline.
app.MapGrpcService<PaymentGrpcService>();
app.MapGrpcService<PaymentMethodGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
