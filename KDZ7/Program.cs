using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KDZ7
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            StationsTgBot bot = new StationsTgBot("tgbotapikey");
            await bot.LaunchBot();

        }

        
    }
}