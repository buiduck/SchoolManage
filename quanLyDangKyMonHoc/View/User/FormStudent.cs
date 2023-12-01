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
            var listmonhoc = SchoolDBConText.Subject.ToList();


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
                    x.Id,
                    x.Name,
                    Ten = x.Teacher.FullName,
                    x.Subject.CourseCredit,
                    TENMONHOC = x.Subject.Name,
                    THOIGIAN =x.DayStart+"->"+x.DayEnd
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

        public IEnumerable<ClassSchedule> getListChuaDangKy(int Masv,int Mamh )
        {
            var listlopchuadangky = SchoolDBConText.ClassSchedule
                .Where(x =>
                    x.Student.All(y => y.Id != Masv)
                    && x.SubjectId == Mamh
                    && !SchoolDBConText.Student.Any(sv => sv.Id == Masv && sv.ClassSchedules.Any(lhp => lhp.Id == Mamh))
                );
                
               
            return listlopchuadangky;
        }
        public IEnumerable<ClassSchedule> getListDaDangKy(int Masv)
        {
            var listlopdangky = SchoolDBConText.ClassSchedule
                .Where(x =>
                    x.Student.Any(y => y.Id == Masv)
                );
            return listlopdangky;
        }
        
        private void ddMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //FormLoad();
            if (ddMonHoc.SelectedIndex == -1) return;
            var maMonHoc = ((Subject)ddMonHoc.SelectedItem)?.Id ?? 1;
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
                    x.Id,
                    x.Name,
                    Ten = x.Teacher.FullName,
                    x.Subject.Name,
                    Soluong = $"{x.Student.Count()}/{x.TotalStudent}",
                    x.DayStart,
                    x.DayEnd
                })
                .ToList();
            tableChuaDangKy.DataSource = listlopchuadangky;

            tableChuaDangKy.Columns[0].Visible = false;
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            int masvToAdd = Masv; // Thay thế bằng giá trị thực của MASV
            int malophpToAdd = MalopSelected ; // Thay thế bằng giá trị thực của MALOPHP

            var Student = SchoolDBConText.Student.Find(masvToAdd);
            var ClassSchedule = SchoolDBConText.ClassSchedule.Find(malophpToAdd);

            // Kiểm tra xem sinh viên và lớp học phần có tồn tại không
            if (Student != null && ClassSchedule != null)
            {
                // Thêm sinh viên vào lớp học phần
                Student.ClassSchedules.Add(ClassSchedule);
                SchoolDBConText.SaveChanges();
                FormLoad();
            }
        }

        private void btnHuyDangKy_Click(object sender, EventArgs e)
        {
            //int masvToRemove = Masv; // Thay thế bằng giá trị thực của MASV
            //int malophpToRemove = MalopUnSelected; // Thay thế bằng giá trị thực của MALOPHP

            //var Student = SchoolDBConText.Student.Find(masvToRemove);
            //var ClassSchedule = SchoolDBConText.ClassSchedule.Find(malophpToRemove);

            //// Kiểm tra xem sinh viên và lớp học phần có tồn tại không
            //if (Student != null && ClassSchedule != null)
            //{
            //    // Thêm sinh viên vào lớp học phần
            //    Student.ClassSchedule.Remove(ClassSchedule);
            //    SchoolDBConText.SaveChanges();
            //    FormLoad();
            //}
            int masvToRemove = Masv; // Thay thế bằng giá trị thực của MASV
            int malophpToRemove = MalopUnSelected; // Thay thế bằng giá trị thực của MALOPHP

            var Student = SchoolDBConText.Student.Find(masvToRemove);
            var ClassSchedule = SchoolDBConText.ClassSchedule.Find(malophpToRemove);


            // Kiểm tra xem sinh viên và lớp học phần có tồn tại không
            if (Student != null && ClassSchedule != null)
            {
                // Xóa mối quan hệ giữa sinh viên và lớp học phần
                Student.ClassSchedules.Remove(ClassSchedule);
                //ClassSchedule.Student.Remove(Student);
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

        private void FormStudent_Load(object sender, EventArgs e)
        {

        }

      
    }
}
