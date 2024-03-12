using System;

public delegate void ScheduledFunction();
namespace recommenders_backend.scheduling
{
    public interface IScheduler
    {
        void ScheduleFunction(DateTime executionTime, ScheduledFunction function);

        void Start();

        void Stop();
    }

}
