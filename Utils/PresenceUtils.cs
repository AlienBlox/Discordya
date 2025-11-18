// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.Utils.PresenceUtils
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using DiscordyaV2.Config;
using DiscordyaV2.DiscordClient;
using Terraria.ModLoader;

#nullable disable
namespace DiscordyaV2.Utils
{
	public class PresenceUtils
	{
		private readonly DiscordyaMod _discordyaMod;
		private readonly DiscordClientHelper _discordClientHelper;
		private const string MainMenuBigText = "DiscordyaV2 v{0} | tModLoader v{1}";

		internal PresenceUtils(DiscordyaMod discordyaMod)
		{
			_discordyaMod = discordyaMod;
			_discordClientHelper = discordyaMod.GetDiscordClientHelper();
		}

		internal void SetMainMenuPresence()
		{
			_discordClientHelper.GetDiscordPresence().SetClientStatus(_discordyaMod.GetLanguageHelper().GetText("Generic.MainMenu"), ModContent.GetInstance<DiscordyaConfig>().DisplayModQuantity ? string.Format(_discordyaMod.GetLanguageHelper().GetText("Generic.MainMenuState"), (object)Terraria.ModLoader.ModLoader.Mods.Length) : (string)null, GetPreferredBigImage(), ModContent.GetInstance<DiscordyaConfig>().DisplayModVersion ? string.Format("DiscordyaV2 v{0} | tModLoader v{1}", (object)_discordyaMod.Version, (object)_discordyaMod.TModLoaderVersion) : (string)null);
			_discordClientHelper.GetDiscordPresence().UpdateClientPresence();
		}

		internal string GetPreferredBigImage()
		{
			return _discordyaMod.GetCrossModCompatibility().GetModList().Count == 0 ? "terraria" : _discordyaMod.GetCrossModCompatibility().GetPreferredMod().GetApplicationBigImage();
		}
	}
}
