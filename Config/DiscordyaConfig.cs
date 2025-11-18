// Decompiled with JetBrains decompiler
// Type: DiscordyaV2.Config.DiscordyaConfig
// Assembly: DiscordyaV2, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\DiscordyaV2\DiscordyaV2.dll

using DiscordyaV2.Player;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

#nullable disable
namespace DiscordyaV2.Config
{
	public class DiscordyaConfig : ModConfig
	{
		[Header("$Mods.DiscordyaV2.Config.InternalDetailsHeader")]
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayModVersion.Label")]
		[TooltipKey("$Mods.DiscordyaV2.Config.DisplayModVersion.Tooltip")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool DisplayModVersion;
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayModQuantity.Label")]
		[TooltipKey("$Mods.DiscordyaV2.Config.DisplayModQuantity.Tooltip")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool DisplayModQuantity;
		[Header("$Mods.DiscordyaV2.Config.GameDetailsHeader")]
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayHealth.Label")]
		[DefaultValue(true)]
		public bool DisplayHealth;
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayMana.Label")]
		[DefaultValue(true)]
		public bool DisplayMana;
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayTime.Label")]
		[DefaultValue(true)]
		public bool DisplayTime;
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayDefense.Label")]
		[DefaultValue(true)]
		public bool DisplayDefense;
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayBossFights.Label")]
		[DefaultValue(true)]
		public bool DisplayBossFights;
		[LabelKey("$Mods.DiscordyaV2.Config.DisplayCurrentItem.Label")]
		[DefaultValue(true)]
		public bool DisplayCurrentItem;

		public override ConfigScope Mode => ConfigScope.ClientSide;

		public override void OnChanged()
		{
			DiscordyaPlayer discordyaPlayer;
			Main.player[Main.myPlayer].TryGetModPlayer<DiscordyaPlayer>(out discordyaPlayer);
			discordyaPlayer?.ClientUpdatePlayer();
		}
	}
}
