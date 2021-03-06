﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RR_RoadWays_Services.Models
{
    public partial class RRRoadwaysDBContext : DbContext
    {
        public RRRoadwaysDBContext()
        {
        }

        public RRRoadwaysDBContext(DbContextOptions<RRRoadwaysDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<VoucherDieselDetails> VoucherDieselDetails { get; set; }
        public virtual DbSet<VoucherOthersExpenses> VoucherOthersExpenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=LAPTOP-D6CQL5UE;Database=RRRoadwaysDB;Trusted_Connection=True;uid=awaisoo;password=test1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContactPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Station>(entity =>
            {
                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StationType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Station)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Station_City");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Role");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.OwnerContactNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OwnerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VehicleNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.DownAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DownDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DownReturnDate).HasColumnType("date");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpDate).HasColumnType("date");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_Voucher_Users");

                entity.HasOne(d => d.DownFromNavigation)
                    .WithMany(p => p.VoucherDownFromNavigation)
                    .HasForeignKey(d => d.DownFrom)
                    .HasConstraintName("FK_Voucher_City1");

                entity.HasOne(d => d.DownToNavigation)
                    .WithMany(p => p.VoucherDownToNavigation)
                    .HasForeignKey(d => d.DownTo)
                    .HasConstraintName("FK_Voucher_City2");

                entity.HasOne(d => d.OilShop)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.OilShopId)
                    .HasConstraintName("FK_Voucher_Station");

                entity.HasOne(d => d.UpFromNavigation)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.UpFrom)
                    .HasConstraintName("FK_Voucher_Company");

                entity.HasOne(d => d.UpToNavigation)
                    .WithMany(p => p.VoucherUpToNavigation)
                    .HasForeignKey(d => d.UpTo)
                    .HasConstraintName("FK_Voucher_City");

                entity.HasOne(d => d.VehicleNumberNavigation)
                    .WithMany(p => p.Voucher)
                    .HasForeignKey(d => d.VehicleNumber)
                    .HasConstraintName("FK_Voucher_Vehicle1");
            });

            modelBuilder.Entity<VoucherDieselDetails>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.OilAndOthers).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.VoucherDieselDetails)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_VoucherDieselDetails_Station");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.VoucherDieselDetails)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK_VoucherDieselDetails_Voucher");
            });

            modelBuilder.Entity<VoucherOthersExpenses>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.OthersExpense)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.VoucherOthersExpenses)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK_VoucherOthersExpenses_Voucher");
            });
        }
    }
}
