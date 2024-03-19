using System;
using System.Text;
namespace StationsLib
{
	public class CSVProcessing
	{
		private static string head = "\"ID\";\"NameOfStation\";\"Line\";\"Longitude_WGS84\";\"Latitude_WGS84\";\"AdmArea\";\"District\";\"Year\";\"Month\";\"global_id\";\"geodata_center\";\"geoarea\";\n\"№ п/п\";\"Станция метрополитена\";\"Линия\";\"Долгота в WGS-84\";\"Широта в WGS-84\";\"Административный округ\";\"Район\";\"Год\";\"Месяц\";\"global_id\";\"geodata_center\";\"geoarea\";\n";
		private static string[] fields = { "ID", "NameOfStation", "Line", "Longitude_WGS84", "Latitude_WGS84", "AdmArea", "District", "Year", "Month", "global_id", "geodata_center", "geoarea" };

        public CSVProcessing()
		{
		}

		public Stream Write(List<Station> stations)
		{

			Stream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(Encoding.UTF32.GetBytes(head));

			foreach(Station station in stations)
			{
				StringBuilder lineToWrite = new StringBuilder();
				foreach(string field in fields)
				{
					lineToWrite.Append($"\"{station[field]?.ToString()}\";");
				}
				lineToWrite.Append('\n');

				writer.Write(Encoding.UTF32.GetBytes(lineToWrite.ToString()));
			}

			writer.Flush();
			stream.Position = 0;
			return stream;
		}

		public List<Station> Read(Stream stream)
		{
			StreamReader reader = new StreamReader(stream);
			string currentHead = reader.ReadLine() + '\n' + reader.ReadLine() + '\n';
			if (currentHead != head)
				throw new ArgumentException("Wrong format of file (head).");

			List<Station> stations = new List<Station>();

			string lineFromStream;

			do
			{
				lineFromStream = reader.ReadLine();
				string[] dataFromLine = lineFromStream.Split(';');
				if (dataFromLine.Length != fields.Length)
					throw new ArgumentException($"Wrong format of file in line :: {stations.Count + 3}");
				try
				{
					Station station = new Station(int.Parse(dataFromLine[0].Trim()), dataFromLine[1].Trim(), dataFromLine[2].Trim(),
						double.Parse(dataFromLine[3].Trim()), double.Parse(dataFromLine[4].Trim()), dataFromLine[5].Trim(),
						dataFromLine[6].Trim(),int.Parse(dataFromLine[7].Trim()), dataFromLine[8].Trim(),
						int.Parse(dataFromLine[9].Trim()),dataFromLine[10].Trim(), dataFromLine[11].Trim());
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

