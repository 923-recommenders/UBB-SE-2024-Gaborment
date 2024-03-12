using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using recommenders_backend.scheduling;

public class FakeScheduler : IScheduler
{
    private readonly ConcurrentDictionary<DateTime, ConcurrentQueue<ScheduledFunction>> scheduledFunctions = new ConcurrentDictionary<DateTime, ConcurrentQueue<ScheduledFunction>>();
    private Thread schedulerThread;
    private volatile bool running;
    private DateTime currentTime;
    private SortedSet<DateTime> executionTimes = new SortedSet<DateTime>();

    public FakeScheduler(DateTime initialTime)
    {
        currentTime = initialTime;
    }

    public DateTime CurrentTime => currentTime;

    public void ScheduleFunction(DateTime executionTime, ScheduledFunction function)
    {
        executionTime = RoundDownDateTime(executionTime, TimeSpan.TicksPerSecond);
        var queue = scheduledFunctions.GetOrAdd(executionTime, _ => new ConcurrentQueue<ScheduledFunction>());
        queue.Enqueue(function);
        executionTimes.Add(executionTime);
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
        schedulerThread.Abort();
    }

    public void SetCurrentTime(DateTime time)
    {
        currentTime = RoundDownDateTime(time, TimeSpan.TicksPerSecond);
    }

    public DateTime RoundDownDateTime(DateTime date, long ticks)
    {
        return new DateTime(date.Ticks - (date.Ticks % ticks), date.Kind);
    }

    private void RunScheduler()
    {
        while (running)
        {
            DateTime nextExecutionTime = GetNextExecutionTime();

            if (nextExecutionTime == DateTime.MaxValue)
            {
                Thread.Sleep(1);
                continue;
            }
            currentTime = nextExecutionTime;
            if (scheduledFunctions.TryGetValue(currentTime, out var queue))
            {
                while (queue.TryDequeue(out var scheduledFunction))
                {
                    scheduledFunction();
                }
                scheduledFunctions.TryRemove(currentTime, out _);
                executionTimes.Remove(currentTime);
            }
        }
    }

    private DateTime GetNextExecutionTime()
    {
        if (executionTimes.Count == 0)
        {
            return DateTime.MaxValue;
        }

        return executionTimes.Min;
    }
}
