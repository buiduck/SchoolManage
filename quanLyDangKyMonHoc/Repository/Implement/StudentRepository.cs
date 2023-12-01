using quanLyDangKyMonHoc.DTO;
using quanLyDangKyMonHoc.Model;
using quanLyDangKyMonHoc.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDangKyMonHoc.Repository.Implement
{ class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext schoolDbContext = new SchoolDbContext();

        public List<Class> getListClass()
        {
           return schoolDbContext.Class.ToList();
        }

        public List<StudentDTO> getListStudent()
        {
            return schoolDbContext.Student.Select(x=>new StudentDTO
            {
                MASV=x.Id,
                HODEM=x.FirstName,
                TEN=x.LastName,
                EMAIL=x.Email,
                NGAYSINH = x.DateOfBirth,
                QUEQUAN = x.Address,
                TENLOP=schoolDbContext.Class.Where(a=>a.Id==x.ClassId).Select(a=>a.Name).FirstOrDefault()
            }).ToList();
        }
        public bool updateStudent(Student student)
        {
            try
            {
                schoolDbContext.Student.AddOrUpdate(student);
                schoolDbContext.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public List<StudentDTO> getListStudentByClassId(int idClass)
        {
            return schoolDbContext.Student
                .Where(x=>x.Id==idClass)
                .OrderBy(x=>x.LastName)
                .Select(x => new StudentDTO
            {
                    MASV = x.Id,
                    HODEM = x.FirstName,
                    TEN = x.LastName,
                    EMAIL = x.Email,
                    NGAYSINH = x.DateOfBirth,
                    QUEQUAN = x.Address,
                    TENLOP = schoolDbContext.Class.Where(a => a.Id == x.ClassId).Select(a => a.Name).FirstOrDefault()
                }).ToList();
        }

        public List<StudentDTO> getListStudentByName(string fullNameSearch)
        {
            string querry= "EXEC searchStudent @fullName";
            object[] parameters = { new SqlParameter("@fullName", fullNameSearch) };
            IEnumerable<Student>result= schoolDbContext.Database.SqlQuery<Student>(querry,parameters);
            return result.Select(x => new StudentDTO
            {
                MASV = x.Id,
                TEN = x.LastName,
                EMAIL = x.Email,
                HODEM = x.FirstName,
                NGAYSINH = x.DateOfBirth,
                QUEQUAN = x.Address,
                TENLOP = schoolDbContext.Class.SingleOrDefault(c => c.Id == x.ClassId).Name
            }).ToList();
        }
    }
}
