namespace quanLyDangKyMonHoc.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [Key]
        [StringLength(30)]
        public string TENTAIKHOAN { get; set; }

        [StringLength(30)]
        public string TENNGUOIDUNG { get; set; }

        [Required]
        [StringLength(150)]
        public string MATKHAU { get; set; }

        public int? CHUCVU { get; set; }
    }
}
