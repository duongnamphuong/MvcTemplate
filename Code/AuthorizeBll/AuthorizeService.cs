using AuthorizeDal;
using LogUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizeBll
{
    public static class AuthorizeService
    {
        /// <summary>
        /// Register an account. Returns true if succeed, otherwise return false and write logs.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="rawPassword"></param>
        /// <param name="hashType"></param>
        /// <returns></returns>
        public static bool RegisterUser(string username, string rawPassword, int hashType)
        {
            try
            {
                string hashSaltBase64 = null;
                string passwordHashBase64 = null;
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
                        saltBytes = new byte[Settings.InitSetting.Instance.MaxNumberOfBytesInSalt];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACMD5(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACRIPEMD160:
                        saltBytes = new byte[Settings.InitSetting.Instance.MaxNumberOfBytesInSalt];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACRIPEMD160(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA1:
                        saltBytes = new byte[Settings.InitSetting.Instance.MaxNumberOfBytesInSalt];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA1(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA256:
                        saltBytes = new byte[Settings.InitSetting.Instance.MaxNumberOfBytesInSalt];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA256(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA384:
                        saltBytes = new byte[Settings.InitSetting.Instance.MaxNumberOfBytesInSalt];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA384(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    case (int)Enums.HMAC.HMACSHA512:
                        saltBytes = new byte[Settings.InitSetting.Instance.MaxNumberOfBytesInSalt];
                        new Random().NextBytes(saltBytes);
                        hashSaltBase64 = Convert.ToBase64String(saltBytes);
                        hmac = new System.Security.Cryptography.HMACSHA512(saltBytes);
                        passwordHashBase64 = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(rawPassword)));
                        break;
                    default:
                        throw new NotImplementedException("Unspecified hash type.");
                }
                var acc = new Account()
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
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "Cannot register user", ex);
                return false;
            }
            return true;
        }

        public static IDictionary<int, string> FetchHashTypes()
        {
            try
            {
                using (AuthorizeEntities ctx = new AuthorizeEntities())
                {
                    return ctx.HashTypes.OrderBy(x => x.Id).ToDictionary(kvp => kvp.Id, kvp => kvp.Name);
                }
            }
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "Cannot fetch hash types", ex);
                return null;
            }
        }

        public static bool Login(string username, string password)
        {
            bool isOk = false;
            byte[] saltBytes;
            System.Security.Cryptography.HMAC hmac;
            try
            {
                using (AuthorizeEntities ctx = new AuthorizeEntities())
                {
                    var acc = ctx.Accounts.Where(x => x.Uname == username).FirstOrDefault();
                    if (acc != null)
                    {
                        switch (acc.HashTypeId)
                        {
                            case (int)Enums.HMAC.MD5:
                                using (System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5.Create())
                                {
                                    isOk = acc.PasswordHashBase64.Equals(Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password))));
                                }
                                break;
                            case (int)Enums.HMAC.RIPEMD160:
                                using (System.Security.Cryptography.RIPEMD160 hasher = System.Security.Cryptography.RIPEMD160.Create())
                                {
                                    isOk = acc.PasswordHashBase64.Equals(Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password))));
                                }
                                break;
                            case (int)Enums.HMAC.SHA1:
                                using (System.Security.Cryptography.SHA1 hasher = System.Security.Cryptography.SHA1.Create())
                                {
                                    isOk = acc.PasswordHashBase64.Equals(Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password))));
                                }
                                break;
                            case (int)Enums.HMAC.SHA256:
                                using (System.Security.Cryptography.SHA256 hasher = System.Security.Cryptography.SHA256.Create())
                                {
                                    isOk = acc.PasswordHashBase64.Equals(Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password))));
                                }
                                break;
                            case (int)Enums.HMAC.SHA384:
                                using (System.Security.Cryptography.SHA384 hasher = System.Security.Cryptography.SHA384.Create())
                                {
                                    isOk = acc.PasswordHashBase64.Equals(Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password))));
                                }
                                break;
                            case (int)Enums.HMAC.SHA512:
                                using (System.Security.Cryptography.SHA512 hasher = System.Security.Cryptography.SHA512.Create())
                                {
                                    isOk = acc.PasswordHashBase64.Equals(Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(password))));
                                }
                                break;
                            case (int)Enums.HMAC.HMACMD5:
                                saltBytes = Convert.FromBase64String(acc.HashSaltBase64);
                                hmac = new System.Security.Cryptography.HMACMD5(saltBytes);
                                isOk = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))).Equals(acc.PasswordHashBase64);
                                break;
                            case (int)Enums.HMAC.HMACRIPEMD160:
                                saltBytes = Convert.FromBase64String(acc.HashSaltBase64);
                                hmac = new System.Security.Cryptography.HMACRIPEMD160(saltBytes);
                                isOk = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))).Equals(acc.PasswordHashBase64);
                                break;
                            case (int)Enums.HMAC.HMACSHA1:
                                saltBytes = Convert.FromBase64String(acc.HashSaltBase64);
                                hmac = new System.Security.Cryptography.HMACSHA1(saltBytes);
                                isOk = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))).Equals(acc.PasswordHashBase64);
                                break;
                            case (int)Enums.HMAC.HMACSHA256:
                                saltBytes = Convert.FromBase64String(acc.HashSaltBase64);
                                hmac = new System.Security.Cryptography.HMACSHA256(saltBytes);
                                isOk = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))).Equals(acc.PasswordHashBase64);
                                break;
                            case (int)Enums.HMAC.HMACSHA384:
                                saltBytes = Convert.FromBase64String(acc.HashSaltBase64);
                                hmac = new System.Security.Cryptography.HMACSHA384(saltBytes);
                                isOk = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))).Equals(acc.PasswordHashBase64);
                                break;
                            case (int)Enums.HMAC.HMACSHA512:
                                saltBytes = Convert.FromBase64String(acc.HashSaltBase64);
                                hmac = new System.Security.Cryptography.HMACSHA512(saltBytes);
                                isOk = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))).Equals(acc.PasswordHashBase64);
                                break;
                            default:
                                throw new NotImplementedException("Unspecified hash type.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "Cannot login", ex);
                isOk = false;
            }
            return isOk;
        }

        public static void CleanExpiredTokens()
        {
            try
            {
                using (AuthorizeEntities ctx = new AuthorizeEntities())
                {
                    var tokenToDelete = ctx.TokenIssueds.Where(x => x.ExpireAtUtc <= DateTime.UtcNow);
                    ctx.TokenIssueds.RemoveRange(tokenToDelete);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, "Error during token cleanup.", ex);
            }
        }

        public static void AddTokenIssued(string username, string token, DateTime IssuedAtUtc, DateTime ExpireAtUtc)
        {
            try
            {
                using (AuthorizeEntities ctx = new AuthorizeEntities())
                {
                    Account account = ctx.Accounts.Where(a => a.Uname == username).SingleOrDefault();
                    if (account != null)
                    {
                        account.TokenIssueds.Add(new TokenIssued()
                        {
                            Val = token,
                            IssuedAtUtc = IssuedAtUtc,
                            ExpireAtUtc = ExpireAtUtc
                        });
                        ctx.SaveChanges();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "Error during adding TokenIssued", ex);
            }
        }

        public static void ExtendTokenIssued(string username, string token)
        {
            try
            {
                using (AuthorizeEntities ctx = new AuthorizeEntities())
                {
                    TokenIssued tokenIssued = ctx.TokenIssueds.Where(t => t.Account.Uname == username && t.Val == token).SingleOrDefault();
                    if (tokenIssued != null)
                    {
                        tokenIssued.ExpireAtUtc = DateTime.UtcNow.AddSeconds(Settings.InitSetting.Instance.AuthorizationTokenLifeSpanInSecond);
                        ctx.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "Error during extending TokenIssued", ex);
            }
        }
    }
}
