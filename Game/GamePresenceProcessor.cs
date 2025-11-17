// Decompiled with JetBrains decompiler
// Type: Discordya.Game.GamePresenceProcessor
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using Discordya.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

#nullable disable
namespace Discordya.Game
{
  public class GamePresenceProcessor
  {
    private readonly DiscordyaMod _discordyaMod;

    internal GamePresenceProcessor(DiscordyaMod discordyaMod)
    {
      this._discordyaMod = discordyaMod;
      this.AddVanillaBiomes();
      this.AddVanillaEvents();
      this.AddBosses();
    }

    public void ReloadCache()
    {
      if (this._discordyaMod == null || this._discordyaMod.GetBiomeManager() == null || this._discordyaMod.bossDictionary == null || this._discordyaMod.GetCrossModCompatibility() == null)
        return;
      this._discordyaMod.GetModLogger().Log("Reloading cache...");
      this._discordyaMod.GetBiomeManager().GetBiomeList().Clear();
      this._discordyaMod.bossDictionary.Clear();
      this.AddVanillaBiomes();
      this.AddVanillaEvents();
      this.AddBosses();
      this._discordyaMod.GetCrossModCompatibility().LoadModCompatibility();
      this._discordyaMod.GetModLogger().Log("Loaded " + this._discordyaMod.GetBiomeManager().GetBiomeList().Count.ToString() + " biomes/events and " + this._discordyaMod.bossDictionary.Count.ToString() + " bosses.");
    }

    internal void AddBiome(
      Func<bool> biomeConditional,
      string bigKey,
      string bigText,
      string customClientAppId = "default",
      float priority = 50f)
    {
      if (this._discordyaMod.GetBiomeManager().GetBiomeList().Count > -1)
        this._discordyaMod.GetBiomeManager().GetBiomeList().Add(new Discordya.Biome.Biome(biomeConditional, bigKey, bigText, (string) null, priority));
      else
        this._discordyaMod.GetModLogger().Log("Failed to add Biome " + bigKey + ".");
    }

    internal void AddBoss(
      List<int> bossIDs,
      string bossName,
      string imageKey,
      float priority = 16f,
      string client = "default")
    {
      if (bossIDs == null)
        return;
      foreach (int bossId in bossIDs)
      {
        if (string.IsNullOrWhiteSpace(bossName))
          bossName = "boss_placeholder";
        if (this._discordyaMod.bossDictionary.Count > -1)
          this._discordyaMod.bossDictionary.Add(bossId, (bossName, imageKey, client, priority));
        else
          this._discordyaMod.GetModLogger().Log("Failed to add Boss " + imageKey + ".");
      }
    }

    internal (string, string, string) GetBiome()
    {
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = "default";
      if (this._discordyaMod.GetBiomeManager().GetBiomeList().Count > -1)
      {
        float num = -1f;
        foreach (Discordya.Biome.Biome biome in this._discordyaMod.GetBiomeManager().GetBiomeList())
        {
          if (biome._biomeConditional() && (double) biome.GetPriority() >= (double) num)
          {
            num = biome.GetPriority();
            str1 = biome.GetBigKey();
            str2 = this._discordyaMod.GetLanguageHelper().GetText("Generic.In") + " " + biome.GetBigText();
            if (ModContent.GetInstance<DiscordyaConfig>().DisplayTime)
              str2 = str2 + " (" + (Main.dayTime ? this._discordyaMod.GetLanguageHelper().GetText("Generic.Day") : this._discordyaMod.GetLanguageHelper().GetText("Generic.Night")) + ")";
          }
        }
      }
      return (str1, str2, str3);
    }

    internal (string, string, string) GetBoss()
    {
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = "default";
      bool flag = false;
      if (this._discordyaMod.bossDictionary.Count > -1)
      {
        float num = -1f;
        NPC[] npc1 = Main.npc;
        foreach (int key in npc1 != null ? ((IEnumerable<NPC>) npc1).Take<NPC>(200).Where<NPC>((Func<NPC, bool>) (npc => ((Entity) npc).active && this._discordyaMod.bossDictionary.ContainsKey(npc.type))).Select<NPC, int>((Func<NPC, int>) (x => x.type)).ToList<int>() : (List<int>) null)
        {
          (string, string, string, float) boss = this._discordyaMod.bossDictionary[key];
          if ((double) boss.Item4 >= (double) num)
          {
            flag = true;
            (str2, str1, str3, _) = boss;
            num = boss.Item4;
          }
        }
      }
      string str4;
      if (flag && ModContent.GetInstance<DiscordyaConfig>().DisplayBossFights)
        str4 = this._discordyaMod.GetLanguageHelper().GetText("Generic.Fighting") + " " + str2;
      else
        (str1, str4, str3) = this.GetBiome();
      return (str1, str4, str3);
    }

    private void AddVanillaEvents()
    {
      this.AddBiome((Func<bool>) (() => (double) Main.player[Main.myPlayer].townNPCs >= 3.0 && BirthdayParty.PartyIsUp), "event_party", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.Party"), priority: 90f);
      this.AddBiome((Func<bool>) (() => Sandstorm.Happening && Main.player[Main.myPlayer].ZoneDesert), "event_sandstorm", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.Sandstorm"), priority: 91f);
      this.AddBiome((Func<bool>) (() => Main.slimeRain && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_slime", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.SlimeRain"), priority: 92f);
      this.AddBiome((Func<bool>) (() => DD2Event.Ongoing && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_oldone", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.OldOnesArmy"), priority: 95f);
      this.AddBiome((Func<bool>) (() => Main.invasionType == 1 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_goblin", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.GoblinInvasion"), priority: 96f);
      this.AddBiome((Func<bool>) (() => Main.bloodMoon && Main.player[Main.myPlayer].ZoneOverworldHeight && !Main.dayTime), "event_bloodmoon", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.BloodMoon"), priority: 97f);
      this.AddBiome((Func<bool>) (() => Main.invasionType == 2 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_frostlegion", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.FrostLegion"), priority: 100f);
      this.AddBiome((Func<bool>) (() => Main.invasionType == 3 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_pirate", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.PirateInvasion"), priority: 110f);
      this.AddBiome((Func<bool>) (() => Main.pumpkinMoon && Main.player[Main.myPlayer].ZoneOverworldHeight && !Main.dayTime), "event_pumpkinmoon", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.PumpkinMoon"), priority: 115f);
      this.AddBiome((Func<bool>) (() => Main.snowMoon && Main.player[Main.myPlayer].ZoneOverworldHeight && !Main.dayTime), "event_frostmoon", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.FrostMoon"), priority: 115f);
      this.AddBiome((Func<bool>) (() => Main.eclipse && Main.player[Main.myPlayer].ZoneOverworldHeight && Main.dayTime), "event_eclipse", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.SolarEclipse"), priority: 115f);
      this.AddBiome((Func<bool>) (() => Main.invasionType == 4 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_martian", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.MartianEvent"), priority: 120f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneTowerSolar), "event_solar", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.SolarPillar"), priority: 130f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneTowerVortex), "event_vortex", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.VortexPillar"), priority: 130f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneTowerNebula), "event_nebula", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.NebulaPillar"), priority: 130f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneTowerStardust), "event_stardust", this._discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.StardustPillar"), priority: 130f);
    }

    private void AddVanillaBiomes()
    {
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneForest), "biome_forest", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Forest"), priority: 0.0f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Underground"), priority: 1f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneDirtLayerHeight), "biome_cavern", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Cavern"), priority: 2f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneBeach), "biome_ocean", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Ocean"), priority: 3f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneGlowshroom), "biome_mushroom", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Mushroom"), priority: 4f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneJungle), "biome_jungle", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Jungle"), priority: 5f);
      this.AddBiome((Func<bool>) (() =>
      {
        if (!Main.player[Main.myPlayer].ZoneJungle)
          return false;
        return Main.player[Main.myPlayer].ZoneDirtLayerHeight || Main.player[Main.myPlayer].ZoneRockLayerHeight;
      }), "biome_underground_jungle", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundJungle"), priority: 6f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneDesert), "biome_desert", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Desert"), priority: 7f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneUndergroundDesert), "biome_underground_desert", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundDesert"), priority: 8f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneSnow), "biome_snow", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Snow"), priority: 9f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneSnow && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_tundra", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Tundra"), priority: 10f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneHallow), "biome_hallow", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Hallow"), priority: 11f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneHallow && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground_hallow", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundHallow"), priority: 12f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneCorrupt), "biome_corruption", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Corruption"), priority: 13f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneCorrupt && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground_corruption", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundCorruption"), priority: 14f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneCrimson), "biome_crimson", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Crimson"), priority: 15f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneCrimson && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground_crimson", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundCrimson"), priority: 16f);
      this.AddBiome((Func<bool>) (() =>
      {
        if (!Main.player[Main.myPlayer].ZoneGlowshroom)
          return false;
        return Main.player[Main.myPlayer].ZoneDirtLayerHeight || Main.player[Main.myPlayer].ZoneRockLayerHeight;
      }), "biome_mushroom", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundMushroom"), priority: 18f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneGranite), "biome_granite_cave", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.GraniteCave"), priority: 19f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneMarble), "biome_marble_cave", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.MarbleCave"), priority: 20f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneHive), "biome_hive", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Hive"), priority: 21f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneDungeon), "biome_dungeon", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Dungeon"), priority: 22f);
      this.AddBiome((Func<bool>) (() =>
      {
        if (!Main.player[Main.myPlayer].ZoneLihzhardTemple)
          return false;
        return Main.player[Main.myPlayer].ZoneJungle || Main.player[Main.myPlayer].ZoneRockLayerHeight;
      }), "biome_jungle_temple", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.JungleTemple"), priority: 23f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneSkyHeight), "biome_space", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Space"), priority: 24f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneUnderworldHeight), "biome_underworld", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Underworld"), priority: 27f);
      this.AddBiome((Func<bool>) (() => Main.player[Main.myPlayer].ZoneMeteor), "biome_meteor", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Meteor"), priority: 28f);
      this.AddBiome((Func<bool>) (() => (double) Main.player[Main.myPlayer].townNPCs >= 3.0), "biome_town", this._discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Town"), priority: 29f);
    }

    private void AddBosses()
    {
      this.AddBoss(new List<int>() { 50 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.KingSlime"), "boss_kingslime", 1f);
      this.AddBoss(new List<int>() { 4 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.EyeOfCthulhu"), "boss_eyeofcthulhu", 2f);
      this.AddBoss(new List<int>() { 14, 13, 15 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.EaterOfWorlds"), "boss_eaterofworlds", 3f);
      this.AddBoss(new List<int>() { 266 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.BrainOfCthulhu"), "boss_brainofcthulhu", 4f);
      this.AddBoss(new List<int>() { 222 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.QueenBee"), "boss_queenbee", 5f);
      this.AddBoss(new List<int>() { 35 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Skeletron"), "boss_skeletron", 6f);
      this.AddBoss(new List<int>() { 668 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Deerclops"), "boss_deerclops", 7f);
      this.AddBoss(new List<int>() { 113 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.WallOfFlesh"), "boss_wallofflesh", 8f);
      this.AddBoss(new List<int>() { 125, 126 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.TheTwins"), "boss_twins", 9f);
      this.AddBoss(new List<int>() { 134 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.TheDestroyer"), "boss_destroyer", 10f);
      this.AddBoss(new List<int>() { (int) sbyte.MaxValue }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.SkeletronPrime"), "boss_skeletronprime", 11f);
      this.AddBoss(new List<int>() { 262 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Plantera"), "boss_plantera", 12f);
      this.AddBoss(new List<int>() { 636 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.EmpressOfLight"), "boss_eol", 13f);
      this.AddBoss(new List<int>() { 245 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Golem"), "boss_golem", 14f);
      this.AddBoss(new List<int>() { 370 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.DukeFishron"), "boss_dukefishron", 15f);
      this.AddBoss(new List<int>() { 439 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.LunaticCultist"), "boss_lunatic");
      this.AddBoss(new List<int>() { 398, 396, 397 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.MoonLord"), "boss_moonlord", 17f);
      this.AddBoss(new List<int>() { 657 }, this._discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.QueenSlime"), "boss_queenslime", 18f);
    }
  }
}
