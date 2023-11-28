namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MONHOC")]
    public partial class MONHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONHOC()
        {
            LOPHOCPHAN = new HashSet<LOPHOCPHAN>();
        }

        [Key]
        public int MAMH { get; set; }

        [StringLength(30)]
        public string TENMH { get; set; }

        public int? SOTIET { get; set; }

        [StringLength(30)]
        public string LOAIMH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOPHOCPHAN> LOPHOCPHAN { get; set; }
    }
}
