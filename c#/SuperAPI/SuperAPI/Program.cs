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
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
        builder.WithOrigins("http://localhost:4200")  // כתובת הלקוח
               .AllowAnyMethod()
               .AllowAnyHeader());
});

// הגדרת Swagger עם תמיכה ב-JWT
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

// הגדרת אימות באמצעות JWT
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

// הוספת Authorization עם המדיניות שהגדרת
builder.Services.AddAuthorization(options =>
{
    // רק למשתמשים שיש להם ROLE_MANAGER
    options.AddPolicy("Manager", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;

            var roles = roleClaim.Value.Split(',');
            return roles.Contains("ROLE_MANAGER");
        }));

    // רק למשתמשים שיש להם לפחות ROLE_ADMIN (או יותר)
    options.AddPolicy("Admin", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;

            var roles = roleClaim.Value.Split(',');
            return roles.Contains("ROLE_ADMIN");
        }));

    // רק למשתמשים שיש להם לפחות ROLE_USER (או יותר)
    options.AddPolicy("User", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;

            var roles = roleClaim.Value.Split(',');
            return roles.Contains("ROLE_USER");
        }));
});

// חיבור ל-DB
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
builder.Services.AddAutoMapper( typeof(PostCategoryMapping));

builder.Services.AddScoped<IBranchProductService, BranchProductService>();
builder.Services.AddScoped<IBranchProductRepository, BranchProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingBranchProduct),typeof(PostBranchProductMapping));

builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddAutoMapper(typeof(MappingBranch), typeof(PostBranchMapping));

var app = builder.Build();

app.UseCors("AllowLocalhost");

// הפעלת Swagger רק בסביבת פיתוח
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// אתחול הרשאות בסיסיות
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
