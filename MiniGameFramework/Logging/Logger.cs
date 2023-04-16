using System.Diagnostics;

namespace MiniGameFramework.Logging
{
    public class Logger : ILogger
    {
        private static Logger? _instance;
        private int _eventId;
        public TraceSource traceSource;
        public TraceListener listener;

        private Logger(string fileName)
        {
            traceSource = new TraceSource("GameTraceSource");
            traceSource.Switch = new SourceSwitch("MySwitch", "Verbose");
            listener = new TextWriterTraceListener(new StreamWriter(fileName) { AutoFlush = true });
            traceSource.Listeners.Add(listener);
        }
        public static Logger GetInstance()
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("Object is not created");
            }
            return _instance;
        }

        /// <summary>
        /// Creates an instance of a logger 
        /// If instance already exists, throws exception
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>Logger</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static Logger CreateInstance(string fileName)
        {
            if (_instance == null)
            {
                _instance = new Logger(fileName);
                _instance.Log(TraceEventType.Information, "New logger created");
            }
            else
            {
                _instance.Log(TraceEventType.Error, "Attempt to create logger failed, logger already exists");

                throw new InvalidOperationException("Object instance is already created");
            }
            return _instance;
        }

        /// <summary>
        /// Add a log with auto generated id
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="message"></param>
        public void Log(TraceEventType eventType, string message)
        {
            traceSource.TraceEvent(eventType, _eventId++, message);
        }

        /// <summary>
        /// Add log with specified id
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="id"></param>
        /// <param name="message"></param>
        public void Log(TraceEventType eventType, int id, string message)
        {
            traceSource.TraceEvent(eventType, id, message);
        }
    }
}
