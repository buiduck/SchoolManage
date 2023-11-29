using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace quanLyDangKyMonHoc.DTO
{
    public class StudentDTO
    {
        [DisplayName("Mã Sinh Viên")]
        public int MASV { get; set; }

        [StringLength(30)]
        [DisplayName("Họ Đệm")]
        public string HODEM { get; set; }

        [StringLength(30)]
        [DisplayName("Tên")]
        public string TEN { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày sinh")]
        public DateTime? NGAYSINH { get; set; }

        [StringLength(30)]
        [DisplayName("Quê quán")]
        public string QUEQUAN { get; set; }

        [StringLength(30)]
        [DisplayName("Email")]
        public string EMAIL { get; set; }
        [StringLength(30)]
        [DisplayName("Lớp ")]
        public string TENLOP{ get; set; }
    }
}
