using System;

namespace UBB_SE_2024_Gaborment.Server.LoggerUtils
{
    public interface ILogger
    {
        void Log(string logLevel, string message);
    }
}