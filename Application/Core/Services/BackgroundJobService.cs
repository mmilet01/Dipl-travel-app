using Core.Interfaces;
using Hangfire;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void FireAndForget()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("ASDASD"));
        }

        public void FireAndForget(Expression<Action> Job)
        {
            BackgroundJob.Enqueue(Job);
        }

        public void ScheduleRecurringJob(Expression<Action> Job, string CronExpression)
        {
            RecurringJob.AddOrUpdate(Job, CronExpression);
        }
    }
}
