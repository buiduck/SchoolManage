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

namespace quanLyDangKySubject.View.Admin
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
            var list = schoolDbContext.Subject.ToList();
            tbDanhsachmh.DataSource = list;
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
               
                string tenSubject = txtTenmh.Text;
                string tenLoaimh = txtLoaimh.Text;
                int soTiet = Convert.ToInt32(numericUpDown1.Value);
                // Kiểm tra xem có thiếu thông tin không
                if ( string.IsNullOrEmpty(tenSubject))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Tạo một đối tượng Subject mới
                Subject SubjectMoi = new Subject
                {
                    Name = tenSubject,
                    TypeOfSubject = tenLoaimh,
                    CourseCredit = soTiet
                };
                // Thêm môn học vào danh sách và lưu vào cơ sở dữ liệu
                schoolDbContext.Subject.Add(SubjectMoi);
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

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tên môn học từ DataGridView
                string tenMonHoc = txtTenmh.Text;

                // Kiểm tra xem môn học đã tồn tại hay không
                var monHoc = schoolDbContext.Subject.FirstOrDefault(mh => mh.Name.ToLower() == tenMonHoc.ToLower());

                if (monHoc == null)
                {
                    MessageBox.Show("Không tìm thấy môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa môn học
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa môn học này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Xóa môn học khỏi danh sách và cơ sở dữ liệu
                    schoolDbContext.Subject.Remove(monHoc);
                    schoolDbContext.SaveChanges();

                    MessageBox.Show("Xóa môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tên môn học từ DataGridView
                string tenMonHoc = txtTenmh.Text;
                // Kiểm tra xem môn học đã tồn tại hay không
                var monHoc = schoolDbContext.Subject.FirstOrDefault(mh => mh.Name.ToLower() == tenMonHoc.ToLower());
                if (monHoc == null)
                {
                    MessageBox.Show("Không tìm thấy môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Lấy thông tin từ các điều khiển
                string loaiMonHoc = txtLoaimh.Text;
                int soTiet = Convert.ToInt32(numericUpDown1.Value);
                // Kiểm tra xem có thiếu thông tin không
                if (string.IsNullOrEmpty(tenMonHoc))
                {
                    MessageBox.Show("Vui lòng nhập tên môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Cập nhật thông tin môn học
                monHoc.TypeOfSubject = loaiMonHoc;
                monHoc.CourseCredit = soTiet;
                // Lưu thay đổi vào cơ sở dữ liệu
                schoolDbContext.SaveChanges();

                // Hiển thị thông báo thành công và làm mới danh sách
                MessageBox.Show("Cập nhật môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearData()
        {
            txtTenmh.Text = string.Empty;
            txtLoaimh.Text = string.Empty;
            numericUpDown1.Value = 0;
        }

        private void TimKiemSubject(string tenSubject)
        {
            var danhSachSubject = schoolDbContext.Subject
                .Where(mh => mh.Name.ToLower().Contains(tenSubject.ToLower()))
                .ToList();

            // Hiển thị kết quả tìm kiếm trên DataGridView
            tbDanhsachmh.DataSource = danhSachSubject;
        }

        private void btTimkiem_Click(object sender, EventArgs e)
        {
            string tenSubjectTimKiem = txtTimkiem.Text;
            TimKiemSubject(tenSubjectTimKiem);
        }
        private void LamMoi()
        {
            // Lấy toàn bộ danh sách môn học
            var danhSachSubject = schoolDbContext.Subject.ToList();
            // Hiển thị toàn bộ danh sách môn học
            tbDanhsachmh.DataSource = danhSachSubject;
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
                SapXepSubjectGiamDan();
            }
            else if (btrTangdan.Checked)
            {
                SapXepSubjectTangDan();
            }
        }
        private void SapXepSubjectGiamDan()
        {
            var danhSachSubjectGiamDan = schoolDbContext.Subject.OrderByDescending(mh => mh.TypeOfSubject).ToList();
            tbDanhsachmh.DataSource = danhSachSubjectGiamDan;
        }
        private void SapXepSubjectTangDan()
        {
            var danhSachSubjectTangDan = schoolDbContext.Subject
                .OrderBy(mh => mh.TypeOfSubject)
                .ToList();
            tbDanhsachmh.DataSource = danhSachSubjectTangDan;
        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
