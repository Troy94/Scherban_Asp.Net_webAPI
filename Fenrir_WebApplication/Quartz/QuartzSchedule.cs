namespace Fenrir_WebApplication.Quartz
{
    using System;
    using System.Threading.Tasks;
    using global::Quartz;
    using global::Quartz.Impl;
    using Quartz;

    public class QuartzSchedule
    {
        private IScheduler _scheduler;

        public async Task StartAsync()
        {         
            _scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await _scheduler.Start();      
            
            await ScheduleJobEveryMinute();
            await ScheduleJobEveryMondayAt2PM();
        }

        public async Task StopAsync()
        {
            await _scheduler?.Shutdown();
        }

        private async Task ScheduleJobEveryMinute()
        {
           
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("job1", "group1")
                .Build();

         
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60) // Запускать каждые 60 секунд (1 минута)
                    .RepeatForever())
                .Build();

            // Запускаем задачу с триггером
            await _scheduler.ScheduleJob(job, trigger);
        }

        private async Task ScheduleJobEveryMondayAt2PM()
        {
            IJobDetail job = JobBuilder.Create<MondayJob>()
                .WithIdentity("job2", "group2")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger2", "group2")
                .StartNow()
                .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 14, 0))
                .Build();

            await _scheduler.ScheduleJob(job, trigger);
        }
    }

    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Задача выполнилась в: " + DateTime.Now);
            return Task.CompletedTask;
        }
    }

    public class MondayJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Monday job executed at: " + DateTime.Now);
            return Task.CompletedTask;
        }
    }

}
