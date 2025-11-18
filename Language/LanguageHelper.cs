// Decompiled with JetBrains decompiler
// Type: Discordya.Language.LanguageHelper
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

#nullable disable
namespace Discordya.Language
{
	public class LanguageHelper
	{
		internal LanguageHelper(DiscordyaMod discordyaMod)
		{
		}

		internal string GetText(string key) => Terraria.Localization.Language.GetText("Mods.Discordya." + key).Value;
	}
}
