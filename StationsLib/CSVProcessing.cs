using System;
using System.Text;
namespace StationsLib
{
	/// <summary>
	/// Class to process csv data.
	/// </summary>
	public class CSVProcessing
	{
		// Correct head of file.
		private static string head = "\"ID\";\"NameOfStation\";\"Line\";\"Longitude_WGS84\";\"Latitude_WGS84\";\"AdmArea\";\"District\";\"Year\";\"Month\";\"global_id\";\"geodata_center\";\"geoarea\";\n\"№ п/п\";\"Станция метрополитена\";\"Линия\";\"Долгота в WGS-84\";\"Широта в WGS-84\";\"Административный округ\";\"Район\";\"Год\";\"Месяц\";\"global_id\";\"geodata_center\";\"geoarea\";\n";

		// Fields in file.
        private static string[] fields = { "ID", "NameOfStation", "Line", "Longitude_WGS84", "Latitude_WGS84", "AdmArea", "District", "Year", "Month", "global_id", "geodata_center", "geoarea" };

		// Constructor.
        public CSVProcessing()
		{
		}

		/// <summary>
		/// Method to write data to stream.
		/// </summary>
		/// <param name="stations">Data to write.</param>
		/// <returns>Stream with data.</returns>
		public Stream Write(List<Station> stations)
		{
			// Init stream.
			Stream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(head,
				System.Text.Encodings.Web.JavaScriptEncoder.Create(
					System.Text.Unicode.UnicodeRanges.Cyrillic,
					System.Text.Unicode.UnicodeRanges.BasicLatin)
				);

			// Writing data in csv format.
			foreach(Station station in stations)
			{
				StringBuilder lineToWrite = new StringBuilder();
				foreach(string field in fields)
				{
					lineToWrite.Append($"\"{station[field]?.ToString()}\";");
				}
				lineToWrite.Append('\n');

				writer.Write(lineToWrite.ToString(),
					System.Text.Encodings.Web.JavaScriptEncoder.Create(
						System.Text.Unicode.UnicodeRanges.Cyrillic,
						System.Text.Unicode.UnicodeRanges.BasicLatin)
					);
			}

			writer.Flush();
			stream.Position = 0;

			return stream;
		}

		/// <summary>
		/// Method to read data from stream.
		/// </summary>
		/// <param name="stream">Stream to read.</param>
		/// <returns> Data</returns>
		/// <exception cref="ArgumentException">Wrong formar of file.</exception>
		public List<Station> Read(Stream stream)
		{
			// Read head of file.
			StreamReader reader = new StreamReader(stream);
			string currentHead = reader.ReadLine() + '\n' + reader.ReadLine() + '\n';

			if (currentHead != head)
				throw new ArgumentException("Wrong format of file (head).");


			List<Station> stations = new List<Station>();

			// REading by lines.
			string? lineFromStream;
			do
			{
				lineFromStream = reader.ReadLine();
				if (lineFromStream is null)
					break;

				string[] dataFromLine = lineFromStream.Split(';');
				if (dataFromLine.Length-1 != fields.Length)
					throw new ArgumentException($"Wrong format of file in line :: {stations.Count + 3}");
				try
				{
					Station station = new Station(int.Parse(dataFromLine[0].Trim('"')), dataFromLine[1].Trim('"'), dataFromLine[2].Trim('"'),
						double.Parse(dataFromLine[3].Trim('"')), double.Parse(dataFromLine[4].Trim('"')), dataFromLine[5].Trim('"'),
						dataFromLine[6].Trim('"'),int.Parse(dataFromLine[7].Trim('"')), dataFromLine[8].Trim('"'),
                        System.Int64.Parse(dataFromLine[9].Trim('"')),dataFromLine[10].Trim('"'), dataFromLine[11].Trim('"'));
					stations.Add(station);
				}
				catch(Exception e) 
				{
					throw new ArgumentException($"Error in line {stations.Count + 3} :: {e.Message}");
				}

			} while (!(lineFromStream is null));


			return stations;
		}
	}
}

