using System;
using System.Text;

namespace Desafio.Infrastructure.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            var sb = new StringBuilder();

            while (ex != null)
            {
                sb.Append(
                        string.Format(
                            "[Exception Message=\"{0}\" Stack=\"{1}\"]",
                            ex.Message ?? string.Empty,
                            ex.StackTrace ?? string.Empty)
                    );

                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}
