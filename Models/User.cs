using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public User(string email, byte[] passwordHash, byte[] passwordSalt)
        {
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            CreateDate = DateTime.Now;
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
