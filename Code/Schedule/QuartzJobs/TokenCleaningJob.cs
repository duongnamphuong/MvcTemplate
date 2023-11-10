using AuthorizeBll;
using LogUtil;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.QuartzJobs
{
    [DisallowConcurrentExecution]
    class TokenCleaningJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Log4netLogger.Info(MethodBase.GetCurrentMethod().DeclaringType, $"Quartz: {nameof(TokenCleaningJob)} was executed at {DateTime.UtcNow} UTC");
            try
            {
                AuthorizeService.CleanExpiredTokens();
            }
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "cannot cleanup tokens.", ex);
            }
        }
    }
}
