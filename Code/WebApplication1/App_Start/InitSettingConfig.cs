using AuthorizeBll;
using LogUtil;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication1
{
    public class InitSettingConfig
    {
        public static void RegisterInitSettings()
        {
            Settings.InitSetting.Instance.InitHashTypes(AuthorizeService.FetchHashTypes());
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitHashTypes)}");

            Settings.InitSetting.Instance.InitMaxNumberOfBytesInSalt(255);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitMaxNumberOfBytesInSalt)}");

            Settings.InitSetting.Instance.InitCultureCookieName(ConfigurationManager.AppSettings["CultureCookieName"]);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitCultureCookieName)}");

            Settings.InitSetting.Instance.InitCookieLifeSpan(new TimeSpan(1, 0, 0));
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitCookieLifeSpan)}");

            Settings.InitSetting.Instance.InitAuthorizationTokenLifeSpanInSecond(int.Parse((ConfigurationManager.AppSettings["TokenExpiresAfter"])));
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitAuthorizationTokenLifeSpanInSecond)}");

            Settings.InitSetting.Instance.InitAuthCookieName(ConfigurationManager.AppSettings["AuthenticationCookieName"]);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitAuthCookieName)}");

            Settings.InitSetting.Instance.InitPaypalClientID(ConfigurationManager.AppSettings["PaypalClientID"]);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitPaypalClientID)}");

            Settings.InitSetting.Instance.InitPaypalTransferAmount(ConfigurationManager.AppSettings["PaypalTransferAmount"]);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitPaypalTransferAmount)}");

            Settings.InitSetting.Instance.InitTokenCleaningCron(ConfigurationManager.AppSettings["TokenCleaningCron"]);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitTokenCleaningCron)}");

            Settings.InitSetting.Instance.InitJwtSecretBase64(ConfigurationManager.AppSettings["JwtSecretInBase64"]);
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"INITIALIZED: {nameof(Settings.InitSetting.Instance.InitJwtSecretBase64)}");
        }
    }
}