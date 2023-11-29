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

        public virtual DbSet<GIANGVIEN> GIANGVIENs { get; set; }
        public virtual DbSet<LOP> LOPs { get; set; }
        public virtual DbSet<LOPHOCPHAN> LOPHOCPHANs { get; set; }
        public virtual DbSet<MONHOC> MONHOCs { get; set; }
        public virtual DbSet<NGANH> NGANHs { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }
        public virtual DbSet<Temp> Temps { get; set; }

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
                .HasMany(e => e.SINHVIENs)
                .WithMany(e => e.LOPHOCPHANs)
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
