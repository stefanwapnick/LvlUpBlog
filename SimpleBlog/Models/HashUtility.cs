using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Infrastructure
{
    public static class HashUtility
    {
        public static string HashPassword(string password){
            return BCrypt.Net.BCrypt.HashPassword(password, 13);
        }
        public static bool VerifyPassword(string inputPassword, string storedPassword){
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword); 
        }
    }
}