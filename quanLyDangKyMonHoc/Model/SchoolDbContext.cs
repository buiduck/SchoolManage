using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace quanLyDangKyMonHoc.Model
{
    public partial class SchoolDbContext : DbContext
    {
        public SchoolDbContext()
            : base("name=SchoolDbContext")
        {
        }

        public virtual DbSet<GIANGVIEN> GIANGVIEN { get; set; }
        public virtual DbSet<LOP> LOP { get; set; }
        public virtual DbSet<LOPHOCPHAN> LOPHOCPHAN { get; set; }
        public virtual DbSet<MONHOC> MONHOC { get; set; }
        public virtual DbSet<NGANH> NGANH { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIEN { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOAN { get; set; }
        public virtual DbSet<Temp> Temp { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GIANGVIEN>()
                .Property(e => e.SDT)
                .IsFixedLength();

            modelBuilder.Entity<LOPHOCPHAN>()
                .Property(e => e.NGAYBD)
                .IsFixedLength();

            modelBuilder.Entity<LOPHOCPHAN>()
                .Property(e => e.NGAYKT)
                .IsFixedLength();

            modelBuilder.Entity<LOPHOCPHAN>()
                .HasMany(e => e.SINHVIEN)
                .WithMany(e => e.LOPHOCPHAN)
                .Map(m => m.ToTable("ctLopHocPhanAndSinhVien").MapLeftKey("MALOPHP").MapRightKey("MASV"));

            modelBuilder.Entity<SINHVIEN>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<SINHVIEN>()
                .Property(e => e.MATKHAU)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.TENTAIKHOAN)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.MATKHAU)
                .IsUnicode(false);
        }
    }
}
