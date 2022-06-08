using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings
{
    public class InitSetting
    {
        private static object syncRoot = new Object();
        private static volatile InitSetting instance;
        public static InitSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new InitSetting();
                        }
                    }
                }
                return instance;
            }
        }
        public IDictionary<int, string> HashTypes { get; private set; }
        public void InitHashTypes(IDictionary<int, string> HashTypes)
        {
            this.HashTypes = HashTypes;
        }
        public int MaxNumberOfBytesInSalt { get; private set; }
        public void InitMaxNumberOfBytesInSalt(int maxLengthOfHashSaltBase64)
        {
            MaxNumberOfBytesInSalt = (int)(3 * maxLengthOfHashSaltBase64 / 4 - 1.75);
        }
        public string CultureCookieName { get; private set; }
        public void InitCultureCookieName(string CultureCookieName)
        {
            this.CultureCookieName = CultureCookieName;
        }
        public TimeSpan CookieLifeSpan { get; private set; }
        public void InitCookieLifeSpan(TimeSpan CookieLifeSpan)
        {
            this.CookieLifeSpan = CookieLifeSpan;
        }
        public int AuthorizationTokenLifeSpanInSecond { get; private set; }
        public void InitAuthorizationTokenLifeSpanInSecond(int AuthorizationTokenLifeSpanInSecond)
        {
            this.AuthorizationTokenLifeSpanInSecond = AuthorizationTokenLifeSpanInSecond;
        }
        public string AuthCookieName { get; private set; }
        public void InitAuthCookieName(string AuthenticationCookieName)
        {
            AuthCookieName = AuthenticationCookieName;
        }
        public string PaypalClientID { get; private set; }
        public void InitPaypalClientID(string ClientID)
        {
            PaypalClientID = ClientID;
        }
        public string PaypalTransferAmount { get; private set; }
        public void InitPaypalTransferAmount(string TransferAmount)
        {
            PaypalTransferAmount = TransferAmount;
        }
        public string TokenCleaningCron { get; private set; }
        public void InitTokenCleaningCron(string TokenCleaningCron)
        {
            this.TokenCleaningCron = TokenCleaningCron;
        }
    }
}
