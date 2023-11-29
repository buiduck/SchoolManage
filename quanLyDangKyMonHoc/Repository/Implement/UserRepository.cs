﻿using BCrypt.Net;
using quanLyDangKyMonHoc.Model;
using System;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using static quanLyDangKyMonHoc.Repository.ConnectDatabase;


namespace quanLyDangKyMonHoc.Repository.Implement
{
    internal class UserRepository : IUserRepository
    {
        private readonly SchoolDbContext schoolDbContext = new SchoolDbContext();
        public TAIKHOANS Login(string username, string password)
        {
            try
            {
                var user = schoolDbContext.TAIKHOANS.SingleOrDefault(x => x.username == username);

                if (user != null)
                {
                    string passHash = user.password;
                    bool checkLogin = BCrypt.Net.BCrypt.Verify(password, passHash);

                    if (checkLogin)
                    {
                        return user;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error login occurred: {ex.Message}");
                return null;
            }
            finally
            {
                schoolDbContext.Dispose();
            }
        }
        public bool Register(TAIKHOANS taikhoan)
        {
            try
            {
                schoolDbContext.TAIKHOANS.Add(taikhoan);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error register occurred: {ex.Message}");
                return false;
            }
            finally
            {
                schoolDbContext.Dispose();
            }

        }
    }
}