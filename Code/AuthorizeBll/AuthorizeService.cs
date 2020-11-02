using AuthorizeDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeBll
{
    public static class AuthorizeService
    {
        public static bool Register(string username, string rawPassword)
        {
            try
            {
                byte[] saltBytes = new byte[189];
                new Random().NextBytes(saltBytes);
                string hashSaltBase64 = Convert.ToBase64String(saltBytes);
                var hmac = new System.Security.Cryptography.HMACSHA256(saltBytes);
                string passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));

                Account acc = new Account()
                {
                    Uname = username,
                    HashSaltBase64 = hashSaltBase64,
                    HashTypeId = (int)Enums.HMAC.HMACSHA256,
                    PasswordHashBase64 = passwordHashBase64,
                    IsTwoFactor = false,
                    TwoFactorSecretBase32 = null
                };
                using (AuthorizeEntities ctx = new AuthorizeEntities())
                {
                    ctx.Accounts.Add(acc);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
