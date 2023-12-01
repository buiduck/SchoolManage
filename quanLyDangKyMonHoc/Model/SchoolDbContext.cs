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

        public virtual DbSet<AcademicYear> AcademicYear { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<ClassSchedule> ClassSchedule { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SchoolYear> SchoolYear { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicYear>()
                .HasMany(e => e.Subject)
                .WithRequired(e => e.AcademicYear)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.AccountName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.Student)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClassSchedule>()
                .HasMany(e => e.Student)
                .WithMany(e => e.ClassSchedule)
                .Map(m => m.ToTable("DetailClassSchedule").MapLeftKey("ClassScheduleId").MapRightKey("StudentId"));

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Class)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Major>()
                .HasMany(e => e.Class)
                .WithRequired(e => e.Major)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Account)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Student)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SchoolYear>()
                .HasMany(e => e.AcademicYear)
                .WithRequired(e => e.SchoolYear)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .HasMany(e => e.ClassSchedule)
                .WithRequired(e => e.Subject)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Teacher>()
                .Property(e => e.NumberPhone)
                .IsFixedLength();

            modelBuilder.Entity<Teacher>()
                .HasMany(e => e.ClassSchedule)
                .WithOptional(e => e.Teacher)
                .HasForeignKey(e => e.TearcherId);
        }
    }
}
