using System;
using System.Collections.Generic;
using Information.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Information.Data
{
    public partial class InfoContext : DbContext
    {
        public InfoContext()
        {
        }

        public InfoContext(DbContextOptions<InfoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CityT> CityTs { get; set; } = null!;
        public virtual DbSet<InformationT> InformationTs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-G3V9EO3; Initial Catalog=Info; User=Sakshi; Password=Temp1234; Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CityT>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("CityT");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InformationT>(entity =>
            {
                entity.ToTable("InformationT");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.InformationTs)
                    .HasForeignKey(d => d.City)
                    .HasConstraintName("FK_InformationT_CityT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
