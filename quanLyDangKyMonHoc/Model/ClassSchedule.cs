namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClassSchedule")]
    public partial class ClassSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClassSchedule()
        {
            Student = new HashSet<Student>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DayStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DayEnd { get; set; }

        public int? TotalStudent { get; set; }

        public int? TearcherId { get; set; }

        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Teacher Teacher { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Student { get; set; }
    }
}
