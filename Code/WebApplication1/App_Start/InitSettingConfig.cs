using AuthorizeBll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class InitSettingConfig
    {
        public static void RegisterInitSettings()
        {
            AuthorizeService.FetchHashTypes();
        }
    }
}