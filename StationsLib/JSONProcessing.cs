using System;
using System.Text.Json;
using System.Text;
namespace StationsLib
{
	public class JSONProcessing
	{
		public JSONProcessing()
		{
		}

		public Stream Write(List<Station> stations)
		{
			Stream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.Cyrillic, System.Text.Unicode.UnicodeRanges.BasicLatin)
            };
            string JsonText = JsonSerializer.Serialize(stations, options);

            writer.Write(JsonText);

			writer.Flush();
			stream.Position = 0;

			return stream;
		}

		public List<Station> Read(Stream stream)
		{
			StreamReader reader = new StreamReader(stream);
			string JsonText = reader.ReadToEnd();

			List<Station> stations;
			try
			{
                stations = JsonSerializer.Deserialize<List<Station>>(JsonText);
            }
			catch(Exception e)
			{
				throw new ArgumentException($"Error while json deserializing :: {e.Message}");
			}

			return stations;

			
		}
	}
}

