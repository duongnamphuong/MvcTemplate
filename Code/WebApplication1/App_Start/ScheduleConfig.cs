using Schedule.SingletonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class ScheduleConfig
    {
        public static void RegisterSchedulers()
        {
            TokenCleaningJobWorker.Instance.Initialize();
            TokenCleaningJobWorker.Instance.Start();
        }
    }
}