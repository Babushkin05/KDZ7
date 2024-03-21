using System;
using Microsoft.Extensions.Logging;

namespace KDZ7
{
	public class TgBotLogger : ILogger
    {
        private string _filePath;

		public TgBotLogger(string filePath)
		{
            _filePath = filePath;
		}

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// Log messages.
        /// </summary>
        /// <typeparam name="TState">State type.</typeparam>
        /// <param name="logLevel">level of loggging.</param>
        /// <param name="eventId">Event id.</param>
        /// <param name="state"> Statte.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="formatter">Method for format message.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
            Func<TState, Exception, string> formatter)
        {
            // Is enabled logging.
            if (!IsEnabled(logLevel))
                return;

            try
            {
                using (var writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine($"{DateTime.Now} [{logLevel}] - {formatter(state, exception)}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Logging Error: {ex.Message}");
            }
        }
    }
}

