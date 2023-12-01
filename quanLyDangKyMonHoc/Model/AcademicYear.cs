namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AcademicYear")]
    public partial class AcademicYear
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AcademicYear()
        {
            Subject = new HashSet<Subject>();
        }

        public int SchoolYearId { get; set; }

        [Required]
        [StringLength(50)]
        public string SemesterName { get; set; }

        public int Id { get; set; }

        public virtual SchoolYear SchoolYear { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subject> Subject { get; set; }
    }
}
