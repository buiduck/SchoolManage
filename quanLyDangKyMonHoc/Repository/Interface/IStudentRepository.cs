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
        List<Class> getListClass();
        bool updateStudent(Student sinhvien);
        List<StudentDTO> getListStudentByClassId(int idClass);
        List<StudentDTO> getListStudentByName(string fullNameSearch);
    }
}
