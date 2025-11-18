namespace DiscordyaV2.Language
{
	public class LanguageHelper
	{
		internal LanguageHelper(DiscordyaMod discordyaMod)
		{
		}

		internal string GetText(string key) => Terraria.Localization.Language.GetText("Mods.DiscordyaV2." + key).Value;
	}
}
