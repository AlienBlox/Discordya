// Decompiled with JetBrains decompiler
// Type: Discordya.DiscordyaMod
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\Discordya\Discordya.dll

using Discordya.Biome;
using Discordya.CrossMod;
using Discordya.DiscordClient;
using Discordya.Game;
using Discordya.Language;
using Discordya.Logger;
using Discordya.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Discordya
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

        public virtual void Load()
        {
            if (Main.dedServ)
                return;
            this._logger = new ModLogger(this);
            this._languageHelper = new LanguageHelper(this);
            this._discordClientHelper = new DiscordClientHelper(this);
            this._presenceUtils = new PresenceUtils(this);
            this._biomeManager = new BiomeManager();
            this.bossDictionary = new Dictionary<int, (string, string, string, float)>();
            this._gamePresenceProcessor = new GamePresenceProcessor(DiscordyaMod._discordyaMod);
            this._discordClientHelper.MakeClient();
        }

        public virtual void PostSetupContent()
        {
            if (Main.dedServ)
                return;
            this._crossModCompatibility = new CrossModCompatibility(DiscordyaMod._discordyaMod);
            this._crossModCompatibility.LoadModCompatibility();
            this._discordClientHelper.SetupCrossModClient();
            Task.Factory.StartNew<Task>((Func<Task>)(() => Task.Factory.StartNew((Action)(() =>
            {
                Task.Delay(2000).Wait();
                this._presenceUtils.SetMainMenuPresence();
            }))));
        }

        public virtual void Unload()
        {
            this._discordClientHelper?.KillClient();
            this._crossModCompatibility?.UnloadModCompatibility();
            this._gamePresenceProcessor = (GamePresenceProcessor)null;
            this.bossDictionary = (Dictionary<int, (string, string, string, float)>)null;
            this.worldInfo = (string)null;
            this._biomeManager = (BiomeManager)null;
            this._discordClientHelper = (DiscordClientHelper)null;
            this._crossModCompatibility = (CrossModCompatibility)null;
            this._presenceUtils = (PresenceUtils)null;
            this._logger = (ModLogger)null;
            DiscordyaMod._discordyaMod = (DiscordyaMod)null;
        }

        internal ModLogger GetModLogger() => this._logger;

        internal LanguageHelper GetLanguageHelper() => this._languageHelper;

        internal CrossModCompatibility GetCrossModCompatibility() => this._crossModCompatibility;

        internal DiscordClientHelper GetDiscordClientHelper() => this._discordClientHelper;

        internal PresenceUtils GetPresenceUtils() => this._presenceUtils;

        internal BiomeManager GetBiomeManager() => this._biomeManager;

        internal GamePresenceProcessor GetGamePresenceProcessor() => this._gamePresenceProcessor;
    }
}
