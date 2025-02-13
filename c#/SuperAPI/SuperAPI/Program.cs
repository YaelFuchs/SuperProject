using Microsoft.EntityFrameworkCore;
using Super.Core;
using Super.Core.Repositories;
using Super.Core.Service;
using Super.Data;
using Super.Data.Repositories;
using Super.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
        builder.WithOrigins("http://localhost:4200")  // הוספת כתובת הלקוח שלך
               .AllowAnyMethod()
               .AllowAnyHeader());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(@"Server=DESKTOP-SSNMLFD;DataBase=SuperDb;TrustServerCertificate=True;Trusted_Connection=True"));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepositoy, ProductRepository>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IBranchProductService, BranchProductService>();
builder.Services.AddScoped<IBranchProductRepository, BranchProductRepository>();

builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();


var app = builder.Build();

app.UseCors("AllowLocalhost");

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

