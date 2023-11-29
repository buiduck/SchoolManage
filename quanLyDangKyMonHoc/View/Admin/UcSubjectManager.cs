using quanLyDangKyMonHoc.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanLyDangKyMonHoc.View.Admin
{
    public partial class UcSubjectManager : UserControl
    {
        SchoolDbContext schoolDbContext = new SchoolDbContext();
        public UcSubjectManager()
        {

            InitializeComponent();
            LoadData();

        }
        private void LoadData()
        {
            var list = schoolDbContext.MONHOC.ToList();
            tbDanhsachmh.DataSource = list;
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

     
        private void tbDanhsachmh_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbDanhsachmh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                DataGridViewRow row = tbDanhsachmh.Rows[r];
                txtTenmh.Text = row.Cells[1].Value.ToString();
                if (int.TryParse(row.Cells[2].Value.ToString(), out int numericValue))
                {
                    numericUpDown1.Value = numericValue;
                }
                else
                {
                    // Xử lý trường hợp giá trị không phải là số                   
                    numericUpDown1.Value = 0;
                }
                txtLoaimh.Text = row.Cells[3].Value.ToString();
            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ TextBox và NumericUpDown
               
                string tenMonHoc = txtTenmh.Text;
                string tenLoaimh = txtLoaimh.Text;
                int soTiet = Convert.ToInt32(numericUpDown1.Value);
                // Kiểm tra xem có thiếu thông tin không
                if ( string.IsNullOrEmpty(tenMonHoc))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

        

                // Tạo một đối tượng MONHOC mới
                MONHOC monHocMoi = new MONHOC
                {
                    TENMH = tenMonHoc,
                    LOAIMH = tenLoaimh,
                    SOTIET = soTiet
                };
                // Thêm môn học vào danh sách và lưu vào cơ sở dữ liệu
                schoolDbContext.MONHOC.Add(monHocMoi);
                schoolDbContext.SaveChanges();
                // Hiển thị thông báo thành công và làm mới danh sách
                MessageBox.Show("Thêm môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy mã môn học từ TextBox
                
               
                // Kiểm tra xem môn học có tồn tại hay không
                //var monHoc = schoolDbContext.MONHOC.FirstOrDefault(mh => mh.MAMH == maMonHoc);
                //if (monHoc == null)
                //{
                //    MessageBox.Show("Mã môn học không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //// Xác nhận việc xóa
                //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (result == DialogResult.Yes)
                //{
                //    // Xóa môn học khỏi danh sách và lưu vào cơ sở dữ liệu
                //    schoolDbContext.MONHOC.Remove(monHoc);
                //    schoolDbContext.SaveChanges();
                //    // Hiển thị thông báo thành công và làm mới danh sách
                //    MessageBox.Show("Xóa môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    LoadData();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            //try
            //{
              
            //    // Kiểm tra xem môn học có tồn tại hay không
               
            //    // Lấy thông tin từ TextBox và NumericUpDown
            //    string tenMonHoc = txtTenmh.Text;
            //    string tenLoaimh = txtLoaimh.Text;
            //    int soTiet = Convert.ToInt32(numericUpDown1.Value);
            //    // Kiểm tra xem có thiếu thông tin không
            //    if (string.IsNullOrEmpty(tenMonHoc))
            //    {
            //        MessageBox.Show("Vui lòng nhập đầy đủ thông tin môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    // Cập nhật thông tin cho môn học
            //    monHoc.TENMH = tenMonHoc;
            //    monHoc.LOAIMH = tenLoaimh;
            //    monHoc.SOTIET = soTiet;
            //    // Lưu thay đổi vào cơ sở dữ liệu
            //    schoolDbContext.SaveChanges();
            //    // Hiển thị thông báo thành công và làm mới danh sách
            //    MessageBox.Show("Cập nhật thông tin môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    LoadData();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        private void ClearData()
        {
            txtTenmh.Text = string.Empty;
            txtLoaimh.Text = string.Empty;
            numericUpDown1.Value = 0;
        }

        private void TimKiemMonHoc(string tenMonHoc)
        {
            var danhSachMonHoc = schoolDbContext.MONHOC
                .Where(mh => mh.TENMH.ToLower().Contains(tenMonHoc.ToLower()))
                .ToList();

            // Hiển thị kết quả tìm kiếm trên DataGridView
            tbDanhsachmh.DataSource = danhSachMonHoc;
        }

        private void btTimkiem_Click(object sender, EventArgs e)
        {
            string tenMonHocTimKiem = txtTimkiem.Text;
            TimKiemMonHoc(tenMonHocTimKiem);
        }
        private void LamMoi()
        {
            // Lấy toàn bộ danh sách môn học
            var danhSachMonHoc = schoolDbContext.MONHOC.ToList();
            // Hiển thị toàn bộ danh sách môn học
            tbDanhsachmh.DataSource = danhSachMonHoc;
            ClearData();
            // Làm mới cả ô tìm kiếm
            txtTimkiem.Text = string.Empty;
        }
        private void btReset_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void btSapxep_Click(object sender, EventArgs e)
        {           
            if (btrGiamdan.Checked)
            {               
                SapXepMonHocGiamDan();
            }
            else if (btrTangdan.Checked)
            {
                SapXepMonHocTangDan();
            }
        }
        private void SapXepMonHocGiamDan()
        {
            var danhSachMonHocGiamDan = schoolDbContext.MONHOC.OrderByDescending(mh => mh.SOTIET).ToList();
            tbDanhsachmh.DataSource = danhSachMonHocGiamDan;
        }
        private void SapXepMonHocTangDan()
        {
            var danhSachMonHocTangDan = schoolDbContext.MONHOC
                .OrderBy(mh => mh.SOTIET)
                .ToList();
            tbDanhsachmh.DataSource = danhSachMonHocTangDan;
        }
    }
}
