using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using StationsLib;

namespace KDZ7
{
	public partial class StationsTgBot
	{
        private ITelegramBotClient _botClient;
        private  ReceiverOptions _receiverOptions;
        private string HttpApiKey;

        private Dictionary<System.Int64, UserData> usersData;
        private List<Station> stations;
        private string currentFileName;

		public StationsTgBot()
		{
			throw new ArgumentNullException("no http api key to start bot");
		}

		public StationsTgBot(string apiKey)
		{
			HttpApiKey = apiKey;
            usersData = new Dictionary<System.Int64, UserData>();
            _botClient = new TelegramBotClient(HttpApiKey);

            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[] // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
            {
                UpdateType.Message, // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
            },
                // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
                // True - не обрабатывать, False (стоит по умолчанию) - обрабаывать
                ThrowPendingUpdates = true,
            };
        }

        public async Task LaunchBot()
        {

            using var cts = new CancellationTokenSource();


            // UpdateHander - обработчик приходящих Update`ов
            // ErrorHandler - обработчик ошибок, связанных с Bot API
            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); // Запускаем бота

            var me = await _botClient.GetMeAsync(); // Создаем переменную, в которую помещаем информацию о нашем боте.

            Console.WriteLine($"{me.FirstName} Started!");

            await Task.Delay(-1); // Устанавливаем бесконечную задержку, чтобы наш бот работал постоянно
        }
	}
}

