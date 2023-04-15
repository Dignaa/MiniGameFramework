using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGameFramework.Logging
{
    public class Logger
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
                Logger.GetInstance().Log(TraceEventType.Information, "New logger created");
            }
            else
            {
                throw new InvalidOperationException("Object instance is already created");
            }
            return _instance;
        }


        public void Log(TraceEventType eventType, string message)
        {
            traceSource.TraceEvent(eventType, _eventId++, message);
        }

        public void Log(TraceEventType eventType,int id, string message)
        {
            traceSource.TraceEvent(eventType, id, message);
        }
    }
}
