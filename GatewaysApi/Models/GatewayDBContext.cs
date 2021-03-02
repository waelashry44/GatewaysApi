using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace GatewaysApi.Models
{
    public partial class GatewayDBContext : DbContext
    {
        public GatewayDBContext()
        {
        }

        public GatewayDBContext(DbContextOptions<GatewayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gateway> Gateway { get; set; }
        public virtual DbSet<PeripheralDevice> PeripheralDevice { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-103V5IN\\LOCALINSTANCE;Database=GatewayDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>(entity =>
            {
                entity.HasIndex(e => e.SerialNumber)
                    .HasName("IX_Gateway")
                    .IsUnique();

                entity.Property(e => e.Ipv4address).HasColumnName("IPV4Address");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.SerialNumber).HasMaxLength(150);
            });

            modelBuilder.Entity<PeripheralDevice>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Uid).HasColumnName("UID");

                entity.HasOne(d => d.Gateway)
                    .WithMany(p => p.PeripheralDevice)
                    .HasForeignKey(d => d.GatewayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PeripheralDevice_Gateway");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
