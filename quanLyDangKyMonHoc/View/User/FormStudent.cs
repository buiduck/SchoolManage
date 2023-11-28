using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanLyDangKyMonHoc.Model;

namespace quanLyDangKyMonHoc.View.User
{
    public partial class FormStudent : Form
    {
        private SchoolDbContext SchoolDBConText = new SchoolDbContext();
        public FormStudent()
        {
            InitializeComponent();
            //var listLopChuaDangKy = SchoolDBConText.Database.SqlQuery<LOPHOCPHAN>("select * from lophocphan").ToList();
            //    .Select(x=> new
            //{
            //    x.MALOPHP,
            //    x.TENLOP,
            //    TenGiangVien = x.GIANGVIEN.TEN,  // Đổi tên trường để tránh lỗi NotSupportedException
            //    TenMonHoc = x.MONHOC.TENMH,
            //    x.SOLUONGSV,
            //    x.NGAYBD,
            //    x.NGAYKT
            //}).ToList();

            var test = SchoolDBConText.LOPHOCPHAN.Where(
                x => x.SINHVIEN.All(y => y.MASV != 1) && x.MONHOC.MAMH==2
                ).ToList();

            tableChuaDangKy.DataSource = test;
        }

        private void panelMain_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        public void FormLoad()
        {
            var test = SchoolDBConText.LOPHOCPHAN.ToList();
            tableChuaDangKy.DataSource=test;
        }
    }
}
