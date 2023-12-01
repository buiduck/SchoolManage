using quanLyDangKyMonHoc.DTO;
using quanLyDangKyMonHoc.Model;
using quanLyDangKyMonHoc.Repository.Implement;
using quanLyDangKyMonHoc.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using ComboBox = System.Windows.Forms.ComboBox;

namespace quanLyDangKyMonHoc.View.Admin
{
    public partial class UcStudentManager : UserControl
    {
        private IStudentRepository studentRepository;
        private List<StudentDTO> studentDTOList;
        private List<LOP> listClass;
        private bool showSetData = false;
        public UcStudentManager()
        {
            studentRepository = new StudentRepository();
            studentDTOList = new List<StudentDTO>();
            listClass = studentRepository.getListClass();
            InitializeComponent();
        }

        private void UcStudentManager_Load(object sender, EventArgs e)
        {
            addComboBoxChoiseClass();
            addDataToTable(studentDTOList);
            setEnable(showSetData);
        }
        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addComboBoxPropertiesStudent(cbLop);
            DataGridViewRow selectedRow = dtTable.SelectedRows[0];
            if (selectedRow != null)
            {
                txtMasv.Text = selectedRow.Cells[0].Value.ToString();
                txtTenSv.Text = selectedRow.Cells[1].Value.ToString();
                txtName.Text = selectedRow.Cells[2].Value.ToString();
                dpNgaySinh.Value = Convert.ToDateTime(selectedRow.Cells[3].Value);
                txtQueQuan.Text = selectedRow.Cells[4].Value.ToString();
                txtEmail.Text = selectedRow.Cells[5].Value.ToString();
                string nameClass = selectedRow.Cells[6].Value.ToString();
                for (int i = 0; i < listClass.Count; i++)
                {
                    if (listClass[i].TenLop.Equals(nameClass))
                    {
                        cbLop.SelectedIndex = i;
                        break;
                    }
                }
            }
            setEnable(!showSetData);
        }

        private void btlUpDate_Click(object sender, EventArgs e)
        {
            if (checkValidation())
            {
                Student sv = new Student();
                sv.MASV = int.Parse(txtMasv.Text);
                sv.TEN = txtName.Text;
                sv.HODEM = txtTenSv.Text;
                sv.QUEQUAN = txtQueQuan.Text;
                sv.EMAIL = txtEmail.Text;
                sv.NGAYSINH = dpNgaySinh.Value;
                LOP lopSelected = cbLop.SelectedItem as LOP;
                if (lopSelected != null)
                {
                    sv.MALOP = lopSelected.MALOP;
                }
                try
                {
                studentRepository.updateStudent(sv);
                dtTable.DataSource = studentRepository.getListStudentByClassId(getIsClassByNameClass(listClass, cbClassShowView.SelectedItem.ToString()));
                setEnable(showSetData);
                setNullDataBoxProperties();
                    MessageBox.Show($"Cập nhật thông tin thông tin sinh viên MSV-{sv.MASV}!!!", "Thông báo");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Cập nhật thông tin thất bại !!!", "Lỗi");
                }
                
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchContent = txtSearch.Text.ToString();

        }


        private void cbClassShowView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbClassShowView.SelectedIndex == 0)
                addDataToTable(new List<StudentDTO>());
            else
            {
                string nameClass = cbClassShowView.SelectedItem.ToString();
                listClass.ForEach(x =>
                {
                    if (x.TenLop.ToString().Equals(nameClass))
                    {
                        addDataToTable(studentRepository.getListStudentByClassId(x.MALOP));
                    }
                });

            }


        }  
        private void rbASC_CheckedChanged(object sender, EventArgs e)
        {
            studentDTOList = getDataInDataGridView(dtTable);
            studentDTOList = studentDTOList.OrderBy(x => x.TEN).ToList();
            addDataToTable(studentDTOList);
        } 
        private void rbDESC_CheckedChanged(object sender, EventArgs e)
        {
            studentDTOList = getDataInDataGridView(dtTable);
            studentDTOList = studentDTOList.OrderByDescending(x => x.TEN).ToList();
            addDataToTable(studentDTOList);
        }



        public bool checkValidation()
        {
            if (
                    txtName.Text.ToString().Equals("") ||
                    txtTenSv.Text.ToString().Equals("") ||
                    txtQueQuan.Text.ToString().Equals("") ||
                    txtEmail.Text.ToString().Equals(""))
            {MessageBox.Show("Dữ liệu không được để trống !!!", "Cảnh báo !!!!!");
                return false;

            }
            if (!IsValidEmail(txtEmail.Text.ToString()))
            {
                MessageBox.Show("Email không đúng định dạng !!!", "Cảnh báo !!!!!");
                return false;
            }
            return true;
        }
        public void addComboBoxPropertiesStudent(ComboBox combo)
        {
            cbLop.DataSource = listClass;
            cbLop.DisplayMember = "TenLop";
            cbLop.SelectedIndex = 0;
        }
        public void addComboBoxChoiseClass()
        {
            List<string> list = new List<string>();
            list.Add("--Vui lòng chọn lớp học--");
            listClass.ForEach(x => list.Add(x.TenLop.ToString()));
            cbClassShowView.DataSource = list;
            listClass = studentRepository.getListClass();
            cbClassShowView.SelectedIndex = 0;
        }
        public void addDataToTable(List<StudentDTO> studentList)
        {
            dtTable.DataSource = studentList;
        }




        public List<StudentDTO> getDataInDataGridView(DataGridView dataGridView)
        {
            List<StudentDTO> listStudentDTO = new List<StudentDTO>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                listStudentDTO.Add(new StudentDTO()
                {
                    MASV = int.Parse(row.Cells[0].Value.ToString()),
                    HODEM = row.Cells[1].Value.ToString(),
                    TEN = row.Cells[2].Value.ToString(),
                    NGAYSINH = Convert.ToDateTime(row.Cells[3].Value),
                    QUEQUAN = row.Cells[4].Value.ToString(),
                    EMAIL = row.Cells[5].Value.ToString(),
                    TENLOP = row.Cells[6].Value.ToString(),

                });
            }
            return listStudentDTO;
        }
        public int getIsClassByNameClass(List<LOP> listClass, string nameClass)
        {
            var foundClass = listClass.FirstOrDefault(x => x.TenLop == nameClass);

            if (foundClass != null)
            {
                return foundClass.MALOP;
            }
            return 0;
        }
        public void setEnable(bool isShow)
        {
            txtEmail.Enabled = isShow;
            txtName.Enabled = isShow;
            txtName.Enabled = isShow;
            txtTenSv.Enabled = isShow;
            txtQueQuan.Enabled = isShow;
            dpNgaySinh.Enabled = isShow;
            cbLop.Enabled = isShow;
            btlUpDate.Enabled = isShow;
        }


        public void setNullDataBoxProperties()
        {
            txtMasv.Text = "";
            txtTenSv.Text = "";
            txtName.Text = "";
            dpNgaySinh.Value = DateTime.Now;
            txtQueQuan.Text = "";
            txtEmail.Text = "";
            cbLop.DataSource = null;
        }
        public bool IsValidEmail(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(emailPattern);
            return regex.IsMatch(email);
        }

    }
}
