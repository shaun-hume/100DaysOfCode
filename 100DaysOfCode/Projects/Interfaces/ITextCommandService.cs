using System;
namespace _100DaysOfCode.Projects.Interfaces
{
	public interface ITextCommandService
	{
		bool ProcessCommand(Player player, string command);
	}
}

