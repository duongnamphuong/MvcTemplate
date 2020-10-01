using LogUtil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JwtGenerate
{
    public class JwtUtility<T1, T2>
    {
        public T1 Header { get; set; }
        public T2 Payload { get; set; }
        public byte[] Secret { get; private set; }

        public JwtUtility(T1 Header, T2 Payload)
        {
            this.Header = Header;
            this.Payload = Payload;
            Secret = Convert.FromBase64String(ConfigurationManager.AppSettings["JwtSecretInBase64"]);
        }
        public bool IsValid(string token)
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
                T2 payloadObj = JsonConvert.DeserializeObject<T2>(Encoding.ASCII.GetString(Convert.FromBase64String(payloadBase64Text)));
                if (headerObj.HashAlgorithm != "HS256" || headerObj.TokenType != "JWT")
                {
                    Log4netLogger.Warn(MethodBase.GetCurrentMethod().DeclaringType, "Invalid token header");
                    return false;
                }
                return token == (new JwtUtility<JwtHeader, T2>(headerObj, payloadObj)).ToString();
            }
            catch (Exception ex)
            {
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, "Cannot validate token", ex);
                return false;
            }
        }
        public T1 GetHeader(string token)
        {
            string[] parts = token.Split('.');
            string headerBase64Text = parts[0];
            return JsonConvert.DeserializeObject<T1>(Encoding.ASCII.GetString(Convert.FromBase64String(headerBase64Text)));
        }
        public T2 GetPayload(string token)
        {
            string[] parts = token.Split('.');
            string headerBase64Text = parts[0];
            string payloadBase64Text = parts[1];
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
            return JsonConvert.DeserializeObject<T2>(Encoding.ASCII.GetString(Convert.FromBase64String(payloadBase64Text)));
        }
        public override string ToString()
        {
            try
            {
                string headerJsonText = JsonConvert.SerializeObject(Header, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
                byte[] headerBytes = Encoding.ASCII.GetBytes(headerJsonText);
                string header = Convert.ToBase64String(headerBytes).Replace("=", "").Replace("+", "-").Replace("/", "_");

                string payloadJsonText = JsonConvert.SerializeObject(Payload, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
                byte[] payloadBytes = Encoding.ASCII.GetBytes(payloadJsonText);
                string payload = Convert.ToBase64String(payloadBytes).Replace("=", "").Replace("+", "-").Replace("/", "_");

                string unsignedToken = header + "." + payload;

                HMACSHA256 HMACSHA256 = new HMACSHA256(Secret);
                byte[] resultBytes = HMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(unsignedToken));

                string signature = Convert.ToBase64String(resultBytes).Replace("=", "").Replace("+", "-").Replace("/", "_");

                string token = header + "." + payload + "." + signature;

                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class JwtHeader
    {
        [JsonProperty("alg")]
        public string HashAlgorithm
        {
            get { return "HS256"; }
            private set { }
        }

        [JsonProperty("typ")]
        public string TokenType
        {
            get { return "JWT"; }
            private set { }
        }
    }
}
