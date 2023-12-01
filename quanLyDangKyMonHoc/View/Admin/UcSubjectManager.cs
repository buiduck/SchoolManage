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
        private int selectedId ;
        List<MyComboBox> listkihoc = new List<MyComboBox>();
        public UcSubjectManager()
        {

            InitializeComponent();
            LoadData();
            listkihoc = schoolDbContext.AcademicYear.ToList()
            .Select(x => new MyComboBox
            {
                Value = x.Id,
                Text = $"{x.SemesterName} ({(x.SchoolYear.TimeStart.HasValue ? x.SchoolYear.TimeStart.Value.Year : (int?)null)}-{(x.SchoolYear.TimeEnd.HasValue ? x.SchoolYear.TimeEnd.Value.Year : (int?)null)})",
            })
            .OrderByDescending(x => x.Value)
            .ToList();
            ddKihoc.DataSource = listkihoc;
            ddKihoc.DisplayMember = "Text";
            ddKihoc.ValueMember = "Value";

            tbDanhsachmh.CellClick += TbDanhsachmh_CellClick;
        }

        private void TbDanhsachmh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = tbDanhsachmh.SelectedRows[0];
            var kiHocId = int.Parse(row.Cells["AcademicYearId"].Value.ToString());
            ddKihoc.SelectedItem = listkihoc.First(x => x.Value == kiHocId);
        }

        private void LoadData()
        {
            var list = schoolDbContext.Subject
       
             .Select(x => new
             {
                 x.Id,
                 x.Name,
                 x.CourseCredit,
                 x.TypeOfSubject,
                 x.AcademicYearId,
                 AcademicYear = x.AcademicYear.SemesterName,
                 SchoolYearStart = x.AcademicYear.SchoolYear.TimeStart,
                 SchoolYearEnd = x.AcademicYear.SchoolYear.TimeEnd
             })
             .ToList()
             .Select(x => new
             {
                 x.Id,
                 x.Name,
                 x.CourseCredit,
                 x.TypeOfSubject,
                 x.AcademicYearId,
                 kihoc = $"{x.AcademicYear} ({(x.SchoolYearStart.HasValue ? x.SchoolYearStart.Value.Year : (int?)null)}-{(x.SchoolYearEnd.HasValue ? x.SchoolYearEnd.Value.Year : (int?)null)})",
             })
             .ToList();

            tbDanhsachmh.DataSource = list;
            tbDanhsachmh.Columns[0].Visible = false;
            tbDanhsachmh.Columns[1].HeaderText = "Tên môn";
            tbDanhsachmh.Columns[2].HeaderText = "số tín chỉ";
            tbDanhsachmh.Columns[2].HeaderText = "Loại Môn Học";
            tbDanhsachmh.Columns[3].Visible = false;
            tbDanhsachmh.Columns[4].HeaderText = "Kì Học";
            
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

                int soTinChi = Convert.ToInt32(numericUpDown1.Value);
                var makihoc = ((MyComboBox)ddKihoc.SelectedItem).Value;
                // Kiểm tra xem có thiếu thông tin không
                if ( string.IsNullOrEmpty(tenSubject)||string.IsNullOrEmpty(tenLoaimh))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin môn học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                // Tạo một đối tượng Subject mới
                Subject SubjectMoi = new Subject
                {
                    Name = tenSubject,
                    TypeOfSubject = tenLoaimh,
                    CourseCredit = soTinChi,
                    AcademicYearId= makihoc,
                };
                // Thêm môn học vào danh sách và lưu vào cơ sở dữ liệu
                schoolDbContext.Subject.Add(SubjectMoi);
                schoolDbContext.SaveChanges();
                // Hiển thị thông báo thành công và làm mới danh sách
                MessageBox.Show("Thêm môn học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                xoaThongTin();
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
                // Kiểm tra xem đã chọn hàng để xóa chưa
                if (tbDanhsachmh.SelectedRows.Count > 0)
                {
                    var row = tbDanhsachmh.SelectedRows[0];
                    // Lấy tên lớp học của hàng được chọn

                    int id = int.Parse(row.Cells["Id"].Value.ToString());

                    string tenMonhoc = row.Cells["Name"].Value.ToString();
                    // Tìm lớp học trong cơ sở dữ liệu bằng tên lớp
                    var monhoc = schoolDbContext.Subject.FirstOrDefault(lop => lop.Id == id);
                    // Kiểm tra xem lớp học có tồn tại hay không
                    if (monhoc != null)
                    {
                        var soHocSinh = schoolDbContext.Student.Count(x => x.ClassSchedule.Any(y => y.SubjectId == id));
                        if(soHocSinh > 0)
                        {
                            MessageBox.Show($"Lớp đang có {soHocSinh} đăng ký, không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                        // Xóa lớp học từ cơ sở dữ liệu
                        schoolDbContext.Subject.Remove(monhoc);
                        schoolDbContext.SaveChanges();
                        // Hiển thị thông báo xóa thành công và làm mới danh sách
                        MessageBox.Show("Xóa lớp học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lớp học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn lớp học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                var monHoc = schoolDbContext.Subject.FirstOrDefault(mh => mh.Id==selectedId);
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
            LoadData();
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
            var danhSachSubjectGiamDan = schoolDbContext.Subject.OrderByDescending(mh => mh.CourseCredit).ToList();
            tbDanhsachmh.DataSource = danhSachSubjectGiamDan;
        }
        private void SapXepSubjectTangDan()
        {
            var danhSachSubjectTangDan = schoolDbContext.Subject
                .OrderBy(mh => mh.CourseCredit)
                .ToList();
            tbDanhsachmh.DataSource = danhSachSubjectTangDan;
        }

        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void xoaThongTin()
        {
            txtTenmh.Text = "";
            txtLoaimh.Text = "";
            numericUpDown1.Value = 0;     
            ddKihoc.SelectedIndex = -1;
        }

    }
}
