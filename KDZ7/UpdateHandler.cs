using System;
using System.Net;
using System.Reflection.Metadata;
using StationsLib;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KDZ7
{
	public partial class StationsTgBot
	{
        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            var user = message.From;

                            Console.WriteLine($"{user.FirstName} ({user.Id}) send message: {message.Text}");

                            var chat = message.Chat;
                            try
                            {
                                if (!(message.Text is null))
                                {
                                    switch (message.Text)
                                    {
                                        case "/start":
                                            {
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Welcome in metrostationsbot. There you can process you data about metrostations. ",
                                                    replyMarkup: menuKeyboard
                                                     );

                                                UserData userData = new UserData(user);
                                                userData.State = UserState.InMenu;
                                                usersData[user.Id] = userData;
                                                return;
                                            }
                                        case "Read data from file":
                                            {
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, send file in json or csv format");
                                                usersData[user.Id].State = UserState.WaitingFile;
                                                return;
                                            }
                                        case "Filter list of stations":
                                            {
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                if (stations is null)
                                                    throw new Exception("Unable to process null data.");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, choose field for filtration : ",
                                                    replyMarkup: menuForFiltration);

                                                usersData[user.Id].State = UserState.WaitingFieldForFiltration;
                                                return;
                                            }
                                        case "Sort list of stations":
                                            {
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                if (stations is null)
                                                    throw new Exception("Unable to process null data.");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, choose field for sorting : ",
                                                    replyMarkup: menuForSorting);

                                                usersData[user.Id].State = UserState.WaitingFieldForSorting;
                                                return;
                                            }
                                        case "Dowloand current file":
                                            {
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                if (stations is null)
                                                    throw new Exception("Unable to download null data.");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Choose type for dowlanded file : ",
                                                    replyMarkup: menuForFileType);
                                                usersData[user.Id].State = UserState.WaitingTypeForFile;
                                                return;
                                            }
                                        case "NameOfStation":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingFieldForFiltration)
                                                    throw new Exception("Unexpect this command in current moment");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, Send name of station to filter data (in quotes : \"NAME\")");
                                                usersData[user.Id].State = UserState.WaitingNameOfStation;
                                                return;
                                            }
                                        case "Line":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingFieldForFiltration)
                                                    throw new Exception("Unexpect this command in current moment");

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, Send name of line to filter data (in quotes : \"NAME\")");
                                                usersData[user.Id].State = UserState.WaitingLine;
                                                return;
                                            }
                                        case "NameOfStation and Month":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingFieldForFiltration)
                                                    throw new Exception("Unexpect this command in current moment");
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, firstly send name of month to filter data (in quotes : \"NAME\")");
                                                usersData[user.Id].State = UserState.WaitingNameOfStationAndMonth;
                                                return;
                                            }
                                        case "Year ascending":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingFieldForSorting)
                                                    throw new Exception("Unexpect this command in current moment");

                                                stations.Sort((a, b) => a.Year.CompareTo(b.Year));

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Sorting finished correct",
                                                    replyMarkup: menuKeyboard);
                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        case "NameOfStation by alphabet":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingFieldForSorting)
                                                    throw new Exception("Unexpect this command in current moment");

                                                stations.Sort((a, b) => a.NameOfStation.CompareTo(b.NameOfStation));

                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Sorting finished correct",
                                                    replyMarkup: menuKeyboard);
                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        case "JSON":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingTypeForFile)
                                                    throw new Exception("Unexpect this command in current moment");

                                                JSONProcessing processer = new JSONProcessing();
                                                await using Stream stream = processer.Write(stations);

                                                await botClient.SendDocumentAsync(
                                                    chat.Id,
                                                    InputFile.FromStream(stream, currentFileName + "_tmp.json"),
                                                    caption: "Yours file :",
                                                    replyMarkup: menuKeyboard);

                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        case "CSV":
                                            {
                                                if (usersData[user.Id].State != UserState.WaitingTypeForFile)
                                                    throw new Exception("Unexpect this command in current moment");

                                                CSVProcessing processor = new CSVProcessing();
                                                await using Stream stream = processor.Write(stations);

                                                await botClient.SendDocumentAsync(
                                                    chat.Id,
                                                    InputFile.FromStream(stream, currentFileName + "_tmp.csv"),
                                                    caption: "Yours file :",
                                                    replyMarkup: menuKeyboard);

                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        default:
                                            {
                                                UserState curState = usersData[user.Id].State;
                                                if (curState == UserState.WaitingNameOfStation ||
                                                    curState == UserState.WaitingLine ||
                                                    curState == UserState.WaitingNameOfStationAndMonth)
                                                {
                                                    if (message.Text[0] != '"' || message.Text[^1] != '"')
                                                    {
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Send name IN QUOTES ( \"NAME\")");
                                                        return;
                                                    }
                                                    string name = message.Text.Trim('"');

                                                    if (curState == UserState.WaitingNameOfStation)
                                                    {
                                                        stations = (from station in stations
                                                                    where station.NameOfStation == name
                                                                    select station).ToList();
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Filtration finished correctly",
                                                            replyMarkup: menuKeyboard);
                                                        usersData[user.Id].State = UserState.InMenu;
                                                    }
                                                    else if (curState == UserState.WaitingLine)
                                                    {
                                                        stations = (from station in stations
                                                                    where station.Line == name
                                                                    select station).ToList();

                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Filtration finished correctly",
                                                            replyMarkup: menuKeyboard);
                                                        usersData[user.Id].State = UserState.InMenu;
                                                    }
                                                    else
                                                    {
                                                        stations = (from station in stations
                                                                    where station.Month == name
                                                                    select station).ToList();
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Okey, now send name of station to filter data (in quotes : \"NAME\")");
                                                        usersData[user.Id].State = UserState.WaitingNameOfStation;
                                                    }

                                                }
                                                else
                                                {
                                                    await botClient.SendTextMessageAsync(
                                                        chat.Id,
                                                        "Sorry, I dont understand",
                                                        replyMarkup: menuKeyboard);
                                                }
                                                return;
                                            }
                                    }
                                }
                                else if (!(message.Document is null))
                                {
                                    if (usersData[user.Id].State!=UserState.WaitingFile)
                                        throw new Exception("Unexpect this command in current moment");

                                    var fileId = message.Document.FileId;
                                    var fileInfo = await botClient.GetFileAsync(fileId);
                                    var filePath = fileInfo.FilePath;
                                    var fileExtension = Path.GetExtension(filePath);

                                    if (fileExtension != ".csv" && fileExtension != ".json")
                                    {
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Unexpected format, send file with extension '.csv' or '.json'");
                                        return;
                                    }

                                    currentFileName = Path.GetFileNameWithoutExtension(message.Document.FileName);
                                    var fileName = $"{currentFileName}_{chat.Id}{fileExtension}";

                                    Stream stream = new MemoryStream();
                                    StreamWriter writer = new StreamWriter(stream);
                                    string apiPath = $"https://api.telegram.org/file/bot{HttpApiKey}/{filePath}";
                                    writer.Write(new WebClient().DownloadString(apiPath));
                                    writer.Flush();
                                    stream.Position = 0;
                                    

                                    if (fileExtension == ".json")
                                    {
                                        JSONProcessing processor = new JSONProcessing();
                                        stations = processor.Read(stream);
                                    }
                                    else
                                    {
                                        CSVProcessing processor = new CSVProcessing();
                                        stations = processor.Read(stream);
                                    }

                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "File readed succesfully.",
                                        replyMarkup: menuKeyboard);

                                    usersData[user.Id].State = UserState.InMenu;

                                }


                                return;
                            }
                            catch (Exception e)
                            {
                                usersData[user.Id].State = UserState.InMenu;
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Exception : {e.Message};\n I return you to main menu",
                                    replyMarkup: menuKeyboard);

                            }
                            break;
                        }
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
    }
}

