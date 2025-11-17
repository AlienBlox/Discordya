// Decompiled with JetBrains decompiler
// Type: Discordya.Biome.BiomeManager
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using System.Collections.Generic;

#nullable disable
namespace Discordya.Biome
{
  public class BiomeManager
  {
    private readonly List<Discordya.Biome.Biome> _biomeList;

    public BiomeManager() => this._biomeList = new List<Discordya.Biome.Biome>();

    internal List<Discordya.Biome.Biome> GetBiomeList() => this._biomeList;
  }
}
