using System;
using Telegram.Bot.Types.ReplyMarkups;

namespace KDZ7
{
	public partial class StationsTgBot
	{
        private ReplyKeyboardMarkup menuKeyboard = new ReplyKeyboardMarkup(
            new List<KeyboardButton[]>()
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("Read data from file"),
                    new KeyboardButton("Dowloand current file")
                },
                new KeyboardButton[]
                {
                    new KeyboardButton("Filter list of stations"),
                    new KeyboardButton("Sort list of stations")
                }
            })
        { ResizeKeyboard = true };

        private ReplyKeyboardMarkup menuForFiltration = new ReplyKeyboardMarkup(
            new List<KeyboardButton>()
            {
                new KeyboardButton("NameOfStation"),
                new KeyboardButton("Line"),
                new KeyboardButton("NameOfStation and Month")
            })
        { ResizeKeyboard = true };

        private ReplyKeyboardMarkup menuForSorting = new ReplyKeyboardMarkup(
            new List<KeyboardButton>()
            {
                new KeyboardButton("Year ascending"),
                new KeyboardButton("NameOfStation by alphabet")
            })
        { ResizeKeyboard = true };

        private ReplyKeyboardMarkup menuForFileType = new ReplyKeyboardMarkup(
            new List<KeyboardButton>()
            {
                new KeyboardButton("JSON"),
                new KeyboardButton("CSV")
            })
        { ResizeKeyboard = true };

    }
}

