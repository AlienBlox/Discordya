// Decompiled with JetBrains decompiler
// Type: Discordya.Biome.Biome
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using System;

#nullable disable
namespace Discordya.Biome
{
  internal class Biome
  {
    public Func<bool> _biomeConditional;
    private readonly string _bigKey;
    private readonly string _bigText;
    private readonly string _customClientAppId;
    private readonly float _priority;

    public Biome(
      Func<bool> biomeConditional = null,
      string bigKey = "unknown_biome",
      string bigText = "Unknown Biome",
      string customClientAppId = "default",
      float priority = 0.0f)
    {
      this._biomeConditional = biomeConditional;
      this._bigKey = bigKey;
      this._bigText = bigText;
      this._customClientAppId = customClientAppId;
      this._priority = priority;
    }

    internal string GetBigKey() => this._bigKey;

    internal string GetBigText() => this._bigText;

    internal string GetCustomClientAppId() => this._customClientAppId;

    internal float GetPriority() => this._priority;
  }
}
