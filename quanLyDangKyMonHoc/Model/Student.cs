namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int Id { get; set; }

        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public int ClassId { get; set; }

        [StringLength(150)]
        public string PassWord { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        public int RoleId { get; set; }

        public virtual Class Class { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
