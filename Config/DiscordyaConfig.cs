// Decompiled with JetBrains decompiler
// Type: Discordya.Config.DiscordyaConfig
// Assembly: Discordya, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 7EE41C03-D9DA-4741-BA28-096DF00C64EC
// Assembly location: C:\Users\Alien\OneDrive\文档\Discordya\Discordya.dll

using Discordya.Player;
using System.ComponentModel;
using Terraria;
using Terraria.ModLoader.Config;

#nullable disable
namespace Discordya.Config
{
	public class DiscordyaConfig : ModConfig
	{
		[Header("$Mods.Discordya.Config.InternalDetailsHeader")]
		[LabelKey("$Mods.Discordya.Config.DisplayModVersion.Label")]
		[TooltipKey("$Mods.Discordya.Config.DisplayModVersion.Tooltip")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool DisplayModVersion;
		[LabelKey("$Mods.Discordya.Config.DisplayModQuantity.Label")]
		[TooltipKey("$Mods.Discordya.Config.DisplayModQuantity.Tooltip")]
		[ReloadRequired]
		[DefaultValue(true)]
		public bool DisplayModQuantity;
		[Header("$Mods.Discordya.Config.GameDetailsHeader")]
		[LabelKey("$Mods.Discordya.Config.DisplayHealth.Label")]
		[DefaultValue(true)]
		public bool DisplayHealth;
		[LabelKey("$Mods.Discordya.Config.DisplayMana.Label")]
		[DefaultValue(true)]
		public bool DisplayMana;
		[LabelKey("$Mods.Discordya.Config.DisplayTime.Label")]
		[DefaultValue(true)]
		public bool DisplayTime;
		[LabelKey("$Mods.Discordya.Config.DisplayDefense.Label")]
		[DefaultValue(true)]
		public bool DisplayDefense;
		[LabelKey("$Mods.Discordya.Config.DisplayBossFights.Label")]
		[DefaultValue(true)]
		public bool DisplayBossFights;
		[LabelKey("$Mods.Discordya.Config.DisplayCurrentItem.Label")]
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
