using System;

namespace DiscordyaV2.Biome
{
	internal class Biomes
	{
		public Func<bool> _biomeConditional;
		private readonly string _bigKey;
		private readonly string _bigText;
		private readonly string _customClientAppId;
		private readonly float _priority;

		public Biomes(
		  Func<bool> biomeConditional = null,
		  string bigKey = "unknown_biome",
		  string bigText = "Unknown Biome",
		  string customClientAppId = "default",
		  float priority = 0.0f)
		{
			_biomeConditional = biomeConditional;
			_bigKey = bigKey;
			_bigText = bigText;
			_customClientAppId = customClientAppId;
			_priority = priority;
		}

		internal string GetBigKey() => _bigKey;

		internal string GetBigText() => _bigText;

		internal string GetCustomClientAppId() => _customClientAppId;

		internal float GetPriority() => _priority;
	}
}
