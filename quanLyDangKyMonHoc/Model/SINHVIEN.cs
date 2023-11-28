namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SINHVIEN")]
    public partial class SINHVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SINHVIEN()
        {
            LOPHOCPHAN = new HashSet<LOPHOCPHAN>();
        }

        [Key]
        public int MASV { get; set; }

        [StringLength(30)]
        public string HODEM { get; set; }

        [StringLength(30)]
        public string TEN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYSINH { get; set; }

        [StringLength(30)]
        public string QUEQUAN { get; set; }

        [StringLength(30)]
        public string EMAIL { get; set; }

        public int? MALOP { get; set; }

        [StringLength(150)]
        public string MATKHAU { get; set; }

        public virtual LOP LOP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOPHOCPHAN> LOPHOCPHAN { get; set; }
    }
}
