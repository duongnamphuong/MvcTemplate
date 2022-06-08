using LogUtil;
using Quartz;
using Quartz.Impl;
using Schedule.QuartzJobs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.SingletonClasses
{
    public class TokenCleaningJobWorker
    {
        #region essential parts of Singleton Pattern
        private static object syncRoot = new Object();
        private static volatile TokenCleaningJobWorker instance;
        public static TokenCleaningJobWorker Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new TokenCleaningJobWorker();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion
        private ISchedulerFactory schedulerFactory { get; set; }
        private IScheduler scheduler { get; set; }
        private IJobDetail job { get; set; }
        private ITrigger trigger { get; set; }
        public bool Initialize()
        {
            bool isSuccessful = true;
            try
            {
                schedulerFactory = new StdSchedulerFactory();
                scheduler = schedulerFactory.GetScheduler();
                scheduler.Start();
                job = JobBuilder.Create<TokenCleaningJob>().WithIdentity("myJob", "group1").Build();
                string cronString = Settings.InitSetting.Instance.TokenCleaningCron;
                trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1").WithCronSchedule(cronString, x => x.InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("UTC"))).Build();
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"cannot initialize", ex);
            }
            return isSuccessful;
        }
        public bool Start()
        {
            bool isSuccessful = true;
            try
            {
                scheduler.ScheduleJob(job, trigger);
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"cannot start", ex);
            }
            return isSuccessful;
        }
        public bool Shutdown()
        {
            bool isSuccessful = true;
            try
            {
                scheduler.Shutdown(true);
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"cannot shutdown", ex);
            }
            return isSuccessful;
        }
        public bool Pause()
        {
            bool isSuccessful = true;
            try
            {
                scheduler.PauseAll();
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"cannot pause", ex);
            }
            return isSuccessful;
        }
        public bool Resume()
        {
            bool isSuccessful = true;
            try
            {
                scheduler.ResumeAll();
            }
            catch (Exception ex)
            {
                isSuccessful = false;
                Log4netLogger.Error(MethodBase.GetCurrentMethod().DeclaringType, $"cannot resume", ex);
            }
            return isSuccessful;
        }
    }
}
