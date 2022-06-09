using AuthorizeBll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class InitSettingConfig
    {
        public static void RegisterInitSettings()
        {
            Settings.InitSetting.Instance.InitHashTypes(AuthorizeService.FetchHashTypes());
            Settings.InitSetting.Instance.InitMaxNumberOfBytesInSalt(255);
            Settings.InitSetting.Instance.InitCultureCookieName(ConfigurationManager.AppSettings["CultureCookieName"]);
            Settings.InitSetting.Instance.InitCookieLifeSpan(new TimeSpan(1, 0, 0));
            Settings.InitSetting.Instance.InitAuthorizationTokenLifeSpanInSecond(int.Parse((ConfigurationManager.AppSettings["TokenExpiresAfter"])));
            Settings.InitSetting.Instance.InitAuthCookieName(ConfigurationManager.AppSettings["AuthenticationCookieName"]);
            Settings.InitSetting.Instance.InitPaypalClientID(ConfigurationManager.AppSettings["PaypalClientID"]);
            Settings.InitSetting.Instance.InitPaypalTransferAmount(ConfigurationManager.AppSettings["PaypalTransferAmount"]);
            Settings.InitSetting.Instance.InitTokenCleaningCron(ConfigurationManager.AppSettings["TokenCleaningCron"]);
            Settings.InitSetting.Instance.InitJwtSecretBase64(ConfigurationManager.AppSettings["JwtSecretInBase64"]);
        }
    }
}