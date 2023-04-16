using System.Diagnostics;

namespace MiniGameFramework.Logging
{
    public interface ILogger
    {
        void Log(TraceEventType eventType, int id, string message);
        void Log(TraceEventType eventType, string message);
    }
}