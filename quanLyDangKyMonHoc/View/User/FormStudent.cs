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
           
        }

        public void FormLoad()
        {
            
          


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

        
        private void ddMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ddMonHoc_SelectedValueChanged(object sender, EventArgs e)
        {

            //FormLoad();
        }

        public void TaiDanhSachDaDangKy(int maMonHoc)
        {
            
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
           
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
