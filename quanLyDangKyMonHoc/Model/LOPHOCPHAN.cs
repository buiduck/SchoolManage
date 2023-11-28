namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOPHOCPHAN")]
    public partial class LOPHOCPHAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOPHOCPHAN()
        {
            SINHVIEN = new HashSet<SINHVIEN>();
        }

        [Key]
        public int MALOPHP { get; set; }

        [StringLength(50)]
        public string TENLOP { get; set; }

        [StringLength(10)]
        public string NGAYBD { get; set; }

        [StringLength(10)]
        public string NGAYKT { get; set; }

        public int? SOLUONGSV { get; set; }

        public int? MAGV { get; set; }

        public int? MAMH { get; set; }

        public virtual GIANGVIEN GIANGVIEN { get; set; }

        public virtual MONHOC MONHOC { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SINHVIEN> SINHVIEN { get; set; }
    }
}
