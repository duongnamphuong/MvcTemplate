using LogUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JwtGenerate.Test
{
    internal class Test
    {
        private static void Main(string[] args)
        {
            JwtHeader head = new JwtHeader();
            TokenPayload payload = new TokenPayload()
            {
                Username = "administrator",
                IssuedAt = 1422979638,
                BusinessUserID = 1,
                BusinessID = 2,
                AssignedBusinessIDs = new List<int>() { 3, 4, 6 }
            };
            JwtUtility<JwtHeader, TokenPayload> jwtUtil = new JwtUtility<JwtHeader, TokenPayload>(head, payload);
            string token = jwtUtil.ToString();
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, token);

            Console.WriteLine();

            Log4netLogger.Warn(MethodBase.GetCurrentMethod().DeclaringType, Validate(token));

            Console.ReadLine();
        }

        private static bool Validate(string token)
        {
            try
            {
                string[] parts = token.Split('.');
                string headerBase64Text = parts[0];
                string payloadBase64Text = parts[1];

                JwtHeader headerObj = JsonConvert.DeserializeObject<JwtHeader>(Encoding.ASCII.GetString(Convert.FromBase64String(headerBase64Text)));
                int mod4 = payloadBase64Text.Length % 4;
                if (mod4 > 0)
                {
                    if (mod4 == 1)
                    {
                        payloadBase64Text += "===";
                    }
                    else if (mod4 == 2)
                    {
                        payloadBase64Text += "==";
                    }
                    else
                    {
                        payloadBase64Text += "=";
                    }
                }
                TokenPayload payloadObj = JsonConvert.DeserializeObject<TokenPayload>(Encoding.ASCII.GetString(Convert.FromBase64String(payloadBase64Text)));
                if (headerObj.HashAlgorithm != "HS256" || headerObj.TokenType != "JWT")
                {
                    return false;
                }
                return token == (new JwtUtility<JwtHeader, TokenPayload>(headerObj, payloadObj)).ToString();
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, ex.Message, ex);
                return false;
            }
        }
    }
}