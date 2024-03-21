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
        /// <summary>
        /// Handler for updates.
        /// </summary>
        /// <param name="botClient"> Client of bot.</param>
        /// <param name="update">Update</param>
        /// <param name="cancellationToken">Token of cancellation</param>
        /// <returns></returns>
        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                // Swith update types.
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var message = update.Message;
                            var user = message.From;
                            // Log data.
                            Console.WriteLine($"{user.FirstName} ({user.Id}) send message: {message.Text}");

                            var chat = message.Chat;
                            // Trying to process sended message.
                            try
                            {
                                if (!(message.Text is null))
                                {
                                    switch (message.Text)
                                    {
                                        case "/start":
                                            {
                                                // First message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Welcome in metroStationsBot. There you can process you data about metrostations. ",
                                                    replyMarkup: menuKeyboard
                                                     );

                                                // Made states.
                                                UserData userData = new UserData(user);
                                                userData.State = UserState.InMenu;
                                                usersData[user.Id] = userData;
                                                return;
                                            }
                                        case "Read data from file":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, send file in json or csv format");

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingFile;
                                                return;
                                            }
                                        case "Filter list of stations":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Checking is null data.
                                                if (usersData[user.Id].Stations is null)
                                                    throw new Exception("Unable to process null data.");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, choose field for filtration : ",
                                                    replyMarkup: menuForFiltration);

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingFieldForFiltration;
                                                return;
                                            }
                                        case "Sort list of stations":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Checking is null data.
                                                if (usersData[user.Id].Stations is null)
                                                    throw new Exception("Unable to process null data.");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, choose field for sorting : ",
                                                    replyMarkup: menuForSorting);

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingFieldForSorting;
                                                return;
                                            }
                                        case "Dowloand current file":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.InMenu)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Checking is null data.
                                                if (usersData[user.Id].Stations is null)
                                                    throw new Exception("Unable to download null data.");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Choose type for dowlanded file : ",
                                                    replyMarkup: menuForFileType);

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingTypeForFile;
                                                return;
                                            }
                                        case "NameOfStation":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingFieldForFiltration)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, Send name of station to filter data (in quotes : \"NAME\")");

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingNameOfStation;
                                                return;
                                            }
                                        case "Line":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingFieldForFiltration)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, Send name of line to filter data (in quotes : \"NAME\")");

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingLine;
                                                return;
                                            }
                                        case "NameOfStation and Month":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingFieldForFiltration)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Okey, firstly send name of month to filter data (in quotes : \"NAME\")");

                                                //Update states.
                                                usersData[user.Id].State = UserState.WaitingNameOfStationAndMonth;
                                                return;
                                            }
                                        case "Year ascending":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingFieldForSorting)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Making Sorting.
                                                usersData[user.Id].Stations.Sort((a, b) => a.Year.CompareTo(b.Year));

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Sorting finished correct",
                                                    replyMarkup: menuKeyboard);

                                                //Update states.
                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        case "NameOfStation by alphabet":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingFieldForSorting)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Making sorting.
                                                usersData[user.Id].Stations.Sort((a, b) => a.NameOfStation.CompareTo(b.NameOfStation));

                                                // Send message.
                                                await botClient.SendTextMessageAsync(
                                                    chat.Id,
                                                    "Sorting finished correct",
                                                    replyMarkup: menuKeyboard);

                                                //Update states.
                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        case "JSON":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingTypeForFile)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Make data stream.
                                                JSONProcessing processer = new JSONProcessing();
                                                await using Stream stream = processer.Write(usersData[user.Id].Stations);

                                                // Send file.
                                                await botClient.SendDocumentAsync(
                                                    chat.Id,
                                                    InputFile.FromStream(stream, currentFileName + "_tmp.json"),
                                                    caption: "Yours file :",
                                                    replyMarkup: menuKeyboard);

                                                //Update states.
                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        case "CSV":
                                            {
                                                // Process states.
                                                if (usersData[user.Id].State != UserState.WaitingTypeForFile)
                                                    throw new Exception("Unexpect this command in current moment");

                                                // Make data stream.
                                                CSVProcessing processor = new CSVProcessing();
                                                await using Stream stream = processor.Write(usersData[user.Id].Stations);

                                                // Send file.
                                                await botClient.SendDocumentAsync(
                                                    chat.Id,
                                                    InputFile.FromStream(stream, currentFileName + "_tmp.csv"),
                                                    caption: "Yours file :",
                                                    replyMarkup: menuKeyboard);

                                                //Update states.
                                                usersData[user.Id].State = UserState.InMenu;
                                                return;
                                            }
                                        default:
                                            {
                                                // Process states.
                                                UserState curState = usersData[user.Id].State;
                                                if (curState == UserState.WaitingNameOfStation ||
                                                    curState == UserState.WaitingLine ||
                                                    curState == UserState.WaitingNameOfStationAndMonth)
                                                {
                                                    // Wrong format.
                                                    if (message.Text[0] != '"' || message.Text[^1] != '"')
                                                    {
                                                        // Send message.
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Send name IN QUOTES ( \"NAME\")");
                                                        return;
                                                    }
                                                    string name = message.Text.Trim('"');

                                                    if (curState == UserState.WaitingNameOfStation)
                                                    {
                                                        // Making filtration.
                                                        usersData[user.Id].Stations =
                                                            (from station in usersData[user.Id].Stations
                                                             where station.NameOfStation == name
                                                             select station).ToList();

                                                        // Send message.
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Filtration finished correctly",
                                                            replyMarkup: menuKeyboard);

                                                        //Update states.
                                                        usersData[user.Id].State = UserState.InMenu;
                                                    }
                                                    else if (curState == UserState.WaitingLine)
                                                    {
                                                        // Making filtration.
                                                        usersData[user.Id].Stations =
                                                            (from station in usersData[user.Id].Stations
                                                             where station.Line == name
                                                             select station).ToList();

                                                        // Send message.
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Filtration finished correctly",
                                                            replyMarkup: menuKeyboard);

                                                        //Update states.
                                                        usersData[user.Id].State = UserState.InMenu;
                                                    }
                                                    else
                                                    {
                                                        // Making filtration.
                                                        usersData[user.Id].Stations =
                                                            (from station in usersData[user.Id].Stations
                                                             where station.Month == name
                                                             select station).ToList();

                                                        // Send message.
                                                        await botClient.SendTextMessageAsync(
                                                            chat.Id,
                                                            "Okey, now send name of station to filter data (in quotes : \"NAME\")");

                                                        //Update states.
                                                        usersData[user.Id].State = UserState.WaitingNameOfStation;
                                                    }

                                                }
                                                else
                                                {
                                                    // Send message.
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
                                    // Process states.
                                    if (usersData[user.Id].State!=UserState.WaitingFile)
                                        throw new Exception("Unexpect this command in current moment");

                                    // Get data about doc.
                                    var fileId = message.Document.FileId;
                                    var fileInfo = await botClient.GetFileAsync(fileId);
                                    var filePath = fileInfo.FilePath;
                                    var fileExtension = Path.GetExtension(filePath);

                                    // Wrong extension.
                                    if (fileExtension != ".csv" && fileExtension != ".json")
                                    {
                                        // Send message.
                                        await botClient.SendTextMessageAsync(
                                            chat.Id,
                                            "Unexpected format, send file with extension '.csv' or '.json'");
                                        return;
                                    }

                                    // Update filename.
                                    currentFileName = Path.GetFileNameWithoutExtension(message.Document.FileName);
                                    
                                    // Read doc with api.
                                    Stream stream = new MemoryStream();
                                    StreamWriter writer = new StreamWriter(stream);
                                    string apiPath = $"https://api.telegram.org/file/bot{HttpApiKey}/{filePath}";
                                    writer.Write(new WebClient().DownloadString(apiPath));
                                    writer.Flush();
                                    stream.Position = 0;
                                    

                                    if (fileExtension == ".json")
                                    {
                                        // Save data from json.
                                        JSONProcessing processor = new JSONProcessing();
                                        usersData[user.Id].Stations = processor.Read(stream);
                                    }
                                    else
                                    {
                                        // Save data from csv.
                                        CSVProcessing processor = new CSVProcessing();
                                        usersData[user.Id].Stations = processor.Read(stream);
                                    }

                                    // Send message.
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "File readed succesfully.",
                                        replyMarkup: menuKeyboard);

                                    //Update states.
                                    usersData[user.Id].State = UserState.InMenu;

                                }


                                return;
                            }
                            catch (Exception e)
                            {
                                // Send message about error.
                                usersData[user.Id].State = UserState.InMenu;
                                await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    $"Exception : {e.Message}\nI return you to main menu",
                                    replyMarkup: menuKeyboard);

                            }
                            break;
                        }
                }
            }
            catch (Exception exc)
            {
                // Log exception.
                Console.WriteLine(exc.Message);
            }
        }
    }
}

