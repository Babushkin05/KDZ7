using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.Extensions.Logging;
using StationsLib;

namespace KDZ7
{
    /// <summary>
    /// Class of StationsTgBot.
    /// </summary>
	public partial class StationsTgBot
	{
        //BotClient.
        private ITelegramBotClient _botClient;

        // Options of reciver.
        private  ReceiverOptions _receiverOptions;

        // Api key to bot.
        private string HttpApiKey;

        // Logger.
        private ILogger _logger;

        // Saving data of users states.
        private Dictionary<System.Int64, UserData> usersData;

        // Name of downloaded file.
        private string currentFileName;

        // Empty constructor.
		public StationsTgBot()
		{
			throw new ArgumentNullException("no http api key to start bot");
		}

        // Constructor.
		public StationsTgBot(string apiKey)
		{
            // Creating Logger.
            var separator = Path.DirectorySeparatorChar;
            var logPath = $"var{separator}log.txt";
            System.IO.File.WriteAllText(logPath,"");
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new TgBotLoggerProvider(logPath));
            });
            _logger = loggerFactory.CreateLogger("FileLogger");

            // Initializing useful fields.
            HttpApiKey = apiKey;
            usersData = new Dictionary<System.Int64, UserData>();
            _botClient = new TelegramBotClient(HttpApiKey);

            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] 
            {
                UpdateType.Message, 
            },
                ThrowPendingUpdates = true,
            };
        }

        /// <summary>
        /// Method for start recieving.
        /// </summary>
        /// <returns></returns>
        public async Task LaunchBot()
        {

            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); 

            var me = await _botClient.GetMeAsync(); 

            _logger.LogInformation($"{me.FirstName} Started!");

            // Endless pooling.
            await Task.Delay(-1); 
        }
	}
}

