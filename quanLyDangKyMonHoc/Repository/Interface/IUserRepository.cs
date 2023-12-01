using quanLyDangKyMonHoc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quanLyDangKyMonHoc.Repository.Interface;

namespace quanLyDangKyMonHoc.Repository
{
    internal interface IUserRepository : IBaseRepository<Account>
    {
        //TAIKHOANS Login(string username, string password);
        
    }
}
