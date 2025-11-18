// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.DiscordyaMod
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\DiscordyaV2\DiscordyaV2.dll

using DiscordyaV2.Biome;
using DiscordyaV2.CrossMod;
using DiscordyaV2.DiscordClient;
using DiscordyaV2.Game;
using DiscordyaV2.Language;
using DiscordyaV2.Logger;
using DiscordyaV2.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace DiscordyaV2
{
	public class DiscordyaMod : Mod
	{
		public static DiscordyaMod _discordyaMod;
		private ModLogger _logger;
		private LanguageHelper _languageHelper;
		private DiscordClientHelper _discordClientHelper;
		private PresenceUtils _presenceUtils;
		private BiomeManager _biomeManager;
		internal Dictionary<int, (string, string, string, float)> bossDictionary;
		internal string worldInfo;
		private GamePresenceProcessor _gamePresenceProcessor;
		private CrossModCompatibility _crossModCompatibility;

		public DiscordyaMod() => DiscordyaMod._discordyaMod = this;

		public override void Load()
		{
			if (Main.dedServ)
				return;
			_logger = new ModLogger(this);
			_languageHelper = new LanguageHelper(this);
			_discordClientHelper = new DiscordClientHelper(this);
			_presenceUtils = new PresenceUtils(this);
			_biomeManager = new BiomeManager();
			bossDictionary = new Dictionary<int, (string, string, string, float)>();
			_gamePresenceProcessor = new GamePresenceProcessor(DiscordyaMod._discordyaMod);
			_discordClientHelper.MakeClient();
		}

		public override void PostSetupContent()
		{
			if (Main.dedServ)
				return;
			_crossModCompatibility = new CrossModCompatibility(DiscordyaMod._discordyaMod);
			_crossModCompatibility.LoadModCompatibility();
			_discordClientHelper.SetupCrossModClient();
			Task.Factory.StartNew<Task>((Func<Task>)(() => Task.Factory.StartNew((Action)(() =>
			{
				Task.Delay(2000).Wait();
				_presenceUtils.SetMainMenuPresence();
			}))));
		}

		public override void Unload()
		{
			_discordClientHelper?.KillClient();
			_crossModCompatibility?.UnloadModCompatibility();
			_gamePresenceProcessor = (GamePresenceProcessor)null;
			bossDictionary = (Dictionary<int, (string, string, string, float)>)null;
			worldInfo = (string)null;
			_biomeManager = (BiomeManager)null;
			_discordClientHelper = (DiscordClientHelper)null;
			_crossModCompatibility = (CrossModCompatibility)null;
			_presenceUtils = (PresenceUtils)null;
			_logger = (ModLogger)null;
			DiscordyaMod._discordyaMod = (DiscordyaMod)null;
		}

		internal ModLogger GetModLogger() => _logger;

		internal LanguageHelper GetLanguageHelper() => _languageHelper;

		internal CrossModCompatibility GetCrossModCompatibility() => _crossModCompatibility;

		internal DiscordClientHelper GetDiscordClientHelper() => _discordClientHelper;

		internal PresenceUtils GetPresenceUtils() => _presenceUtils;

		internal BiomeManager GetBiomeManager() => _biomeManager;

		internal GamePresenceProcessor GetGamePresenceProcessor() => _gamePresenceProcessor;
	}
}
