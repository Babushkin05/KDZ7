using System;
using Telegram.Bot;
using StationsLib;
namespace KDZ7
{
	public class UserData
	{
		public readonly Telegram.Bot.Types.User User;
		public List<Station> Stations;
		public UserState State;

		public UserData()
		{
			throw new ArgumentNullException("Null user");
		}

		public UserData(Telegram.Bot.Types.User user)
		{
			User = user;
		}
	}
}

