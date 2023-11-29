using quanLyDangKyMonHoc.DTO;
using quanLyDangKyMonHoc.Model;
using quanLyDangKyMonHoc.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDangKyMonHoc.Repository.Implement
{
    internal class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext schoolDbContext = new SchoolDbContext();

        public List<LOP> getListClass()
        {
           return schoolDbContext.LOPs.ToList();
        }

        public List<StudentDTO> getListStudent()
        {
            return schoolDbContext.SINHVIENs.Select(x=>new StudentDTO
            {
                MASV=x.MASV,
                HODEM=x.HODEM,
                TEN=x.TEN,
                EMAIL=x.EMAIL,
                NGAYSINH = x.NGAYSINH,
                QUEQUAN = x.QUEQUAN,
                TENLOP=schoolDbContext.LOPs.Where(a=>a.MALOP==x.MALOP).Select(a=>a.TenLop).FirstOrDefault()
            }).ToList();
        }
        public bool updateStudent(SINHVIEN sinhvien)
        {
            try
            {
                schoolDbContext.SINHVIENs.AddOrUpdate(sinhvien);
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
            return schoolDbContext.SINHVIENs
                .Where(x=>x.MALOP==idClass)
                .OrderBy(x=>x.TEN)
                .Select(x => new StudentDTO
            {
                MASV = x.MASV,
                HODEM = x.HODEM,
                TEN = x.TEN,
                EMAIL = x.EMAIL,
                NGAYSINH = x.NGAYSINH,
                QUEQUAN = x.QUEQUAN,
                TENLOP = schoolDbContext.LOPs.Where(a => a.MALOP == x.MALOP).Select(a => a.TenLop).FirstOrDefault()
            }).ToList();
            schoolDbContext.SINHVIENs.SqlQuery("");
        }
    }
}
