using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBackgroundJobService
    {
        public void FireAndForget(Expression<Action> Job);
        public void FireAndForget();
        public void ScheduleRecurringJob(Expression<Action> Job, string CronExpression);

    }
}
