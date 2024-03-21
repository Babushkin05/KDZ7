using System;
using Telegram.Bot;
using StationsLib;
namespace KDZ7
{
	/// <summary>
	/// Class of tgbot user.
	/// </summary>
	public class UserData
	{
		// User.
		public readonly Telegram.Bot.Types.User User;

		// Data for that user.
		public List<Station> Stations;

		// Curent state in work.
		public UserState State;

		// Empty constructor.
		public UserData()
		{
			throw new ArgumentNullException("Null user");
		}

		// Constructor.
		public UserData(Telegram.Bot.Types.User user)
		{
			User = user;
		}
	}
}

