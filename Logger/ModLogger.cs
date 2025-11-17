// Decompiled with JetBrains decompiler
// Type: Discordya.Logger.ModLogger
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

#nullable disable
namespace Discordya.Logger
{
  public class ModLogger
  {
    private DiscordyaMod _discordyaMod;

    public ModLogger(DiscordyaMod discordyaMod) => this._discordyaMod = discordyaMod;

    public void Log(string msg, byte level = 0)
    {
      switch (level)
      {
        case 1:
          this._discordyaMod.Logger.Debug((object) msg);
          break;
        case 2:
          this._discordyaMod.Logger.Warn((object) msg);
          break;
        case 3:
          this._discordyaMod.Logger.Error((object) msg);
          break;
        default:
          this._discordyaMod.Logger.Info((object) msg);
          break;
      }
    }
  }
}
