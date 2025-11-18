using System.Collections.Generic;

namespace DiscordyaV2.DiscordClient
{
	public class DiscordClientStorage
	{
		private readonly Dictionary<string, string> _discordApplicationDictionary;

		internal DiscordClientStorage()
		{
			_discordApplicationDictionary = new Dictionary<string, string>();
		}

		internal void AddApplicationId(string applicationIdentifier, string applicationId)
		{
			_discordApplicationDictionary.Add(applicationIdentifier, applicationId);
		}

		internal string GetApplicationId(string applicationIdentifier)
		{
			return _discordApplicationDictionary[applicationIdentifier];
		}
	}
}
