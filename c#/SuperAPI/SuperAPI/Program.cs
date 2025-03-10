using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Super.Core;
using Super.Core.Mapping;
using Super.Core.Models;
using Super.Core.Repositories;
using Super.Core.Service;
using Super.Data;
using Super.Data.Repositories;
using Super.Service;
using SuperAPI.Mapping;
using SuperAPI.Models;
using System.Security.Claims;
using System.Text;
using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
        policy.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        RoleClaimType = ClaimTypes.Role,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Manager", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;

            var roles = roleClaim.Value.Split(',');
            return roles.Contains("ROLE_MANAGER");
        }));
    options.AddPolicy("Admin", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;
            var roles = roleClaim.Value.Split(',');
            return roles.Contains("ROLE_ADMIN");
        }));

    options.AddPolicy("User", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;
            var roles = roleClaim.Value.Split(',');
            return roles.Contains("ROLE_USER");
        }));
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(@"Server=DESKTOP-SSNMLFD;DataBase=SuperDb;TrustServerCertificate=True;Trusted_Connection=True"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile), typeof(PostModelsMappingProfile));

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepositoy, ProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingProduct), typeof(PostProductMapping));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddAutoMapper(typeof(PostCategoryMapping));

builder.Services.AddScoped<IBranchProductService, BranchProductService>();
builder.Services.AddScoped<IBranchProductRepository, BranchProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingBranchProduct), typeof(PostBranchProductMapping));
builder.Services.AddAutoMapper(typeof(MappingProductPrice));

builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddAutoMapper(typeof(MappingBranch), typeof(PostBranchMapping));

builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddAutoMapper(typeof(MappingShoppingCartItem), typeof(ShoppingCartMapping));

builder.Services.AddTransient<IPayPalRepository, PayPalRepository>();
builder.Services.AddScoped<IPayPalService, PayPalService>();
builder.Services.AddAutoMapper(typeof(OrderMapping));

var paypalClient = new PaypalServerSdkClient.Builder()
    .ClientCredentialsAuth(
        new ClientCredentialsAuthModel.Builder(
            builder.Configuration["PayPal:ClientId"],
            builder.Configuration["PayPal:ClientSecret"]
        )
        .OAuthTokenProvider(async (credentialsManager, token) =>
        {
            return await credentialsManager.FetchTokenAsync();
        })
        .Build())
    .Build();

builder.Services.AddSingleton(paypalClient);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});
var app = builder.Build();

app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (!context.Roles.Any())
    {
        context.Roles.AddRange(
            new Role { Id = 1, Name = "ROLE_USER" },
            new Role { Id = 2, Name = "ROLE_ADMIN" },
            new Role { Id = 3, Name = "ROLE_MANAGER" }
        );
        context.SaveChanges();
    }
}
