// https://github.com/Babushkin05/KDZ7.git
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KDZ7
{
    class Program
    {
        /// <summary>
        /// Main methon to start program.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            // I host this bot: @stations_tgbot

            // Api key to bot.
            string tgBotApiKey = "6853916219:AAEcJ6-faiX_VIPaLKFYkcIHP3p5QzygpN8";

            // Create tgBot example.
            StationsTgBot bot = new StationsTgBot(tgBotApiKey);

            // Start bot working.
            await bot.LaunchBot();

        }

        
    }
}