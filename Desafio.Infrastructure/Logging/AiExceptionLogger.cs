using Microsoft.ApplicationInsights;
using System.Web.Http.ExceptionHandling;

namespace Desafio.Infrastructure.Logging
{
    public class AiExceptionLogger : ExceptionLogger
    {
        public static AiExceptionLogger Factory()
        {
            return new AiExceptionLogger();
        }

        public override void Log(ExceptionLoggerContext context)
        {
            if (context != null && context.Exception != null)
            {
                var ai = new TelemetryClient();
                ai.TrackException(context.Exception);
            }

            base.Log(context);
        }
    }
}
