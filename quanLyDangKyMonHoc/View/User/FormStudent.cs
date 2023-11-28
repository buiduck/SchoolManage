using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using quanLyDangKyMonHoc.Model;

namespace quanLyDangKyMonHoc.View.User
{
    public partial class FormStudent : Form
    {
        private SchoolDbContext SchoolDBConText = new SchoolDbContext();
        public FormStudent()
        {
            InitializeComponent();
            var listlopchuadangky = SchoolDBConText.LOPHOCPHAN.Where(x=>x.SINHVIEN.Select(y=>y.MASV)!='1')ToList();




            tableChuaDangKy.DataSource = listlopchuadangky;
        }

        private void panelMain_Click(object sender, EventArgs e)
        {
            FormLoad();
        }

        public void FormLoad()
        {
            var test = SchoolDBConText.LOPHOCPHAN.ToList();
            tableChuaDangKy.DataSource=test;
        }

       
    }
}
