using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI.WinForms.Helpers.Transitions;
using quanLyDangKyMonHoc.Model;

namespace quanLyDangKyMonHoc.View.User
{
    public partial class FormStudent : Form
    {
        private SchoolDbContext SchoolDBConText = new SchoolDbContext();

        private int Masv=1;
        private int MalopSelected;
        private int MalopUnSelected;
        public FormStudent()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            var listmonhoc = SchoolDBConText.MONHOC.ToList();


            ddMonHoc.DataSource = listmonhoc;
            ddMonHoc.DisplayMember = "TENMH";
            ddMonHoc.ValueMember = "MAMH";
           
            FormLoad();
           
        }

        public void FormLoad()
        {
            
            var listlopdangky = getListDaDangKy(1)
                .ToList()
                .Select(x => new
                {
                    x.MALOPHP,
                    x.TENLOP,
                    Ten = x.GIANGVIEN.HODEM + " " + x.GIANGVIEN.TEN,
                    x.MONHOC.SOTIET,
                    TENMONHOC = x.MONHOC.TENMH,
                    THOIGIAN =x.NGAYBD+"->"+x.NGAYKT
                })
                .ToList();
            tableDangKy.DataSource = listlopdangky; 
            tableDangKy.Columns[0].Visible = false;


        }


        private void tableChuaDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object cellValue = tableChuaDangKy.Rows[e.RowIndex].Cells[0].Value;
                this.MalopSelected = cellValue != null ? (int)cellValue : 0;
                lbtest.Text = cellValue != null ? cellValue.ToString() : string.Empty;
            }
        }

        public IEnumerable<LOPHOCPHAN> getListChuaDangKy(int Masv,int Mamh )
        {
            var listlopchuadangky = SchoolDBConText.LOPHOCPHAN
                .Where(x =>
                    x.SINHVIEN.All(y => y.MASV != Masv)
                    && x.MAMH == Mamh
                    && !SchoolDBConText.SINHVIEN.Any(sv => sv.MASV == Masv && sv.LOPHOCPHAN.Any(lhp => lhp.MAMH == Mamh))
                );
                
               
            return listlopchuadangky;
        }
        public IEnumerable<ClassSchedule> getListDaDangKy(int Masv)
        {
            var listlopdangky = SchoolDBConText.ClassSchedule
                .Where(x =>
                    x.SINHVIEN.Any(y => y.MASV == Masv)
                );
            return listlopdangky;
        }
        
        private void ddMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //FormLoad();
            if (ddMonHoc.SelectedIndex == -1) return;
            var maMonHoc = ((MONHOC)ddMonHoc.SelectedItem)?.MAMH ?? 1;
            TaiDanhSachDaDangKy(maMonHoc);
        }

        private void ddMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {

            //FormLoad();
        }

        public void TaiDanhSachDaDangKy(int maMonHoc)
        {
            var listlopchuadangky = getListChuaDangKy(1, maMonHoc)
                .ToList()
                .Select(x => new
                {
                    x.MALOPHP,
                    x.TENLOP,
                    Ten = x.GIANGVIEN.HODEM + " " + x.GIANGVIEN.TEN,
                    x.MONHOC.SOTIET,
                    Soluong = $"{x.SINHVIEN.Count()}/{x.SOLUONGSV}",
                    x.NGAYBD,
                    x.NGAYKT
                })
                .ToList();
            tableChuaDangKy.DataSource = listlopchuadangky;

            tableChuaDangKy.Columns[0].Visible = false;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            int masvToAdd = Masv; // Thay thế bằng giá trị thực của MASV
            int malophpToAdd = MalopSelected ; // Thay thế bằng giá trị thực của MALOPHP

            var sinhVien = SchoolDBConText.SINHVIEN.Find(masvToAdd);
            var lopHocPhan = SchoolDBConText.LOPHOCPHAN.Find(malophpToAdd);

            // Kiểm tra xem sinh viên và lớp học phần có tồn tại không
            if (sinhVien != null && lopHocPhan != null)
            {
                // Thêm sinh viên vào lớp học phần
                sinhVien.LOPHOCPHAN.Add(lopHocPhan);
                SchoolDBConText.SaveChanges();
                FormLoad();
            }
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            //int masvToRemove = Masv; // Thay thế bằng giá trị thực của MASV
            //int malophpToRemove = MalopUnSelected; // Thay thế bằng giá trị thực của MALOPHP

            //var sinhVien = SchoolDBConText.SINHVIEN.Find(masvToRemove);
            //var lopHocPhan = SchoolDBConText.LOPHOCPHAN.Find(malophpToRemove);

            //// Kiểm tra xem sinh viên và lớp học phần có tồn tại không
            //if (sinhVien != null && lopHocPhan != null)
            //{
            //    // Thêm sinh viên vào lớp học phần
            //    sinhVien.LOPHOCPHAN.Remove(lopHocPhan);
            //    SchoolDBConText.SaveChanges();
            //    FormLoad();
            //}
            int masvToRemove = Masv; // Thay thế bằng giá trị thực của MASV
            int malophpToRemove = MalopUnSelected; // Thay thế bằng giá trị thực của MALOPHP

            var sinhVien = SchoolDBConText.SINHVIEN.Find(masvToRemove);
            var lopHocPhan = SchoolDBConText.LOPHOCPHAN.Find(malophpToRemove);


            // Kiểm tra xem sinh viên và lớp học phần có tồn tại không
            if (sinhVien != null && lopHocPhan != null)
            {
                // Xóa mối quan hệ giữa sinh viên và lớp học phần
                sinhVien.LOPHOCPHAN.Remove(lopHocPhan);
                //lopHocPhan.SINHVIEN.Remove(sinhVien);
                // Lưu thay đổi vào cơ sở dữ liệu
                SchoolDBConText.SaveChanges();

                // Tải lại dữ liệu (nếu cần)
                FormLoad();
            }

            lbtest.Text = "haha";
        }

        private void tableDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object cellValue = tableDangKy.Rows[e.RowIndex].Cells[0].Value;
                this.MalopUnSelected = cellValue != null ? (int)cellValue : 0;
                lbtest.Text = cellValue != null ? cellValue.ToString() : string.Empty;
            }
        }
    }
}
