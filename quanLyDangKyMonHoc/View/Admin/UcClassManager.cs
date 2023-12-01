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
        //SchoolDbContext schoolDbContext = new SchoolDbContext();
        //public UcClassManager()
        //{
        //    InitializeComponent();
        //    LoadData();
        //    var listSelectGV = schoolDbContext.GIANGVIEN.Select(
        //        x => new
        //        {
        //            Value = x.MAGV,
        //            Text = x.HODEM + " " + x.TEN
        //        })
        //        .ToList();
        //    // Gắn sự kiện Click cho nút tìm kiếm
        //    btTimkiem.Click += BtTimkiem_Click;

        //    // Gắn sự kiện Click cho nút làm mới
        //    btReset.Click += BtReset_Click;
        //}
        //private void LoadData()
        //{
        //    var list = schoolDbContext.LOPHOCPHAN
        //       .Select(x => new {
        //           x.TENLOP,
        //           ten = x.GIANGVIEN.HODEM + " " + x.GIANGVIEN.TEN,
        //           TenMonHoc = x.MONHOC.TENMH,
        //           x.SOLUONGSV,
        //           x.NGAYBD,
        //           x.NGAYKT,
        //       })
        //       .ToList();
        //    tbDanhsachlh.DataSource = list;
        //}

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

        //    }
        //}
        //private void btThem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Lấy thông tin từ các điều khiển
        //        string tenLop = txtTenlop.Text;
        //        if (string.IsNullOrEmpty(tenLop))
        //        {
        //            MessageBox.Show("Vui lòng nhập tên lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        DateTime ngayBD = DPNgaybd.Value;
        //        DateTime ngayKT = DPNgaykt.Value;
        //        int soLuongSV;
        //        if (!int.TryParse(txtSoluongsv.Text, out soLuongSV) || soLuongSV <= 0)
        //        {
        //            MessageBox.Show("Vui lòng nhập số lượng sinh viên hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        int maGV, maMH;
        //        if (!int.TryParse(ddMaGV.SelectedValue?.ToString(), out maGV) || maGV <= 0)
        //        {
        //            MessageBox.Show("Vui lòng chọn mã giảng viên hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        if (!int.TryParse(ddMaMH.SelectedValue?.ToString(), out maMH) || maMH <= 0)
        //        {
        //            MessageBox.Show("Vui lòng chọn mã môn học hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        // Tạo một đối tượng LOPHOCPHAN mới
        //        LOPHOCPHAN lopMoi = new LOPHOCPHAN
        //        {
        //            TENLOP = tenLop,
        //            NGAYBD = ngayBD.ToString("yyyy-MM-dd"), // Định dạng ngày để lưu vào cơ sở dữ liệu
        //            NGAYKT = ngayKT.ToString("yyyy-MM-dd"),
        //            SOLUONGSV = soLuongSV,
        //            MAGV = maGV,
        //            MAMH = maMH
        //        };
        //        schoolDbContext.LOPHOCPHAN.Add(lopMoi);
        //        schoolDbContext.SaveChanges();
        //        MessageBox.Show("Thêm lớp học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
        //private void btSua_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // Kiểm tra xem đã chọn hàng để sửa chưa
        //        if (tbDanhsachlh.SelectedRows.Count > 0)
        //        {
        //            // Lấy mã lớp học của hàng được chọn
        //            int maLopHoc = Convert.ToInt32(tbDanhsachlh.SelectedRows[0].Cells["MALOPHP"].Value);
        //            // Tìm lớp học trong cơ sở dữ liệu
        //            var lopHoc = schoolDbContext.LOPHOCPHAN.Find(maLopHoc);
        //            // Lấy thông tin từ các điều khiển
        //            string tenLop = txtTenlop.Text;
        //            DateTime ngayBD = DPNgaybd.Value;
        //            DateTime ngayKT = DPNgaykt.Value;
        //            int soLuongSV = Convert.ToInt32(txtSoluongsv.Text);
        //            int maGV = Convert.ToInt32(ddMaGV.SelectedValue);
        //            int maMH = Convert.ToInt32(ddMaMH.SelectedValue);
        //            // Kiểm tra xem có thiếu thông tin không
        //            if (string.IsNullOrEmpty(tenLop))
        //            {
        //                MessageBox.Show("Vui lòng nhập tên lớp học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }
        //            // Cập nhật thông tin cho lớp học
        //            lopHoc.TENLOP = tenLop;
        //            lopHoc.NGAYBD = ngayBD.ToString("yyyy-MM-dd");
        //            lopHoc.NGAYKT = ngayKT.ToString("yyyy-MM-dd");
        //            lopHoc.SOLUONGSV = soLuongSV;
        //            lopHoc.MAGV = maGV;
        //            lopHoc.MAMH = maMH;
        //            schoolDbContext.SaveChanges();
        //            MessageBox.Show("Sửa thông tin lớp học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            LoadData();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Vui lòng chọn lớp học để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void btXoa_Click(object sender, EventArgs e)
        //{        
        //        try
        //        {
        //            // Kiểm tra xem đã chọn hàng để xóa chưa
        //            if (tbDanhsachlh.SelectedRows.Count > 0)
        //            {
        //                // Lấy mã lớp học của hàng được chọn
        //                int maLopHoc = Convert.ToInt32(tbDanhsachlh.SelectedRows[0].Cells["MALOPHP"].Value);
        //                // Tìm lớp học trong cơ sở dữ liệu
        //                var lopHoc = schoolDbContext.LOPHOCPHAN.Find(maLopHoc);
        //                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp học này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //                if (result == DialogResult.Yes)
        //                {                
        //                    schoolDbContext.LOPHOCPHAN.Remove(lopHoc);
        //                    schoolDbContext.SaveChanges();                       
        //                    MessageBox.Show("Xóa lớp học thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                    LoadData();
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Vui lòng chọn lớp học để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //private void LamMoi()
        //{
        //    // Xóa nội dung trong TextBox và DateTimePicker
        //    txtTenlop.Text = string.Empty;
        //    txtSoluongsv.Text = string.Empty;
        //    DPNgaybd.Value = DateTime.Now;
        //    DPNgaykt.Value = DateTime.Now;         
        //    LoadData();
        //}
        //private void BtTimkiem_Click(object sender, EventArgs e)
        //{
        //    // Lấy mã lớp từ TextBox
        //    string maLop = txtTimkiem.Text;

        //    // Kiểm tra xem mã lớp có được nhập hay không
        //    if (string.IsNullOrEmpty(maLop))
        //    {
        //        MessageBox.Show("Vui lòng nhập mã lớp để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Tìm kiếm lớp theo mã
        //    var lop = schoolDbContext.LOPHOCPHAN.FirstOrDefault(l => l.MALOPHP.ToString() == maLop);

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

        //    LamMoi();
        //}
        //private void HienThiThongTinLop(LOPHOCPHAN lop)
        //{
        //    // Hiển thị thông tin lớp trên các điều khiển
        //    txtTenlop.Text = lop.TENLOP;
        //    txtSoluongsv.Text = lop.SOLUONGSV.ToString();
        //    DPNgaybd.Value = DateTime.ParseExact(lop.NGAYBD, "yyyy-MM-dd", null);
        //    DPNgaykt.Value = DateTime.ParseExact(lop.NGAYKT, "yyyy-MM-dd", null);
        //}

        //private void bunifuGroupBox2_Enter(object sender, EventArgs e)
        //{

        //}
    }
}
