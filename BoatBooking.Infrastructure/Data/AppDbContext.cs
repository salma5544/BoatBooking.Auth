using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BoatBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoatBooking.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasIndex(x => x.Email)
                      .IsUnique();

                entity.Property(x => x.FullName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(x => x.Email)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(x => x.PasswordHash)
                      .IsRequired();

                entity.Property(x => x.Role)
                      .IsRequired();

                entity.Property(x => x.IsApproved)
                      .IsRequired();

                entity.Property(x => x.CreatedAt)
                      .IsRequired();
            });
        }
    }
}

