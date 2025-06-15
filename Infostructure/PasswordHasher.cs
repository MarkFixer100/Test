using Application.interfaces;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infostructure
{


    public class PasswordHasher:IPasswordHasher
    {
     

        public string Generate (string password) =>
               BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool VerifyPassword(string password, string hashedPassword) =>
               BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);        
        
    }
}
