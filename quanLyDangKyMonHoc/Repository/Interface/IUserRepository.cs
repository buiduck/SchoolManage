﻿using quanLyDangKyMonHoc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanLyDangKyMonHoc.Repository
{
    internal interface IUserRepository
    {
        TAIKHOAN Login(string username, string password);
        
    }
}
