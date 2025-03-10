using Microsoft.EntityFrameworkCore;
using Super.Core;
using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public  DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<BranchProduct> BranchProducts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartsItem { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)

        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // הגדרת קשר Many-to-Many בין Users ל- Roles
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
             .HasOne(ur => ur.User)
             .WithMany(u => u.UserRoles)
             .HasForeignKey(ur => ur.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        
            // קשר בין ShoppingCart ל- ShoppingCartItem
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(si => si.ShoppingCart)  // שייך ל- ShoppingCart
                .WithMany(c => c.Carts)         // ShoppingCart יכול להכיל הרבה ShoppingCartItem
                .HasForeignKey(si => si.ShoppingCartId)  // מפתח זר ל- ShoppingCart
                .OnDelete(DeleteBehavior.Cascade); // אם הסל נמחק, גם הפריטים יימחקו

            // קשר בין ShoppingCartItem ל- Product
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(si => si.Product)       // שייך ל- Product
                .WithMany()                     // למוצר יכולים להיות הרבה ShoppingCartItem
                .HasForeignKey(si => si.ProductId)  // מפתח זר ל- Product
                .OnDelete(DeleteBehavior.Restrict); // לא מאפשר למחוק מוצר אם יש לו פריטים בקניות
        }
    }
}
