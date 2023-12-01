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
            var listSelectGV = schoolDbContext.Teacher.Select(
                x => new
                {
                    Value = x.id,
                    Text = x.FirstName + " " + x.LastName
                })
                .ToList();
            // Gắn sự kiện Click cho nút tìm kiếm
            btTimkiem.Click += BtTimkiem_Click;

            // Gắn sự kiện Click cho nút làm mới
            btReset.Click += BtReset_Click;
        }
        private void LoadData()
        {
            var list = schoolDbContext.ClassSchedule
               .Select(x => new {
                   x.Name,
                   ten = x.Teacher.FirstName + " " + x.Teacher.LastName,
                   TenMonHoc = x.Subject.Name,
                   x.TotalStudent,
                   x.DayStart,
                   x.DayEnd,
               })
               .ToList();
            tbDanhsachlh.DataSource = list;
        }

        //private void tbDanhsachlh_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    int r = e.RowIndex;
        //    if (r >= 0)
        //    {
        //        DataGridViewRow row = tbDanhsachlh.Rows[r];
                
        //        txtTenlop.Text = row.Cells[1].Value.ToString();
        //        txtSoluongsv.Text = row.Cells[3].Value.ToString();

        //        if (DateTime.TryParse(row.Cells[4].Value.ToString(), out DateTime dateValue))
        //        {
        //            DPNgaybd.Value = dateValue;
        //        }
        //        else
        //        {      
        //            DPNgaybd.Value = DateTime.Now; 
        //        }
        //        if (DateTime.TryParse(row.Cells[5].Value.ToString(), out DateTime dateValue1))
        //        {
        //            DPNgaykt.Value = dateValue1;
        //        }
        //        else
        //        {
        //            DPNgaykt.Value = DateTime.Now;
        //        }

            }
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
                    int maLopHoc = Convert.ToInt32(tbDanhsachlh.SelectedRows[0].Cells["MALOPHP"].Value);
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
                    // Lấy tên lớp học của hàng được chọn
                    string tenLopHoc = tbDanhsachlh.SelectedRows[0].Cells["TENLOP"].Value.ToString();
                    // Tìm lớp học trong cơ sở dữ liệu bằng tên lớp
                    var lopHoc = schoolDbContext.ClassSchedule.FirstOrDefault(lop => lop.Name == tenLopHoc);
                    // Kiểm tra xem lớp học có tồn tại hay không
                    if (lopHoc != null)
                    {
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
        private void BtTimkiem_Click(object sender, EventArgs e)
        {
            // Lấy mã lớp từ TextBox
            string maLop = txtTimkiem.Text;

        //    // Kiểm tra xem mã lớp có được nhập hay không
        //    if (string.IsNullOrEmpty(maLop))
        //    {
        //        MessageBox.Show("Vui lòng nhập mã lớp để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

            // Tìm kiếm lớp theo mã
            var lop = schoolDbContext.ClassSchedule.FirstOrDefault(l => l.Id.ToString() == maLop);

        //    if (lop != null)
        //    {
        //        HienThiThongTinLop(lop);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không tìm thấy lớp có mã " + maLop, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}
        //private void BtReset_Click(object sender, EventArgs e)
        //{

            LamMoi();
        }
        private void HienThiThongTinLop(ClassSchedule lop)
        {
            // Hiển thị thông tin lớp trên các điều khiển
            txtTenlop.Text = lop.Name;
            txtSoluongsv.Text = lop.TotalStudent.ToString();
            // Kiểm tra lớp có giá trị ngày bắt đầu
            if (lop.DayStart != null)
            {
                DPNgaybd.Value = DateTime.ParseExact(lop.DayStart.ToString(), "yyyy-MM-dd", null);
            }
            // Kiểm tra lớp có giá trị ngày kết thúc
            if (lop.DayEnd != null)
            {
                DPNgaykt.Value = DateTime.ParseExact(lop.DayEnd.ToString(), "yyyy-MM-dd", null);
            }
        }
    }
}
