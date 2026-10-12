using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bai1c9.Models;

public partial class TiemTapHoaDbContext : DbContext
{
    public TiemTapHoaDbContext()
    {
    }

    public TiemTapHoaDbContext(DbContextOptions<TiemTapHoaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HangHoa> HangHoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(DatabaseHelper.GetConnectionString());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HangHoa>(entity =>
        {
            entity.HasKey(e => e.MaHang).HasName("PK__HangHoa__19C0DB1D70A24DF6");

            entity.ToTable("HangHoa");

            entity.Property(e => e.ConKinhDoanh).HasDefaultValue(true);
            entity.Property(e => e.DonVi).HasMaxLength(20);
            entity.Property(e => e.GiaBan).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.LoaiHang).HasMaxLength(50);
            entity.Property(e => e.NgayNhap).HasColumnType("datetime");
            entity.Property(e => e.TenHang).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
