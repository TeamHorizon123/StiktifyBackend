using Domain.Responses;
using Grpc.Core;
using Grpc.Net.Client.Balancer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using StiktifyShopBackend.Cart;
using StiktifyShopBackend.Category;
using StiktifyShopBackend.CategorySize;
using StiktifyShopBackend.Interfaces;
using StiktifyShopBackend.Order;
using StiktifyShopBackend.OrderDetails;
using StiktifyShopBackend.Payment;
using StiktifyShopBackend.PaymentMethod;
using StiktifyShopBackend.Product;
using StiktifyShopBackend.ProductItem;
using StiktifyShopBackend.ProductOption;
using StiktifyShopBackend.ProductRating;
using StiktifyShopBackend.ProductVarriant;
using StiktifyShopBackend.Providers;
using StiktifyShopBackend.ReceiveAddress;
using StiktifyShopBackend.Shop;
using StiktifyShopBackend.ShopRating;
using StiktifyShopBackend.Tracking;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var odataBuilder = new ODataConventionModelBuilder();
odataBuilder.EntitySet<ResponseShop>("shop");
odataBuilder.EntitySet<ResponseShopRating>("shop-rating");
odataBuilder.EntitySet<ResponseReceiveAddress>("address");
odataBuilder.EntitySet<ResponseCategory>("category");
odataBuilder.EntitySet<ResponseCategorySize>("category-size");
odataBuilder.EntitySet<ResponseProduct>("product");
odataBuilder.EntitySet<ResponseProductOption>("product-option");
odataBuilder.EntitySet<ResponseProductItem>("product-item");
//odataBuilder.EntitySet<ResponseProductVarriant>("product-varriant");
odataBuilder.EntitySet<ResponseProductRating>("product-rating");
odataBuilder.EntitySet<ResponseCart>("cart");
odataBuilder.EntitySet<ResponseOrder>("order");
odataBuilder.EntitySet<ResponseOrderDetail>("order-detail");
odataBuilder.EntitySet<ResponsePayment>("payment");
odataBuilder.EntitySet<ResponsePaymentMethod>("payment-method");
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
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecuritySchema = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Enter your JWT Access Token",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition("Bearer", jwtSecuritySchema);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecuritySchema, Array.Empty<string>() }
    });
});

var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true; 
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, 
        ValidateAudience = false, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

        ClockSkew = TimeSpan.Zero 
    };
});

// Add Authorization
builder.Services.AddAuthorization();

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

builder.Services.AddGrpcClient<CategorySizeGrpc.CategorySizeGrpcClient>(o
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

builder.Services.AddGrpcClient<ProductItemGrpc.ProductItemGrpcClient>(o
    => o.Address = new Uri(productGrpc))
    .ConfigureChannel(o => o.Credentials = ChannelCredentials.Insecure);

builder.Services.AddGrpcClient<ProductVarriantGrpc.ProductVarriantGrpcClient>(o
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
builder.Services.AddScoped<IAddressProvider, AddressProvider>();
builder.Services.AddScoped<ICategoryProvider, CategoryProvider>();
builder.Services.AddScoped<ICategorySizeProvider, CategorySizeProvider>();
builder.Services.AddScoped<IProductProvider, ProductProvider>();
builder.Services.AddScoped<IProductRatingProvider, ProductRatingProvider>();
builder.Services.AddScoped<IProductOptionProvider, ProductOptionProvider>();
builder.Services.AddScoped<IProductItemProvider, ProductItemProvider>();
builder.Services.AddScoped<IProductVarriantProvider, ProductVarriantProvider>();
builder.Services.AddScoped<ICartProvider, CartProvider>();
builder.Services.AddScoped<IOrderProvider, OrderProvider>();
builder.Services.AddScoped<IOrderDetailProvider, OrderDetailProvider>();
builder.Services.AddScoped<IOrderTrackingProvider, OrderTrackingProvider>();
builder.Services.AddScoped<IPaymentProvider, PaymentProvider>();
builder.Services.AddScoped<IPaymentMethodProvider, PaymentMethodProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
