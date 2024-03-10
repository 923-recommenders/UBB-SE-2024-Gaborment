using System;
using recommenders_backend.scheduling;

namespace recommenders_backend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Scheduler scheduler = new Scheduler();
            scheduler.Start();

            DateTime executionTime = DateTime.Now.AddSeconds(5);
            scheduler.ScheduleFunction(executionTime, () =>
            {
                Console.WriteLine("Function executed at: " + DateTime.Now);
            });

            DateTime anotherExecutionTime = DateTime.Now.AddSeconds(20);
            scheduler.ScheduleFunction(anotherExecutionTime, () =>
            {
                Console.WriteLine("Another Function executed at: " + DateTime.Now);
            });


            Console.WriteLine("Scheduled function to execute at: " + executionTime);

            Console.WriteLine("Scheduled function to execute at: " + anotherExecutionTime);

            Console.ReadLine();

            scheduler.Stop();
        }
    }
}
