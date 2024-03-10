using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public enum LogLevel
{
    INFO,
    WARNING,
    ERROR,
    CRITICAL
}

public class LoggerHelper
{
    public static String GetTodayDate()
    {
        String CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
        Console.WriteLine(CurrentDate);
        return "C:\\Users\\Theo\\source\\repos\\923-recommenders\\recommenders-backend\\Logger\\" +
            CurrentDate + ".txt";
    }
}

public class Logger
{
    private static readonly string LogFilePath = LoggerHelper.GetTodayDate();
    private static readonly object LockObject = new object();
    private static readonly List<string> InfoBuffer = new List<string>();
    private static readonly Timer FlushTimer = new Timer(FlushInfoBuffer, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

    public static void Log(LogLevel level, string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ {level} ] {message}";

        lock (LockObject)
        {
            if (level == LogLevel.INFO)
            {
                InfoBuffer.Add(logEntry);
                Console.WriteLine(message);
            }
            else
            {
                Console.WriteLine($"[ {level} ] {message}");
                WriteToLogFile(logEntry);
            }
        }
    }
    private static void WriteToLogFile(string logEntry)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }

    private static void FlushInfoBuffer(object state)
    {
        lock (LockObject)
        {
            foreach (var entry in InfoBuffer)
            {
                WriteToLogFile(entry);
                Console.Write(entry.ToString());
            }
            InfoBuffer.Clear();
        }
    }
}

class Logging
{
    static void Main()
    {
        Logger.Log(LogLevel.INFO, "Application started");
        Logger.Log(LogLevel.WARNING, "Warning: Something might be wrong");
        Logger.Log(LogLevel.ERROR, "Error: Something went wrong");
        Logger.Log(LogLevel.INFO, "Additional info");

        // Simulate the program running for a while
        Thread.Sleep(3000);

        Logger.Log(LogLevel.CRITICAL, "Critical error: Termination.");
        Logger.Log(LogLevel.INFO, "Application shutting down");
    }
}
