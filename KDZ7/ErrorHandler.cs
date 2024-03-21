using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace KDZ7
{
	public partial class StationsTgBot
	{
        /// <summary>
        /// Metho to handle errors in tgbot work.
        /// </summary>
        /// <param name="botClient"> Bot client.</param>
        /// <param name="error"> Error to handle.</param>
        /// <param name="cancellationToken"> Token of cancellation.</param>
        /// <returns></returns>
        private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            // Create example of error message.
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            // Log error.
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}

