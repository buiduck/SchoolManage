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
    public partial class FormHome : Form
    {
        private UcStudentManager ucStudentManager;
        private UcClassManager ucClassManager;
        private UcSubjectManager ucSubjectManager;
        private UcRegisterSubjectManager ucRegisterSubjectManager;
        public FormHome()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnSudentManager_Click(object sender, EventArgs e)
        {
            LoadUcAction();
        }

        private void btnSubjectManager_Click(object sender, EventArgs e)
        {
            LoadUcAction1();
        }

        private void LoadUcAction()
        {
            if (ucStudentManager == null)
            {
                ucStudentManager = new UcStudentManager();
                {
                    Dock = DockStyle.Fill;
                };
                panelMain.Controls.Add(ucStudentManager);

                ucStudentManager.BringToFront();
            }
            else
            {
                ucStudentManager.BringToFront();
            }
        }
        private void LoadUcAction1()
        {

            if (ucSubjectManager == null)
            {
                ucSubjectManager = new UcSubjectManager();
                {
                    Dock = DockStyle.Fill;
                };

                try
                {
                    panelMain.Controls.Add(ucSubjectManager);
                    ucSubjectManager.BringToFront();
                }
                catch (Exception ex)
                {
                    // Xử lý ngoại lệ ở đây
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                ucSubjectManager.BringToFront();
            }


        }
        private void LoadUcAction2()
        {
            if (ucClassManager == null)
            {
                ucClassManager = new UcClassManager();
                {
                    Dock = DockStyle.Fill;
                };
                panelMain.Controls.Add(ucClassManager);

                ucClassManager.BringToFront();
            }
            else
            {
                ucClassManager.BringToFront();
            }
        }
        
        private void btnClassManager_Click_1(object sender, EventArgs e)
        {
            LoadUcAction2();
        }

        private void FormHome_Load(object sender, EventArgs e)
        {

        }
    }
}
