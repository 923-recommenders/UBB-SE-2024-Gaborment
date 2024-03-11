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
        return "C:\\Users\\Theo\\Source\\Repos\\923-recommenders\\recommenders-backend\\Logger\\" + CurrentDate;
    }
}

public class Logger
{
    public bool DisplayToConsoleFlag { get; set; }

    private static string LogFilePath = LoggerHelper.GetTodayDate();
    private static readonly object LockObject = new object();
    private static readonly List<string> InfoBuffer = new List<string>();
    private static readonly Timer FlushTimer = new Timer(FlushInfoBuffer, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));

    public Logger(bool displayToConsoleFlag)
    {
        DisplayToConsoleFlag = displayToConsoleFlag;
    }

    public Logger()
    {
        DisplayToConsoleFlag = false;
    }

    public void Log(string level, string message)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [ {level} ] {message}";
        LogLevel logLevel;

        if(!Enum.TryParse<LogLevel>(level, true, out logLevel))
        {
            return;
        }

        lock (LockObject)
        {
            if (level.ToUpper() == "INFO")
            {
                InfoBuffer.Add(logEntry);
                if (DisplayToConsoleFlag)
                {
                    Console.WriteLine(message);
                }
            }
            else
            {
                if (DisplayToConsoleFlag)
                {
                    Console.WriteLine($"[ {level} ] {message}");
                }
                WriteToLogFile("----------------------------------------------------------------------------------------");
                WriteToLogFile(logEntry);
                WriteToLogFile("----------------------------------------------------------------------------------------");


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
            }
            InfoBuffer.Clear();
        }
    }
}

/*
class Logging
{
       
    static void Main()
    {
        Logger logger = new Logger(true);

        // Usecases
        logger.Log("INFO", "Application started");
        logger.Log("WARNING", "Warning: Something might be wrong");
        logger.Log("ERROR", "Error: Something went wrong");
        logger.Log("INFO", "Additional info");

        Thread.Sleep(5000);

        logger.Log("CRITICAL", "Critical error: Termination.");
        logger.Log("iNFO", "Application shutting down");

        Thread.Sleep(5000);
        // Simulate the program running for a while

    }
    

}
*/