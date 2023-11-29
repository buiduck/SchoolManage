using quanLyDangKyMonHoc.Model;
using quanLyDangKyMonHoc.Repository;
using quanLyDangKyMonHoc.Repository.Implement;
using quanLyDangKyMonHoc.View.Admin;
using System;
using System.Windows.Forms;

namespace quanLyDangKyMonHoc.View
{
    public partial class Login : Form
    {
        private readonly IUserRepository _userRepository;
        public string username;
        public string password;
        public Login()
        {
            _userRepository=new UserRepository();
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _userRepository.Login("vdt", "hahahaah");
        }

        private void bunifuTextBox1_OnIconRightClick(object sender, EventArgs e)
        {
            txtUserName.Text = "";
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            username=txtUserName.Text;
            password=txtPassword.Text;
            TAIKHOANS checkLogin=_userRepository.Login(username, password);
            if (true)
            {
                FormHome formHome = new FormHome();
                formHome.ShowDialog();
                this.Dispose();
            }
        }
    }
}
