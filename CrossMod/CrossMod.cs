// Decompiled with JetBrains decompiler
// Type: Discordya.CrossMod.CrossMod
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

#nullable disable
namespace Discordya.CrossMod
{
  public class CrossMods
  {
    private readonly string _modIdentifier;
    private readonly string _applicationIdentifier;
    private readonly string _applicationBigImage;

    internal CrossMods(
      string modIdentifier,
      string applicationIdentifier,
      string applicationBigImage)
    {
      this._modIdentifier = modIdentifier;
      this._applicationIdentifier = applicationIdentifier;
      this._applicationBigImage = applicationBigImage;
    }

    internal string GetModIdentifier() => this._modIdentifier;

    internal string GetApplicationIdentifier() => this._applicationIdentifier;

    internal string GetApplicationBigImage() => this._applicationBigImage;
  }
}
