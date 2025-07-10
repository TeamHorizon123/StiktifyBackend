using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.ModelBuilder;
using StiktifyShop.Application.DTOs.Responses;
using StiktifyShop.Application.Interfaces;
using StiktifyShop.Infrastructure;
using StiktifyShop.Infrastructure.Repository;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(option
    => option.UseNpgsql(builder.Configuration["ConnectionStrings:Db"]));

// Register OData model builder
var odataBuilder = new ODataConventionModelBuilder();

odataBuilder.EntitySet<ResponseCart>("cart");
odataBuilder.EntitySet<ResponseCategory>("category");
odataBuilder.EntitySet<ResponseOrder>("order");
odataBuilder.EntitySet<ResponseOrderDetail>("order-detail");
odataBuilder.EntitySet<ResponseOrderTracking>("order-tracking");
odataBuilder.EntitySet<ResponsePayment>("payment");
odataBuilder.EntitySet<ResponsePaymentMethod>("payment-method");
odataBuilder.EntitySet<ResponsePaymentRefund>("payment-refund");
odataBuilder.EntitySet<ResponseProduct>("product");
odataBuilder.EntitySet<ResponseProductRating>("product-rating");
odataBuilder.EntitySet<ResponseProductOption>("product-option");
odataBuilder.EntitySet<ResponseProductItem>("product-item");
odataBuilder.EntitySet<ResponseProductSize>("product-size");
odataBuilder.EntitySet<ResponseProductVariant>("product-variant");
odataBuilder.EntitySet<ResponseShop>("shop");
odataBuilder.EntitySet<ResponseShopRating>("shop-rating");
odataBuilder.EntitySet<ResponseUserAddress>("user-address");

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

// Confiure cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://127.0.0.1:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
         .AllowCredentials();
    });
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Config JWT Authentication
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

// Add dependency injection for repositories
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderDetailRepo, OrderDetailRepo>();
builder.Services.AddScoped<IOrderTrackingRepo, OrderTrackingRepo>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
builder.Services.AddScoped<IPaymentMethodRepo, PaymentMethodRepo>();
builder.Services.AddScoped<IPaymentRefundRepo, PaymentRefundRepo>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductOptionRepo, ProductOptionRepo>();
builder.Services.AddScoped<IProductRatingRepo, ProductRatingRepo>();
builder.Services.AddScoped<IProductSizeRepo, ProductSizeRepo>();
builder.Services.AddScoped<IProductVariantRepo, ProductVariantRepo>();
builder.Services.AddScoped<IProductItemRepo, ProductItemRepo>();
builder.Services.AddScoped<IShopRepo, ShopRepo>();
builder.Services.AddScoped<IShopRatingRepo, ShopRatingRepo>();
builder.Services.AddScoped<IUserAddressRepo, UserAddressRepo>();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var context = service.GetRequiredService<AppDbContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseODataRouteDebug();
app.UseCors("AllowFrontend");
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
