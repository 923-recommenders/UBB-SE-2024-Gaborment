using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public delegate void ScheduledFunction();


namespace recommenders_backend.scheduling
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Scheduler
    {
        private readonly Dictionary<DateTime, List<ScheduledFunction>> scheduledFunctions = new Dictionary<DateTime, List<ScheduledFunction>>();
        private readonly object lockObject = new object();
        private Thread schedulerThread;

        public void ScheduleFunction(DateTime executionTime, ScheduledFunction function)
        {
            lock (lockObject)
            {
                executionTime = Trim(executionTime, TimeSpan.TicksPerSecond);
                if (!scheduledFunctions.ContainsKey(executionTime)) {
                    scheduledFunctions.Add(executionTime, new List<ScheduledFunction>());
                }
                scheduledFunctions[executionTime].Add(function);
            }
        }

        public void Start()
        {
            schedulerThread = new Thread(RunScheduler);
            schedulerThread.Start();
        }

        public void Stop()
        {
            schedulerThread.Abort();
        }
        public DateTime Trim(DateTime date, long ticks)
        {
            return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
        }
        private void RunScheduler()
        {
            while (true)
            {
                DateTime now = DateTime.Now;
                now = Trim(now, TimeSpan.TicksPerSecond);

                lock (lockObject)
                {

                    if (scheduledFunctions.ContainsKey(now)) {
                        foreach (var scheduledFunction in scheduledFunctions[now])
                        {
                            scheduledFunction();
                        }

                        scheduledFunctions.Remove(now);
                    }
                }

                Thread.Sleep(1000); // Check every second
            }
        }
    }

}
