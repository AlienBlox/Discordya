using DiscordyaV2.Biome;
using DiscordyaV2.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ModLoader;

namespace DiscordyaV2.Game
{
	public class GamePresenceProcessor
	{
		private readonly DiscordyaMod _discordyaMod;

		internal GamePresenceProcessor(DiscordyaMod discordyaMod)
		{
			_discordyaMod = discordyaMod;
			AddVanillaBiomes();
			AddVanillaEvents();
			AddBosses();
		}

		public void ReloadCache()
		{
			if (_discordyaMod == null || _discordyaMod.GetBiomeManager() == null || _discordyaMod.bossDictionary == null || _discordyaMod.GetCrossModCompatibility() == null)
				return;
			_discordyaMod.GetModLogger().Log("Reloading cache...");
			_discordyaMod.GetBiomeManager().GetBiomeList().Clear();
			_discordyaMod.bossDictionary.Clear();
			AddVanillaBiomes();
			AddVanillaEvents();
			AddBosses();
			_discordyaMod.GetCrossModCompatibility().LoadModCompatibility();
			_discordyaMod.GetModLogger().Log("Loaded " + _discordyaMod.GetBiomeManager().GetBiomeList().Count.ToString() + " biomes/events and " + _discordyaMod.bossDictionary.Count.ToString() + " bosses.");
		}

		internal void AddBiome(
		  Func<bool> biomeConditional,
		  string bigKey,
		  string bigText,
		  string customClientAppId = "default",
		  float priority = 50f)
		{
			if (_discordyaMod.GetBiomeManager().GetBiomeList().Count > -1)
				_discordyaMod.GetBiomeManager().GetBiomeList().Add(new Biomes(biomeConditional, bigKey, bigText, (string)null, priority));
			else
				_discordyaMod.GetModLogger().Log("Failed to add Biome " + bigKey + ".");
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
				if (_discordyaMod.bossDictionary.Count > -1)
					_discordyaMod.bossDictionary.Add(bossId, (bossName, imageKey, client, priority));
				else
					_discordyaMod.GetModLogger().Log("Failed to add Boss " + imageKey + ".");
			}
		}

		internal (string, string, string) GetBiome()
		{
			string str1 = (string)null;
			string str2 = (string)null;
			string str3 = "default";
			if (_discordyaMod.GetBiomeManager().GetBiomeList().Count > -1)
			{
				float num = -1f;
				foreach (Biomes biome in _discordyaMod.GetBiomeManager().GetBiomeList())
				{
					if (biome._biomeConditional() && (double)biome.GetPriority() >= (double)num)
					{
						num = biome.GetPriority();
						str1 = biome.GetBigKey();
						str2 = _discordyaMod.GetLanguageHelper().GetText("Generic.In") + " " + biome.GetBigText();
						if (ModContent.GetInstance<DiscordyaConfig>().DisplayTime)
							str2 = str2 + " (" + (Main.dayTime ? _discordyaMod.GetLanguageHelper().GetText("Generic.Day") : _discordyaMod.GetLanguageHelper().GetText("Generic.Night")) + ")";
					}
				}
			}
			return (str1, str2, str3);
		}

		internal (string, string, string) GetBoss()
		{
			string str1 = (string)null;
			string str2 = (string)null;
			string str3 = "default";
			bool flag = false;
			if (_discordyaMod.bossDictionary.Count > -1)
			{
				float num = -1f;
				NPC[] npc1 = Main.npc;
				foreach (int key in npc1 != null ? ((IEnumerable<NPC>)npc1).Take<NPC>(200).Where<NPC>((Func<NPC, bool>)(npc => ((Entity)npc).active && _discordyaMod.bossDictionary.ContainsKey(npc.type))).Select<NPC, int>((Func<NPC, int>)(x => x.type)).ToList<int>() : (List<int>)null)
				{
					(string, string, string, float) boss = _discordyaMod.bossDictionary[key];
					if ((double)boss.Item4 >= (double)num)
					{
						flag = true;
						(str2, str1, str3, _) = boss;
						num = boss.Item4;
					}
				}
			}
			string str4;
			if (flag && ModContent.GetInstance<DiscordyaConfig>().DisplayBossFights)
				str4 = _discordyaMod.GetLanguageHelper().GetText("Generic.Fighting") + " " + str2;
			else
				(str1, str4, str3) = GetBiome();
			return (str1, str4, str3);
		}

		private void AddVanillaEvents()
		{
			AddBiome((Func<bool>)(() => (double)Main.player[Main.myPlayer].townNPCs >= 3.0 && BirthdayParty.PartyIsUp), "event_party", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.Party"), priority: 90f);
			AddBiome((Func<bool>)(() => Sandstorm.Happening && Main.player[Main.myPlayer].ZoneDesert), "event_sandstorm", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.Sandstorm"), priority: 91f);
			AddBiome((Func<bool>)(() => Main.slimeRain && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_slime", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.SlimeRain"), priority: 92f);
			AddBiome((Func<bool>)(() => DD2Event.Ongoing && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_oldone", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.OldOnesArmy"), priority: 95f);
			AddBiome((Func<bool>)(() => Main.invasionType == 1 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_goblin", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.GoblinInvasion"), priority: 96f);
			AddBiome((Func<bool>)(() => Main.bloodMoon && Main.player[Main.myPlayer].ZoneOverworldHeight && !Main.dayTime), "event_bloodmoon", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.BloodMoon"), priority: 97f);
			AddBiome((Func<bool>)(() => Main.invasionType == 2 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_frostlegion", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.FrostLegion"), priority: 100f);
			AddBiome((Func<bool>)(() => Main.invasionType == 3 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_pirate", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.PirateInvasion"), priority: 110f);
			AddBiome((Func<bool>)(() => Main.pumpkinMoon && Main.player[Main.myPlayer].ZoneOverworldHeight && !Main.dayTime), "event_pumpkinmoon", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.PumpkinMoon"), priority: 115f);
			AddBiome((Func<bool>)(() => Main.snowMoon && Main.player[Main.myPlayer].ZoneOverworldHeight && !Main.dayTime), "event_frostmoon", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.FrostMoon"), priority: 115f);
			AddBiome((Func<bool>)(() => Main.eclipse && Main.player[Main.myPlayer].ZoneOverworldHeight && Main.dayTime), "event_eclipse", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.SolarEclipse"), priority: 115f);
			AddBiome((Func<bool>)(() => Main.invasionType == 4 && Main.invasionSize > 0 && Main.player[Main.myPlayer].ZoneOverworldHeight), "event_martian", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.MartianEvent"), priority: 120f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneTowerSolar), "event_solar", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.SolarPillar"), priority: 130f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneTowerVortex), "event_vortex", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.VortexPillar"), priority: 130f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneTowerNebula), "event_nebula", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.NebulaPillar"), priority: 130f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneTowerStardust), "event_stardust", _discordyaMod.GetLanguageHelper().GetText("Event.Vanilla.StardustPillar"), priority: 130f);
		}

		private void AddVanillaBiomes()
		{
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneForest), "biome_forest", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Forest"), priority: 0.0f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Underground"), priority: 1f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneDirtLayerHeight), "biome_cavern", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Cavern"), priority: 2f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneBeach), "biome_ocean", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Ocean"), priority: 3f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneGlowshroom), "biome_mushroom", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Mushroom"), priority: 4f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneJungle), "biome_jungle", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Jungle"), priority: 5f);
			AddBiome((Func<bool>)(() =>
			{
				if (!Main.player[Main.myPlayer].ZoneJungle)
					return false;
				return Main.player[Main.myPlayer].ZoneDirtLayerHeight || Main.player[Main.myPlayer].ZoneRockLayerHeight;
			}), "biome_underground_jungle", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundJungle"), priority: 6f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneDesert), "biome_desert", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Desert"), priority: 7f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneUndergroundDesert), "biome_underground_desert", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundDesert"), priority: 8f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneSnow), "biome_snow", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Snow"), priority: 9f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneSnow && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_tundra", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Tundra"), priority: 10f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneHallow), "biome_hallow", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Hallow"), priority: 11f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneHallow && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground_hallow", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundHallow"), priority: 12f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneCorrupt), "biome_corruption", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Corruption"), priority: 13f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneCorrupt && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground_corruption", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundCorruption"), priority: 14f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneCrimson), "biome_crimson", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Crimson"), priority: 15f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneCrimson && Main.player[Main.myPlayer].ZoneRockLayerHeight), "biome_underground_crimson", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundCrimson"), priority: 16f);
			AddBiome((Func<bool>)(() =>
			{
				if (!Main.player[Main.myPlayer].ZoneGlowshroom)
					return false;
				return Main.player[Main.myPlayer].ZoneDirtLayerHeight || Main.player[Main.myPlayer].ZoneRockLayerHeight;
			}), "biome_mushroom", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.UndergroundMushroom"), priority: 18f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneGranite), "biome_granite_cave", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.GraniteCave"), priority: 19f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneMarble), "biome_marble_cave", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.MarbleCave"), priority: 20f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneHive), "biome_hive", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Hive"), priority: 21f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneDungeon), "biome_dungeon", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Dungeon"), priority: 22f);
			AddBiome((Func<bool>)(() =>
			{
				if (!Main.player[Main.myPlayer].ZoneLihzhardTemple)
					return false;
				return Main.player[Main.myPlayer].ZoneJungle || Main.player[Main.myPlayer].ZoneRockLayerHeight;
			}), "biome_jungle_temple", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.JungleTemple"), priority: 23f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneSkyHeight), "biome_space", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Space"), priority: 24f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneUnderworldHeight), "biome_underworld", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Underworld"), priority: 27f);
			AddBiome((Func<bool>)(() => Main.player[Main.myPlayer].ZoneMeteor), "biome_meteor", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Meteor"), priority: 28f);
			AddBiome((Func<bool>)(() => (double)Main.player[Main.myPlayer].townNPCs >= 3.0), "biome_town", _discordyaMod.GetLanguageHelper().GetText("Biome.Vanilla.Town"), priority: 29f);
		}

		private void AddBosses()
		{
			AddBoss(new List<int>() { 50 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.KingSlime"), "boss_kingslime", 1f);
			AddBoss(new List<int>() { 4 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.EyeOfCthulhu"), "boss_eyeofcthulhu", 2f);
			AddBoss(new List<int>() { 14, 13, 15 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.EaterOfWorlds"), "boss_eaterofworlds", 3f);
			AddBoss(new List<int>() { 266 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.BrainOfCthulhu"), "boss_brainofcthulhu", 4f);
			AddBoss(new List<int>() { 222 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.QueenBee"), "boss_queenbee", 5f);
			AddBoss(new List<int>() { 35 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Skeletron"), "boss_skeletron", 6f);
			AddBoss(new List<int>() { 668 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Deerclops"), "boss_deerclops", 7f);
			AddBoss(new List<int>() { 113 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.WallOfFlesh"), "boss_wallofflesh", 8f);
			AddBoss(new List<int>() { 125, 126 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.TheTwins"), "boss_twins", 9f);
			AddBoss(new List<int>() { 134 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.TheDestroyer"), "boss_destroyer", 10f);
			AddBoss(new List<int>() { (int)sbyte.MaxValue }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.SkeletronPrime"), "boss_skeletronprime", 11f);
			AddBoss(new List<int>() { 262 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Plantera"), "boss_plantera", 12f);
			AddBoss(new List<int>() { 636 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.EmpressOfLight"), "boss_eol", 13f);
			AddBoss(new List<int>() { 245 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.Golem"), "boss_golem", 14f);
			AddBoss(new List<int>() { 370 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.DukeFishron"), "boss_dukefishron", 15f);
			AddBoss(new List<int>() { 439 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.LunaticCultist"), "boss_lunatic");
			AddBoss(new List<int>() { 398, 396, 397 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.MoonLord"), "boss_moonlord", 17f);
			AddBoss(new List<int>() { 657 }, _discordyaMod.GetLanguageHelper().GetText("Boss.Vanilla.QueenSlime"), "boss_queenslime", 18f);
		}
	}
}
