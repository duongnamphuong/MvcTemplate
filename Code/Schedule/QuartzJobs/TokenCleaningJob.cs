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
            try
            {
                AuthorizeService.CleanExpiredTokens();
            }
            catch (Exception ex)
            {
                Log4netLogger.Fatal(MethodBase.GetCurrentMethod().DeclaringType, "Cannot cleanup tokens.", ex);
            }
        }
    }
}
