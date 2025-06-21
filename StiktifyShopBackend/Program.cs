using Domain.Responses;
using Grpc.Core;
using Grpc.Net.Client.Balancer;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using StiktifyShopBackend.Cart;
using StiktifyShopBackend.Category;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Order;
using StiktifyShopBackend.OrderDetails;
using StiktifyShopBackend.Payment;
using StiktifyShopBackend.PaymentMethod;
using StiktifyShopBackend.Product;
using StiktifyShopBackend.ProductOption;
using StiktifyShopBackend.ProductRating;
using StiktifyShopBackend.Providers;
using StiktifyShopBackend.ReceiveAddress;
using StiktifyShopBackend.Shop;
using StiktifyShopBackend.ShopRating;
using StiktifyShopBackend.Tracking;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<ResponseShop>("shop");
builder.Services.AddControllers()
    .AddOData(options => options
        .SetMaxTop(100)
        .Filter()
        .OrderBy()
        .Count()
        .Expand()
        .Select()
        .AddRouteComponents("odata", odataBuilder.GetEdmModel())
        );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config Grpc Client of User Grpc service
var userGrpc = builder.Configuration["ConnectionStrings:UserGrpc"]!;
builder.Services.AddGrpcClient<ShopGrpc.ShopGrpcClient>(o
    => o.Address = new Uri(userGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<ShopRatingGrpc.ShopRatingGrpcClient>(o
    => o.Address = new Uri(userGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<AddressGrpc.AddressGrpcClient>(o
    => o.Address = new Uri(userGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);

// Config Grpc Client of Product Grpc service
var productGrpc = builder.Configuration["ConnectionStrings:ProductGrpc"]!;
builder.Services.AddGrpcClient<CategoryGrpc.CategoryGrpcClient>(o
    => o.Address = new Uri(productGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(o
    => o.Address = new Uri(productGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<ProductOptionGrpc.ProductOptionGrpcClient>(o
    => o.Address = new Uri(productGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<ProductRatingGrpc.ProductRatingGrpcClient>(o
    => o.Address = new Uri(productGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);

// Config grpc client of order grpc service
var orderGrpc = builder.Configuration["ConnectionStrings:OrderGrpc"]!;
builder.Services.AddGrpcClient<OrderGrpc.OrderGrpcClient>(o
    => o.Address = new Uri(orderGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<CartGrpc.CartGrpcClient>(o
    => o.Address = new Uri(orderGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<OrderTrackingGrpc.OrderTrackingGrpcClient>(o
    => o.Address = new Uri(orderGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<OrderDetailGrpc.OrderDetailGrpcClient>(o
    => o.Address = new Uri(orderGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);

// Config grpc client of purchase grpc service
var purchaseGrpc = builder.Configuration["ConnectionStrings:PurchaseGrpc"]!;
builder.Services.AddGrpcClient<PaymentGrpc.PaymentGrpcClient>(o
    => o.Address = new Uri(purchaseGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);
builder.Services.AddGrpcClient<PaymentMethodGrpc.PaymentMethodGrpcClient>(o
    => o.Address = new Uri(purchaseGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);

builder.Services.AddSingleton<ResolverFactory>(
    sp => new DnsResolverFactory(refreshInterval: TimeSpan.FromSeconds(30)));

builder.Services.AddScoped<IShopProvider, ShopProvider>();
builder.Services.AddScoped<IShopRatingProvider, ShopRatingProvider>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
