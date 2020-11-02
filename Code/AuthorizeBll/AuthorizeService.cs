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
        public static bool Register(string username, string rawPassword, int hashType)
        {
            try
            {
                string hashSaltBase64 = null;
                string passwordHashBase64 = null;
                Account acc = null;
                byte[] saltBytes;
                System.Security.Cryptography.HMAC hmac;
                switch (hashType)
                {
                    case (int)Enums.HMAC.MD5:
                        hashSaltBase64 = null;
                        using (System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5.Create())
                        {
                            passwordHashBase64 = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        }
                        break;
                    case (int)Enums.HMAC.RIPEMD160:
                        hashSaltBase64 = null;
                        using (System.Security.Cryptography.RIPEMD160 hasher = System.Security.Cryptography.RIPEMD160.Create())
                        {
                            passwordHashBase64 = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        }
                        break;
                    case (int)Enums.HMAC.SHA1:
                        hashSaltBase64 = null;
                        using (System.Security.Cryptography.SHA1 hasher = System.Security.Cryptography.SHA1.Create())
                        {
                            passwordHashBase64 = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        }
                        break;
                    case (int)Enums.HMAC.SHA256:
                        hashSaltBase64 = null;
                        using (System.Security.Cryptography.SHA256 hasher = System.Security.Cryptography.SHA256.Create())
                        {
                            passwordHashBase64 = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        }
                        break;
                    case (int)Enums.HMAC.SHA384:
                        hashSaltBase64 = null;
                        using (System.Security.Cryptography.SHA384 hasher = System.Security.Cryptography.SHA384.Create())
                        {
                            passwordHashBase64 = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        }
                        break;
                    case (int)Enums.HMAC.SHA512:
                        hashSaltBase64 = null;
                        using (System.Security.Cryptography.SHA512 hasher = System.Security.Cryptography.SHA512.Create())
                        {
                            passwordHashBase64 = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        }
                        break;
                    case (int)Enums.HMAC.HMACMD5:
                        saltBytes = new byte[189];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACMD5(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACRIPEMD160:
                        saltBytes = new byte[189];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACRIPEMD160(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA1:
                        saltBytes = new byte[189];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA1(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA256:
                        saltBytes = new byte[189];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA256(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA384:
                        saltBytes = new byte[189];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA384(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA512:
                        saltBytes = new byte[189];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA512(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    default:
                        throw new NotImplementedException();
                }
                acc = new Account()
                {
                    Uname = username,
                    HashSaltBase64 = hashSaltBase64,
                    HashTypeId = hashType,
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
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static IDictionary<int, string> FetchHashTypes()
        {
            IDictionary<int, string> result;
            using (AuthorizeEntities ctx = new AuthorizeEntities())
            {
                result = ctx.HashTypes.OrderBy(x => x.Id).ToDictionary(kvp => kvp.Id, kvp => kvp.Name);
            }
            return result;
        }
    }
}
