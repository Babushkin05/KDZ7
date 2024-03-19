using Telegram.Bot;

namespace KDZ7
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("code");

            Stream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write("hello");
            writer.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            Console.WriteLine(reader.ReadLine());
        }
    }
}