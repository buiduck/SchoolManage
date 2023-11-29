using quanLyDangKyMonHoc.DTO;
using quanLyDangKyMonHoc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDangKyMonHoc.Repository.Interface
{
    internal interface IStudentRepository
    {
        List<StudentDTO> getListStudent();
        List<LOP> getListClass();
        bool updateStudent(SINHVIEN sinhvien);
        List<StudentDTO> getListStudentByClassId(int idClass);
    }
}
