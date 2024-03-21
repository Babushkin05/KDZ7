using System;
using System.Text.Json;
using System.Text;
namespace StationsLib
{
	public class JSONProcessing
	{
		// Constructor.
		public JSONProcessing()
		{
		}

		/// <summary>
		/// MEthod to write data to stream.
		/// </summary>
		/// <param name="stations"> Data to write.</param>
		/// <returns></returns>
		public Stream Write(List<Station> stations)
		{
			// Init stream.
			Stream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				WriteIndented = true,
				Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.Cyrillic, System.Text.Unicode.UnicodeRanges.BasicLatin)
            };

			// Serialize data.
            string JsonText = JsonSerializer.Serialize(stations, options);

			//Write data.
            writer.Write(JsonText);

			writer.Flush();
			stream.Position = 0;

			return stream;
		}


		/// <summary>
		/// Method to read data from stream.
		/// </summary>
		/// <param name="stream">stream to read.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Wrong data.</exception>
		public List<Station> Read(Stream stream)
		{
			// Read data.
			StreamReader reader = new StreamReader(stream);
			string JsonText = reader.ReadToEnd();

			//Serialize data.
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

