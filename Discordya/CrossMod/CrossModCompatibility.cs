// Decompiled with JetBrains decompiler
// Type: Discordya.CrossMod.CrossModCompatibility
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using Discordya.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace Discordya.CrossMod
{
  public class CrossModCompatibility
  {
    private DiscordyaMod _discordyaMod;
    private Dictionary<Discordya.CrossMod.CrossMod, float> _externalModDictionary = new Dictionary<Discordya.CrossMod.CrossMod, float>();

    internal CrossModCompatibility(DiscordyaMod discordyaMod) => this._discordyaMod = discordyaMod;

    internal void LoadModCompatibility() => this.SetupCrossModCompatibility();

    internal void UnloadModCompatibility()
    {
      this._externalModDictionary = (Dictionary<Discordya.CrossMod.CrossMod, float>) null;
      this._discordyaMod = (DiscordyaMod) null;
    }

    private void SetupCrossModCompatibility()
    {
      Mod calamity;
      Terraria.ModLoader.ModLoader.TryGetMod("CalamityMod", ref calamity);
      if (calamity != null)
      {
        this.AddMod(new Discordya.CrossMod.CrossMod("calamity", "844487120416407573", "calamity"), 3f);
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => (bool) calamity.Call(new object[3]
        {
          (object) "Zone",
          (object) Main.LocalPlayer,
          (object) "sunkensea"
        })), "biome_sunken_sea", this._discordyaMod.GetLanguageHelper().GetText("Biome.Calamity.SunkenSea"));
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => (bool) calamity.Call(new object[3]
        {
          (object) "Zone",
          (object) Main.LocalPlayer,
          (object) "Sulfur"
        })), "biome_sulphur", this._discordyaMod.GetLanguageHelper().GetText("Biome.Calamity.SulphurousSea"));
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => (bool) calamity.Call(new object[3]
        {
          (object) "Zone",
          (object) Main.LocalPlayer,
          (object) "Abyss"
        })), "biome_abyss", this._discordyaMod.GetLanguageHelper().GetText("Biome.Calamity.Abyss"));
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => (bool) calamity.Call(new object[3]
        {
          (object) "Zone",
          (object) Main.LocalPlayer,
          (object) "Brimstone"
        })), "biome_brimstone", this._discordyaMod.GetLanguageHelper().GetText("Biome.Calamity.BrimstoneCrag"));
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => (bool) calamity.Call(new object[3]
        {
          (object) "Zone",
          (object) Main.LocalPlayer,
          (object) "Astral"
        })), "biome_astral_infection", this._discordyaMod.GetLanguageHelper().GetText("Biome.Calamity.AstralInfection"));
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() =>
        {
          if (!(bool) calamity.Call(new object[1]
          {
            (object) "AcidRainActive"
          }))
            return false;
          return (bool) calamity.Call(new object[3]
          {
            (object) "GetInZone",
            (object) Main.LocalPlayer,
            (object) "sulphursea"
          });
        }), "event_acid_rain", this._discordyaMod.GetLanguageHelper().GetText("Event.Calamity.AcidRain"));
        this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => (bool) calamity.Call(new object[2]
        {
          (object) "GetDifficultyActive",
          (object) "bossrush"
        })), "event_boss_rush", this._discordyaMod.GetLanguageHelper().GetText("Event.Calamity.BossRush"));
        ModNPC modNpc1;
        calamity.TryFind<ModNPC>("DesertScourgeHead", ref modNpc1);
        GamePresenceProcessor presenceProcessor1 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs1 = new List<int>();
        bossIDs1.Add(modNpc1.NPC.type);
        string text1 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.DesertScourge");
        presenceProcessor1.AddBoss(bossIDs1, text1, "boss_desert_scourge", 19f);
        ModNPC modNpc2;
        calamity.TryFind<ModNPC>("GiantClam", ref modNpc2);
        GamePresenceProcessor presenceProcessor2 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs2 = new List<int>();
        bossIDs2.Add(modNpc2.NPC.type);
        string text2 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.GiantClam");
        presenceProcessor2.AddBoss(bossIDs2, text2, "boss_giant_clam", 20f);
        ModNPC modNpc3;
        calamity.TryFind<ModNPC>("Crabulon", ref modNpc3);
        GamePresenceProcessor presenceProcessor3 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs3 = new List<int>();
        bossIDs3.Add(modNpc3.NPC.type);
        string text3 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Crabulon");
        presenceProcessor3.AddBoss(bossIDs3, text3, "boss_crabulon", 21f);
        ModNPC modNpc4;
        calamity.TryFind<ModNPC>("PerforatorHive", ref modNpc4);
        GamePresenceProcessor presenceProcessor4 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs4 = new List<int>();
        bossIDs4.Add(modNpc4.NPC.type);
        string text4 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.ThePerforators");
        presenceProcessor4.AddBoss(bossIDs4, text4, "boss_perforators", 22f);
        ModNPC modNpc5;
        calamity.TryFind<ModNPC>("HiveMind", ref modNpc5);
        GamePresenceProcessor presenceProcessor5 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs5 = new List<int>();
        bossIDs5.Add(modNpc5.NPC.type);
        string text5 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.TheHiveMind");
        presenceProcessor5.AddBoss(bossIDs5, text5, "boss_hive_mind", 23f);
        ModNPC modNpc6;
        calamity.TryFind<ModNPC>("SlimeGodCore", ref modNpc6);
        GamePresenceProcessor presenceProcessor6 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs6 = new List<int>();
        bossIDs6.Add(modNpc6.NPC.type);
        string text6 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.SlimeGod");
        presenceProcessor6.AddBoss(bossIDs6, text6, "boss_slimegod", 24f);
        ModNPC modNpc7;
        calamity.TryFind<ModNPC>("Cryogen", ref modNpc7);
        GamePresenceProcessor presenceProcessor7 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs7 = new List<int>();
        bossIDs7.Add(modNpc7.NPC.type);
        string text7 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Cryogen");
        presenceProcessor7.AddBoss(bossIDs7, text7, "boss_cirno", 25f);
        ModNPC modNpc8;
        calamity.TryFind<ModNPC>("AquaticScourgeHead", ref modNpc8);
        GamePresenceProcessor presenceProcessor8 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs8 = new List<int>();
        bossIDs8.Add(modNpc8.NPC.type);
        string text8 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.AquaticScourge");
        presenceProcessor8.AddBoss(bossIDs8, text8, "boss_aquatic_scourge", 26f);
        ModNPC modNpc9;
        calamity.TryFind<ModNPC>("BrimstoneElemental", ref modNpc9);
        GamePresenceProcessor presenceProcessor9 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs9 = new List<int>();
        bossIDs9.Add(modNpc9.NPC.type);
        string text9 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.BrimstoneElemental");
        presenceProcessor9.AddBoss(bossIDs9, text9, "boss_brimstone_elemental", 27f);
        ModNPC modNpc10;
        calamity.TryFind<ModNPC>("CalamitasClone", ref modNpc10);
        GamePresenceProcessor presenceProcessor10 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs10 = new List<int>();
        bossIDs10.Add(modNpc10.NPC.type);
        string text10 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Calamitas");
        presenceProcessor10.AddBoss(bossIDs10, text10, "boss_calamitas", 28f);
        ModNPC modNpc11;
        calamity.TryFind<ModNPC>("GreatSandShark", ref modNpc11);
        GamePresenceProcessor presenceProcessor11 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs11 = new List<int>();
        bossIDs11.Add(modNpc11.NPC.type);
        string text11 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.GreatSandShark");
        presenceProcessor11.AddBoss(bossIDs11, text11, "boss_yesterandom", 29f);
        ModNPC modNpc12;
        calamity.TryFind<ModNPC>("Leviathan", ref modNpc12);
        ModNPC modNpc13;
        calamity.TryFind<ModNPC>("Anahita", ref modNpc13);
        GamePresenceProcessor presenceProcessor12 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs12 = new List<int>();
        bossIDs12.Add(modNpc12.NPC.type);
        bossIDs12.Add(modNpc13.NPC.type);
        string text12 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Leviathan");
        presenceProcessor12.AddBoss(bossIDs12, text12, "boss_leviathan", 30f);
        ModNPC modNpc14;
        calamity.TryFind<ModNPC>("AstrumAureus", ref modNpc14);
        GamePresenceProcessor presenceProcessor13 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs13 = new List<int>();
        bossIDs13.Add(modNpc14.NPC.type);
        string text13 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.AstrumAureus");
        presenceProcessor13.AddBoss(bossIDs13, text13, "boss_aureus", 31f);
        ModNPC modNpc15;
        calamity.TryFind<ModNPC>("PlaguebringerGoliath", ref modNpc15);
        GamePresenceProcessor presenceProcessor14 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs14 = new List<int>();
        bossIDs14.Add(modNpc15.NPC.type);
        string text14 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Plaguebringer");
        presenceProcessor14.AddBoss(bossIDs14, text14, "boss_plaguebringer", 32f);
        ModNPC modNpc16;
        calamity.TryFind<ModNPC>("RavagerHead", ref modNpc16);
        GamePresenceProcessor presenceProcessor15 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs15 = new List<int>();
        bossIDs15.Add(modNpc16.NPC.type);
        string text15 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Ravager");
        presenceProcessor15.AddBoss(bossIDs15, text15, "boss_ravager", 33f);
        ModNPC modNpc17;
        calamity.TryFind<ModNPC>("AstrumDeusHead", ref modNpc17);
        GamePresenceProcessor presenceProcessor16 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs16 = new List<int>();
        bossIDs16.Add(modNpc17.NPC.type);
        string text16 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.AstrumDeus");
        presenceProcessor16.AddBoss(bossIDs16, text16, "boss_deus", 34f);
        ModNPC modNpc18;
        calamity.TryFind<ModNPC>("ProfanedGuardianCommander", ref modNpc18);
        ModNPC modNpc19;
        calamity.TryFind<ModNPC>("ProfanedGuardianDefender", ref modNpc19);
        ModNPC modNpc20;
        calamity.TryFind<ModNPC>("ProfanedGuardianHealer", ref modNpc20);
        GamePresenceProcessor presenceProcessor17 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs17 = new List<int>();
        bossIDs17.Add(modNpc18.NPC.type);
        bossIDs17.Add(modNpc19.NPC.type);
        bossIDs17.Add(modNpc20.NPC.type);
        string text17 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.ProfanedGuardians");
        presenceProcessor17.AddBoss(bossIDs17, text17, "boss_profaned_guardians", 35f);
        ModNPC modNpc21;
        calamity.TryFind<ModNPC>("Bumblefuck", ref modNpc21);
        ModNPC modNpc22;
        calamity.TryFind<ModNPC>("Bumblefuck2", ref modNpc22);
        GamePresenceProcessor presenceProcessor18 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs18 = new List<int>();
        bossIDs18.Add(modNpc21.NPC.type);
        bossIDs18.Add(modNpc22.NPC.type);
        string text18 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Dragonfolly");
        presenceProcessor18.AddBoss(bossIDs18, text18, "boss_dragonfolly", 36f);
        ModNPC modNpc23;
        calamity.TryFind<ModNPC>("Providence", ref modNpc23);
        GamePresenceProcessor presenceProcessor19 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs19 = new List<int>();
        bossIDs19.Add(modNpc23.NPC.type);
        string text19 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Providence");
        presenceProcessor19.AddBoss(bossIDs19, text19, "boss_providence", 37f);
        ModNPC modNpc24;
        calamity.TryFind<ModNPC>("CeaselessVoid", ref modNpc24);
        GamePresenceProcessor presenceProcessor20 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs20 = new List<int>();
        bossIDs20.Add(modNpc24.NPC.type);
        string text20 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.CeaselessVoid");
        presenceProcessor20.AddBoss(bossIDs20, text20, "boss_ceaseless_void", 38f);
        ModNPC modNpc25;
        calamity.TryFind<ModNPC>("StormWeaverHead", ref modNpc25);
        GamePresenceProcessor presenceProcessor21 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs21 = new List<int>();
        bossIDs21.Add(modNpc25.NPC.type);
        string text21 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.StormWeaver");
        presenceProcessor21.AddBoss(bossIDs21, text21, "boss_storm_weaver", 39f);
        ModNPC modNpc26;
        calamity.TryFind<ModNPC>("Signus", ref modNpc26);
        GamePresenceProcessor presenceProcessor22 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs22 = new List<int>();
        bossIDs22.Add(modNpc26.NPC.type);
        string text22 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Signus");
        presenceProcessor22.AddBoss(bossIDs22, text22, "boss_signus", 40f);
        ModNPC modNpc27;
        calamity.TryFind<ModNPC>("Polterghast", ref modNpc27);
        ModNPC modNpc28;
        calamity.TryFind<ModNPC>("PhantomFuckYou", ref modNpc28);
        GamePresenceProcessor presenceProcessor23 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs23 = new List<int>();
        bossIDs23.Add(modNpc27.NPC.type);
        bossIDs23.Add(modNpc28.NPC.type);
        string text23 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Polterghast");
        presenceProcessor23.AddBoss(bossIDs23, text23, "boss_polterghast", 41f);
        ModNPC modNpc29;
        calamity.TryFind<ModNPC>("OldDuke", ref modNpc29);
        GamePresenceProcessor presenceProcessor24 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs24 = new List<int>();
        bossIDs24.Add(modNpc29.NPC.type);
        string text24 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.OldDuke");
        presenceProcessor24.AddBoss(bossIDs24, text24, "boss_oldduke", 42f);
        ModNPC modNpc30;
        calamity.TryFind<ModNPC>("DevourerofGodsHead", ref modNpc30);
        GamePresenceProcessor presenceProcessor25 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs25 = new List<int>();
        bossIDs25.Add(modNpc30.NPC.type);
        string text25 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.TheDevourerOfGods");
        presenceProcessor25.AddBoss(bossIDs25, text25, "boss_devourer", 43f);
        ModNPC modNpc31;
        calamity.TryFind<ModNPC>("Yharon", ref modNpc31);
        GamePresenceProcessor presenceProcessor26 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs26 = new List<int>();
        bossIDs26.Add(modNpc31.NPC.type);
        string text26 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.Yharon");
        presenceProcessor26.AddBoss(bossIDs26, text26, "boss_yharon", 44f);
        ModNPC modNpc32;
        calamity.TryFind<ModNPC>("Draedon", ref modNpc32);
        GamePresenceProcessor presenceProcessor27 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs27 = new List<int>();
        bossIDs27.Add(modNpc32.NPC.type);
        string text27 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.ExoMechs");
        presenceProcessor27.AddBoss(bossIDs27, text27, "boss_draedon", 45f);
        ModNPC modNpc33;
        calamity.TryFind<ModNPC>("SupremeCalamitas", ref modNpc33);
        GamePresenceProcessor presenceProcessor28 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs28 = new List<int>();
        bossIDs28.Add(modNpc33.NPC.type);
        string text28 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.SupremeCalamitas");
        presenceProcessor28.AddBoss(bossIDs28, text28, "boss_scal", 46f);
        ModNPC modNpc34;
        calamity.TryFind<ModNPC>("PrimordialWyrmHead", ref modNpc34);
        GamePresenceProcessor presenceProcessor29 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs29 = new List<int>();
        bossIDs29.Add(modNpc34.NPC.type);
        string text29 = this._discordyaMod.GetLanguageHelper().GetText("Boss.Calamity.ImpossibleDamnBoss");
        presenceProcessor29.AddBoss(bossIDs29, text29, "boss_adult_wyrm", 47f);
      }
      Mod mod1;
      Terraria.ModLoader.ModLoader.TryGetMod("StarsAbove", ref mod1);
      if (mod1 != null)
      {
        this.AddMod(new Discordya.CrossMod.CrossMod("starsabove", "824723328580976660", "starsabove"), 2f);
        ModNPC modNpc35;
        mod1.TryFind<ModNPC>("VagrantBoss", ref modNpc35);
        GamePresenceProcessor presenceProcessor30 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs30 = new List<int>();
        bossIDs30.Add(modNpc35.NPC.type);
        string text30 = this._discordyaMod.GetLanguageHelper().GetText("Boss.StarsAbove.TheVagrantOfSpaceAndTime");
        presenceProcessor30.AddBoss(bossIDs30, text30, "boss_vagrant", 48f);
        ModNPC modNpc36;
        mod1.TryFind<ModNPC>("NalhaunBoss", ref modNpc36);
        ModNPC modNpc37;
        mod1.TryFind<ModNPC>("NalhaunBossPhase2", ref modNpc37);
        GamePresenceProcessor presenceProcessor31 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs31 = new List<int>();
        bossIDs31.Add(modNpc36.NPC.type);
        bossIDs31.Add(modNpc37.NPC.type);
        string text31 = this._discordyaMod.GetLanguageHelper().GetText("Boss.StarsAbove.NaulhaunTheBurnishedKing");
        presenceProcessor31.AddBoss(bossIDs31, text31, "boss_nalhaun", 49f);
        ModNPC modNpc38;
        mod1.TryFind<ModNPC>("WarriorOfLightBoss", ref modNpc38);
        ModNPC modNpc39;
        mod1.TryFind<ModNPC>("WarriorOfLightBossFinalPhase", ref modNpc39);
        GamePresenceProcessor presenceProcessor32 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs32 = new List<int>();
        bossIDs32.Add(modNpc38.NPC.type);
        bossIDs32.Add(modNpc39.NPC.type);
        string text32 = this._discordyaMod.GetLanguageHelper().GetText("Boss.StarsAbove.TheWarriorOfLight");
        presenceProcessor32.AddBoss(bossIDs32, text32, "boss_warrioroflight", 51f);
        ModNPC modNpc40;
        mod1.TryFind<ModNPC>("TsukiyomiBoss", ref modNpc40);
        GamePresenceProcessor presenceProcessor33 = this._discordyaMod.GetGamePresenceProcessor();
        List<int> bossIDs33 = new List<int>();
        bossIDs33.Add(modNpc40.NPC.type);
        string text33 = this._discordyaMod.GetLanguageHelper().GetText("Boss.StarsAbove.Tsukiyomi");
        presenceProcessor33.AddBoss(bossIDs33, text33, "boss_tsukiyomi", 52f);
      }
      Mod mod2;
      Terraria.ModLoader.ModLoader.TryGetMod("Gensokyo", ref mod2);
      if (mod2 == null)
        return;
      this.AddMod(new Discordya.CrossMod.CrossMod("gensokyo", "833828769260371978", "gensokyo"), 1f);
      ModBiome subterraneanSun;
      ModContent.TryFind<ModBiome>("Gensokyo/SubterraneanSun", ref subterraneanSun);
      this._discordyaMod.GetGamePresenceProcessor().AddBiome((Func<bool>) (() => Main.LocalPlayer.InModBiome(subterraneanSun)), "biome_subsun", this._discordyaMod.GetLanguageHelper().GetText("Biome.Gensokyo.SubterraneanSun"));
      ModNPC modNpc41;
      mod2.TryFind<ModNPC>("LilyWhite", ref modNpc41);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc41.NPC.type
      }, "Lily White", "boss_lily", 53f);
      ModNPC modNpc42;
      mod2.TryFind<ModNPC>("Rumia", ref modNpc42);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc42.NPC.type
      }, "Rumia", "boss_rumia", 54f);
      ModNPC modNpc43;
      mod2.TryFind<ModNPC>("EternityLarva", ref modNpc43);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc43.NPC.type
      }, "Eternity Larva", "boss_eternity_larva", 55f);
      ModNPC modNpc44;
      mod2.TryFind<ModNPC>("HinaKagiyama", ref modNpc44);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc44.NPC.type
      }, "Hina Kagiyama", "boss_hina_kagiyama", 56f);
      ModNPC modNpc45;
      mod2.TryFind<ModNPC>("Sekibanki", ref modNpc45);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc45.NPC.type
      }, "Sekibanki", "boss_sekibanki", 57f);
      ModNPC modNpc46;
      mod2.TryFind<ModNPC>("NitoriKawashiro", ref modNpc46);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc46.NPC.type
      }, "Nitori Kawashiro", "boss_nitori", 58f);
      ModNPC modNpc47;
      mod2.TryFind<ModNPC>("MedicineMelancholy", ref modNpc47);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc47.NPC.type
      }, "Medicine Melancholy", "boss_medicine_melancholy", 59f);
      ModNPC modNpc48;
      mod2.TryFind<ModNPC>("Cirno", ref modNpc48);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc48.NPC.type
      }, "Cirno", "boss_cirno", 60f);
      ModNPC modNpc49;
      mod2.TryFind<ModNPC>("MinamitsuMurasa", ref modNpc49);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc49.NPC.type
      }, "Minamitsu Murasa", "boss_minamitsu", 61f);
      ModNPC modNpc50;
      mod2.TryFind<ModNPC>("AliceMargatroid", ref modNpc50);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc50.NPC.type
      }, "Alice Margatroid", "boss_alice", 62f);
      ModNPC modNpc51;
      mod2.TryFind<ModNPC>("SakuyaIzayoi", ref modNpc51);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc51.NPC.type
      }, "Sakuya Izayoi", "boss_sakuya", 63f);
      ModNPC modNpc52;
      mod2.TryFind<ModNPC>("SeijaKijin", ref modNpc52);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc52.NPC.type
      }, "Seija Kijin", "boss_seija", 64f);
      ModNPC modNpc53;
      mod2.TryFind<ModNPC>("MayumiJoutouguu", ref modNpc53);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc53.NPC.type
      }, "Mayumi Joutouguu", "boss_mayumi", 65f);
      ModNPC modNpc54;
      mod2.TryFind<ModNPC>("ToyosatomimiNoMiko", ref modNpc54);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc54.NPC.type
      }, "Toyosatomimi no Miko", "boss_toyosatomimi", 66f);
      ModNPC modNpc55;
      mod2.TryFind<ModNPC>("KaguyaHouraisan", ref modNpc55);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc55.NPC.type
      }, "Kaguya Houraisan", "boss_kaguya", 67f);
      ModNPC modNpc56;
      mod2.TryFind<ModNPC>("UtsuhoReiuji", ref modNpc56);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc56.NPC.type
      }, "Utsuho 'Okuu' Reiuji", "boss_utsuho", 68f);
      ModNPC modNpc57;
      mod2.TryFind<ModNPC>("TenshiHinanawi", ref modNpc57);
      this._discordyaMod.GetGamePresenceProcessor().AddBoss(new List<int>()
      {
        modNpc57.NPC.type
      }, "Tenshi Hinanawi", "boss_tenshi", 69f);
    }

    private void AddMod(Discordya.CrossMod.CrossMod modKey, float modWeight)
    {
      if (this._externalModDictionary.ContainsKey(modKey))
        return;
      this._externalModDictionary.Add(modKey, modWeight);
    }

    internal List<Discordya.CrossMod.CrossMod> GetModList()
    {
      return new List<Discordya.CrossMod.CrossMod>((IEnumerable<Discordya.CrossMod.CrossMod>) this._externalModDictionary.Keys);
    }

    internal Discordya.CrossMod.CrossMod GetPreferredMod()
    {
      return this._externalModDictionary.OrderByDescending<KeyValuePair<Discordya.CrossMod.CrossMod, float>, float>((Func<KeyValuePair<Discordya.CrossMod.CrossMod, float>, float>) (x => x.Value)).Select<KeyValuePair<Discordya.CrossMod.CrossMod, float>, Discordya.CrossMod.CrossMod>((Func<KeyValuePair<Discordya.CrossMod.CrossMod, float>, Discordya.CrossMod.CrossMod>) (x => x.Key)).First<Discordya.CrossMod.CrossMod>();
    }
  }
}
