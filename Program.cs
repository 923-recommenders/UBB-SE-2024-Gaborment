using System;
using recommenders_backend.scheduling;

namespace recommenders_backend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime initialTime = new DateTime(2024, 3, 10, 10, 0, 0);
            RunTestScenario(initialTime);
            RunRealScenario();
        }

        static void RunRealScenario() { 
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
                scheduler.ScheduleFunction(DateTime.Now.AddSeconds(5), () =>
                {
                    Console.WriteLine("Nested Function executed at: " + DateTime.Now);
                });
            });


            Console.WriteLine("Scheduled function to execute at: " + executionTime);

            Console.WriteLine("Scheduled function to execute at: " + anotherExecutionTime);

            scheduler.ScheduleFunction(DateTime.Now.AddMonths(1), () =>
            {
                Console.WriteLine("Another Function executed at: " + DateTime.Now);
            });
            scheduler.ScheduleFunction(DateTime.Now.AddSeconds(40), () =>
            {
                Console.WriteLine("Stopped executed at: " + DateTime.Now);
                scheduler.Stop();
            });
            Console.ReadLine();

            scheduler.Stop();
        }
        static void RunTestScenario(DateTime initialTime)
        {
            FakeScheduler scheduler = new FakeScheduler(initialTime);
            scheduler.Start();

            DateTime executionTime = initialTime.AddSeconds(5);
            scheduler.ScheduleFunction(executionTime, () =>
            {
                Console.WriteLine("Function executed at: " + scheduler.CurrentTime);
            });

            DateTime anotherExecutionTime = initialTime.AddSeconds(20);
            scheduler.ScheduleFunction(anotherExecutionTime, () =>
            {
                Console.WriteLine("Another Function executed at: " + scheduler.CurrentTime);
                scheduler.ScheduleFunction(scheduler.CurrentTime.AddSeconds(5), () =>
                {
                    Console.WriteLine("Nested Function executed at: " + scheduler.CurrentTime);
                });
            });

            Console.WriteLine("Scheduled function to execute at: " + executionTime);
            Console.WriteLine("Scheduled function to execute at: " + anotherExecutionTime);

            DateTime stopTime = initialTime.AddSeconds(40);
            scheduler.ScheduleFunction(stopTime, () =>
            {
                Console.WriteLine("Stopped executed at: " + scheduler.CurrentTime);
                scheduler.Stop();
            });

            Console.ReadLine();
        }
    }
}
