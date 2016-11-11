using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;

namespace Desafio.Infrastructure.Logging
{
    [EventSource(Name = "Desafio.Infrastructure.Logging.UnityEventLogger")]
    public sealed class UnityEventLogger : EventSource
    {
        public class Keywords
        {
            public const EventKeywords Diagnostic = (EventKeywords)1;
            public const EventKeywords Performance = (EventKeywords)2;
            public const EventKeywords Information = (EventKeywords)4;
        }

        public class Tasks
        {
            public const EventTask CreateInstance = (EventTask)1;
            public const EventTask DisposeInstance = (EventTask)10;
            public const EventTask LogInstance = (EventTask)20;
        }

        public class EventIds
        {
            public const int CreateUnityMessageEvent = 1;
            public const int DisposeUnityMessageEvent = 2;
            public const int LogUnityMessageEvent = 3;
        }

        private static Lazy<UnityEventLogger> Instance
        {
            get
            {
                return new Lazy<UnityEventLogger>(() => new UnityEventLogger());
            }
        }

        private UnityEventLogger() { }

        public static UnityEventLogger Log { get { return Instance.Value; } }

        [Event(EventIds.CreateUnityMessageEvent, Message = "CreateUnityMessage: {0}", Keywords = Keywords.Diagnostic, Task = Tasks.CreateInstance, Level = EventLevel.Informational)]
        public void CreateUnityMessage(string message)
        {
            if (this.IsEnabled())
                this.WriteEvent(EventIds.CreateUnityMessageEvent, message);

            this.DebugLog(message);
        }

        [Event(EventIds.DisposeUnityMessageEvent, Message = "DisposeUnityMessage: {0}", Keywords = Keywords.Diagnostic, Task = Tasks.DisposeInstance, Level = EventLevel.Informational)]
        public void DisposeUnityMessage(string message)
        {
            if (this.IsEnabled())
                this.WriteEvent(EventIds.DisposeUnityMessageEvent, message);

            this.DebugLog(message);
        }

        [Event(EventIds.LogUnityMessageEvent, Message = "{0}", Keywords = Keywords.Information, Task = Tasks.LogInstance, Level = EventLevel.Informational)]
        public void LogUnityMessage(string message)
        {
            if (this.IsEnabled())
                this.WriteEvent(EventIds.LogUnityMessageEvent, message);

            this.DebugLog(message);
        }

        [Conditional("DEBUG")]
        private void DebugLog(string message) => Debug.WriteLine(message);
    }
}
