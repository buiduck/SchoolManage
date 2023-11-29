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
                    Soluong = $"{x.SINHVIEN.Count()}/{x.SOLUONGSV}",
                    THOIGIAN =x.NGAYBD+"->"+x.NGAYKT
                })
                .ToList();
            tableDangKy.DataSource = listlopdangky; 
            tableDangKy.Columns[0].Visible = false;


        }

        private void tableChuaDangKy_Click(object sender, EventArgs e)
        {
            
        }

        private void tableChuaDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                object cellValue = tableChuaDangKy.Rows[e.RowIndex].Cells[0].Value;
                lbtest.Text = cellValue != null ? cellValue.ToString() : string.Empty;
            }
        }

        public IEnumerable<LOPHOCPHAN> getListChuaDangKy(int Masv,int Mamh )
        {
            var listlopchuadangky = SchoolDBConText.LOPHOCPHAN
                .Where(x =>
                    x.SINHVIEN.All(y => y.MASV != Masv)
                    && x.MAMH == Mamh
                );
               
            return listlopchuadangky;
        }
        public IEnumerable<LOPHOCPHAN> getListDaDangKy(int Masv)
        {
            var listlopdangky = SchoolDBConText.LOPHOCPHAN
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
            
        }

     
    }
}
