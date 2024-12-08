using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using MvcSoruCevap.Models;
namespace MvcSoruCevap.Models
{
    /*    public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Question> Questions { get; set; }
            public DbSet<Answer> Answers { get; set; }

            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Question>()
                    .HasMany(q => q.Tags)
                    .WithMany(t => t.Questions)
                    .UsingEntity(j => j.ToTable("QuestionTags"));

                base.OnModelCreating(modelBuilder);
            }
        }*/
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=QnAPlatform;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API ile ilişkiler
            modelBuilder.Entity<Question>()
                .HasOne(q => q.User)
                .WithMany(u => u.Questions)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.CategoryId);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.User)
                .WithMany(u => u.Answers)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }


}


