using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class HashGenerator
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool IsCorrectPassword(string newPassword, byte[] oldPassword, byte[] passwordSalt)
        {
            using (var hmac=new HMACSHA512(passwordSalt))
            {
                var _computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));

                var _result= _computeHash.SequenceEqual(oldPassword);

                return _result;
            }
        }
    }
}
