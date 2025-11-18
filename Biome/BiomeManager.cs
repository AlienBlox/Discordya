// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.Biome.BiomeManager
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using System.Collections.Generic;

#nullable disable
namespace DiscordyaV2.Biome
{
	public class BiomeManager
	{
		private readonly List<Biomes> _biomeList;

		public BiomeManager() => this._biomeList = new List<Biomes>();

		internal List<Biomes> GetBiomeList() => this._biomeList;
	}
}
