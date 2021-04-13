using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

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

        public virtual DbSet<Advance> Advance { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<ExpanseHead> ExpanseHead { get; set; }
        public virtual DbSet<FixedExpanse> FixedExpanse { get; set; }
        public virtual DbSet<Installment> Installment { get; set; }
        public virtual DbSet<Maintenance> Maintenance { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Station> Station { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<VehicleClaim> VehicleClaim { get; set; }
        public virtual DbSet<VehicleLoading> VehicleLoading { get; set; }
        public virtual DbSet<VehicleLoadingDetail> VehicleLoadingDetail { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<VoucherDieselDetails> VoucherDieselDetails { get; set; }
        public virtual DbSet<VoucherOthersExpenses> VoucherOthersExpenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                IConfigurationRoot configuration = new ConfigurationBuilder()
              .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
              .AddJsonFile("appsettings.json")
              .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("RRRDbConstr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Advance>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdvanceDate).HasColumnType("datetime");

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Advance)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_Advance_Station");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Advance)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Advance_Vehicle");
            });

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

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExpanseHead>(entity =>
            {
                entity.Property(e => e.HeadName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FixedExpanse>(entity =>
            {
                entity.Property(e => e.BhattaDetails).IsUnicode(false);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExpanseMonth)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ExtraExpenseDetails).IsUnicode(false);

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.FixedExpanse)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_FixedExpanse_Vehicle");
            });

            modelBuilder.Entity<Installment>(entity =>
            {
                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InstallmentDate).HasColumnType("date");

                entity.Property(e => e.InstallmentDetail).IsUnicode(false);

                entity.Property(e => e.InstallmentsMonth)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Installment)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Installment_Vehicle");
            });

            modelBuilder.Entity<Maintenance>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.MaintenanceDate).HasColumnType("date");

                entity.Property(e => e.MaintenanceDesc)
                    .HasColumnName("maintenanceDesc")
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Maintenance)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Maintenance_Department");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.Maintenance)
                    .HasForeignKey(d => d.StationId)
                    .HasConstraintName("FK_Maintenance_Station");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Maintenance)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_Maintenance_Vehicle");
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

            modelBuilder.Entity<VehicleClaim>(entity =>
            {
                entity.Property(e => e.Claim)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimDate).HasColumnType("date");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleClaim)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_VehicleClaim_Vehicle");
            });

            modelBuilder.Entity<VehicleLoading>(entity =>
            {
                entity.Property(e => e.EntryDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoadingDate).HasColumnType("date");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.VehicleLoading)
                    .HasForeignKey(d => d.VehicleId)
                    .HasConstraintName("FK_VehicleLoadingDetail_Vehicle");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.VehicleLoading)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK_VehicleLoading_Voucher");
            });

            modelBuilder.Entity<VehicleLoadingDetail>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.VloadingId })
                    .HasName("PK__VehicleL__8B8F42457341171F");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.VloadingId).HasColumnName("VLoadingId");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.VehicleName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vloading)
                    .WithMany(p => p.VehicleLoadingDetail)
                    .HasForeignKey(d => d.VloadingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VehicleLoadingDetail_VehicleLoading");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DownAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DownDescription).IsUnicode(false);

                entity.Property(e => e.DownReturnDate).HasColumnType("date");

                entity.Property(e => e.Month)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OilAmount).HasColumnType("decimal(9, 0)");

                entity.Property(e => e.UpAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpDate).HasColumnType("date");

                entity.Property(e => e.UpDescription).IsUnicode(false);

                entity.Property(e => e.VoucherNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

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

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.OilAndOthers)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Expanse)
                    .WithMany(p => p.VoucherOthersExpenses)
                    .HasForeignKey(d => d.ExpanseId)
                    .HasConstraintName("FK_VoucherOthersExpenses_ExpanseHead");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.VoucherOthersExpenses)
                    .HasForeignKey(d => d.VoucherId)
                    .HasConstraintName("FK_VoucherOthersExpenses_Voucher");
            });
        }
    }
}
