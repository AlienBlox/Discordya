// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.DiscordClient.DiscordClientStorage
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using System.Collections.Generic;

#nullable disable
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
