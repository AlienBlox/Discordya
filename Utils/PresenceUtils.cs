// Decompiled with JetBrains decompiler
// Type: Discordya.Utils.PresenceUtils
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using Discordya.Config;
using Discordya.DiscordClient;
using Terraria.ModLoader;

#nullable disable
namespace Discordya.Utils
{
  public class PresenceUtils
  {
    private readonly DiscordyaMod _discordyaMod;
    private readonly DiscordClientHelper _discordClientHelper;
    private const string MainMenuBigText = "Discordya v{0} | tModLoader v{1}";

    internal PresenceUtils(DiscordyaMod discordyaMod)
    {
      this._discordyaMod = discordyaMod;
      this._discordClientHelper = discordyaMod.GetDiscordClientHelper();
    }

    internal void SetMainMenuPresence()
    {
      this._discordClientHelper.GetDiscordPresence().SetClientStatus(this._discordyaMod.GetLanguageHelper().GetText("Generic.MainMenu"), ModContent.GetInstance<DiscordyaConfig>().DisplayModQuantity ? string.Format(this._discordyaMod.GetLanguageHelper().GetText("Generic.MainMenuState"), (object) Terraria.ModLoader.ModLoader.Mods.Length) : (string) null, this.GetPreferredBigImage(), ModContent.GetInstance<DiscordyaConfig>().DisplayModVersion ? string.Format("Discordya v{0} | tModLoader v{1}", (object) this._discordyaMod.Version, (object) this._discordyaMod.TModLoaderVersion) : (string) null);
      this._discordClientHelper.GetDiscordPresence().UpdateClientPresence();
    }

    internal string GetPreferredBigImage()
    {
      return this._discordyaMod.GetCrossModCompatibility().GetModList().Count == 0 ? "terraria" : this._discordyaMod.GetCrossModCompatibility().GetPreferredMod().GetApplicationBigImage();
    }
  }
}
