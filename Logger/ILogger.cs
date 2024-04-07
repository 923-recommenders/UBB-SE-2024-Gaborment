using System;

namespace UBB_SE_2024_Gaborment
{
    public interface ILogger
    {
        void Log(string logLevel, string message);
    }
}