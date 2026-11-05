using AHTS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AHTS.DAL.Contexts;

public class AhtsContext : DbContext
{
    public AhtsContext()
    {
    }

    public AhtsContext(DbContextOptions<AhtsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Assets__434923727D97471F");

            entity.HasIndex(e => e.SerialNumber, "UQ__Assets__048A0008616E13CC").IsUnique();

            entity.HasIndex(e => e.DeviceType, "idx_Assets_DeviceType");

            entity.HasIndex(e => e.Status, "idx_Assets_Status");

            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.AssignedToEmployeeId).HasColumnName("AssignedToEmployeeID");
            entity.Property(e => e.DeviceType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Laptop");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Available");

            entity.HasOne(d => d.AssignedToEmployee).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssignedToEmployeeId)
                .HasConstraintName("fk_AssignedToEmployeeID");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF17CF4EB41");

            entity.HasIndex(e => e.Name, "idx_Employees_Name");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HireDate).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });
    }

}