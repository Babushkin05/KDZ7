using System;
namespace KDZ7
{
	// States of dialog with user.
	public enum UserState
	{
		InMenu,
		WaitingFile,
		WaitingFieldForFiltration,
		WaitingNameOfStation,
		WaitingLine,
		WaitingNameOfStationAndMonth,
		WaitingFieldForSorting,
		WaitingTypeForFile
	}
}

