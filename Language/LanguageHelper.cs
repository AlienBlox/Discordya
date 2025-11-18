// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.Language.LanguageHelper
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

#nullable disable
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
