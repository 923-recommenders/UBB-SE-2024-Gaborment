using System.Collections.Concurrent;
using System.Threading;
using System;
using recommenders_backend.scheduling;


public class Scheduler:IScheduler
{
    private readonly ConcurrentDictionary<DateTime, ConcurrentQueue<ScheduledFunction>> scheduledFunctions = new ConcurrentDictionary<DateTime, ConcurrentQueue<ScheduledFunction>>();
    private Thread schedulerThread;
    private volatile bool running;

    public void ScheduleFunction(DateTime executionTime, ScheduledFunction function)
    {
        executionTime = RoundDownDateTime(executionTime, TimeSpan.TicksPerSecond);
        var queue = scheduledFunctions.GetOrAdd(executionTime, _ => new ConcurrentQueue<ScheduledFunction>());
        queue.Enqueue(function);
    }

    public void Start()
    {
        running = true;
        schedulerThread = new Thread(RunScheduler);
        schedulerThread.Start();
    }

    public void Stop()
    {
        running = false;
        schedulerThread.Join();
    }

    public DateTime RoundDownDateTime(DateTime date, long ticks)
    {
        return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
    }

    private void RunScheduler()
    {
        while (running)
        {
            DateTime now = DateTime.Now;
            now = RoundDownDateTime(now, TimeSpan.TicksPerSecond);

            if (scheduledFunctions.TryGetValue(now, out var queue))
            {
                while (queue.TryDequeue(out var scheduledFunction))
                {
                    scheduledFunction();
                }
                scheduledFunctions.TryRemove(now, out _);
            }

            Thread.Sleep(1000);
        }
    }

}
