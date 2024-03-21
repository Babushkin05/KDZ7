using System;
using Microsoft.Extensions.Logging;

namespace KDZ7
{
	public class TgBotLoggerProvider : ILoggerProvider
    {
		private string _path;

        //Constructor.
		public TgBotLoggerProvider(string path)
		{
			_path = path;
		}

        /// <summary>
        /// Creating logger.
        /// </summary>
        /// <param name="categoryName">Category of logger.</param>
        /// <returns>Logger.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            return new TgBotLogger(_path);
        }

        /// <summary>
        /// REquired method.
        /// </summary>
        public void Dispose() { }
    }
}

