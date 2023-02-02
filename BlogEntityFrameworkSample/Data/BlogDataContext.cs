using BlogEntityFrameworkSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace BlogEntityFrameworkSample.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        //public DbSet<PostTag> PostTags { get; set; }         
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=BlogEF;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True;Encrypt=True");
            options.LogTo(Console.WriteLine);
        }

    }
}
