using System.Collections.Generic;

namespace DiscordyaV2.Biome
{
	public class BiomeManager
	{
		private readonly List<Biomes> _biomeList;

		public BiomeManager() => _biomeList = new List<Biomes>();

		internal List<Biomes> GetBiomeList() => _biomeList;
	}
}
