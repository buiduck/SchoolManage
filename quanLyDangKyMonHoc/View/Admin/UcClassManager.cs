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
    public partial class UcClassManager : UserControl
    {
        SchoolDbContext schoolDbContext = new SchoolDbContext();
        public UcClassManager()
        {
            InitializeComponent();
            LoadData();
            //
            var listSelectGV = schoolDbContext.Teacher.Select(
                x => new
                {
                    Value = x.id,
                    Text = x.FirstName + " " + x.LastName
                   
                })
                .ToList();
              ddMaGV.DataSource = listSelectGV;
              ddMaGV.DisplayMember= "Text";
            ddMaGV.ValueMember = "Value";
            //
            var listSelectMH = schoolDbContext.Subject.Select(
                x => new
                {
                    Value = x.Id,
                    Text = x.Name

                })
                .ToList();
            ddMaMH.DataSource = listSelectMH;
            ddMaMH.DisplayMember = "Text";
            ddMaMH.ValueMember = "Value";
            

                // Gắn sự kiện Click cho nút làm mới
               btReset.Click += BtReset_Click;

        }

        private void LoadData()
        {

            var list = schoolDbContext.ClassSchedule
               .Select(x => new {
                   x.Id,
                   x.Name,                  
                   ten = x.Teacher.FullName,
                   TenMonHoc = x.Subject.Name,
                   x.TotalStudent,
                   x.DayStart,
                   x.DayEnd,
                   
                 
               })
               .ToList();
            tbDanhsachlh.DataSource = list;
           // tbDanhsachlh.Columns[1].HeaderText = "Tên lớp";
          //  tbDanhsachlh.Columns[0].Visible= false;
        }

        private void tbDanhsachlh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
            
        private void btThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin từ các điều khiển
                string tenLop = txtTenlop.Text;
                if (string.IsNullOrEmpty(tenLop))
                {
                    MessageBox.Show("Vui lòng nhập tên lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DateTime ngayBD = DPNgaybd.Value;
                DateTime ngayKT = DPNgaykt.Value;
                int soLuongSV;
                if (!int.TryParse(txtSoluongsv.Text, out soLuongSV) || soLuongSV <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng sinh viên hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int maGV, maMH;
                if (!int.TryParse(ddMaGV.SelectedValue?.ToString(), out maGV) || maGV <= 0)
                {
                    MessageBox.Show("Vui lòng chọn mã giảng viên hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(ddMaMH.SelectedValue?.ToString(), out maMH) || maMH <= 0)
                {
                    MessageBox.Show("Vui lòng chọn mã môn học hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Tạo một đối tượng LOPHOCPHAN mới
                ClassSchedule lopMoi = new ClassSchedule
                {
                    Name = tenLop,
                    DayStart = ngayBD,
                    DayEnd = ngayKT,
                    TotalStudent = soLuongSV,
                    TearcherId = maGV,
                    SubjectId = maMH
                };
                schoolDbContext.ClassSchedule.Add(lopMoi);
                schoolDbContext.SaveChanges();
                MessageBox.Show("Thêm lớp học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
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
                // Kiểm tra xem đã chọn hàng để sửa chưa
                if (tbDanhsachlh.SelectedRows.Count > 0)
                {
                    // Lấy mã lớp học của hàng được chọn
                    int maLopHoc = Convert.ToInt32(tbDanhsachlh.SelectedRows[0].Cells["Id"].Value);
                    // Tìm lớp học trong cơ sở dữ liệu
                    var lopHoc = schoolDbContext.ClassSchedule.Find(maLopHoc);
                    // Lấy thông tin từ các điều khiển
                    string tenLop = txtTenlop.Text;
                    DateTime ngayBD = DPNgaybd.Value;
                    DateTime ngayKT = DPNgaykt.Value;
                    int soLuongSV = Convert.ToInt32(txtSoluongsv.Text);
                    int maGV = Convert.ToInt32(ddMaGV.SelectedValue);
                    int maMH = Convert.ToInt32(ddMaMH.SelectedValue);
                    // Kiểm tra xem có thiếu thông tin không
                    if (string.IsNullOrEmpty(tenLop))
                    {
                        MessageBox.Show("Vui lòng nhập tên lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // Cập nhật thông tin cho lớp học
                    lopHoc.Name = tenLop;
                    lopHoc.DayStart = ngayBD;
                    lopHoc.DayEnd = ngayKT;
                    lopHoc.TotalStudent = soLuongSV;
                    lopHoc.TearcherId = maGV;
                    lopHoc.SubjectId = maMH;
                    schoolDbContext.SaveChanges();
                    MessageBox.Show("Sửa thông tin lớp học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn lớp học để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                if (tbDanhsachlh.SelectedRows.Count > 0)
                {
                    var row = tbDanhsachlh.SelectedRows[0];
                    // Lấy tên lớp học của hàng được chọn

                    int id = int.Parse(row.Cells["Id"].Value.ToString());

                    string tenLopHoc = row.Cells["TenMonHoc"].Value.ToString();
                    // Tìm lớp học trong cơ sở dữ liệu bằng tên lớp
                    var lopHoc = schoolDbContext.ClassSchedule.FirstOrDefault(lop => lop.Id == id);
                    // Kiểm tra xem lớp học có tồn tại hay không
                    if (lopHoc != null)
                    {
                        var soHocSinh = schoolDbContext.Student.Count(x => x.ClassSchedule.Any(y => y.SubjectId == id));
                        if (soHocSinh > 0)
                        {
                            MessageBox.Show($"Lớp đang có {soHocSinh} đăng ký, không thể xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;

                        }
                        // Xóa lớp học từ cơ sở dữ liệu
                        schoolDbContext.ClassSchedule.Remove(lopHoc);
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
        private void LamMoi()
        {
            // Xóa nội dung trong TextBox và DateTimePicker
            txtTenlop.Text = string.Empty;
            txtSoluongsv.Text = string.Empty;
            DPNgaybd.Value = DateTime.Now;
            DPNgaykt.Value = DateTime.Now;
            LoadData();
        }
    
       private void BtReset_Click(object sender, EventArgs e)
        {
            LamMoi();
        }



        private void TimKiemClass(string tenClass)
        {
            var danhSachClass = schoolDbContext.ClassSchedule
                .Where(lh => lh.Name.ToLower().Contains(tenClass.ToLower()))
                .ToList();

            // Hiển thị kết quả tìm kiếm trên DataGridView
            tbDanhsachlh.DataSource = danhSachClass;
        }
        private void btTimkiem_Click_1(object sender, EventArgs e)
        {
            string tenClassTimKiem = txtTimkiem.Text;
            TimKiemClass(tenClassTimKiem);
        }

        private void tbDanhsachlh_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0)
            {
                DataGridViewRow row = tbDanhsachlh.Rows[r];

                txtTenlop.Text = row.Cells[1].Value.ToString();
                txtSoluongsv.Text = row.Cells[4].Value.ToString();

                if (DateTime.TryParse(row.Cells[5].Value.ToString(), out DateTime dateValue))
                {
                    DPNgaybd.Value = dateValue;
                }
                else
                {
                    DPNgaybd.Value = DateTime.Now;
                }
                if (DateTime.TryParse(row.Cells[6].Value.ToString(), out DateTime dateValue1))
                {
                    DPNgaykt.Value = dateValue1;
                }
                else
                {
                    DPNgaykt.Value = DateTime.Now;
                }
          
            }
        }

        private void bunifuGroupBox1_Enter(object sender, EventArgs e)
        {

        }

    }
}
